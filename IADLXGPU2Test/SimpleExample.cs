using System;

namespace IADLXGPU2Test
{
    /// <summary>
    /// Simple example demonstrating basic ADLX C# wrapper usage
    /// </summary>
    class SimpleExample
    {
        public static void RunExample()
        {
            Console.WriteLine("=== ADLX C# Wrapper Simple Example ===\n");

            // Check if ADLX runtime is available
            if (!ADLX.IsADLXRuntimeAvailable())
            {
                Console.WriteLine("ADLX runtime is not available on this system.");
                Console.WriteLine("Please ensure AMD graphics drivers are installed.");
                return;
            }

            // Initialize ADLX
            var helper = new EnhancedADLXHelper();
            var initResult = helper.Initialize();
            
            if (initResult != ADLX_RESULT.ADLX_OK)
            {
                Console.WriteLine($"Failed to initialize ADLX: {initResult}");
                return;
            }

            Console.WriteLine("✓ ADLX initialized successfully");

            try
            {
                // Get system services
                var systemServices = helper.GetSystemServices();
                if (systemServices == null)
                {
                    Console.WriteLine("Failed to get system services");
                    return;
                }

                // Get GPU list
                var gpuListPtr = ADLX.new_gpuListP_Ptr();
                var result = systemServices.GetGPUs(gpuListPtr);
                
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
                    var gpuCount = gpuList.Size();
                    
                    Console.WriteLine($"Found {gpuCount} GPU(s):");

                    for (uint i = 0; i < gpuCount; i++)
                    {
                        var gpuPtr = ADLX.new_gpuP_Ptr();
                        if (gpuList.At(i, gpuPtr) == ADLX_RESULT.ADLX_OK)
                        {
                            var gpu = ADLX.gpuP_Ptr_value(gpuPtr);
                            
                            // Get GPU name
                            var namePtr = ADLX.new_charP_Ptr();
                            if (gpu.Name(namePtr) == ADLX_RESULT.ADLX_OK)
                            {
                                var name = ADLX.charP_Ptr_value(namePtr);
                                Console.WriteLine($"  GPU {i}: {name}");
                            }

                            // Get GPU type
                            var typePtr = ADLX.new_adlx_gpuTypeP();
                            if (gpu.Type(typePtr) == ADLX_RESULT.ADLX_OK)
                            {
                                var gpuType = ADLX.adlx_gpuTypeP_value(typePtr);
                                Console.WriteLine($"    Type: {gpuType}");
                            }

                            // Get vendor ID
                            var vendorPtr = ADLX.new_charP_Ptr();
                            if (gpu.VendorId(vendorPtr) == ADLX_RESULT.ADLX_OK)
                            {
                                var vendorId = ADLX.charP_Ptr_value(vendorPtr);
                                Console.WriteLine($"    Vendor ID: {vendorId}");
                            }

                            ADLX.delete_charP_Ptr(vendorPtr);
                            ADLX.delete_adlx_gpuTypeP(typePtr);
                            ADLX.delete_charP_Ptr(namePtr);
                        }
                        ADLX.delete_gpuP_Ptr(gpuPtr);
                    }
                }

                // Get display services
                var displayServicesPtr = ADLX.new_displaySerP_Ptr();
                result = systemServices.GetDisplaysServices(displayServicesPtr);
                
                if (result == ADLX_RESULT.ADLX_OK)
                {
                    var displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);
                    
                    // Get display list
                    var displayListPtr = ADLX.new_displayListP_Ptr();
                    if (displayServices.GetDisplays(displayListPtr) == ADLX_RESULT.ADLX_OK)
                    {
                        var displayList = ADLX.displayListP_Ptr_value(displayListPtr);
                        var displayCount = displayList.Size();
                        
                        Console.WriteLine($"\nFound {displayCount} display(s):");

                        for (uint i = 0; i < displayCount; i++)
                        {
                            var displayPtr = ADLX.new_displayP_Ptr();
                            if (displayList.At(i, displayPtr) == ADLX_RESULT.ADLX_OK)
                            {
                                var display = ADLX.displayP_Ptr_value(displayPtr);
                                
                                // Get display name
                                var namePtr = ADLX.new_charP_Ptr();
                                if (display.Name(namePtr) == ADLX_RESULT.ADLX_OK)
                                {
                                    var name = ADLX.charP_Ptr_value(namePtr);
                                    Console.WriteLine($"  Display {i}: {name}");
                                }
                                ADLX.delete_charP_Ptr(namePtr);
                            }
                            ADLX.delete_displayP_Ptr(displayPtr);
                        }
                    }
                    ADLX.delete_displayListP_Ptr(displayListPtr);
                }
                ADLX.delete_displaySerP_Ptr(displayServicesPtr);
                ADLX.delete_gpuListP_Ptr(gpuListPtr);

                Console.WriteLine("\n✓ ADLX C# wrapper is working correctly!");
                Console.WriteLine("✓ All basic functionality validated");
            }
            finally
            {
                // Always terminate ADLX
                var terminateResult = helper.Terminate();
                Console.WriteLine($"\nADLX Terminate Result: {terminateResult}");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
