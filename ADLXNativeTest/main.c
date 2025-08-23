//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------
// Native C ADLX Test Application
//
// This application tests ADLX functionality using the native C API to isolate
// whether issues are with the ADLX library itself or the SWIG wrapper.
//-------------------------------------------------------------------------------------------------

#include <stdio.h>
#include <stdlib.h>
#include <string.h>

// Include ADLX headers using the official helper
#include "../ADLX/SDK/ADLXHelper/Windows/C/ADLXHelper.h"
#include "../ADLX/SDK/Include/ISystem.h"
#include "../ADLX/SDK/Include/ISystem1.h"
#include "../ADLX/SDK/Include/ISystem2.h"
#include "../ADLX/SDK/Include/IPowerTuning1.h"
#include "../ADLX/SDK/Include/IApplications.h"

// Global variables
IADLXSystem* g_pSystem = NULL;

// Helper function to get ADLX error description
const char* GetADLXErrorDescription(ADLX_RESULT result) {
    switch (result) {
        case ADLX_OK: return "Success";
        case ADLX_ALREADY_ENABLED: return "Already enabled";
        case ADLX_ALREADY_INITIALIZED: return "Already initialized";
        case ADLX_FAIL: return "General failure";
        case ADLX_INVALID_ARGS: return "Invalid arguments";
        case ADLX_BAD_VER: return "Bad version";
        case ADLX_UNKNOWN_INTERFACE: return "Unknown interface";
        case ADLX_TERMINATED: return "ADLX terminated";
        case ADLX_ADL_INIT_ERROR: return "ADL initialization error";
        case ADLX_NOT_FOUND: return "Not found";
        case ADLX_INVALID_OBJECT: return "Invalid object";
        case ADLX_ORPHAN_OBJECTS: return "Orphan objects";
        case ADLX_NOT_SUPPORTED: return "Not supported";
        case ADLX_PENDING_OPERATION: return "Pending operation";
        case ADLX_GPU_INACTIVE: return "GPU inactive";
        case ADLX_GPU_IN_USE: return "GPU in use";
        case ADLX_TIMEOUT_OPERATION: return "Timeout operation";
        case ADLX_NOT_ACTIVE: return "Not active";
        default: return "Unknown error";
    }
}

// Initialize ADLX using official ADLXHelper
ADLX_RESULT InitializeADLX() {
    printf("1. Initializing ADLX using ADLXHelper...\n");
    
    // Initialize ADLX using the official helper
    ADLX_RESULT result = ADLXHelper_Initialize();
    printf("   ADLXHelper_Initialize result: %s\n", GetADLXErrorDescription(result));
    
    if (result != ADLX_OK) {
        printf("   ERROR: ADLXHelper initialization failed\n");
        return result;
    }
    
    printf("   SUCCESS: ADLXHelper initialized\n");
    
    // Get system services interface
    printf("\n2. Getting system services interface...\n");
    g_pSystem = ADLXHelper_GetSystemServices();
    
    if (g_pSystem == NULL) {
        printf("   ERROR: Failed to get system services interface\n");
        ADLXHelper_Terminate();
        return ADLX_FAIL;
    }
    
    printf("   SUCCESS: System services interface obtained\n");
    return ADLX_OK;
}

