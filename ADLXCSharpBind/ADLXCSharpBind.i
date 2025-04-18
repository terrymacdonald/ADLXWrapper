//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------

%module(directors="1") ADLX
%{
#include <Windows.h>
#include "../ADLX/SDK/Include/ADLX.h"
#include "../ADLX/SDK/Include/ADLXDefines.h"
#include "../ADLX/SDK/Include/ADLXStructures.h"
#include "../ADLX/SDK/Include/ADLXVersion.h"
#include "../ADLX/SDK/Include/I3DSettings.h"
#include "../ADLX/SDK/Include/I3DSettings1.h"
#include "../ADLX/SDK/Include/I3DSettings2.h"
#include "../ADLX/SDK/Include/IApplications.h"
#include "../ADLX/SDK/Include/IChangedEvent.h"
#include "../ADLX/SDK/Include/ICollections.h"
#include "../ADLX/SDK/Include/IDesktops.h"
#include "../ADLX/SDK/Include/IDisplay3DLUT.h"
#include "../ADLX/SDK/Include/IDisplayGamma.h"
#include "../ADLX/SDK/Include/IDisplayGamut.h"
#include "../ADLX/SDK/Include/IDisplays.h"
#include "../ADLX/SDK/Include/IDisplays1.h"
#include "../ADLX/SDK/Include/IDisplays2.h"
#include "../ADLX/SDK/Include/IDisplays3.h"
#include "../ADLX/SDK/Include/IDisplaySettings.h"
#include "../ADLX/SDK/Include/IGPUAutoTuning.h"
#include "../ADLX/SDK/Include/IGPUManualFanTuning.h"
#include "../ADLX/SDK/Include/IGPUManualGFXTuning.h"
#include "../ADLX/SDK/Include/IGPUManualPowerTuning.h"
#include "../ADLX/SDK/Include/IGPUManualVRAMTuning.h"
#include "../ADLX/SDK/Include/IGPUPresetTuning.h"
#include "../ADLX/SDK/Include/IGPUTuning.h"
#include "../ADLX/SDK/Include/IGPUTuning1.h"
#include "../ADLX/SDK/Include/II2C.h"
#include "../ADLX/SDK/Include/ILog.h"
#include "../ADLX/SDK/Include/IMultiMedia.h"
#include "../ADLX/SDK/Include/IPerformanceMonitoring.h"
#include "../ADLX/SDK/Include/IPerformanceMonitoring1.h"
#include "../ADLX/SDK/Include/IPerformanceMonitoring2.h"
#include "../ADLX/SDK/Include/IPerformanceMonitoring3.h"
#include "../ADLX/SDK/Include/IPowerTuning.h"
#include "../ADLX/SDK/Include/IPowerTuning1.h"
#include "../ADLX/SDK/Include/ISmartAccessMemory.h"
#include "../ADLX/SDK/Include/ISystem.h"
#include "../ADLX/SDK/Include/ISystem1.h"
#include "../ADLX/SDK/Include/ISystem2.h"
#include "../ADLX/SDK/ADLXHelper/Windows/Cpp/ADLXHelper.h"

typedef     int64_t             adlx_int64;
typedef     int32_t             adlx_int32;
typedef     int16_t             adlx_int16;
typedef     int8_t              adlx_int8;
typedef     uint64_t            adlx_uint64;
typedef     uint32_t            adlx_uint32;
typedef     uint16_t            adlx_uint16;
typedef     uint8_t             adlx_uint8;
typedef     size_t              adlx_size;
typedef     void*               adlx_handle;
typedef     double              adlx_double;
typedef     float               adlx_float;
typedef     void                adlx_void;
typedef     long                adlx_long;
typedef     adlx_int32          adlx_int;
typedef     unsigned long       adlx_ulong;
typedef     adlx_uint32         adlx_uint;
typedef     bool                adlx_bool;
typedef wchar_t WCHAR;    // wc,   16-bit UNICODE character
typedef WCHAR TCHAR;

// Microsoft
#define ADLX_CORE_LINK          __declspec(dllexport)
#define ADLX_STD_CALL           __stdcall
#define ADLX_CDECL_CALL         __cdecl
#define ADLX_FAST_CALL          __fastcall
#define ADLX_INLINE              __inline
#define ADLX_FORCEINLINE         __forceinline
#define ADLX_NO_VTABLE          __declspec(novtable)

//IID's
#define ADLX_DECLARE_IID(X) static ADLX_INLINE const wchar_t* IID()  { return X; }
#define ADLX_IS_IID(X, Y) (!wcscmp (X, Y))
#define ADLX_DECLARE_ITEM_IID(X) static ADLX_INLINE const wchar_t* ITEM_IID()  { return X; }

using namespace adlx;
%}

