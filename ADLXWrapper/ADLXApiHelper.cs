using System;
using System.Runtime.InteropServices;
namespace ADLXWrapper
{
    /// <summary>
    /// Exception thrown when an ADLX API call fails
    /// </summary>
    public class ADLXException : Exception
    {
        public ADLX_RESULT Result { get; }

        /// <summary>
        /// Creates a new exception for a failed ADLX call.
        /// </summary>
        /// <param name="result">The ADLX result code returned by the native call.</param>
        /// <param name="message">Optional message to include; if omitted a default message is used.</param>
        public ADLXException(ADLX_RESULT result, string? message = null)
            : base(message ?? $"ADLX API error: {result}")
        {
            Result = result;
        }

        /// <summary>
        /// Checks if this exception is due to ADLX not being initialized
        /// </summary>
        public bool IsNotInitializedError()
        {
            return Result == ADLX_RESULT.ADLX_TERMINATED;
        }

        /// <summary>
        /// Checks if this exception is due to already being initialized
        /// </summary>
        public bool IsAlreadyInitializedError()
        {
            return Result == ADLX_RESULT.ADLX_ALREADY_INITIALIZED;
        }

        /// <summary>
        /// Checks if this exception is due to GPU being inactive
        /// </summary>
        public bool IsGPUInactiveError()
        {
            return Result == ADLX_RESULT.ADLX_GPU_INACTIVE;
        }
    }

    /// <summary>
    /// Main ADLX API wrapper providing safe access to AMD ADLX Library
    /// Implements IDisposable for proper resource cleanup
    /// </summary>
    public sealed class ADLXApiHelper : IDisposable
    {
        private static readonly object _sync = new();
        private static int _globalRefCount;
        private static unsafe IADLXSystem* _sharedSystem;
        private static IntPtr _sharedDll;
        private static ADLXNative.ADLXTerminate_Fn? _sharedTerminateFn;
        private static ulong _sharedFullVersion;
        private static string? _sharedVersion;

        private unsafe IADLXSystem* _systemServices;
        private bool _disposed;

        private ulong _fullVersion;
        private string? _version;

        private unsafe ADLXApiHelper(IADLXSystem* pSystemServices, ulong fullVersion, string? version)
        {
            _systemServices = pSystemServices;
            _fullVersion = fullVersion;
            _version = version;
        }

        /// <summary>
        /// Initialize the ADLX API with default settings
        /// </summary>
        public static unsafe ADLXApiHelper Initialize()
        {
            using (ADLXSync.EnterWrite())
            {
                if (_globalRefCount == 0)
                {
                    // If ADLX was previously initialized in this process and the shared pointers are still available,
                    // reuse them to avoid ADLX_ALREADY_INITIALIZED from the native initialize call.
                    if (_sharedSystem != null)
                    {
                        _globalRefCount++;
                        return new ADLXApiHelper(_sharedSystem, _sharedFullVersion, _sharedVersion);
                    }

                    var hDLL = LoadADLXDll();

                    var queryFullVersionFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryFullVersion_Fn>(
                        hDLL, ADLXNative.GetQueryFullVersionFunctionName());
                    var queryVersionFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryVersion_Fn>(
                        hDLL, ADLXNative.GetQueryVersionFunctionName());
                    var initializeFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXInitialize_Fn>(
                        hDLL, ADLXNative.GetInitializeFunctionName());
                    var terminateFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXTerminate_Fn>(
                        hDLL, ADLXNative.GetTerminateFunctionName());

                    ulong fullVersion = 0;
                    var result = queryFullVersionFn(&fullVersion);
                    if (result != ADLX_RESULT.ADLX_OK)
                    {
                        ADLXNative.FreeLibrary(hDLL);
                        throw new ADLXException(result, $"Failed to query ADLX version (result={result})");
                    }

                    IntPtr pSystem;
                    var initResult = initializeFn(fullVersion, &pSystem);
                    if (initResult == ADLX_RESULT.ADLX_ALREADY_INITIALIZED && pSystem != IntPtr.Zero)
                    {
                        // ADLX already initialized elsewhere; reuse the system pointer without failing.
                    }
                    else if (initResult != ADLX_RESULT.ADLX_OK)
                    {
                        ADLXNative.FreeLibrary(hDLL);
                        throw new ADLXException(initResult, $"Failed to initialize ADLX (result={initResult})");
                    }

                    if (pSystem == IntPtr.Zero)
                    {
                        ADLXNative.FreeLibrary(hDLL);
                        throw new ADLXException(ADLX_RESULT.ADLX_ALREADY_INITIALIZED, "ADLX is already initialized by another component and no system pointer was provided.");
                    }

                    _sharedDll = hDLL;
                    _sharedTerminateFn = terminateFn;
                    _sharedFullVersion = fullVersion;

                    byte* pVersion;
                    result = queryVersionFn(&pVersion);
                    _sharedVersion = (result == ADLX_RESULT.ADLX_OK && pVersion != null)
                        ? Marshal.PtrToStringAnsi((IntPtr)pVersion)
                        : null;

                    _sharedSystem = (IADLXSystem*)pSystem;
                }

                _globalRefCount++;
                return new ADLXApiHelper(_sharedSystem, _sharedFullVersion, _sharedVersion);
            }
        }

