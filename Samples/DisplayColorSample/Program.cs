using System;
using System.Runtime.InteropServices;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Color Sample (gamma/gamut/3DLUT/custom color) ===");
    Console.WriteLine(" 1) Facade  - first display color capabilities (read-only)");
    Console.WriteLine(" 2) Native  - first display color capabilities (read-only)");
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
                    RunFacadeColor();
                    break;
                case "2":
                    RunNativeColor();
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

static ADLXDisplay? GetFirstFacadeDisplay(out ADLXApiHelper adlx, out ADLXSystemServicesHelper sys)
{
    adlx = ADLXApiHelper.Initialize();
    sys = adlx.GetSystemServices();
    var displays = sys.EnumerateDisplays();
    if (displays.Count == 0)
    {
        Console.WriteLine("No displays found.");
        adlx.Dispose();
        sys.Dispose();
        return null;
    }

    return displays[0];
}

static void RunFacadeColor()
{
    using var display = GetFirstFacadeDisplay(out var adlx, out var sys);
    if (display == null) return;
    using (adlx)
    using (sys)
    {
        Console.WriteLine($"Using display: {display.Name}");

        var gamma = display.GetGamma();
        Console.WriteLine($"Gamma supported: {gamma.IsSupported}");

        var gamut = display.GetGamut();
        Console.WriteLine($"Gamut supported: {gamut.IsGamutSupported}, custom white point supported: {gamut.IsWhitePointSupported}");

        var lut = display.GetThreeDLut();
        Console.WriteLine($"3DLUT: SCE={lut.IsSceSupported}, VividGaming={lut.IsSceVividGamingSupported}, DynamicContrast={lut.IsSceDynamicContrastSupported}, User3DLUT={lut.IsUser3DLutSupported}");

        var custom = display.GetCustomColor();
        Console.WriteLine($"Custom Color: Supported={custom.IsSupported}, Temperature={custom.Temperature}, Hue={custom.Hue}, Saturation={custom.Saturation}");
    }
}

static unsafe void RunNativeColor()
{
    using var session = NativeAdlxSession.Create();

    IADLXDisplayServices* displayServices = null;
    var dsResult = session.System->GetDisplaysServices(&displayServices);
    if (dsResult != ADLX_RESULT.ADLX_OK || displayServices == null)
    {
        Console.WriteLine("No display services.");
        return;
    }
    using var ds = new ComPtr<IADLXDisplayServices>(displayServices);

    IADLXDisplayList* displayList = null;
    var listResult = ds.Get()->GetDisplays(&displayList);
    if (listResult != ADLX_RESULT.ADLX_OK || displayList == null)
    {
        Console.WriteLine("No displays found.");
        return;
    }
    using var list = new ComPtr<IADLXDisplayList>(displayList);
    if (list.Get()->Size() == 0)
    {
        Console.WriteLine("No displays found.");
        return;
    }

    IADLXDisplay* pDisplay = null;
    if (list.Get()->At(0, &pDisplay) != ADLX_RESULT.ADLX_OK || pDisplay == null)
    {
        Console.WriteLine("Failed to access display.");
        return;
    }

    using var display = new ComPtr<IADLXDisplay>(pDisplay);
    sbyte* namePtr = null;
    display.Get()->Name(&namePtr);
    var name = Marshal.PtrToStringUTF8((IntPtr)namePtr) ?? string.Empty;
    Console.WriteLine($"Using display: {name}");

    IADLXDisplayGamma* pGamma = null;
    if (ds.Get()->GetGamma(display.Get(), &pGamma) == ADLX_RESULT.ADLX_OK && pGamma != null)
    {
        using var gamma = new ComPtr<IADLXDisplayGamma>(pGamma);
        bool supported = false;
        gamma.Get()->IsSupportedReGammaSRGB(&supported);
        Console.WriteLine($"Gamma (sRGB re-gamma) supported: {supported}");
    }

    IADLXDisplayGamut* pGamut = null;
    if (ds.Get()->GetGamut(display.Get(), &pGamut) == ADLX_RESULT.ADLX_OK && pGamut != null)
    {
        using var gamut = new ComPtr<IADLXDisplayGamut>(pGamut);
        bool customSpace = false, customWhitePoint = false;
        gamut.Get()->IsSupportedCustomColorSpace(&customSpace);
        gamut.Get()->IsSupportedCustomWhitePoint(&customWhitePoint);
        Console.WriteLine($"Gamut: CustomColorSpace={customSpace}, CustomWhitePoint={customWhitePoint}");
    }

    IADLXDisplay3DLUT* pLut = null;
    if (ds.Get()->Get3DLUT(display.Get(), &pLut) == ADLX_RESULT.ADLX_OK && pLut != null)
    {
        using var lut = new ComPtr<IADLXDisplay3DLUT>(pLut);
        bool sce = false, vg = false, dc = false, user = false;
        lut.Get()->IsSupportedSCE(&sce);
        lut.Get()->IsSupportedSCEVividGaming(&vg);
        lut.Get()->IsSupportedSCEDynamicContrast(&dc);
        lut.Get()->IsSupportedUser3DLUT(&user);
        Console.WriteLine($"3DLUT: SCE={sce}, VividGaming={vg}, DynamicContrast={dc}, User3DLUT={user}");
    }

    IADLXDisplayCustomColor* pCustom = null;
    if (ds.Get()->GetCustomColor(display.Get(), &pCustom) == ADLX_RESULT.ADLX_OK && pCustom != null)
    {
        using var custom = new ComPtr<IADLXDisplayCustomColor>(pCustom);
        bool hueSupported = false;
        custom.Get()->IsHueSupported(&hueSupported);
        int temperature = 0, hue = 0, sat = 0;
        custom.Get()->GetTemperature(&temperature);
        custom.Get()->GetHue(&hue);
        custom.Get()->GetSaturation(&sat);
        Console.WriteLine($"Custom Color: HueSupported={hueSupported}, Temperature={temperature}, Hue={hue}, Saturation={sat}");
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