typedef     int64_t             adlx_int64;
typedef     int32_t             adlx_int32;
typedef     int16_t             adlx_int16;
typedef     int8_t              adlx_int8;
typedef     uint64_t            adlx_uint64;
typedef     uint32_t            adlx_uint32;
typedef     uint16_t            adlx_uint16;
typedef     uint8_t             adlx_uint8;
typedef     size_t              adlx_size;
typedef     void*               adlx_handle;
typedef     double              adlx_double;
typedef     float               adlx_float;
typedef     void                adlx_void;
typedef     long                adlx_long;
typedef     adlx_int32          adlx_int;
typedef     unsigned long       adlx_ulong;
typedef     adlx_uint32         adlx_uint;
typedef     bool                adlx_bool;
typedef wchar_t WCHAR;    // wc,   16-bit UNICODE character
typedef WCHAR TCHAR;

// Microsoft
#define ADLX_CORE_LINK          __declspec(dllexport)
#define ADLX_STD_CALL           __stdcall
#define ADLX_CDECL_CALL         __cdecl
#define ADLX_FAST_CALL          __fastcall
#define ADLX_INLINE              __inline
#define ADLX_FORCEINLINE         __forceinline
#define ADLX_NO_VTABLE          __declspec(novtable)

//IID's
#define ADLX_DECLARE_IID(X) static ADLX_INLINE const wchar_t* IID()  { return X; }
#define ADLX_IS_IID(X, Y) (!wcscmp (X, Y))

#define ADLX_DECLARE_ITEM_IID(X) static ADLX_INLINE const wchar_t* ITEM_IID()  { return X; }

/* Callback to turn on director wrapping */
%feature("director") IADLXDisplayListChangedListener;

// Create a speciual pointer for us to use to get a IADLXEyefinityDesktop reference from an IADLXDesktop reference.
%apply void *VOID_INT_PTR { void * };

%include stdint.i
%include carrays.i
%include windows.i
%include typemaps.i


%include "../ADLX/SDK/Include/ADLX.h"
%include "../ADLX/SDK/Include/ADLXDefines.h"
%include "../ADLX/SDK/Include/ADLXStructures.h"
%include "../ADLX/SDK/Include/ADLXVersion.h"
%include "../ADLX/SDK/Include/I3DSettings.h"
%include "../ADLX/SDK/Include/I3DSettings1.h"
%include "../ADLX/SDK/Include/I3DSettings2.h"
%include "../ADLX/SDK/Include/IApplications.h"
%include "../ADLX/SDK/Include/IChangedEvent.h"
%include "../ADLX/SDK/Include/ICollections.h"
%include "../ADLX/SDK/Include/IDesktops.h"
%include "../ADLX/SDK/Include/IDisplay3DLUT.h"
%include "../ADLX/SDK/Include/IDisplayGamma.h"
%include "../ADLX/SDK/Include/IDisplayGamut.h"
%include "../ADLX/SDK/Include/IDisplays.h"
%include "../ADLX/SDK/Include/IDisplays1.h"
%include "../ADLX/SDK/Include/IDisplays2.h"
%include "../ADLX/SDK/Include/IDisplays3.h"
%include "../ADLX/SDK/Include/IDisplaySettings.h"
%include "../ADLX/SDK/Include/IGPUAutoTuning.h"
%include "../ADLX/SDK/Include/IGPUManualFanTuning.h"
%include "../ADLX/SDK/Include/IGPUManualGFXTuning.h"
%include "../ADLX/SDK/Include/IGPUManualPowerTuning.h"
%include "../ADLX/SDK/Include/IGPUManualVRAMTuning.h"
%include "../ADLX/SDK/Include/IGPUPresetTuning.h"
%include "../ADLX/SDK/Include/IGPUTuning.h"
%include "../ADLX/SDK/Include/IGPUTuning1.h"
%include "../ADLX/SDK/Include/II2C.h"
%include "../ADLX/SDK/Include/ILog.h"
%include "../ADLX/SDK/Include/IMultiMedia.h"
%include "../ADLX/SDK/Include/IPerformanceMonitoring.h"
%include "../ADLX/SDK/Include/IPerformanceMonitoring1.h"
%include "../ADLX/SDK/Include/IPerformanceMonitoring2.h"
%include "../ADLX/SDK/Include/IPerformanceMonitoring3.h"
%include "../ADLX/SDK/Include/IPowerTuning.h"
%include "../ADLX/SDK/Include/IPowerTuning1.h"
%include "../ADLX/SDK/Include/ISmartAccessMemory.h"
%include "../ADLX/SDK/Include/ISystem.h"
%include "../ADLX/SDK/Include/ISystem1.h"
%include "../ADLX/SDK/Include/ISystem2.h"
%include "../ADLX/SDK/ADLXHelper/Windows/Cpp/ADLXHelper.h"

