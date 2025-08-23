//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------
// Simplified ADLX Test Based on AMD's Official mainGPUs.c Sample
//
// This test follows AMD's exact approach to isolate interface issues
//-------------------------------------------------------------------------------------------------

#include <stdio.h>
#include <stdlib.h>

// Include ADLX headers using AMD's exact pattern
#include "../ADLX/SDK/ADLXHelper/Windows/C/ADLXHelper.h"
#include "../ADLX/SDK/Include/ISystem2.h"

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

// Display GPU information using AMD's exact approach
void ShowGPUInfo(IADLXGPU* gpu)
{
    if (gpu != NULL)
    {
        printf("\n==== GPU info (AMD Official Method) ====\n");
        
        // Show basic GPU info first
        const char* vendorId = NULL;
        ADLX_RESULT ret = gpu->pVtbl->VendorId(gpu, &vendorId);
        printf("VendorId: %s, return code: %s\n", vendorId ? vendorId : "NULL", GetADLXErrorDescription(ret));

        ADLX_ASIC_FAMILY_TYPE asicFamilyType = ASIC_UNDEFINED;
        ret = gpu->pVtbl->ASICFamilyType(gpu, &asicFamilyType);
        printf("ASICFamilyType: %d, return code: %s\n", asicFamilyType, GetADLXErrorDescription(ret));

        ADLX_GPU_TYPE gpuType = GPUTYPE_UNDEFINED;
        ret = gpu->pVtbl->Type(gpu, &gpuType);
        printf("Type: %d, return code: %s\n", gpuType, GetADLXErrorDescription(ret));

        adlx_bool isExternal = false;
        ret = gpu->pVtbl->IsExternal(gpu, &isExternal);
        printf("IsExternal: %d, return code: %s\n", isExternal, GetADLXErrorDescription(ret));

        const char* gpuName = NULL;
        ret = gpu->pVtbl->Name(gpu, &gpuName);
        printf("Name: %s, return code: %s\n", gpuName ? gpuName : "NULL", GetADLXErrorDescription(ret));

        const char* driverPath = NULL;
        ret = gpu->pVtbl->DriverPath(gpu, &driverPath);
        printf("DriverPath: %s, return code: %s\n", driverPath ? driverPath : "NULL", GetADLXErrorDescription(ret));

        const char* pnpString = NULL;
        ret = gpu->pVtbl->PNPString(gpu, &pnpString);
        printf("PNPString: %s, return code: %s\n", pnpString ? pnpString : "NULL", GetADLXErrorDescription(ret));

        adlx_bool hasDesktops = false;
        ret = gpu->pVtbl->HasDesktops(gpu, &hasDesktops);
        printf("HasDesktops: %d, return code: %s\n", hasDesktops, GetADLXErrorDescription(ret));

        adlx_uint totalVRAM = 0;
        ret = gpu->pVtbl->TotalVRAM(gpu, &totalVRAM);
        printf("TotalVRAM: %d MB, return code: %s\n", totalVRAM, GetADLXErrorDescription(ret));

        adlx_int id;
        ret = gpu->pVtbl->UniqueId(gpu, &id);
        printf("UniqueId: %d, return code: %s\n", id, GetADLXErrorDescription(ret));

        // Test IADLXGPU1 using AMD's exact approach
        printf("\n--- Testing IADLXGPU1 (AMD Official Method) ---\n");
        IADLXGPU1* gpu1 = NULL;
        ret = gpu->pVtbl->QueryInterface(gpu, IID_IADLXGPU1(), &gpu1);
        printf("IADLXGPU1 QueryInterface result: %s\n", GetADLXErrorDescription(ret));
        
        if (ADLX_SUCCEEDED(ret) && gpu1 != NULL)
        {
            printf("*** SUCCESS: IADLXGPU1 interface obtained! ***\n");
            
            const char* productName = NULL;
            ret = gpu1->pVtbl->ProductName(gpu1, &productName);
            printf("ProductName: %s (result: %s)\n", productName ? productName : "NULL", GetADLXErrorDescription(ret));

            ADLX_MGPU_MODE mode = MGPU_NONE;
            ret = gpu1->pVtbl->MultiGPUMode(gpu1, &mode);
            printf("Multi-GPU Mode: ");
            if (ADLX_SUCCEEDED(ret)) {
                if (mode == MGPU_PRIMARY)
                    printf("GPU is the primary GPU\n");
                else if (mode == MGPU_SECONDARY)
                    printf("GPU is the secondary GPU\n");
                else
                    printf("GPU is not in Multi-GPU\n");
            } else {
                printf("Failed to get mode (%s)\n", GetADLXErrorDescription(ret));
            }

            ADLX_PCI_BUS_TYPE busType = UNDEFINED;
            ret = gpu1->pVtbl->PCIBusType(gpu1, &busType);
            printf("PCIBusType: %d (result: %s)\n", busType, GetADLXErrorDescription(ret));

            adlx_uint laneWidth = 0;
            ret = gpu1->pVtbl->PCIBusLaneWidth(gpu1, &laneWidth);
            printf("PCIBusLaneWidth: %d (result: %s)\n", laneWidth, GetADLXErrorDescription(ret));

            gpu1->pVtbl->Release(gpu1);
            gpu1 = NULL;
        }
        else
        {
            printf("IADLXGPU1 interface NOT available on this hardware/driver\n");
        }

        // Test IADLXGPU2 using AMD's exact approach
        printf("\n--- Testing IADLXGPU2 (AMD Official Method) ---\n");
        IADLXGPU2* gpu2 = NULL;
        ret = gpu->pVtbl->QueryInterface(gpu, IID_IADLXGPU2(), &gpu2);
        printf("IADLXGPU2 QueryInterface result: %s\n", GetADLXErrorDescription(ret));
        
        if (ADLX_SUCCEEDED(ret) && gpu2 != NULL)
        {
            printf("*** SUCCESS: IADLXGPU2 interface obtained! ***\n");
            
            const char* driverInfo = NULL;
            ret = gpu2->pVtbl->AMDSoftwareEdition(gpu2, &driverInfo);
            printf("AMD software edition: %s (result: %s)\n", driverInfo ? driverInfo : "NULL", GetADLXErrorDescription(ret));
            
            ret = gpu2->pVtbl->AMDSoftwareVersion(gpu2, &driverInfo);
            printf("AMD software version: %s (result: %s)\n", driverInfo ? driverInfo : "NULL", GetADLXErrorDescription(ret));
            
            ret = gpu2->pVtbl->DriverVersion(gpu2, &driverInfo);
            printf("driver version: %s (result: %s)\n", driverInfo ? driverInfo : "NULL", GetADLXErrorDescription(ret));
            
            ret = gpu2->pVtbl->AMDWindowsDriverVersion(gpu2, &driverInfo);
            printf("AMD Windows driver version: %s (result: %s)\n", driverInfo ? driverInfo : "NULL", GetADLXErrorDescription(ret));
            
            adlx_uint year, month, day;
            ret = gpu2->pVtbl->AMDSoftwareReleaseDate(gpu2, &year, &month, &day);
            if (ADLX_SUCCEEDED(ret)) {
                printf("AMD software release date: %d-%d-%d\n", year, month, day);
            } else {
                printf("AMD software release date: Failed (%s)\n", GetADLXErrorDescription(ret));
            }

            ADLX_LUID luid;
            luid.lowPart = 0;
            luid.highPart = 0;
            ret = gpu2->pVtbl->LUID(gpu2, &luid);
            if (ADLX_SUCCEEDED(ret)) {
                printf("LUID: lowPart: %lu , highPart: %ld\n", luid.lowPart, luid.highPart);
            } else {
                printf("LUID: Failed (%s)\n", GetADLXErrorDescription(ret));
            }

            gpu2->pVtbl->Release(gpu2);
            gpu2 = NULL;
        }
        else
        {
            printf("IADLXGPU2 interface NOT available on this hardware/driver\n");
        }

        // Don't release gpu here - let caller handle it
    }
}

