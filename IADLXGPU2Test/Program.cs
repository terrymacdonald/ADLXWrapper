//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------
// IADLXGPU2 Test Application
//
// This application demonstrates comprehensive usage of the enhanced ADLX C# wrapper
// with full IADLXGPU2 support including power management and application monitoring.
//-------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IADLXGPU2Test
{
    class Program
    {
    static void MainOld(string[] args)
        {
            Console.WriteLine("=== ADLX IADLXGPU2 Comprehensive Test Application ===");
            Console.WriteLine("Testing enhanced C# wrapper with QueryInterface support\n");

            // Test ADLX runtime availability first
            Console.WriteLine("1. Testing ADLX Runtime Detection:");
            if (!ADLX.IsADLXRuntimeAvailable())
            {
                Console.WriteLine("   ERROR: ADLX runtime (amdadlx64.dll) not found!");
                Console.WriteLine("   Please ensure AMD drivers with ADLX support are installed.");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("   ✓ ADLX runtime detected");

            // Validate ADLX installation
            ADLX_RESULT validationResult = ADLX.ValidateADLXInstallation();
            Console.WriteLine($"   Installation validation: {ADLX.GetADLXErrorDescription(validationResult)}");
            
            if (validationResult != ADLX_RESULT.ADLX_OK)
            {
                Console.WriteLine("   ERROR: ADLX installation validation failed!");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                return;
            }

            // Initialize Enhanced ADLX Helper
            Console.WriteLine("\n2. Initializing Enhanced ADLX Helper:");
            EnhancedADLXHelper enhancedHelper = new EnhancedADLXHelper();
            ADLX_RESULT initResult = enhancedHelper.Initialize();
            
            if (initResult != ADLX_RESULT.ADLX_OK)
            {
                Console.WriteLine($"   ERROR: Enhanced ADLX Helper initialization failed: {ADLX.GetADLXErrorDescription(initResult)}");
                Console.WriteLine("\nPress any key to exit...");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("   ✓ Enhanced ADLX Helper initialized successfully");

            try
            {
                // Get system services
                Console.WriteLine("\n3. Getting System Services:");
                IADLXSystem system = enhancedHelper.GetSystemServices();
                if (system == null)
                {
                    Console.WriteLine("   ERROR: Failed to get system services");
                    return;
                }
                Console.WriteLine("   ✓ System services obtained");

                // Test System interface progression
                Console.WriteLine("\n4. Testing System Interface Progression:");
                
                // Test System1 interface
                bool supportsSystem1 = ADLX.SupportsSystem1Interface(system);
                Console.WriteLine($"   System1 interface supported: {supportsSystem1}");
                
                if (supportsSystem1)
                {
                    IADLXSystem1 system1 = ADLX.QuerySystem1Interface(system);
                    if (system1 != null)
                    {
                        Console.WriteLine("   ✓ Successfully obtained IADLXSystem1 interface");
                        
                        // Test System2 interface
                        bool supportsSystem2 = ADLX.SupportsSystem2Interface(system);
                        Console.WriteLine($"   System2 interface supported: {supportsSystem2}");
                        
                        if (supportsSystem2)
                        {
                            IADLXSystem2 system2 = ADLX.QuerySystem2InterfaceFromSystem1(system1);
                            if (system2 != null)
                            {
                                Console.WriteLine("   ✓ Successfully obtained IADLXSystem2 interface");
                                
                                // Test multimedia services (System2 feature)
                                TestMultimediaServices(system2);
                                
                                system2.Release();
                            }
                        }
                        
                        system1.Release();
                    }
                }

                // Get GPU list
                Console.WriteLine("\n5. Enumerating GPUs:");
                SWIGTYPE_p_p_adlx__IADLXGPUList ppGPUList = ADLX.new_gpuListP_Ptr();
                ADLX_RESULT gpuResult = system.GetGPUs(ppGPUList);
                
                if (gpuResult != ADLX_RESULT.ADLX_OK)
                {
                    Console.WriteLine($"   ERROR: Failed to get GPU list: {ADLX.GetADLXErrorDescription(gpuResult)}");
                    return;
                }

                IADLXGPUList gpuList = ADLX.gpuListP_Ptr_value(ppGPUList);
                if (gpuList == null)
                {
                    Console.WriteLine("   ERROR: GPU list is null");
                    return;
                }

                uint gpuCount = gpuList.Size();
                Console.WriteLine($"   Found {gpuCount} GPU(s)");

                // Test each GPU
                for (uint i = 0; i < gpuCount; i++)
                {
                    Console.WriteLine($"\n6. Testing GPU [{i}]:");
                    
                    SWIGTYPE_p_p_adlx__IADLXGPU ppGPU = ADLX.new_gpuP_Ptr();
                    ADLX_RESULT gpuAtResult = gpuList.At(i, ppGPU);
                    
                    if (gpuAtResult != ADLX_RESULT.ADLX_OK)
                    {
                        Console.WriteLine($"   ERROR: Failed to get GPU at index {i}");
                        continue;
                    }

                    IADLXGPU gpu = ADLX.gpuP_Ptr_value(ppGPU);
                    if (gpu == null)
                    {
                        Console.WriteLine($"   ERROR: GPU at index {i} is null");
                        continue;
                    }

                    // Get basic GPU info
                    TestBasicGPUInfo(gpu, i);

                    // Test GPU interface progression
                    TestGPUInterfaceProgression(gpu, i);

                    gpu.Release();
                }

                gpuList.Release();
                system.Release();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
            finally
            {
                // Cleanup
                Console.WriteLine("\n7. Cleaning up:");
                ADLX_RESULT terminateResult = enhancedHelper.Terminate();
                Console.WriteLine($"   Enhanced ADLX Helper termination: {ADLX.GetADLXErrorDescription(terminateResult)}");
            }

            Console.WriteLine("\n=== Test completed ===");
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        static void TestBasicGPUInfo(IADLXGPU gpu, uint index)
        {
            try
            {
                // Get GPU name
                SWIGTYPE_p_p_char ppName = ADLX.new_charP_Ptr();
                ADLX_RESULT nameResult = gpu.Name(ppName);
                if (nameResult == ADLX_RESULT.ADLX_OK)
                {
                    string name = ADLX.charP_Ptr_value(ppName);
                    Console.WriteLine($"   GPU Name: {name}");
                }

                // Get GPU type
                SWIGTYPE_p_ADLX_GPU_TYPE pGPUType = ADLX.new_adlx_gpuTypeP();
                ADLX_RESULT typeResult = gpu.Type(pGPUType);
                if (typeResult == ADLX_RESULT.ADLX_OK)
                {
                    ADLX_GPU_TYPE gpuType = ADLX.adlx_gpuTypeP_value(pGPUType);
                    Console.WriteLine($"   GPU Type: {gpuType}");
                }

                // Get VRAM info
                SWIGTYPE_p_unsigned_int pVRAM = ADLX.new_adlx_uintP();
                ADLX_RESULT vramResult = gpu.TotalVRAM(pVRAM);
                if (vramResult == ADLX_RESULT.ADLX_OK)
                {
                    uint vramMB = ADLX.adlx_uintP_value(pVRAM);
                    Console.WriteLine($"   Total VRAM: {vramMB} MB");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ERROR getting basic GPU info: {ex.Message}");
            }
        }

        static void TestGPUInterfaceProgression(IADLXGPU gpu, uint index)
        {
            Console.WriteLine($"   Testing interface progression for GPU [{index}]:");

            // Test GPU1 interface support
            bool supportsGPU1 = ADLX.SupportsGPU1Interface(gpu);
            Console.WriteLine($"   - IADLXGPU1 supported: {supportsGPU1}");

            if (supportsGPU1)
            {
                IADLXGPU1 gpu1 = ADLX.QueryGPU1Interface(gpu);
                if (gpu1 != null)
                {
                    Console.WriteLine("   ✓ Successfully obtained IADLXGPU1 interface");
                    
                    // Test GPU1 specific features
                    TestGPU1Features(gpu1, index);

                    // Test GPU2 interface support
                    bool supportsGPU2 = ADLX.SupportsGPU2Interface(gpu);
                    Console.WriteLine($"   - IADLXGPU2 supported: {supportsGPU2}");

                    if (supportsGPU2)
                    {
                        IADLXGPU2 gpu2 = ADLX.QueryGPU2InterfaceFromGPU1(gpu1);
                        if (gpu2 != null)
                        {
                            Console.WriteLine("   ✓ Successfully obtained IADLXGPU2 interface");
                            
                            // Test GPU2 specific features
                            TestGPU2Features(gpu2, index);
                            
                            gpu2.Release();
                        }
                        else
                        {
                            Console.WriteLine("   ✗ Failed to obtain IADLXGPU2 interface");
                        }
                    }
                    else
                    {
                        Console.WriteLine("   - IADLXGPU2 not supported on this GPU");
                    }

                    gpu1.Release();
                }
                else
                {
                    Console.WriteLine("   ✗ Failed to obtain IADLXGPU1 interface");
                }
            }
            else
            {
                Console.WriteLine("   - IADLXGPU1 not supported on this GPU");
            }
        }

        static void TestGPU1Features(IADLXGPU1 gpu1, uint index)
        {
            try
            {
                Console.WriteLine($"   Testing IADLXGPU1 features for GPU [{index}]:");

                // Test PCI Bus Type
                SWIGTYPE_p_ADLX_PCI_BUS_TYPE pBusType = ADLX.new_adlx_pciBusTypeP();
                ADLX_RESULT busResult = gpu1.PCIBusType(pBusType);
                if (busResult == ADLX_RESULT.ADLX_OK)
                {
                    ADLX_PCI_BUS_TYPE busType = ADLX.adlx_pciBusTypeP_value(pBusType);
                    Console.WriteLine($"     PCI Bus Type: {busType}");
                }

                // Test PCI Bus Lane Width
                SWIGTYPE_p_unsigned_int pLaneWidth = ADLX.new_adlx_uintP();
                ADLX_RESULT laneResult = gpu1.PCIBusLaneWidth(pLaneWidth);
                if (laneResult == ADLX_RESULT.ADLX_OK)
                {
                    uint laneWidth = ADLX.adlx_uintP_value(pLaneWidth);
                    Console.WriteLine($"     PCI Lane Width: {laneWidth}");
                }

                // Test Multi-GPU Mode
                SWIGTYPE_p_ADLX_MGPU_MODE pMGPUMode = ADLX.new_adlx_mgpuModeP();
                ADLX_RESULT mgpuResult = gpu1.MultiGPUMode(pMGPUMode);
                if (mgpuResult == ADLX_RESULT.ADLX_OK)
                {
                    ADLX_MGPU_MODE mgpuMode = ADLX.adlx_mgpuModeP_value(pMGPUMode);
                    Console.WriteLine($"     Multi-GPU Mode: {mgpuMode}");
                }

                // Test Product Name
                SWIGTYPE_p_p_char ppProductName = ADLX.new_charP_Ptr();
                ADLX_RESULT productResult = gpu1.ProductName(ppProductName);
                if (productResult == ADLX_RESULT.ADLX_OK)
                {
                    string productName = ADLX.charP_Ptr_value(ppProductName);
                    Console.WriteLine($"     Product Name: {productName}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"     ERROR testing IADLXGPU1 features: {ex.Message}");
            }
        }

        static void TestGPU2Features(IADLXGPU2 gpu2, uint index)
        {
            try
            {
                Console.WriteLine($"   Testing IADLXGPU2 features for GPU [{index}]:");

                // Test Power Management Features
                Console.WriteLine("     === Power Management ===");
                
                // Check if GPU is powered off
                SWIGTYPE_p_bool pPowerState = ADLX.new_adlx_boolP();
                ADLX_RESULT powerResult = gpu2.IsPowerOff(pPowerState);
                if (powerResult == ADLX_RESULT.ADLX_OK)
                {
                    bool isPowerOff = ADLX.adlx_boolP_value(pPowerState);
                    Console.WriteLine($"     GPU Power State: {(isPowerOff ? "Powered Off" : "Powered On")}");
                }
                else if (powerResult == ADLX_RESULT.ADLX_PENDING_OPERATION)
                {
                    Console.WriteLine("     GPU Power State: Pending operation");
                }
                else
                {
                    Console.WriteLine($"     Failed to get power state: {ADLX.GetADLXErrorDescription(powerResult)}");
                }

                // Test Application List Support
                Console.WriteLine("     === Application Monitoring ===");
                
                SWIGTYPE_p_bool pAppListSupported = ADLX.new_adlx_boolP();
                ADLX_RESULT appSupportResult = gpu2.IsSupportedApplicationList(pAppListSupported);
                if (appSupportResult == ADLX_RESULT.ADLX_OK)
                {
                    bool appListSupported = ADLX.adlx_boolP_value(pAppListSupported);
                    Console.WriteLine($"     Application List Supported: {appListSupported}");
                    
                    if (appListSupported)
                    {
                        TestApplicationList(gpu2, index);
                    }
                }
                else
                {
                    Console.WriteLine($"     Failed to check application list support: {ADLX.GetADLXErrorDescription(appSupportResult)}");
                }

                // Test Extended GPU Information
                Console.WriteLine("     === Extended GPU Information ===");
                
                // AMD Software Version
                SWIGTYPE_p_p_char ppSoftwareVersion = ADLX.new_charP_Ptr();
                ADLX_RESULT softwareResult = gpu2.AMDSoftwareVersion(ppSoftwareVersion);
                if (softwareResult == ADLX_RESULT.ADLX_OK)
                {
                    string softwareVersion = ADLX.charP_Ptr_value(ppSoftwareVersion);
                    Console.WriteLine($"     AMD Software Version: {softwareVersion}");
                }

                // Driver Version
                SWIGTYPE_p_p_char ppDriverVersion = ADLX.new_charP_Ptr();
                ADLX_RESULT driverResult = gpu2.DriverVersion(ppDriverVersion);
                if (driverResult == ADLX_RESULT.ADLX_OK)
                {
                    string driverVersion = ADLX.charP_Ptr_value(ppDriverVersion);
                    Console.WriteLine($"     Driver Version: {driverVersion}");
                }

                // AMD Windows Driver Version
                SWIGTYPE_p_p_char ppWinDriverVersion = ADLX.new_charP_Ptr();
                ADLX_RESULT winDriverResult = gpu2.AMDWindowsDriverVersion(ppWinDriverVersion);
                if (winDriverResult == ADLX_RESULT.ADLX_OK)
                {
                    string winDriverVersion = ADLX.charP_Ptr_value(ppWinDriverVersion);
                    Console.WriteLine($"     AMD Windows Driver Version: {winDriverVersion}");
                }

                // AMD Software Edition
                SWIGTYPE_p_p_char ppSoftwareEdition = ADLX.new_charP_Ptr();
                ADLX_RESULT editionResult = gpu2.AMDSoftwareEdition(ppSoftwareEdition);
                if (editionResult == ADLX_RESULT.ADLX_OK)
                {
                    string softwareEdition = ADLX.charP_Ptr_value(ppSoftwareEdition);
                    Console.WriteLine($"     AMD Software Edition: {softwareEdition}");
                }

                // AMD Software Release Date
                SWIGTYPE_p_unsigned_int pYear = ADLX.new_adlx_uintP();
                SWIGTYPE_p_unsigned_int pMonth = ADLX.new_adlx_uintP();
                SWIGTYPE_p_unsigned_int pDay = ADLX.new_adlx_uintP();
                ADLX_RESULT dateResult = gpu2.AMDSoftwareReleaseDate(pYear, pMonth, pDay);
                if (dateResult == ADLX_RESULT.ADLX_OK)
                {
                    uint year = ADLX.adlx_uintP_value(pYear);
                    uint month = ADLX.adlx_uintP_value(pMonth);
                    uint day = ADLX.adlx_uintP_value(pDay);
                    Console.WriteLine($"     AMD Software Release Date: {year}-{month:D2}-{day:D2}");
                }

                // LUID (Local Unique Identifier)
                SWIGTYPE_p_ADLX_LUID pLUID = ADLX.new_adlx_luidP();
                ADLX_RESULT luidResult = gpu2.LUID(pLUID);
                if (luidResult == ADLX_RESULT.ADLX_OK)
                {
                    ADLX_LUID luid = ADLX.adlx_luidP_value(pLUID);
                    Console.WriteLine($"     LUID: {luid.HighPart:X8}-{luid.LowPart:X8}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"     ERROR testing IADLXGPU2 features: {ex.Message}");
            }
        }

        static void TestApplicationList(IADLXGPU2 gpu2, uint gpuIndex)
        {
            try
            {
                Console.WriteLine($"     Getting application list for GPU [{gpuIndex}]:");
                
                SWIGTYPE_p_p_adlx__IADLXApplicationList ppAppList = ADLX.new_applicationListP_Ptr();
                ADLX_RESULT appResult = gpu2.GetApplications(ppAppList);
                
                if (appResult == ADLX_RESULT.ADLX_OK)
                {
                    IADLXApplicationList appList = ADLX.applicationListP_Ptr_value(ppAppList);
                    if (appList != null)
                    {
                        uint appCount = appList.Size();
                        Console.WriteLine($"     Found {appCount} application(s) running on this GPU");
                        
                        // List first few applications
                        uint maxAppsToShow = Math.Min(appCount, 5);
                        for (uint i = 0; i < maxAppsToShow; i++)
                        {
                            SWIGTYPE_p_p_adlx__IADLXApplication ppApp = ADLX.new_applicationP_Ptr();
                            ADLX_RESULT appAtResult = appList.At(i, ppApp);
                            
                            if (appAtResult == ADLX_RESULT.ADLX_OK)
                            {
                                IADLXApplication app = ADLX.applicationP_Ptr_value(ppApp);
                                if (app != null)
                                {
                                    // Get application name
                                    SWIGTYPE_p_p_char ppAppName = ADLX.new_charP_Ptr();
                                    ADLX_RESULT nameResult = app.Name(ppAppName);
                                    if (nameResult == ADLX_RESULT.ADLX_OK)
                                    {
                                        string appName = ADLX.charP_Ptr_value(ppAppName);
                                        Console.WriteLine($"       [{i}] {appName}");
                                    }
                                    
                                    app.Release();
                                }
                            }
                        }
                        
                        if (appCount > maxAppsToShow)
                        {
                            Console.WriteLine($"       ... and {appCount - maxAppsToShow} more applications");
                        }
                        
                        appList.Release();
                    }
                }
                else
                {
                    Console.WriteLine($"     Failed to get application list: {ADLX.GetADLXErrorDescription(appResult)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"     ERROR testing application list: {ex.Message}");
            }
        }

        static void TestMultimediaServices(IADLXSystem2 system2)
        {
            try
            {
                Console.WriteLine("   Testing System2 Multimedia Services:");
                
                SWIGTYPE_p_p_adlx__IADLXMultimediaServices ppMultimedia = ADLX.new_multimediaServicesP_Ptr();
                ADLX_RESULT multimediaResult = system2.GetMultimediaServices(ppMultimedia);
                
                if (multimediaResult == ADLX_RESULT.ADLX_OK)
                {
                    IADLXMultimediaServices multimedia = ADLX.multimediaServicesP_Ptr_value(ppMultimedia);
                    if (multimedia != null)
                    {
                        Console.WriteLine("   ✓ Successfully obtained multimedia services");
                        multimedia.Release();
                    }
                }
                else
                {
                    Console.WriteLine($"   Multimedia services not available: {ADLX.GetADLXErrorDescription(multimediaResult)}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"   ERROR testing multimedia services: {ex.Message}");
            }
        }
    }
}
