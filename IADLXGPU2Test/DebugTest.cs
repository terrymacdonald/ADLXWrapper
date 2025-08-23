using System;

class DebugTest
{
    static void Main(string[] args)
    {
        Console.WriteLine("=== ADLX QueryInterface Debug Test ===");
        Console.WriteLine();

        try
        {
            // Test ADLX runtime detection
            Console.WriteLine("1. Testing ADLX Runtime Detection...");
            bool runtimeAvailable = ADLX.IsADLXRuntimeAvailable();
            Console.WriteLine($"   ADLX Runtime Available: {runtimeAvailable}");
            
            if (!runtimeAvailable)
            {
                Console.WriteLine("   ADLX runtime not available. Cannot proceed with interface tests.");
                Console.WriteLine("\n=== Test Completed (Runtime Not Available) ===");
                Console.WriteLine("\nPress any key to exit...");
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
                Console.WriteLine("\n=== Test Completed (Initialization Failed) ===");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                return;
            }

            // Get system services
            Console.WriteLine("\n3. Testing System Services...");
            IADLXSystem systemServices = helper.GetSystemServices();
            if (systemServices != null)
            {
                Console.WriteLine("   System services obtained successfully");
                
                // Test System Interface Capability Detection
                Console.WriteLine("\n4. Testing System Interface Capability Detection...");
                bool supportsSystem1 = ADLX.SupportsSystem1Interface(systemServices);
                bool supportsSystem2 = ADLX.SupportsSystem2Interface(systemServices);
                Console.WriteLine($"   Supports IADLXSystem1: {supportsSystem1}");
                Console.WriteLine($"   Supports IADLXSystem2: {supportsSystem2}");
                
                // Test QueryInterface for System1 (even if not supported)
                Console.WriteLine("\n5. Testing System QueryInterface...");
                IADLXSystem1 system1 = ADLX.QuerySystem1Interface(systemServices);
                if (system1 != null)
                {
                    Console.WriteLine("   *** SUCCESS: IADLXSystem1 interface obtained! ***");
                    
                    // Test QueryInterface for System2
                    IADLXSystem2 system2 = ADLX.QuerySystem2Interface(systemServices);
                    if (system2 != null)
                    {
                        Console.WriteLine("   *** SUCCESS: IADLXSystem2 interface obtained! ***");
                    }
                    else
                    {
                        Console.WriteLine("   IADLXSystem2 interface not available");
                    }
                }
                else
                {
                    Console.WriteLine("   IADLXSystem1 interface not available (this may be expected)");
                }
                
                // Test GPU enumeration regardless of system interface availability
                Console.WriteLine("\n6. Testing GPU Enumeration...");
                
                // Create output parameter for GPU list
                SWIGTYPE_p_p_adlx__IADLXGPUList gpuListPtr = ADLX.new_gpuListP_Ptr();
                ADLX_RESULT getGPUsResult = systemServices.GetGPUs(gpuListPtr);
                
                if (getGPUsResult == ADLX_RESULT.ADLX_OK)
                {
                    IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(gpuListPtr);
                    if (gpuList != null)
                    {
                        uint gpuCount = gpuList.Size();
                        Console.WriteLine($"   Found {gpuCount} GPU(s)");
                        
                        // Test each GPU for interface support
                        for (uint i = 0; i < gpuCount; i++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXGPU gpuPtr = ADLX.new_gpuP_Ptr();
                            ADLX_RESULT getGPUResult = gpuList.At(i, gpuPtr);
                            
                            if (getGPUResult == ADLX_RESULT.ADLX_OK)
                            {
                                IADLXGPU gpu = ADLX.gpuP_Ptr_value(gpuPtr);
                                if (gpu != null)
                                {
                                    // Get GPU name
                                    SWIGTYPE_p_p_char namePtr = ADLX.new_charP_Ptr();
                                    ADLX_RESULT getNameResult = gpu.Name(namePtr);
                                    
                                    string gpuName = "Unknown";
                                    if (getNameResult == ADLX_RESULT.ADLX_OK)
                                    {
                                        gpuName = ADLX.charP_Ptr_value(namePtr);
                                    }
                                    
                                    Console.WriteLine($"\n   GPU {i}: {gpuName}");
                                    
                                    // Test GPU interface capability detection
                                    Console.WriteLine("   Testing GPU Interface Capability Detection:");
                                    bool supportsGPU1 = ADLX.SupportsGPU1Interface(gpu);
                                    bool supportsGPU2 = ADLX.SupportsGPU2Interface(gpu);
                                    Console.WriteLine($"     Supports IADLXGPU1: {supportsGPU1}");
                                    Console.WriteLine($"     Supports IADLXGPU2: {supportsGPU2}");
                                    
                                    // Test GPU QueryInterface
                                    Console.WriteLine("   Testing GPU QueryInterface:");
                                    
                                    // Test IADLXGPU1
                                    IADLXGPU1 gpu1 = ADLX.QueryGPU1Interface(gpu);
                                    if (gpu1 != null)
                                    {
                                        Console.WriteLine("     *** SUCCESS: IADLXGPU1 interface obtained! ***");
                                        
                                        // Test IADLXGPU2 from GPU1
                                        IADLXGPU2 gpu2FromGPU1 = ADLX.QueryGPU2InterfaceFromGPU1(gpu1);
                                        if (gpu2FromGPU1 != null)
                                        {
                                            Console.WriteLine("     *** SUCCESS: IADLXGPU2 interface obtained from GPU1! ***");
                                            Console.WriteLine("     *** IADLXGPU2 QueryInterface WORKING! ***");
                                            
                                            // Test IADLXGPU2 specific features
                                            Console.WriteLine("     Testing IADLXGPU2 Features:");
                                            
                                            try
                                            {
                                                // Test power management features
                                                SWIGTYPE_p_bool powerOffState = ADLX.new_boolP();
                                                ADLX_RESULT powerResult = gpu2FromGPU1.IsPowerOff(powerOffState);
                                                if (powerResult == ADLX_RESULT.ADLX_OK)
                                                {
                                                    bool isPowerOff = ADLX.boolP_value(powerOffState);
                                                    Console.WriteLine($"       IsPowerOff: {isPowerOff}");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"       IsPowerOff failed: {powerResult}");
                                                }
                                                
                                                // Test extended GPU information
                                                SWIGTYPE_p_p_char versionPtr = ADLX.new_charP_Ptr();
                                                ADLX_RESULT versionResult = gpu2FromGPU1.AMDSoftwareVersion(versionPtr);
                                                if (versionResult == ADLX_RESULT.ADLX_OK)
                                                {
                                                    string amdSoftwareVersion = ADLX.charP_Ptr_value(versionPtr);
                                                    Console.WriteLine($"       AMD Software Version: {amdSoftwareVersion}");
                                                }
                                                else
                                                {
                                                    Console.WriteLine($"       AMDSoftwareVersion failed: {versionResult}");
                                                }
                                                
                                                ADLX.delete_charP_Ptr(versionPtr);
                                                ADLX.delete_boolP(powerOffState);
                                                
                                                Console.WriteLine("     *** IADLXGPU2 features tested successfully! ***");
                                            }
                                            catch (Exception ex)
                                            {
                                                Console.WriteLine($"     Error testing IADLXGPU2 features: {ex.Message}");
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("     IADLXGPU2 interface not available from GPU1");
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("     IADLXGPU1 interface not available");
                                    }
                                    
                                    // Test direct IADLXGPU2 QueryInterface
                                    IADLXGPU2 gpu2Direct = ADLX.QueryGPU2Interface(gpu);
                                    if (gpu2Direct != null)
                                    {
                                        Console.WriteLine("     *** SUCCESS: IADLXGPU2 interface obtained directly! ***");
                                    }
                                    else
                                    {
                                        Console.WriteLine("     IADLXGPU2 interface not available directly");
                                    }
                                    
                                    ADLX.delete_charP_Ptr(namePtr);
                                }
                            }
                            ADLX.delete_gpuP_Ptr(gpuPtr);
                        }
                    }
                    else
                    {
                        Console.WriteLine("   Failed to get GPU list from pointer");
                    }
                }
                else
                {
                    Console.WriteLine($"   Failed to get GPU list: {getGPUsResult}");
                }
                
                ADLX.delete_gpuListP_Ptr(gpuListPtr);
            }
            else
            {
                Console.WriteLine("   Failed to get system services");
            }

            // Cleanup
            Console.WriteLine("\n7. Cleaning up...");
            ADLX_RESULT terminateResult = helper.Terminate();
            Console.WriteLine($"   Helper Terminate Result: {terminateResult}");

            Console.WriteLine("\n=== Debug Test Completed ===");
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