int main()
{
    printf("=== Simplified ADLX Test (AMD Official Method) ===\n");
    printf("Testing ADLX using AMD's exact mainGPUs.c approach\n\n");
    
    // Define return code
    ADLX_RESULT res = ADLX_FAIL;

    // Initialize ADLX using AMD's exact method
    printf("1. Initializing ADLX...\n");
    res = ADLXHelper_Initialize();
    printf("   ADLXHelper_Initialize result: %s\n", GetADLXErrorDescription(res));
    
    if (ADLX_SUCCEEDED(res))
    {
        printf("   SUCCESS: ADLX initialized\n");
        
        // Get system services using AMD's exact method
        printf("\n2. Getting system services...\n");
        IADLXSystem* sys = ADLXHelper_GetSystemServices();
        if (sys == NULL) {
            printf("   ERROR: Failed to get system services\n");
            ADLXHelper_Terminate();
            return 1;
        }
        printf("   SUCCESS: System services obtained\n");

        // Get GPU list using AMD's exact method
        printf("\n3. Getting GPU list...\n");
        IADLXGPUList* gpus = NULL;
        res = sys->pVtbl->GetGPUs(sys, &gpus);
        printf("   GetGPUs result: %s\n", GetADLXErrorDescription(res));
        
        if (ADLX_SUCCEEDED(res) && gpus != NULL)
        {
            adlx_uint gpuCount = gpus->pVtbl->Size(gpus);
            printf("   Found %u GPU(s)\n", gpuCount);
            
            if (gpuCount > 0) {
                // Get first GPU using AMD's exact method
                IADLXGPU* gpu = NULL;
                res = gpus->pVtbl->At_GPUList(gpus, 0, &gpu);
                if (ADLX_SUCCEEDED(res) && gpu != NULL)
                {
                    // Test the GPU using AMD's approach
                    ShowGPUInfo(gpu);
                    
                    // Release GPU
                    gpu->pVtbl->Release(gpu);
                }
                else
                {
                    printf("   ERROR: Failed to get GPU at index 0\n");
                }
            }
            else
            {
                printf("   ERROR: No GPUs found\n");
            }

            // Release GPU list
            gpus->pVtbl->Release(gpus);
        }
        else
        {
            printf("   ERROR: Failed to get GPU list\n");
        }
    }
    else
    {
        printf("   ERROR: ADLX initialization failed\n");
        return 1;
    }

    // Destroy ADLX using AMD's exact method
    printf("\n4. Terminating ADLX...\n");
    res = ADLXHelper_Terminate();
    printf("   ADLXHelper_Terminate result: %s\n", GetADLXErrorDescription(res));

    printf("\n=== AMD Official Method Test Completed ===\n");
    printf("Press any key to exit...\n");
    getchar();

    return 0;
}
