using System;
using System.Runtime.InteropServices;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Power Tuning Sample (read-only) ===");
    Console.WriteLine(" 1) Facade  - SmartShift Max info");
    Console.WriteLine(" 2) Native  - SmartShift Max info");
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
                    RunFacadePower();
                    break;
                case "2":
                    RunNativePower();
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

static unsafe void RunFacadePower()
{
    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = new ADLXSystemServicesHelper(adlx.GetSystemServicesNative());
    using var powerServicesHelper = new ADLXPowerTuningServicesHelper(sysHelper.GetPowerTuningServicesNative());

    var ssm = powerServicesHelper.GetSmartShiftMax();
    Console.WriteLine($"[Facade] SmartShift Max -> supported={ssm.IsSupported}, biasMode={ssm.BiasMode}, biasValue={ssm.BiasValue}, range=({ssm.BiasRange.minValue}-{ssm.BiasRange.maxValue})");
}

static unsafe void RunNativePower()
{
    using var session = NativeAdlxSession.Create();

    // Power tuning lives on IADLXSystem1+
    IntPtr pSystem1 = IntPtr.Zero;
    if (!ADLXUtils.TryQueryInterface((IntPtr)session.System, nameof(IADLXSystem1), out pSystem1) || pSystem1 == IntPtr.Zero)
    {
        Console.WriteLine("Power tuning not supported on this system (no IADLXSystem1).");
        return;
    }
    using var system1 = new ComPtr<IADLXSystem1>((IADLXSystem1*)pSystem1);

    IADLXPowerTuningServices* pServices = null;
    var result = system1.Get()->GetPowerTuningServices(&pServices);
    if (result != ADLX_RESULT.ADLX_OK || pServices == null)
    {
        Console.WriteLine("Power tuning not supported on this system.");
        return;
    }
    using var services = new ComPtr<IADLXPowerTuningServices>(pServices);

    IADLXSmartShiftMax* pMax = null;
    if (services.Get()->GetSmartShiftMax(&pMax) == ADLX_RESULT.ADLX_OK && pMax != null)
    {
        using var max = new ComPtr<IADLXSmartShiftMax>(pMax);
        bool supported = false;
        max.Get()->IsSupported(&supported);
        ADLX_SSM_BIAS_MODE mode = ADLX_SSM_BIAS_MODE.SSM_BIAS_AUTO;
        max.Get()->GetBiasMode(&mode);
        ADLX_IntRange range = default;
        max.Get()->GetBiasRange(&range);
        int bias = 0;
        max.Get()->GetBias(&bias);
        Console.WriteLine($"[Native] SmartShift Max -> supported={supported}, biasMode={mode}, biasValue={bias}, range=({range.minValue}-{range.maxValue})");
    }
    else
    {
        Console.WriteLine("SmartShift Max not supported.");
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