using namespace adlx;

// T* pointer
%include cpointer.i

%pointer_functions(adlx_int, adlx_intP);
%pointer_functions(adlx_uint, adlx_uintP);
%pointer_functions(adlx_bool, adlx_BoolP);
//%pointer_functions(adlx_string, adlx_stringP);
%pointer_functions(double, doubleP);
%pointer_functions(bool, boolP);
%pointer_functions(WCHAR, wcharP);
%pointer_functions(ADLX_DISPLAY_TYPE, adlx_displayTypeP);
%pointer_functions(ADLX_DISPLAY_CONNECTOR_TYPE, adlx_displayConnectTypeP);
%pointer_functions(ADLX_DISPLAY_SCAN_TYPE, adlx_displayScanTypeP);
%pointer_functions(adlx_size, adlx_sizeP);
%pointer_functions(ADLX_IntRange, adlx_intRangeP);
%pointer_functions(ADLX_GPU_TYPE, adlx_gpuTypeP);
%pointer_functions(ADLX_ORIENTATION, adlx_orientationP);
%pointer_functions(ADLX_Point, adlx_pointP);
%pointer_functions(ADLX_3DLUT_Data, adlx_3dlutP);
%pointer_functions(ADLX_CustomResolution, adlx_customResolutionP);
%pointer_functions(ADLX_GammaRamp, adlx_gamaRampP);
%pointer_functions(ADLX_GamutColorSpace, adlx_gamutColorSpaceP);
%pointer_functions(ADLX_LUID, adlx_luidP);
%pointer_functions(ADLX_RGB, adlx_rgbP);
%pointer_functions(ADLX_RegammaCoeff, adlx_regammaCoeffP);
%pointer_functions(ADLX_TimingInfo, adlx_timingInfoP);
%pointer_functions(ADLX_UINT16_RGB, adlx_uint16RgbP);



