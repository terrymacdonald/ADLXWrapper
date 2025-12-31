using System;
using System.Runtime.InteropServices;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Multimedia Sample (Video Upscale / VSR) ===");
    Console.WriteLine(" 1) Facade  - query VSR/Video Upscale");
    Console.WriteLine(" 2) Native  - query VSR/Video Upscale");
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
                    RunFacadeMultimedia();
                    break;
                case "2":
                    RunNativeMultimedia();
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

static ADLXInterfaceHandle? GetFirstGpuHandle(ADLXSystemServicesHelper sysHelper)
{
    var gpus = sysHelper.EnumerateGPUsHandle();
    return gpus.Length > 0 ? gpus[0] : null;
}

static unsafe void RunFacadeMultimedia()
{
    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    var gpuHandle = GetFirstGpuHandle(sysHelper);
    if (!gpuHandle.HasValue)
    {
        Console.WriteLine("No AMD GPU found.");
        return;
    }

    using var mmHelper = new ADLXMultimediaServicesHelper(sysHelper.GetMultimediaServicesNative());
    using var gpu = gpuHandle.Value;

    var upscale = mmHelper.GetVideoUpscale(gpu.As<IADLXGPU>());
    Console.WriteLine($"[Facade] Video Upscale: supported={upscale.IsSupported}, enabled={upscale.IsEnabled}, sharpness={upscale.Sharpness}, range=({upscale.SharpnessRange.minValue}-{upscale.SharpnessRange.maxValue})");

    var vsr = mmHelper.GetVideoSuperResolution(gpu.As<IADLXGPU>());
    Console.WriteLine($"[Facade] Video Super Resolution: supported={vsr.IsSupported}, enabled={vsr.IsEnabled}");
}

static unsafe void RunNativeMultimedia()
{
    using var session = NativeAdlxSession.Create();

    IADLXGPUList* gpuList = null;
    if (session.System->GetGPUs(&gpuList) != ADLX_RESULT.ADLX_OK || gpuList == null)
    {
        Console.WriteLine("No AMD GPU found.");
        return;
    }
    using var gpus = new ComPtr<IADLXGPUList>(gpuList);
    if (gpus.Get()->Size() == 0)
    {
        Console.WriteLine("No AMD GPU found.");
        return;
    }
    IADLXGPU* pGpu = null;
    gpus.Get()->At(0, &pGpu);
    using var gpu = new ComPtr<IADLXGPU>(pGpu);

    IntPtr pSystem2 = IntPtr.Zero;
    if (!ADLXUtils.TryQueryInterface((IntPtr)session.System, nameof(IADLXSystem2), out pSystem2) || pSystem2 == IntPtr.Zero)
    {
        Console.WriteLine("Multimedia services not supported (no IADLXSystem2).");
        return;
    }
    using var system2 = new ComPtr<IADLXSystem2>((IADLXSystem2*)pSystem2);

    IADLXMultimediaServices* pMm = null;
    var mmResult = system2.Get()->GetMultimediaServices(&pMm);
    if (mmResult != ADLX_RESULT.ADLX_OK || pMm == null)
    {
        Console.WriteLine("Multimedia services not supported.");
        return;
    }
    using var mm = new ComPtr<IADLXMultimediaServices>(pMm);

    IADLXVideoUpscale* pUpscale = null;
    if (mm.Get()->GetVideoUpscale(gpu.Get(), &pUpscale) == ADLX_RESULT.ADLX_OK && pUpscale != null)
    {
        using var upscale = new ComPtr<IADLXVideoUpscale>(pUpscale);
        bool supported = false, enabled = false;
        int sharpness = 0;
        ADLX_IntRange range = default;
        upscale.Get()->IsSupported(&supported);
        upscale.Get()->IsEnabled(&enabled);
        upscale.Get()->GetSharpnessRange(&range);
        upscale.Get()->GetSharpness(&sharpness);
        Console.WriteLine($"[Native] Video Upscale: supported={supported}, enabled={enabled}, sharpness={sharpness}, range=({range.minValue}-{range.maxValue})");
    }

    IADLXVideoSuperResolution* pVsr = null;
    if (mm.Get()->GetVideoSuperResolution(gpu.Get(), &pVsr) == ADLX_RESULT.ADLX_OK && pVsr != null)
    {
        using var vsr = new ComPtr<IADLXVideoSuperResolution>(pVsr);
        bool supported = false, enabled = false;
        vsr.Get()->IsSupported(&supported);
        vsr.Get()->IsEnabled(&enabled);
        Console.WriteLine($"[Native] Video Super Resolution: supported={supported}, enabled={enabled}");
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
