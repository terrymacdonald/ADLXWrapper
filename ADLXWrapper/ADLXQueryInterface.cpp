//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------
// ADLX QueryInterface Helper Functions Implementation
//-------------------------------------------------------------------------------------------------

#include "ADLXQueryInterface.h"
#include <Windows.h>
#include "../ADLX/SDK/Include/ADLX.h"
#include "../ADLX/SDK/ADLXHelper/Windows/Cpp/ADLXHelper.h"

using namespace adlx;

// ADLX Runtime Detection and Validation Functions
bool IsADLXRuntimeAvailable() {
    HMODULE hADLX = LoadLibraryA("amdadlx64.dll");
    if (hADLX) {
        FreeLibrary(hADLX);
        return true;
    }
    return false;
}

ADLX_RESULT ValidateADLXInstallation() {
    if (!IsADLXRuntimeAvailable()) {
        return ADLX_FAIL;
    }
    
    // Try to initialize ADLX to validate installation
    ADLXHelper helper;
    ADLX_RESULT res = helper.Initialize();
    if (res == ADLX_OK) {
        helper.Terminate();
    }
    return res;
}

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

// QueryInterface Helper Functions for GPU Interfaces
IADLXGPU1* QueryGPU1Interface(IADLXGPU* pGPU) {
    if (!pGPU) return nullptr;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pGPU->QueryInterface(IADLXGPU1::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        return static_cast<IADLXGPU1*>(pInterface);
    }
    return nullptr;
}

IADLXGPU2* QueryGPU2Interface(IADLXGPU* pGPU) {
    if (!pGPU) return nullptr;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pGPU->QueryInterface(IADLXGPU2::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        return static_cast<IADLXGPU2*>(pInterface);
    }
    return nullptr;
}

IADLXGPU2* QueryGPU2InterfaceFromGPU1(IADLXGPU1* pGPU1) {
    if (!pGPU1) return nullptr;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pGPU1->QueryInterface(IADLXGPU2::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        return static_cast<IADLXGPU2*>(pInterface);
    }
    return nullptr;
}

// QueryInterface Helper Functions for System Interfaces
IADLXSystem1* QuerySystem1Interface(IADLXSystem* pSystem) {
    if (!pSystem) return nullptr;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pSystem->QueryInterface(IADLXSystem1::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        return static_cast<IADLXSystem1*>(pInterface);
    }
    return nullptr;
}

IADLXSystem2* QuerySystem2Interface(IADLXSystem* pSystem) {
    if (!pSystem) return nullptr;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pSystem->QueryInterface(IADLXSystem2::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        return static_cast<IADLXSystem2*>(pInterface);
    }
    return nullptr;
}

IADLXSystem2* QuerySystem2InterfaceFromSystem1(IADLXSystem1* pSystem1) {
    if (!pSystem1) return nullptr;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pSystem1->QueryInterface(IADLXSystem2::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        return static_cast<IADLXSystem2*>(pInterface);
    }
    return nullptr;
}

// Interface Capability Detection
bool SupportsGPU1Interface(IADLXGPU* pGPU) {
    if (!pGPU) return false;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pGPU->QueryInterface(IADLXGPU1::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        static_cast<IADLXInterface*>(pInterface)->Release();
        return true;
    }
    return false;
}

bool SupportsGPU2Interface(IADLXGPU* pGPU) {
    if (!pGPU) return false;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pGPU->QueryInterface(IADLXGPU2::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        static_cast<IADLXInterface*>(pInterface)->Release();
        return true;
    }
    return false;
}

bool SupportsSystem1Interface(IADLXSystem* pSystem) {
    if (!pSystem) return false;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pSystem->QueryInterface(IADLXSystem1::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        static_cast<IADLXInterface*>(pInterface)->Release();
        return true;
    }
    return false;
}

bool SupportsSystem2Interface(IADLXSystem* pSystem) {
    if (!pSystem) return false;
    
    void* pInterface = nullptr;
    ADLX_RESULT result = pSystem->QueryInterface(IADLXSystem2::IID(), &pInterface);
    
    if (result == ADLX_OK && pInterface) {
        static_cast<IADLXInterface*>(pInterface)->Release();
        return true;
    }
    return false;
}

// Enhanced ADLX Helper Implementation
EnhancedADLXHelper::EnhancedADLXHelper() : initialized(false) {
}

ADLX_RESULT EnhancedADLXHelper::Initialize() {
    if (!IsADLXRuntimeAvailable()) {
        return ADLX_FAIL;
    }
    
    ADLX_RESULT res = helper.Initialize();
    if (res == ADLX_OK) {
        initialized = true;
    }
    return res;
}

ADLX_RESULT EnhancedADLXHelper::Terminate() {
    if (initialized) {
        ADLX_RESULT res = helper.Terminate();
        initialized = false;
        return res;
    }
    return ADLX_OK;
}

IADLXSystem* EnhancedADLXHelper::GetSystemServices() {
    if (!initialized) return nullptr;
    return helper.GetSystemServices();
}

bool EnhancedADLXHelper::IsInitialized() const {
    return initialized;
}

EnhancedADLXHelper::~EnhancedADLXHelper() {
    if (initialized) {
        Terminate();
    }
}

// Note: Gamma Ramp Data Access Helper Functions are now implemented in SWIG %inline block
// in ADLXWrapper.i to avoid duplicate symbol errors during linking.
// The SWIG-generated implementations provide the same functionality and are accessible
// from both C++ and C# code.
