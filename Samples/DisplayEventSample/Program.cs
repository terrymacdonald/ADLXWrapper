using System;
using ADLXWrapper;

unsafe
{
    Console.WriteLine("=== ADLX Display Event Sample ===");
    Console.WriteLine(" 1) Facade  - display settings listener");
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
                    RunFacadeListener();
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

static void RunFacadeListener()
{
    using var adlx = ADLXApiHelper.Initialize();
    using var sysHelper = adlx.GetSystemServices();
    using var displayHelper = sysHelper.GetDisplayServices();

    using var listener = displayHelper.AddDisplaySettingsEventListener(evt =>
    {
        Console.WriteLine("[Facade] Display settings changed");
        return true; // keep listening
    });

    Console.WriteLine("Facade listener registered. Press Enter to stop...");
    Console.ReadLine();
}


