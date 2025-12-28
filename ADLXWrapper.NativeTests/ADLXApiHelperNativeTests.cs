using System;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXApiHelperNativeTests
{
    private readonly NativeSession _session;

    public ADLXApiHelperNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    private static void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    [SkippableFact]
    public void Initialize_and_get_system_services_native()
    {
        SkipIfNoAdlxSupport();

        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)_session.System);
        Assert.NotEqual<ulong>(0, _session.FullVersion);
        Assert.False(string.IsNullOrWhiteSpace(_session.Version));
    }

    [SkippableFact]
    public unsafe void Enumerate_gpus_native()
    {
        SkipIfNoAdlxSupport();

        var system = _session.System;
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)system);

        IADLXGPUList* gpuList;
        var listResult = system->GetGPUs(&gpuList);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);

        using var gpuListPtr = new ComPtr<IADLXGPUList>(gpuList);
        var count = gpuList->Size();
        Skip.If(count == 0, "No GPUs returned by ADLX.");

        IADLXGPU* gpu;
        var gpuResult = gpuList->At(0, &gpu);
        Skip.If(gpuResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU access not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, gpuResult);

        using var gpuPtr = new ComPtr<IADLXGPU>(gpu);
        sbyte* namePtr = null;
        var nameResult = gpu->Name(&namePtr);
        Skip.If(nameResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU name not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, nameResult);

        var name = Marshal.PtrToStringAnsi((IntPtr)namePtr) ?? string.Empty;
        Assert.False(string.IsNullOrWhiteSpace(name));
    }

    [SkippableFact]
    public unsafe void Enumerate_displays_native()
    {
        SkipIfNoAdlxSupport();

        var system = _session.System;
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)system);

        IADLXDisplayServices* displayServices;
        var displayServicesResult = system->GetDisplaysServices(&displayServices);
        Skip.If(displayServicesResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, displayServicesResult);

        using var displayServicesPtr = new ComPtr<IADLXDisplayServices>(displayServices);
        IADLXDisplayList* displayList;
        var listResult = displayServices->GetDisplays(&displayList);
        Skip.If(listResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, listResult);

        using var displayListPtr = new ComPtr<IADLXDisplayList>(displayList);
        var count = displayList->Size();
        Skip.If(count == 0, "No displays returned by ADLX.");

        IADLXDisplay* display;
        var displayResult = displayList->At(0, &display);
        Skip.If(displayResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display access not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, displayResult);

        using var displayPtr = new ComPtr<IADLXDisplay>(display);

        sbyte* namePtr = null;
        var nameResult = display->Name(&namePtr);
        Skip.If(nameResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display name not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, nameResult);

        var name = Marshal.PtrToStringAnsi((IntPtr)namePtr) ?? string.Empty;
        Assert.False(string.IsNullOrWhiteSpace(name));

        int width = 0, height = 0;
        var resResult = display->NativeResolution(&width, &height);
        Skip.If(resResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display native resolution not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, resResult);
        Skip.If(width == 0 || height == 0, "Display native resolution not reported.");

        double refreshRate = 0;
        var rrResult = display->RefreshRate(&refreshRate);
        Skip.If(rrResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "Display refresh rate not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, rrResult);
        Skip.If(refreshRate <= 0, "Display refresh rate not reported.");
    }

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
}
