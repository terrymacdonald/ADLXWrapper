using System;
using System.Runtime.InteropServices;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Desktop Sample ===");
    Console.WriteLine(" 1) Facade  - list desktops");
    Console.WriteLine(" 2) Native  - list desktops");
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
                    RunFacade();
                    break;
                case "2":
                    RunNative();
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

static void RunFacade()
{
    using var adlx = ADLXApiHelper.Initialize();
    using var sys = adlx.GetSystemServices();
    var desktops = sys.EnumerateDesktops();
    Console.WriteLine($"[Facade] Found {desktops.Count} desktop(s)");
    foreach (var desk in desktops)
    {
        using (desk)
        {
            var info = desk.Identity;
            Console.WriteLine($"- {info.Type} {info.Width}x{info.Height}");
            foreach (var disp in desk.EnumerateDisplaysForDesktop())
            {
                using (disp)
                {
                    Console.WriteLine($"    Display: {disp.Name} {disp.NativeResolutionWidth}x{disp.NativeResolutionHeight} @ {disp.RefreshRate:F2} Hz");
                }
            }
        }
    }
}

static unsafe void RunNative()
{
    using var session = NativeAdlxSession.Create();

    IADLXDesktopServices* desktopServices = null;
    var dsResult = session.System->GetDesktopsServices(&desktopServices);
    if (dsResult != ADLX_RESULT.ADLX_OK || desktopServices == null)
        throw new InvalidOperationException($"GetDesktopsServices failed: {dsResult}");
    using var ds = new ComPtr<IADLXDesktopServices>(desktopServices);

    IADLXDisplayServices* displayServices = null;
    if (session.System->GetDisplaysServices(&displayServices) != ADLX_RESULT.ADLX_OK || displayServices == null)
        throw new InvalidOperationException("GetDisplaysServices failed");
    using var dispServices = new ComPtr<IADLXDisplayServices>(displayServices);

    IADLXDesktopList* desktopList = null;
    var listResult = ds.Get()->GetDesktops(&desktopList);
    if (listResult != ADLX_RESULT.ADLX_OK || desktopList == null)
        throw new InvalidOperationException($"GetDesktops failed: {listResult}");

    using var list = new ComPtr<IADLXDesktopList>(desktopList);
    var count = list.Get()->Size();
    Console.WriteLine($"[Native] Found {count} desktop(s)");

    for (uint i = 0; i < count; i++)
    {
        IADLXDesktop* pDesktop = null;
        if (list.Get()->At(i, &pDesktop) != ADLX_RESULT.ADLX_OK || pDesktop == null)
            continue;

        using var desk = new ComPtr<IADLXDesktop>(pDesktop);
        int w = 0, h = 0;
        desk.Get()->Size(&w, &h);
        ADLX_DESKTOP_TYPE dtype = default;
        desk.Get()->Type(&dtype);

        Console.WriteLine($"- {dtype} {w}x{h}");

        IADLXDisplayList* pDisplays = null;
        if (desk.Get()->GetDisplays(&pDisplays) == ADLX_RESULT.ADLX_OK && pDisplays != null)
        {
            using var dlist = new ComPtr<IADLXDisplayList>(pDisplays);
            var dcount = dlist.Get()->Size();
            for (uint d = 0; d < dcount; d++)
            {
                IADLXDisplay* pDisp = null;
                if (dlist.Get()->At(d, &pDisp) != ADLX_RESULT.ADLX_OK || pDisp == null)
                    continue;
                using var disp = new ComPtr<IADLXDisplay>(pDisp);
                sbyte* namePtr = null;
                disp.Get()->Name(&namePtr);
                var name = Marshal.PtrToStringUTF8((IntPtr)namePtr) ?? string.Empty;
                int dw = 0, dh = 0;
                disp.Get()->NativeResolution(&dw, &dh);
                double rr = 0;
                disp.Get()->RefreshRate(&rr);
                Console.WriteLine($"    Display: {name} {dw}x{dh} @ {rr:F2} Hz");
            }
        }
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