// Test System interface progression
void TestSystemInterfaces() {
    printf("\n3. Testing System Interface Progression...\n");
    
    if (g_pSystem == NULL) {
        printf("   ERROR: System interface is NULL\n");
        return;
    }
    
    // Test IADLXSystem1
    printf("   Testing IADLXSystem1 QueryInterface...\n");
    IADLXSystem1* pSystem1 = NULL;
    ADLX_RESULT result = g_pSystem->pVtbl->QueryInterface(g_pSystem, IID_IADLXSystem1(), (void**)&pSystem1);
    printf("   IADLXSystem1 QueryInterface result: %s\n", GetADLXErrorDescription(result));
    
    if (result == ADLX_OK && pSystem1 != NULL) {
        printf("   SUCCESS: IADLXSystem1 interface obtained!\n");
        
        // Test System1 functionality - GetPowerTuningServices
        printf("   Testing System1 GetPowerTuningServices...\n");
        IADLXPowerTuningServices* pPowerTuningServices = NULL;
        result = pSystem1->pVtbl->GetPowerTuningServices(pSystem1, &pPowerTuningServices);
        printf("   GetPowerTuningServices result: %s\n", GetADLXErrorDescription(result));
        
        if (result == ADLX_OK && pPowerTuningServices != NULL) {
            printf("   SUCCESS: PowerTuningServices obtained from System1!\n");
            printf("   *** SYSTEM1 VTBL LOOKUP WORKING! ***\n");
            pPowerTuningServices->pVtbl->Release(pPowerTuningServices);
        } else {
            printf("   PowerTuningServices not available from System1\n");
        }
        
        // Test IADLXSystem2
        printf("   Testing IADLXSystem2 QueryInterface...\n");
        IADLXSystem2* pSystem2 = NULL;
        result = pSystem1->pVtbl->QueryInterface(pSystem1, IID_IADLXSystem2(), (void**)&pSystem2);
        printf("   IADLXSystem2 QueryInterface result: %s\n", GetADLXErrorDescription(result));
        
        if (result == ADLX_OK && pSystem2 != NULL) {
            printf("   SUCCESS: IADLXSystem2 interface obtained!\n");
            pSystem2->pVtbl->Release(pSystem2);
        } else {
            printf("   IADLXSystem2 interface not available\n");
        }
        
        pSystem1->pVtbl->Release(pSystem1);
    } else {
        printf("   IADLXSystem1 interface not available\n");
    }
}

