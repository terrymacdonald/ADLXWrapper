using System;
using System.Runtime.InteropServices;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

/// <summary>
/// Shared native ADLX session and collection for all Native test classes.
/// </summary>
public sealed unsafe class NativeSession : IDisposable
{
    private const string ADLX_DLL_NAME_64 = "amdadlx64.dll";
    private const string ADLX_DLL_NAME_32 = "amdadlx32.dll";

    private const uint LOAD_LIBRARY_SEARCH_USER_DIRS = 0x00000400;
    private const uint LOAD_LIBRARY_SEARCH_APPLICATION_DIR = 0x00000200;
    private const uint LOAD_LIBRARY_SEARCH_DEFAULT_DIRS = 0x00001000;
    private const uint LOAD_LIBRARY_SEARCH_SYSTEM32 = 0x00000800;

    private readonly IntPtr _module;
    private readonly ADLXTerminateFn _terminate;
    private readonly bool _ownsInitialization;
    private bool _disposed;

    public IADLXSystem* System { get; }
    public ulong FullVersion { get; }
    public string Version { get; }

    private NativeSession(IntPtr module, ADLXTerminateFn terminate, IADLXSystem* system, ulong fullVersion, string version, bool ownsInitialization)
    {
        _module = module;
        _terminate = terminate;
        System = system;
        FullVersion = fullVersion;
        Version = version;
        _ownsInitialization = ownsInitialization;
    }

    public static unsafe NativeSession Create()
    {
        var dllName = Environment.Is64BitProcess ? ADLX_DLL_NAME_64 : ADLX_DLL_NAME_32;

        var module = LoadLibraryEx(
            dllName,
            IntPtr.Zero,
            LOAD_LIBRARY_SEARCH_USER_DIRS |
            LOAD_LIBRARY_SEARCH_APPLICATION_DIR |
            LOAD_LIBRARY_SEARCH_DEFAULT_DIRS |
            LOAD_LIBRARY_SEARCH_SYSTEM32);

        if (module == IntPtr.Zero)
        {
            var error = Marshal.GetLastWin32Error();
            throw new Xunit.SkipException($"Failed to load ADLX DLL '{dllName}' (Win32 error {error}).");
        }

        try
        {
            var queryFullVersionFn = GetFunction<ADLXQueryFullVersionFn>(module, "ADLXQueryFullVersion");
            var queryVersionFn = GetFunction<ADLXQueryVersionFn>(module, "ADLXQueryVersion");
            var initializeFn = GetFunction<ADLXInitializeFn>(module, "ADLXInitialize");
            var terminateFn = GetFunction<ADLXTerminateFn>(module, "ADLXTerminate");

            ulong fullVersion = 0;
            var fullVerResult = queryFullVersionFn(&fullVersion);
            if (fullVerResult != ADLX_RESULT.ADLX_OK)
            {
                throw new Xunit.SkipException($"ADLXQueryFullVersion failed: {fullVerResult}");
            }

            byte* versionPtr = null;
            var versionResult = queryVersionFn(&versionPtr);
            var version = (versionResult == ADLX_RESULT.ADLX_OK && versionPtr != null)
                ? Marshal.PtrToStringAnsi((IntPtr)versionPtr) ?? "Unknown"
                : "Unknown";

            IntPtr systemPtr;
            var initResult = initializeFn(fullVersion, &systemPtr);
            bool ownsInit = initResult == ADLX_RESULT.ADLX_OK;

            if (initResult == ADLX_RESULT.ADLX_ALREADY_INITIALIZED && systemPtr == IntPtr.Zero)
            {
                // ADLX already initialized elsewhere; we can't proceed without a system pointer.
                throw new Xunit.SkipException("ADLX already initialized by another component; system pointer unavailable.");
            }

            if (initResult != ADLX_RESULT.ADLX_OK && initResult != ADLX_RESULT.ADLX_ALREADY_INITIALIZED)
            {
                throw new Xunit.SkipException($"ADLXInitialize failed: {initResult}");
            }

            return new NativeSession(module, terminateFn, (IADLXSystem*)systemPtr, fullVersion, version, ownsInit);
        }
        catch
        {
            FreeLibrary(module);
            throw;
        }
    }

    private static unsafe TDelegate GetFunction<TDelegate>(IntPtr module, string name) where TDelegate : Delegate
    {
        var proc = GetProcAddress(module, name);
        if (proc == IntPtr.Zero)
        {
            throw new Xunit.SkipException($"ADLX function '{name}' not found.");
        }

        return Marshal.GetDelegateForFunctionPointer<TDelegate>(proc);
    }

    public void Dispose()
    {
        if (_disposed)
            return;

        try
        {
            if (_ownsInitialization)
            {
                _terminate();
            }
        }
        catch
        {
            // ignore termination failures in tests
        }

        FreeLibrary(_module);
        _disposed = true;
    }

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private unsafe delegate ADLX_RESULT ADLXQueryFullVersionFn(ulong* fullVersion);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private unsafe delegate ADLX_RESULT ADLXQueryVersionFn(byte** version);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private unsafe delegate ADLX_RESULT ADLXInitializeFn(ulong version, IntPtr* ppSystem);

    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    private unsafe delegate ADLX_RESULT ADLXTerminateFn();

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hFile, uint dwFlags);

    [DllImport("kernel32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool FreeLibrary(IntPtr hModule);

    [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Ansi)]
    private static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
}

public sealed class NativeSessionFixture : IDisposable
{
    public NativeSession Session { get; }

    public NativeSessionFixture()
    {
        Session = NativeSession.Create();
    }

    public void Dispose()
    {
        Session.Dispose();
    }
}

[CollectionDefinition("NativeSessionCollection")]
public class NativeSessionCollection : ICollectionFixture<NativeSessionFixture>
{
    // Intentionally empty; this wires the fixture into the collection.
}
