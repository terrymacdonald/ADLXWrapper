//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------
// ADLX QueryInterface Helper Functions for C# Binding
//
// This file provides helper functions to safely access IADLXGPU2 and other extended
// interfaces through the QueryInterface mechanism from C#.
//-------------------------------------------------------------------------------------------------

#ifndef ADLX_QUERYINTERFACE_H
#define ADLX_QUERYINTERFACE_H

#include "../ADLX/SDK/Include/ADLX.h"
#include "../ADLX/SDK/Include/ISystem.h"
#include "../ADLX/SDK/Include/ISystem1.h"
#include "../ADLX/SDK/Include/ISystem2.h"
#include "../ADLX/SDK/ADLXHelper/Windows/Cpp/ADLXHelper.h"

using namespace adlx;

#ifdef __cplusplus
extern "C" {
#endif

// ADLX Runtime Detection and Validation Functions
bool IsADLXRuntimeAvailable();
ADLX_RESULT ValidateADLXInstallation();
const char* GetADLXErrorDescription(ADLX_RESULT result);

// QueryInterface Helper Functions for GPU Interfaces
IADLXGPU1* QueryGPU1Interface(IADLXGPU* pGPU);
IADLXGPU2* QueryGPU2Interface(IADLXGPU* pGPU);
IADLXGPU2* QueryGPU2InterfaceFromGPU1(IADLXGPU1* pGPU1);

// QueryInterface Helper Functions for System Interfaces
IADLXSystem1* QuerySystem1Interface(IADLXSystem* pSystem);
IADLXSystem2* QuerySystem2Interface(IADLXSystem* pSystem);
IADLXSystem2* QuerySystem2InterfaceFromSystem1(IADLXSystem1* pSystem1);

// Interface Capability Detection
bool SupportsGPU1Interface(IADLXGPU* pGPU);
bool SupportsGPU2Interface(IADLXGPU* pGPU);
bool SupportsSystem1Interface(IADLXSystem* pSystem);
bool SupportsSystem2Interface(IADLXSystem* pSystem);

// Gamma Ramp Data Access Helper Functions
ADLX_RESULT GetGammaRampData(ADLX_GammaRamp* pGammaRamp, adlx_uint16* pRedData, adlx_uint16* pGreenData, adlx_uint16* pBlueData, adlx_size dataSize);
ADLX_RESULT SetGammaRampData(ADLX_GammaRamp* pGammaRamp, const adlx_uint16* pRedData, const adlx_uint16* pGreenData, const adlx_uint16* pBlueData, adlx_size dataSize);
adlx_size GetGammaRampSize();
bool ValidateGammaRampData(const adlx_uint16* pData, adlx_size dataSize);

// Enhanced ADLX Helper Class with Runtime Validation
class EnhancedADLXHelper {
private:
    ADLXHelper helper;
    bool initialized;
    
public:
    EnhancedADLXHelper();
    ADLX_RESULT Initialize();
    ADLX_RESULT Terminate();
    IADLXSystem* GetSystemServices();
    bool IsInitialized() const;
    ~EnhancedADLXHelper();
};

#ifdef __cplusplus
}
#endif

#endif // ADLX_QUERYINTERFACE_H