// Test GPU enumeration and interface progression
void TestGPUInterfaces() {
    printf("\n4. Testing GPU Enumeration and Interface Progression...\n");
    
    if (g_pSystem == NULL) {
        printf("   ERROR: System interface is NULL\n");
        return;
    }
    
    // Get GPU list
    IADLXGPUList* pGPUList = NULL;
    ADLX_RESULT result = g_pSystem->pVtbl->GetGPUs(g_pSystem, &pGPUList);
    printf("   GetGPUs result: %s\n", GetADLXErrorDescription(result));
    
    if (result != ADLX_OK || pGPUList == NULL) {
        printf("   ERROR: Failed to get GPU list\n");
        return;
    }
    
    // Get GPU count
    adlx_uint gpuCount = pGPUList->pVtbl->Size(pGPUList);
    if (gpuCount == 0) {
        printf("   ERROR: No GPUs found\n");
        pGPUList->pVtbl->Release(pGPUList);
        return;
    }
    
    printf("   Found %u GPU(s)\n", gpuCount);
    
    // Test each GPU
    for (adlx_uint i = 0; i < gpuCount; i++) {
        printf("\n   Testing GPU [%u]:\n", i);
        
        IADLXGPU* pGPU = NULL;
        result = pGPUList->pVtbl->At_GPUList(pGPUList, i, &pGPU);
        if (result != ADLX_OK || pGPU == NULL) {
            printf("     ERROR: Failed to get GPU at index %u\n", i);
            continue;
        }
        
        // Get GPU name
        const char* gpuName = NULL;
        result = pGPU->pVtbl->Name(pGPU, &gpuName);
        if (result == ADLX_OK && gpuName != NULL) {
            printf("     GPU Name: %s\n", gpuName);
        } else {
            printf("     GPU Name: Unknown\n");
        }
        
        // Test IADLXGPU1 interface (using AMD's exact approach)
        printf("     Testing IADLXGPU1 QueryInterface (AMD method)...\n");
        IADLXGPU1* pGPU1 = NULL;
        result = pGPU->pVtbl->QueryInterface(pGPU, IID_IADLXGPU1(), &pGPU1);
        printf("     IADLXGPU1 QueryInterface result: %s\n", GetADLXErrorDescription(result));
        
        if (ADLX_SUCCEEDED(result) && pGPU1 != NULL) {
            printf("     SUCCESS: IADLXGPU1 interface obtained!\n");
            printf("     *** GPU1 VTBL LOOKUP WORKING! ***\n");
            
            // Test GPU1 functionality - ProductName (AMD's exact approach)
            printf("     Testing GPU1 ProductName (AMD method)...\n");
            const char* productName = NULL;
            result = pGPU1->pVtbl->ProductName(pGPU1, &productName);
            printf("     ProductName result: %s\n", GetADLXErrorDescription(result));
            if (ADLX_SUCCEEDED(result) && productName != NULL) {
                printf("     Product Name: %s\n", productName);
            }
            
            // Test GPU1 functionality - PCIBusType (AMD's exact approach)
            printf("     Testing GPU1 PCIBusType (AMD method)...\n");
            ADLX_PCI_BUS_TYPE busType = UNDEFINED;
            result = pGPU1->pVtbl->PCIBusType(pGPU1, &busType);
            printf("     PCIBusType result: %s\n", GetADLXErrorDescription(result));
            if (ADLX_SUCCEEDED(result)) {
                printf("     PCI Bus Type: %d\n", busType);
            }
            
            // Test GPU1 functionality - MultiGPUMode (AMD's exact approach)
            printf("     Testing GPU1 MultiGPUMode (AMD method)...\n");
            ADLX_MGPU_MODE mode = MGPU_NONE;
            result = pGPU1->pVtbl->MultiGPUMode(pGPU1, &mode);
            printf("     MultiGPUMode result: %s\n", GetADLXErrorDescription(result));
            if (ADLX_SUCCEEDED(result)) {
                printf("     Multi-GPU Mode: ");
                if (mode == MGPU_PRIMARY)
                    printf("GPU is the primary GPU\n");
                else if (mode == MGPU_SECONDARY)
                    printf("GPU is the secondary GPU\n");
                else
                    printf("GPU is not in Multi-GPU\n");
            }
            
            // Test IADLXGPU2 interface
            printf("     Testing IADLXGPU2 QueryInterface...\n");
            IADLXGPU2* pGPU2 = NULL;
            result = pGPU1->pVtbl->QueryInterface(pGPU1, IID_IADLXGPU2(), (void**)&pGPU2);
            printf("     IADLXGPU2 QueryInterface result: %s\n", GetADLXErrorDescription(result));
            
            if (result == ADLX_OK && pGPU2 != NULL) {
                printf("     SUCCESS: IADLXGPU2 interface obtained!\n");
                printf("     *** NATIVE C IADLXGPU2 WORKING! ***\n");
                
                // Test IADLXGPU2 specific features
                printf("     Testing IADLXGPU2 Features:\n");
                
                // Test power management
                adlx_bool isPowerOff = false;
                result = pGPU2->pVtbl->IsPowerOff(pGPU2, &isPowerOff);
                if (result == ADLX_OK) {
                    printf("       IsPowerOff: %s\n", isPowerOff ? "True" : "False");
                } else {
                    printf("       IsPowerOff failed: %s\n", GetADLXErrorDescription(result));
                }
                
                // Test AMD Software Version
                const char* softwareVersion = NULL;
                result = pGPU2->pVtbl->AMDSoftwareVersion(pGPU2, &softwareVersion);
                if (result == ADLX_OK && softwareVersion != NULL) {
                    printf("       AMD Software Version: %s\n", softwareVersion);
                } else {
                    printf("       AMDSoftwareVersion failed: %s\n", GetADLXErrorDescription(result));
                }
                
                // Test Driver Version
                const char* driverVersion = NULL;
                result = pGPU2->pVtbl->DriverVersion(pGPU2, &driverVersion);
                if (result == ADLX_OK && driverVersion != NULL) {
                    printf("       Driver Version: %s\n", driverVersion);
                } else {
                    printf("       DriverVersion failed: %s\n", GetADLXErrorDescription(result));
                }
                
                // Test Application List Support
                adlx_bool appListSupported = false;
                result = pGPU2->pVtbl->IsSupportedApplicationList(pGPU2, &appListSupported);
                if (result == ADLX_OK) {
                    printf("       Application List Supported: %s\n", appListSupported ? "True" : "False");
                } else {
                    printf("       IsSupportedApplicationList failed: %s\n", GetADLXErrorDescription(result));
                }
                
                printf("     IADLXGPU2 features tested successfully!\n");
                pGPU2->pVtbl->Release(pGPU2);
            } else {
                printf("     IADLXGPU2 interface not available\n");
            }
            
            pGPU1->pVtbl->Release(pGPU1);
        } else {
            printf("     IADLXGPU1 interface not available\n");
        }
        
        pGPU->pVtbl->Release(pGPU);
    }
    
    pGPUList->pVtbl->Release(pGPUList);
}