        /// <summary>
        /// Initialize ADLX with an existing ADL context (for applications migrating from ADL)
        /// </summary>
        public static unsafe ADLXApiHelper InitializeWithCallerAdl(IntPtr adlContext, IntPtr adlMainMemoryFree)
        {
            if (adlContext == IntPtr.Zero || adlMainMemoryFree == IntPtr.Zero)
            {
                throw new ArgumentException("ADL context and memory free function must not be null");
            }

            using (ADLXSync.EnterWrite())
            {
                if (_globalRefCount == 0)
                {
                    if (_sharedSystem != null)
                    {
                        _globalRefCount++;
                        return new ADLXApiHelper(_sharedSystem, _sharedFullVersion, _sharedVersion);
                    }

                    var hDLL = LoadADLXDll();

                    var queryFullVersionFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryFullVersion_Fn>(
                        hDLL, ADLXNative.GetQueryFullVersionFunctionName());
                    var queryVersionFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryVersion_Fn>(
                        hDLL, ADLXNative.GetQueryVersionFunctionName());
                    var initializeWithCallerAdlFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXInitializeWithCallerAdl_Fn>(
                        hDLL, ADLXNative.GetInitializeWithCallerAdlFunctionName());
                    var terminateFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXTerminate_Fn>(
                        hDLL, ADLXNative.GetTerminateFunctionName());

                    ulong fullVersion = 0;
                    var result = queryFullVersionFn(&fullVersion);
                    if (result != ADLX_RESULT.ADLX_OK)
                    {
                        ADLXNative.FreeLibrary(hDLL);
                        throw new ADLXException(result, $"Failed to query ADLX version (result={result})");
                    }

                    IntPtr pSystem;
                    IntPtr pAdlMapping;
                    var initResult = initializeWithCallerAdlFn(fullVersion, &pSystem, &pAdlMapping, adlContext, adlMainMemoryFree);
                    if (initResult == ADLX_RESULT.ADLX_ALREADY_INITIALIZED && pSystem != IntPtr.Zero)
                    {
                        // Reuse existing initialization owned elsewhere.
                    }
                    else if (initResult != ADLX_RESULT.ADLX_OK)
                    {
                        ADLXNative.FreeLibrary(hDLL);
                        throw new ADLXException(initResult, $"Failed to initialize ADLX with ADL context (result={initResult})");
                    }

                    if (pSystem == IntPtr.Zero)
                    {
                        ADLXNative.FreeLibrary(hDLL);
                        throw new ADLXException(ADLX_RESULT.ADLX_ALREADY_INITIALIZED, "ADLX already initialized by another component and no system pointer was provided.");
                    }

                    _sharedDll = hDLL;
                    _sharedTerminateFn = terminateFn;
                    _sharedFullVersion = fullVersion;

                    byte* pVersion;
                    result = queryVersionFn(&pVersion);
                    _sharedVersion = (result == ADLX_RESULT.ADLX_OK && pVersion != null)
                        ? Marshal.PtrToStringAnsi((IntPtr)pVersion)
                        : null;

                    _sharedSystem = (IADLXSystem*)pSystem;
                }

                _globalRefCount++;
                return new ADLXApiHelper(_sharedSystem, _sharedFullVersion, _sharedVersion);
            }
        }

