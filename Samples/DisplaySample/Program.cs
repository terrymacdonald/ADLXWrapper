using System;
using System.Linq;
using System.Runtime.InteropServices;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Sample ===");
    Console.WriteLine("This sample shows equivalent operations via Facade and Native ADLX interfaces.");
    Console.WriteLine("---------------------------------------------------------");
    Console.WriteLine(" 1) Facade  - list displays with identity & timing");
    Console.WriteLine(" 2) Native  - list displays with identity & timing");
    Console.WriteLine(" Q) Quit");
    Console.WriteLine("---------------------------------------------------------");

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
                    RunFacadeListDisplays();
                    break;
                case "2":
                    RunNativeListDisplays();
                    break;
                default:
                    Console.WriteLine("Unknown option.");
                    break;
            }
        }
        catch (ADLXException ex) when (ex.Result == ADLX_RESULT.ADLX_NOT_SUPPORTED)
        {
            Console.WriteLine($"Feature not supported: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }

        Console.WriteLine();
    }
}

static void RunFacadeListDisplays()
{
    using var adlx = ADLXApiHelper.Initialize();
    using var sys = adlx.GetSystemServices();
    var displays = sys.EnumerateDisplays().ToList();
    Console.WriteLine($"[Facade] Found {displays.Count} display(s)");

    foreach (var display in displays)
    {
        using (display)
        {
            Console.WriteLine($"- {display.Name} (GPU {display.GpuUniqueId})");
            Console.WriteLine($"  Native: {display.NativeResolutionWidth}x{display.NativeResolutionHeight} @ {display.RefreshRate:F2} Hz");
            Console.WriteLine($"  Type={display.Type}, Connector={display.ConnectorType}, Scan={display.ScanType}, MfgId={display.ManufacturerId}");
            Console.WriteLine($"  IDs: DisplayUID={display.UniqueId}, EDID length={display.Edid?.Length ?? 0}, PixelClock={display.PixelClock} kHz");
        }
    }
}

static unsafe void RunNativeListDisplays()
{
    using var session = NativeAdlxSession.Create();

    IADLXDisplayServices* displayServices = null;
    var dsResult = session.System->GetDisplaysServices(&displayServices);
    if (dsResult != ADLX_RESULT.ADLX_OK || displayServices == null)
        throw new InvalidOperationException($"GetDisplaysServices failed: {dsResult}");

    using var ds = new ComPtr<IADLXDisplayServices>(displayServices);

    IADLXDisplayList* displayList = null;
    var listResult = ds.Get()->GetDisplays(&displayList);
    if (listResult != ADLX_RESULT.ADLX_OK || displayList == null)
        throw new InvalidOperationException($"GetDisplays failed: {listResult}");

    using var list = new ComPtr<IADLXDisplayList>(displayList);
    var count = list.Get()->Size();
    Console.WriteLine($"[Native] Found {count} display(s)");

    for (uint i = 0; i < count; i++)
    {
        IADLXDisplay* pDisplay = null;
        var atResult = list.Get()->At(i, &pDisplay);
        if (atResult != ADLX_RESULT.ADLX_OK || pDisplay == null)
            continue;

        using var display = new ComPtr<IADLXDisplay>(pDisplay);
        sbyte* namePtr = null;
        display.Get()->Name(&namePtr);
        var name = Marshal.PtrToStringUTF8((IntPtr)namePtr) ?? string.Empty;

        sbyte* edidPtr = null;
        display.Get()->EDID(&edidPtr);
        var edid = Marshal.PtrToStringUTF8((IntPtr)edidPtr) ?? string.Empty;

        int width = 0, height = 0;
        display.Get()->NativeResolution(&width, &height);

        double refresh = 0;
        display.Get()->RefreshRate(&refresh);

        ADLX_DISPLAY_TYPE type = default;
        display.Get()->DisplayType(&type);
        ADLX_DISPLAY_CONNECTOR_TYPE connector = default;
        display.Get()->ConnectorType(&connector);
        ADLX_DISPLAY_SCAN_TYPE scan = default;
        display.Get()->ScanType(&scan);

        uint manufacturerId = 0;
        display.Get()->ManufacturerID(&manufacturerId);

        nuint displayUid = 0;
        display.Get()->UniqueId(&displayUid);

        uint pixelClock = 0;
        display.Get()->PixelClock(&pixelClock);

        int gpuUid = 0;
        IADLXGPU* pGpu = null;
        if (display.Get()->GetGPU(&pGpu) == ADLX_RESULT.ADLX_OK && pGpu != null)
        {
            using var gpu = new ComPtr<IADLXGPU>(pGpu);
            gpu.Get()->UniqueId(&gpuUid);
        }

        Console.WriteLine($"- {name} (EDID len={edid.Length})");
        Console.WriteLine($"  Native: {width}x{height} @ {refresh:F2} Hz");
        Console.WriteLine($"  Type={type}, Connector={connector}, Scan={scan}, MfgId={manufacturerId}");
        Console.WriteLine($"  IDs: DisplayUID={(ulong)displayUid}, GPUUID={gpuUid}, PixelClock={pixelClock} kHz");
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
            var queryFullVersion = ADLXNative.GetFunctionPointer<ADLXNative.ADLXQueryFullVersion_Fn>(module, ADLXNative.GetQueryFullVersionFunctionName());
            ulong fullVersion = 0;
            var qfv = queryFullVersion(&fullVersion);
            if (qfv != ADLX_RESULT.ADLX_OK)
                throw new InvalidOperationException($"ADLXQueryFullVersion failed: {qfv}");

            var initialize = ADLXNative.GetFunctionPointer<ADLXNative.ADLXInitialize_Fn>(module, ADLXNative.GetInitializeFunctionName());
            IntPtr pSystem = IntPtr.Zero;
            var init = initialize(fullVersion, &pSystem);
            if (init != ADLX_RESULT.ADLX_OK || pSystem == IntPtr.Zero)
                throw new InvalidOperationException($"ADLXInitialize failed: {init}");

            var terminate = ADLXNative.GetFunctionPointer<ADLXNative.ADLXTerminate_Fn>(module, ADLXNative.GetTerminateFunctionName());

            return new NativeAdlxSession(module, terminate, (IADLXSystem*)pSystem);
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