// Test IADLXGPU2 access through Power Tuning Services (AMD Official Pattern)
void TestPowerTuningGPU2Access() {
    printf("\n5. Testing IADLXGPU2 Access via Power Tuning Services (AMD Official Pattern)...\n");
    printf("   Using AMD documented pattern: System → System2 → PowerTuning → PowerTuning1 → GPU Connect\n");
    
    if (g_pSystem == NULL) {
        printf("   ERROR: System interface is NULL\n");
        return;
    }
    
    // Get IADLXSystem2 interface (AMD official approach)
    printf("   Getting IADLXSystem2 interface...\n");
    IADLXSystem2* pSystem2 = NULL;
    ADLX_RESULT result = g_pSystem->pVtbl->QueryInterface(g_pSystem, IID_IADLXSystem2(), (void**)&pSystem2);
    printf("   IADLXSystem2 QueryInterface result: %s\n", GetADLXErrorDescription(result));
    
    if (result != ADLX_OK || pSystem2 == NULL) {
        printf("   ERROR: IADLXSystem2 interface not available on this system\n");
        printf("   This means GPU Connect and IADLXGPU2 features are not supported\n");
        return;
    }
    
    printf("   SUCCESS: IADLXSystem2 interface obtained!\n");
    
    // Get Power Tuning Services from System2 (AMD official approach)
    printf("   Getting Power Tuning Services from System2...\n");
    IADLXPowerTuningServices* pPowerTuningService = NULL;
    result = pSystem2->pVtbl->GetPowerTuningServices(pSystem2, &pPowerTuningService);
    printf("   GetPowerTuningServices result: %s\n", GetADLXErrorDescription(result));
    
    if (result == ADLX_OK && pPowerTuningService != NULL) {
        printf("   SUCCESS: Power Tuning Services obtained!\n");
        
        // Get Power Tuning Services1 for advanced features
        printf("   Getting Power Tuning Services1...\n");
        IADLXPowerTuningServices1* pPowerTuningService1 = NULL;
        result = pPowerTuningService->pVtbl->QueryInterface(pPowerTuningService, IID_IADLXPowerTuningServices1(), (void**)&pPowerTuningService1);
        printf("   IADLXPowerTuningServices1 QueryInterface result: %s\n", GetADLXErrorDescription(result));
        
        if (result == ADLX_OK && pPowerTuningService1 != NULL) {
            printf("   SUCCESS: Power Tuning Services1 obtained!\n");
            
            // Check GPU Connect support
            printf("   Checking GPU Connect support...\n");
            adlx_bool gpuConnectSupported = false;
            result = pPowerTuningService1->pVtbl->IsGPUConnectSupported(pPowerTuningService1, &gpuConnectSupported);
            printf("   GPU Connect supported: %s\n", gpuConnectSupported ? "True" : "False");
            
            if (gpuConnectSupported) {
                // Get IADLXGPU2 list through Power Tuning Services
                printf("   Getting IADLXGPU2 list via Power Tuning Services...\n");
                IADLXGPU2List* pGPU2List = NULL;
                result = pPowerTuningService1->pVtbl->GetGPUConnectGPUs(pPowerTuningService1, &pGPU2List);
                printf("   GetGPUConnectGPUs result: %s\n", GetADLXErrorDescription(result));
                
                if (result == ADLX_OK && pGPU2List != NULL && !pGPU2List->pVtbl->Empty(pGPU2List)) {
                    printf("   SUCCESS: IADLXGPU2 list obtained via Power Tuning!\n");
                    printf("   *** NATIVE C IADLXGPU2 ACCESS VIA POWER TUNING WORKING! ***\n");
                    
                    // Test the first GPU in the list
                    IADLXGPU2* pGPU2 = NULL;
                    result = pGPU2List->pVtbl->At_GPU2List(pGPU2List, pGPU2List->pVtbl->Begin(pGPU2List), &pGPU2);
                    
                    if (result == ADLX_OK && pGPU2 != NULL) {
                        printf("   Testing IADLXGPU2 Features via Power Tuning:\n");
                        
                        // Test power management
                        adlx_bool isPowerOff = false;
                        result = pGPU2->pVtbl->IsPowerOff(pGPU2, &isPowerOff);
                        if (result == ADLX_OK) {
                            printf("     IsPowerOff: %s\n", isPowerOff ? "True" : "False");
                        } else {
                            printf("     IsPowerOff failed: %s\n", GetADLXErrorDescription(result));
                        }
                        
                        // Test AMD Software Version
                        const char* softwareVersion = NULL;
                        result = pGPU2->pVtbl->AMDSoftwareVersion(pGPU2, &softwareVersion);
                        if (result == ADLX_OK && softwareVersion != NULL) {
                            printf("     AMD Software Version: %s\n", softwareVersion);
                        } else {
                            printf("     AMDSoftwareVersion failed: %s\n", GetADLXErrorDescription(result));
                        }
                        
                        // Test Driver Version
                        const char* driverVersion = NULL;
                        result = pGPU2->pVtbl->DriverVersion(pGPU2, &driverVersion);
                        if (result == ADLX_OK && driverVersion != NULL) {
                            printf("     Driver Version: %s\n", driverVersion);
                        } else {
                            printf("     DriverVersion failed: %s\n", GetADLXErrorDescription(result));
                        }
                        
                        // Test Application List Support
                        adlx_bool appListSupported = false;
                        result = pGPU2->pVtbl->IsSupportedApplicationList(pGPU2, &appListSupported);
                        if (result == ADLX_OK) {
                            printf("     Application List Supported: %s\n", appListSupported ? "True" : "False");
                            
                            if (appListSupported) {
                                // Try to get application list
                                IADLXApplicationList* pAppList = NULL;
                                result = pGPU2->pVtbl->GetApplications(pGPU2, &pAppList);
                                if (result == ADLX_OK && pAppList != NULL) {
                                    adlx_uint appCount = pAppList->pVtbl->Size(pAppList);
                                    printf("     Found %u applications running on GPU\n", appCount);
                                    pAppList->pVtbl->Release(pAppList);
                                } else {
                                    printf("     GetApplications failed: %s\n", GetADLXErrorDescription(result));
                                }
                            }
                        } else {
                            printf("     IsSupportedApplicationList failed: %s\n", GetADLXErrorDescription(result));
                        }
                        
                        printf("   IADLXGPU2 Power Tuning features tested successfully!\n");
                        pGPU2->pVtbl->Release(pGPU2);
                    }
                    
                    pGPU2List->pVtbl->Release(pGPU2List);
                } else {
                    printf("   No IADLXGPU2 devices found via Power Tuning Services\n");
                }
            } else {
                printf("   GPU Connect not supported - IADLXGPU2 may not be available\n");
            }
            
            pPowerTuningService1->pVtbl->Release(pPowerTuningService1);
        } else {
            printf("   Failed to get Power Tuning Services1: %s\n", GetADLXErrorDescription(result));
        }
        
        pPowerTuningService->pVtbl->Release(pPowerTuningService);
    } else {
        printf("   Failed to get Power Tuning Services: %s\n", GetADLXErrorDescription(result));
    }
    
    pSystem2->pVtbl->Release(pSystem2);
}

