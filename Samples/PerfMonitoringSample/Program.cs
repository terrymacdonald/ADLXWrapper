using System;
using System.Runtime.InteropServices;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Performance Monitoring Sample ===");
    Console.WriteLine(" 1) Facade  - current GPU/system metrics");
    Console.WriteLine(" 2) Native  - current GPU/system metrics");
    Console.WriteLine(" Q) Quit");

    while (true)
    {
        Console.Write("Select option: ");
        var input = Console.ReadLine()?.Trim();
        if (string.IsNullOrEmpty(input))
            continue;
        if (input.Equals("q", StringComparison.OrdinalIgnoreCase))
            break;

        try
        {
            switch (input)
            {
                case "1":
                    RunFacadePerf();
                    break;
                case "2":
                    RunNativePerf();
                    break;
                default:
                    Console.WriteLine("Unknown option.");
                    break;
            }
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            Console.WriteLine($"Not supported: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
    }
}

static unsafe void RunFacadePerf()
{
    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var gpus = sysHelper.EnumerateGPUsHandle();
    if (gpus.Length == 0)
    {
        Console.WriteLine("No AMD GPU found; exiting.");
        return;
    }

    using var perfHelper = new ADLXPerformanceMonitoringServicesHelper(sysHelper.GetPerformanceMonitoringServicesNative());
    using var gpu = gpus[0];

    var current = perfHelper.GetCurrentGpuMetrics(gpu.As<IADLXGPU>());
    Console.WriteLine($"[Facade] GPU: Temp={current.Temperature:F1}C, Usage={current.Usage:F1}%, Clock={current.ClockSpeed}MHz, VRAM={current.VRAMUsage}MB");

    var sysMetrics = perfHelper.GetCurrentSystemMetrics();
    Console.WriteLine($"[Facade] System: CPU={sysMetrics.CpuUsage:F1}%, RAM={sysMetrics.SystemRam}MB");
}

static unsafe void RunNativePerf()
{
    using var session = NativeAdlxSession.Create();

    IADLXGPUList* gpuList = null;
    if (session.System->GetGPUs(&gpuList) != ADLX_RESULT.ADLX_OK || gpuList == null)
    {
        Console.WriteLine("No AMD GPU found; exiting.");
        return;
    }
    using var gpus = new ComPtr<IADLXGPUList>(gpuList);
    if (gpus.Get()->Size() == 0)
    {
        Console.WriteLine("No AMD GPU found; exiting.");
        return;
    }

    IADLXGPU* pGpu = null;
    gpus.Get()->At(0, &pGpu);
    using var gpu = new ComPtr<IADLXGPU>(pGpu);

    IADLXPerformanceMonitoringServices* perfPtr = null;
    var perfResult = session.System->GetPerformanceMonitoringServices(&perfPtr);
    if (perfResult != ADLX_RESULT.ADLX_OK || perfPtr == null)
    {
        Console.WriteLine("Performance monitoring not supported.");
        return;
    }
    using var perf = new ComPtr<IADLXPerformanceMonitoringServices>(perfPtr);

    IADLXGPUMetrics* pGpuMetrics = null;
    var gpuMetricsResult = perf.Get()->GetCurrentGPUMetrics(gpu.Get(), &pGpuMetrics);
    if (gpuMetricsResult == ADLX_RESULT.ADLX_OK && pGpuMetrics != null)
    {
        using var metrics = new ComPtr<IADLXGPUMetrics>(pGpuMetrics);
        double usage = 0, temp = 0;
        int clock = 0;
        metrics.Get()->GPUUsage(&usage);
        metrics.Get()->GPUTemperature(&temp);
        metrics.Get()->GPUClockSpeed(&clock);
        Console.WriteLine($"[Native] GPU: Temp={temp:F1}C, Usage={usage:F1}%, Clock={clock}MHz");
    }
    else
    {
        Console.WriteLine("[Native] Failed to query GPU metrics.");
    }

    IADLXSystemMetrics* pSysMetrics = null;
    var sysMetricsResult = perf.Get()->GetCurrentSystemMetrics(&pSysMetrics);
    if (sysMetricsResult == ADLX_RESULT.ADLX_OK && pSysMetrics != null)
    {
        using var sysMetrics = new ComPtr<IADLXSystemMetrics>(pSysMetrics);
        double cpu = 0;
        int ram = 0;
        sysMetrics.Get()->CPUUsage(&cpu);
        sysMetrics.Get()->SystemRAM(&ram);
        Console.WriteLine($"[Native] System: CPU={cpu:F1}%, RAM={ram}MB");
    }
    else
    {
        Console.WriteLine("[Native] Failed to query system metrics.");
    }
}

file unsafe sealed class NativeAdlxSession : IDisposable
{
    private readonly IntPtr _module;
    private readonly ADLXNative.ADLXTerminate_Fn _terminate;
    public IADLXSystem* System { get; }
    private bool _disposed;

    private NativeAdlxSession(IntPtr module, ADLXNative.ADLXTerminate_Fn terminate, IADLXSystem* system)
    {
        _module = module;
        _terminate = terminate;
        System = system;
    }

    public static NativeAdlxSession Create()
    {
        var dllName = ADLXNative.GetDllName();
        var module = ADLXNative.LoadLibraryEx(
            dllName,
            IntPtr.Zero,
            ADLXNative.LOAD_LIBRARY_SEARCH_USER_DIRS |
            ADLXNative.LOAD_LIBRARY_SEARCH_APPLICATION_DIR |
            ADLXNative.LOAD_LIBRARY_SEARCH_DEFAULT_DIRS |
            ADLXNative.LOAD_LIBRARY_SEARCH_SYSTEM32);

        if (module == IntPtr.Zero)
            throw new InvalidOperationException($"Failed to load ADLX DLL '{dllName}' (Win32 error {Marshal.GetLastWin32Error()}).");

        try
        {
            var qfv = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryFullVersion_Fn>(module, ADLXNative.GetQueryFullVersionFunctionName());
            ulong fullVersion = 0;
            if (qfv(&fullVersion) != ADLX_RESULT.ADLX_OK)
                throw new InvalidOperationException("ADLXQueryFullVersion failed");

            var initFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXInitialize_Fn>(module, ADLXNative.GetInitializeFunctionName());
            IntPtr pSystem = IntPtr.Zero;
            var init = initFn(fullVersion, &pSystem);
            if (init != ADLX_RESULT.ADLX_OK || pSystem == IntPtr.Zero)
                throw new InvalidOperationException($"ADLXInitialize failed: {init}");

            var termFn = ADLXNative.GetFunctionPointer<ADLXNative.ADLXTerminate_Fn>(module, ADLXNative.GetTerminateFunctionName());
            return new NativeAdlxSession(module, termFn, (IADLXSystem*)pSystem);
        }
        catch
        {
            ADLXNative.FreeLibrary(module);
            throw;
        }
    }

    public void Dispose()
    {
        if (_disposed) return;
        _terminate();
        ADLXNative.FreeLibrary(_module);
        _disposed = true;
        GC.SuppressFinalize(this);
    }

    ~NativeAdlxSession()
    {
        if (!_disposed)
            Dispose();
    }
}
