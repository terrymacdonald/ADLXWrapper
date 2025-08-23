using System;

class SimpleProgram
{
    static void MainOld(string[] args)
    {
        Console.WriteLine("=== ADLX C# Wrapper with IADLXGPU2 Support Test ===");
        Console.WriteLine();

        try
        {
            // Test ADLX runtime detection
            Console.WriteLine("1. Testing ADLX Runtime Detection...");
            bool runtimeAvailable = ADLX.IsADLXRuntimeAvailable();
            Console.WriteLine($"   ADLX Runtime Available: {runtimeAvailable}");
            
            if (!runtimeAvailable)
            {
                Console.WriteLine("   ADLX runtime not available. Please install AMD drivers with ADLX support.");
                Console.WriteLine("   Press any key to exit...");
                Console.ReadKey();
                return;
            }

            // Test Enhanced ADLX Helper initialization
            Console.WriteLine("\n2. Testing Enhanced ADLX Helper...");
            EnhancedADLXHelper helper = new EnhancedADLXHelper();
            ADLX_RESULT initResult = helper.Initialize();
            Console.WriteLine($"   Helper Initialize Result: {initResult}");
            
            if (initResult != ADLX_RESULT.ADLX_OK)
            {
                Console.WriteLine($"   Failed to initialize ADLX Helper: {ADLX.GetADLXErrorDescription(initResult)}");
                Console.WriteLine("   Press any key to exit...");
                Console.ReadKey();
                return;
            }

            // Get system services
            Console.WriteLine("\n3. Testing System Services...");
            IADLXSystem systemServices = helper.GetSystemServices();
            if (systemServices != null)
            {
                Console.WriteLine("   System services obtained successfully");
                
                // Test QueryInterface for System1
                Console.WriteLine("\n4. Testing System Interface Progression...");
                IADLXSystem1 system1 = ADLX.QuerySystem1Interface(systemServices);
                if (system1 != null)
                {
                    Console.WriteLine("   IADLXSystem1 interface obtained successfully");
                    
                    // Test QueryInterface for System2
                    IADLXSystem2 system2 = ADLX.QuerySystem2Interface(systemServices);
                    if (system2 != null)
                    {
                        Console.WriteLine("   IADLXSystem2 interface obtained successfully");
                        
                        // Test GPU enumeration
                        Console.WriteLine("\n5. Testing GPU Enumeration...");
                        IADLXGPUList gpuList = systemServices.GetGPUs();
                        if (gpuList != null)
                        {
                            uint gpuCount = gpuList.Size();
                            Console.WriteLine($"   Found {gpuCount} GPU(s)");
                            
                            // Test each GPU for IADLXGPU2 support
                            for (uint i = 0; i < gpuCount; i++)
                            {
                                IADLXGPU gpu = gpuList.At(i);
                                if (gpu != null)
                                {
                                    string gpuName = gpu.Name();
                                    Console.WriteLine($"\n   GPU {i}: {gpuName}");
                                    
                                    // Test GPU interface progression
                                    Console.WriteLine("   Testing GPU Interface Progression:");
                                    
                                    // Test IADLXGPU1
                                    bool supportsGPU1 = ADLX.SupportsGPU1Interface(gpu);
                                    Console.WriteLine($"     Supports IADLXGPU1: {supportsGPU1}");
                                    
                                    if (supportsGPU1)
                                    {
                                        IADLXGPU1 gpu1 = ADLX.QueryGPU1Interface(gpu);
                                        if (gpu1 != null)
                                        {
                                            Console.WriteLine("     IADLXGPU1 interface obtained successfully");
                                        }
                                    }
                                    
                                    // Test IADLXGPU2 - This is the main goal!
                                    bool supportsGPU2 = ADLX.SupportsGPU2Interface(gpu);
                                    Console.WriteLine($"     Supports IADLXGPU2: {supportsGPU2}");
                                    
                                    if (supportsGPU2)
                                    {
                                        IADLXGPU2 gpu2 = ADLX.QueryGPU2Interface(gpu);
                                        if (gpu2 != null)
                                        {
                                            Console.WriteLine("     IADLXGPU2 interface obtained successfully!");
                                            Console.WriteLine("     *** IADLXGPU2 QueryInterface SUCCESS! ***");
                                            
                                            // Test IADLXGPU2 specific features
                                            Console.WriteLine("     Testing IADLXGPU2 Features:");
                                            
                                            try
                                            {
                                                // Test power management features
                                                bool isPowerOff = gpu2.IsPowerOff();
                                                Console.WriteLine($"       IsPowerOff: {isPowerOff}");
                                                
                                                // Test extended GPU information
                                                string amdSoftwareVersion = gpu2.AMDSoftwareVersion();
                                                Console.WriteLine($"       AMD Software Version: {amdSoftwareVersion}");
                                                
                                                string driverVersion = gpu2.DriverVersion();
                                                Console.WriteLine($"       Driver Version: {driverVersion}");
                                                
                                                Console.WriteLine("     IADLXGPU2 features tested successfully!");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"     Error testing IADLXGPU2 features: {ex.Message}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("     Failed to obtain IADLXGPU2 interface");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("     IADLXGPU2 not supported on this GPU");
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("   Failed to get GPU list");
                        }
                    }
                    else
                    {
                        Console.WriteLine("   IADLXSystem2 interface not available");
                    }
                }
                else
                {
                    Console.WriteLine("   IADLXSystem1 interface not available");
                }
            }
            else
            {
                Console.WriteLine("   Failed to get system services");
            }

            // Cleanup
            Console.WriteLine("\n6. Cleaning up...");
            ADLX_RESULT terminateResult = helper.Terminate();
            Console.WriteLine($"   Helper Terminate Result: {terminateResult}");

            Console.WriteLine("\n=== Test Completed Successfully ===");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error during test: {ex.Message}");
            Console.WriteLine($"Stack trace: {ex.StackTrace}");
        }

        Console.WriteLine("\nPress any key to exit...");
        Console.ReadKey();
    }
}