// Cleanup ADLX using official ADLXHelper
void TerminateADLX() {
    printf("\n5. Cleaning up ADLX...\n");
    
    // Note: IADLXSystem is a singleton interface and should not be released manually
    // The system interface will be cleaned up automatically by ADLXHelper_Terminate
    if (g_pSystem != NULL) {
        printf("   System interface will be cleaned up by ADLXHelper_Terminate\n");
        g_pSystem = NULL;
    }
    
    // Terminate ADLX using the official helper
    ADLX_RESULT result = ADLXHelper_Terminate();
    printf("   ADLXHelper_Terminate result: %s\n", GetADLXErrorDescription(result));
    printf("   ADLX cleanup completed\n");
}

int main() {
    printf("=== Native C ADLX Test Application ===\n");
    printf("Testing ADLX functionality using native C API\n");
    printf("This will isolate whether issues are with ADLX library or SWIG wrapper\n\n");
    
    // Initialize ADLX
    ADLX_RESULT result = InitializeADLX();
    if (result != ADLX_OK) {
        printf("\nFATAL ERROR: ADLX initialization failed\n");
        TerminateADLX();
        printf("\nPress any key to exit...\n");
        getchar();
        return 1;
    }
    
    // Test system interfaces
    TestSystemInterfaces();
    
    // Test GPU interfaces
    TestGPUInterfaces();
    
    // Test Power Tuning GPU2 access (AMD recommended approach)
    TestPowerTuningGPU2Access();
    
    // Cleanup
    TerminateADLX();
    
    printf("\n=== Native C ADLX Test Completed ===\n");
    printf("\nPress any key to exit...\n");
    getchar();
    
    return 0;
}