        /// <summary>
        /// Get the ADLX full version number
        /// </summary>
        public ulong GetFullVersion()
        {
            ThrowIfDisposed();
            return _fullVersion;
        }

        /// <summary>
        /// Get the ADLX version string
        /// </summary>
        public string GetVersion()
        {
            ThrowIfDisposed();
            return _version ?? "Unknown";
        }

        /// <summary>
        /// Get the system services native pointer (lifetime owned by this API helper).
        /// Callers must not Release() it directly.
        /// </summary>
        public unsafe IADLXSystem* GetSystemServicesNative()
        {
            ThrowIfDisposed();
            return _systemServices;
        }

        /// <summary>
        /// Get the system services as an ADLXInterfaceHandle (AddRef'd for caller ownership).
        /// </summary>
        public unsafe ADLXInterfaceHandle GetSystemServicesHandle()
        {
            ThrowIfDisposed();
            var ptr = _systemServices;
            return ADLXInterfaceHandle.FromNonRefCounted(ptr);
        }

        /// <summary>
        /// Get the system services wrapped in an ADLXSystemServicesHelper.
        /// </summary>
        public unsafe ADLXSystemServicesHelper GetSystemServices()
        {
            ThrowIfDisposed();
            var ptr = _systemServices;
            return new ADLXSystemServicesHelper(ptr, addRef: false);
        }

        /// <summary>
        /// Dispose pattern implementation
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private unsafe void Dispose(bool disposing)
        {
            if (_disposed)
                return;

            using (ADLXSync.EnterWrite())
            {
                unsafe
                {
                    _globalRefCount--;
                    // To avoid use-after-free crashes across the process lifetime,
                    // we intentionally keep the ADLX DLL loaded and leave the
                    // shared system pointer intact until process exit.
                }
            }
            
            _systemServices = null;
            _disposed = true;
        }

        /// <summary>
        /// Finalizer
        /// </summary>
        ~ADLXApiHelper()
        {
            Dispose(false);
        }

        /// <summary>
        /// Load the ADLX DLL dynamically
        /// </summary>
        private static IntPtr LoadADLXDll()
        {
            var dllName = ADLXNative.GetDllName();
            
            var hDLL = ADLXNative.LoadLibraryEx(
                dllName,
                IntPtr.Zero,
                ADLXNative.LOAD_LIBRARY_SEARCH_USER_DIRS |
                ADLXNative.LOAD_LIBRARY_SEARCH_APPLICATION_DIR |
                ADLXNative.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS |
                ADLXNative.LOAD_LIBRARY_SEARCH_SYSTEM32);

            if (hDLL == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                throw new DllNotFoundException(
                    $"Failed to load ADLX DLL '{dllName}'. Error code: {error}. " +
                    "Make sure AMD GPU drivers are installed.");
            }

            return hDLL;
        }

        /// <summary>
        /// Throw if object has been disposed
        /// </summary>
        private void ThrowIfDisposed()
        {
            if (_disposed)
            {
                throw new ObjectDisposedException(nameof(ADLXApiHelper));
            }
        }

        /// <summary>
        /// Check if the ADLX DLL is available in the DLL search path
        /// </summary>
        /// <param name="errorMessage">Details about why the DLL could not be loaded</param>
        /// <returns>True if DLL can be loaded, false otherwise</returns>
        public static bool IsADLXDllAvailable(out string errorMessage)
        {
            var dllName = ADLXNative.GetDllName();
            
            IntPtr handle = ADLXNative.LoadLibraryEx(
                dllName,
                IntPtr.Zero,
                ADLXNative.LOAD_LIBRARY_SEARCH_USER_DIRS |
                ADLXNative.LOAD_LIBRARY_SEARCH_APPLICATION_DIR |
                ADLXNative.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS |
                ADLXNative.LOAD_LIBRARY_SEARCH_SYSTEM32);

            if (handle == IntPtr.Zero)
            {
                var error = Marshal.GetLastWin32Error();
                errorMessage = $"ADLX SDK DLL '{dllName}' not found in DLL search path (Error: {error})";
                return false;
            }

            ADLXNative.FreeLibrary(handle);
            errorMessage = string.Empty;
            return true;
        }

    }
}