// T** pointers
%pointer_functions(IADLXDisplayServices*, displaySerP_Ptr);
%pointer_functions(IADLXDisplayServices1*, displaySer1P_Ptr);
%pointer_functions(IADLXDisplayServices2*, displaySer2P_Ptr);
%pointer_functions(IADLXDisplayServices3*, displaySer3P_Ptr);
%pointer_functions(IADLXDisplayList*, displayListP_Ptr);
%pointer_functions(IADLXDisplay*, displayP_Ptr);
%pointer_functions(IADLXDisplayChangedHandling*, displayChangeHandlP_Ptr);
%pointer_functions(IADLXDesktopServices*, desktopSerP_Ptr);
%pointer_functions(IADLXDesktopList*, desktopListP_Ptr);
%pointer_functions(IADLXDesktop*, desktopP_Ptr);
%pointer_functions(IADLXDesktopChangedHandling*, desktopChangeHandlP_Ptr);
%pointer_functions(IADLXEyefinityDesktop*, eyefinityDesktopP_Ptr);
%pointer_functions(IADLXSimpleEyefinity*, simpleEyefinityP_Ptr);
%pointer_functions(IADLXGPU*, gpuP_Ptr);
%pointer_functions(IADLXGPU1*, gpu1P_Ptr);
%pointer_functions(IADLXGPU2*, gpu2P_Ptr);
%pointer_functions(IADLXGPUList*, gpuListP_Ptr);
%pointer_functions(IADLXList*, adlxListP_Ptr);
%pointer_functions(IADLXInterface*, adlxInterfaceP_Ptr);
%pointer_functions(IADLXGPUTuningServices*, gpuTuningP_Ptr);
%pointer_functions(IADLXManualFanTuning*, manualFanTuningP_Ptr);
%pointer_functions(IADLXManualPowerTuning*, manualPowerTuningP_Ptr);
%pointer_functions(IADLXPerformanceMonitoringServices*, performanceP_Ptr);
%pointer_functions(IADLXManualFanTuningStateList*, fanTuningStateListP_Ptr);
%pointer_functions(IADLXManualFanTuningState*, fanTuningStateP_Ptr);
%pointer_functions(IADLXGPUMetrics*, metricsP_Ptr);
%pointer_functions(IADLXGPUMetrics1*, metrics1P_Ptr);
%pointer_functions(IADLXGPUMetricsSupport*, metricsSupportP_Ptr);
%pointer_functions(IADLXGPUMetricsSupport1*, metricsSupport1P_Ptr);
//%pointer_functions(adlx_string*, stringP_Ptr);
%pointer_functions(IADLXGPUMetricsList*, gpuMetricsListP_Ptr);
%pointer_functions(char*, charP_Ptr);
%pointer_functions(void*, voidP_Ptr);
%pointer_functions( IADLXDisplay3DLUT*, display3DLUTP_Ptr);
%pointer_functions( IADLXDisplayColorDepth*, displayColorDepthP_Ptr);
%pointer_functions( IADLXDisplayCustomColor*, displayCustomColorP_Ptr);
%pointer_functions( IADLXDisplayCustomResolution*, displayCustomResolutionP_Ptr);
%pointer_functions( IADLXDisplayFreeSync*, displayFreeSyncP_Ptr);
%pointer_functions( IADLXDisplayGPUScaling*, displayGPUScalingP_Ptr);
%pointer_functions( IADLXDisplayGamma*, displayGammaP_Ptr);
%pointer_functions( IADLXDisplayGamut*, displayGamutP_Ptr);
%pointer_functions( IADLXDisplayHDCP*, displayHDCPP_Ptr);
%pointer_functions( IADLXDisplayIntegerScaling*, displayIntegerScalingP_Ptr);
%pointer_functions( IADLXDisplayPixelFormat*, displayPixelFormatP_Ptr);
%pointer_functions( IADLXDisplayScalingMode*, displayScalingModeP_Ptr);
%pointer_functions( IADLXDisplayVariBright*, displayVariBrightP_Ptr);
%pointer_functions( IADLXDisplayVSR*, displayVSRP_Ptr);
%pointer_functions( IADLXDisplayBlanking*, displayDisplayBlankingP_Ptr);
%pointer_functions( IADLXDisplayConnectivityExperience*, displayConnectivityExperienceP_Ptr);
%pointer_functions( IADLXDisplayDynamicRefreshRateControl*, displayDynamicRefreshRateControlP_Ptr);
%pointer_functions( IADLXDisplayFreeSyncColorAccuracy*, displayCFreeSyncColorAccuracyP_Ptr);


// T** ppointer
%define %ppointer_functions(TYPE,NAME)
%{
static TYPE *new_##NAME() { %}
%{  return new TYPE(); %}
%{}

static TYPE *copy_##NAME(TYPE value) { %}
%{  return new TYPE(value); %}
%{}

static void delete_##NAME(TYPE *obj) { %}
%{  if (*obj) delete *obj; %}
%{}

static void NAME ##_assign(TYPE *obj, TYPE value) {
  *obj = value;
}

static TYPE NAME ##_value(TYPE *obj) {
  return *obj;
}
%}

TYPE *new_##NAME();
TYPE *copy_##NAME(TYPE value);
void  delete_##NAME(TYPE *obj);
void  NAME##_assign(TYPE *obj, TYPE value);
TYPE  NAME##_value(TYPE *obj);

%enddef

%define %pointer_cast(TYPE1,TYPE2,NAME)
%inline %{
TYPE2 NAME(TYPE1 x) {
   return (TYPE2) x;
}
%}
%enddef

/* %pointer_cast(IADLXManualFanTuning**, void**, CastManualFanTuningVoidPtr);
%pointer_cast(IADLXManualPowerTuning**, void**, CastManualPowerTuningVoidPtr);

%pointer_cast(IADLXGPUMetrics**, IADLXGPUMetrics1**, CastGPUMetricsToGPUMetrics1);
%pointer_cast(IADLXGPUMetricsSupport**, IADLXGPUMetricsSupport1**, CastGPUMetricsSupportToGPUMetricsSupport1); */