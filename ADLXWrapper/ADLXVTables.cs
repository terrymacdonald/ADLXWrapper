using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    /// <summary>
    /// VTable structures for ADLX COM-like interfaces
    /// These structures define the memory layout of interface vtables
    /// Based on C API patterns from ADLX samples
    /// </summary>
    internal static unsafe class ADLXVTables
    {
        // Base interface vtable (IADLXInterface)
        // All ADLX interfaces inherit from IADLXInterface which has:
        // - QueryInterface
        // - AddRef  
        // - Release
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXInterfaceVtbl
        {
            public IntPtr QueryInterface;  // ADLX_RESULT QueryInterface(const wchar_t* interfaceId, void** ppInterface)
            public IntPtr AddRef;          // adlx_long AddRef()
            public IntPtr Release;         // adlx_long Release()
        }

        // IADLXList vtable (base for all list interfaces)
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXListVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXList methods
            public IntPtr Size;            // adlx_uint Size()
            public IntPtr Empty;           // adlx_bool Empty()
            public IntPtr Begin;           // adlx_uint Begin()
            public IntPtr End;             // adlx_uint End()
            public IntPtr At;              // ADLX_RESULT At(adlx_uint location, IADLXInterface** ppItem)
            public IntPtr Clear;           // ADLX_RESULT Clear()
            public IntPtr Remove_Back;     // ADLX_RESULT Remove_Back()
            public IntPtr Add_Back;        // ADLX_RESULT Add_Back(IADLXInterface* pItem)
        }

        // IADLXGPUList vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXGPUListVtbl
        {
            // Inherit from IADLXList
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;
            public IntPtr Size;
            public IntPtr Empty;
            public IntPtr Begin;
            public IntPtr End;
            public IntPtr At;              // ADLX_RESULT At(adlx_uint location, IADLXGPU** ppItem)
            public IntPtr Clear;
            public IntPtr Remove_Back;
            public IntPtr Add_Back;        // ADLX_RESULT Add_Back(IADLXGPU* pItem)
        }

        // IADLXGPU vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXGPUVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXGPU methods (based on ISystem.h)
            public IntPtr VendorId;              // ADLX_RESULT VendorId(const char** vendorId)
            public IntPtr ASICFamilyType;        // ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
            public IntPtr Type;                  // ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
            public IntPtr IsExternal;            // ADLX_RESULT IsExternal(adlx_bool* isExternal)
            public IntPtr Name;                  // ADLX_RESULT Name(const char** name)
            public IntPtr DriverPath;            // ADLX_RESULT DriverPath(const char** driverPath)
            public IntPtr PNPString;             // ADLX_RESULT PNPString(const char** pnpString)
            public IntPtr HasDesktops;           // ADLX_RESULT HasDesktops(adlx_bool* hasDesktops)
            public IntPtr TotalVRAM;             // ADLX_RESULT TotalVRAM(adlx_uint* vramMB)
            public IntPtr VRAMType;              // ADLX_RESULT VRAMType(const char** type)
            public IntPtr BIOSInfo;              // ADLX_RESULT BIOSInfo(const char** partNumber, const char** version, const char** date)
            public IntPtr DeviceId;              // ADLX_RESULT DeviceId(const char** deviceId)
            public IntPtr RevisionId;            // ADLX_RESULT RevisionId(const char** revisionId)
            public IntPtr SubSystemId;           // ADLX_RESULT SubSystemId(const char** subSystemId)
            public IntPtr SubSystemVendorId;     // ADLX_RESULT SubSystemVendorId(const char** subSystemVendorId)
            public IntPtr UniqueId;              // ADLX_RESULT UniqueId(adlx_int* uniqueId)
        }

        // IADLXSystem vtable (main system interface)
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXSystemVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXSystem methods (based on ISystem.h)
            public IntPtr HybridGraphicsType;        // ADLX_RESULT HybridGraphicsType(ADLX_HG_TYPE* hgType)
            public IntPtr GetGPUs;                   // ADLX_RESULT GetGPUs(IADLXGPUList** ppGPUs)
            public IntPtr GetGPUsChangedHandling;    // ADLX_RESULT GetGPUsChangedHandling(IADLXGPUsChangedHandling** ppGPUsChangedHandling)
            public IntPtr EnableLog;                 // ADLX_RESULT EnableLog(ADLX_LOG_DESTINATION mode, ADLX_LOG_SEVERITY severity, IADLXLog* pLogger, const wchar_t* fileName)
            public IntPtr Get3DSettingsServices;     // ADLX_RESULT Get3DSettingsServices(IADLX3DSettingsServices** pp3DSettingsServices)
            public IntPtr GetGPUTuningServices;      // ADLX_RESULT GetGPUTuningServices(IADLXGPUTuningServices** ppGPUTuningServices)
            public IntPtr GetDisplaysServices;       // ADLX_RESULT GetDisplaysServices(IADLXDisplayServices** ppDispServices)
            public IntPtr GetDesktopsServices;       // ADLX_RESULT GetDesktopsServices(IADLXDesktopServices** ppDeskServices)
            public IntPtr GetI2C;                    // ADLX_RESULT GetI2C(IADLXI2C** ppI2C)
            public IntPtr GetPerformanceMonitoringServices; // ADLX_RESULT GetPerformanceMonitoringServices(IADLXPerformanceMonitoringServices** ppPerfMonitoring)
            public IntPtr GetPowerTuningServices;    // ADLX_RESULT GetPowerTuningServices(IADLXPowerTuningServices** ppPowerTuningServices)
            public IntPtr GetMultiMediaServices;     // ADLX_RESULT GetMultiMediaServices(IADLXMultiMediaServices** ppMultiMediaServices)
        }

        // Helper function pointer delegates for VTable method calls
        // These match the calling convention used by ADLX (__stdcall)

        // Base interface methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT QueryInterfaceFn(IntPtr pThis, char* interfaceId, IntPtr* ppInterface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate int AddRefFn(IntPtr pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate int ReleaseFn(IntPtr pThis);

        // List methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate uint SizeFn(IntPtr pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate byte EmptyFn(IntPtr pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT AtFn(IntPtr pThis, uint location, IntPtr* ppItem);

        // System methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetGPUsFn(IntPtr pThis, IntPtr* ppGPUList);

        // GPU methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT NameFn(IntPtr pThis, byte** name);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT VendorIdFn(IntPtr pThis, byte** vendorId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT UniqueIdFn(IntPtr pThis, int* uniqueId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT TotalVRAMFn(IntPtr pThis, uint* vramMB);

        // Boolean property methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT IsExternalFn(IntPtr pThis, byte* isExternal);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT HasDesktopsFn(IntPtr pThis, byte* hasDesktops);

        // Display service methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetDisplaysFn(IntPtr pThis, IntPtr* ppDisplayList);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetNumberOfDisplaysFn(IntPtr pThis, uint* numDisplays);

        // Display property methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT DisplayNameFn(IntPtr pThis, byte** name);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT NativeResolutionFn(IntPtr pThis, int* maxHResolution, int* maxVResolution);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT RefreshRateFn(IntPtr pThis, double* refreshRate);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT ManufacturerIDFn(IntPtr pThis, uint* manufacturerID);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT PixelClockFn(IntPtr pThis, uint* pixelClock);

        // GPU Tuning service methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetGPUTuningServicesFn(IntPtr pThis, IntPtr* ppGPUTuningServices);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT IsSupportedTuningFn(IntPtr pThis, IntPtr pGPU, byte* supported);

        // Performance monitoring methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetPerformanceMonitoringServicesFn(IntPtr pThis, IntPtr* ppPerfMonServices);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT IsSupportedMetricFn(IntPtr pThis, byte* supported);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetCurrentGPUMetricsFn(IntPtr pThis, IntPtr pGPU, IntPtr* ppMetrics);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetSupportedGPUMetricsFn(IntPtr pThis, IntPtr pGPU, IntPtr* ppMetricsSupport);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GPUTemperatureFn(IntPtr pThis, double* temperature);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GPUUsageFn(IntPtr pThis, double* usage);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GPUClockSpeedFn(IntPtr pThis, int* clockSpeed);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GPUVRAMFn(IntPtr pThis, int* vram);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GPUPowerFn(IntPtr pThis, double* power);

        // Desktop services methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetDesktopsServicesFn(IntPtr pThis, IntPtr* ppDeskServices);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetNumberOfDesktopsFn(IntPtr pThis, uint* pNumDesktops);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetDesktopsFn(IntPtr pThis, IntPtr* ppDesktops);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetDesktopChangedHandlingFn(IntPtr pThis, IntPtr* ppHandling);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetSimpleEyefinityFn(IntPtr pThis, IntPtr* ppSimpleEyefinity);

        // Desktop methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT DesktopOrientationFn(IntPtr pThis, ADLX_ORIENTATION* orientation);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT DesktopSizeFn(IntPtr pThis, int* width, int* height);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT DesktopTopLeftFn(IntPtr pThis, ADLX_Point* topLeft);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT DesktopTypeFn(IntPtr pThis, ADLX_DESKTOP_TYPE* desktopType);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT DesktopGetDisplaysFn(IntPtr pThis, IntPtr* ppDisplays);

        // Eyefinity desktop methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT EyefinityGridSizeFn(IntPtr pThis, uint* rows, uint* cols);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT EyefinityGetDisplayFn(IntPtr pThis, uint row, uint col, IntPtr* ppDisplay);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT EyefinityDisplayOrientationFn(IntPtr pThis, uint row, uint col, ADLX_ORIENTATION* orientation);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT EyefinityDisplaySizeFn(IntPtr pThis, uint row, uint col, int* width, int* height);

        // Simple Eyefinity methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT EyefinityIsSupportedFn(IntPtr pThis, byte* supported);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT EyefinityCreateFn(IntPtr pThis, IntPtr* ppEyefinityDesktop);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT EyefinityDestroyFn(IntPtr pThis, IntPtr pDesktop);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT EyefinityDestroyAllFn(IntPtr pThis);

        // Display settings methods
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetFreeSyncFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppFreeSync);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetGPUScalingFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppGPUScaling);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetScalingModeFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppScalingMode);
        internal delegate ADLX_RESULT GetIntegerScalingFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppIntegerScaling);
        internal delegate ADLX_RESULT GetVirtualSuperResolutionFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppVsr);
        internal delegate ADLX_RESULT GetCustomColorFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppCustomColor);
        internal delegate ADLX_RESULT GetHDCPFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppHdcp);
        internal delegate ADLX_RESULT GetVariBrightFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppVariBright);
        internal delegate ADLX_RESULT GetDisplayBlankingFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppBlanking);
        internal delegate ADLX_RESULT GetCustomResolutionFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppCustomResolution);
        internal delegate ADLX_RESULT GetCurrentResolutionFn(IntPtr pThis, IntPtr* ppResolution);
        internal delegate ADLX_RESULT GetDisplayConnectivityExperienceFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppConnectivity);
        internal delegate ADLX_RESULT GetDynamicRefreshRateControlFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppDRRC);
        internal delegate ADLX_RESULT GetFreeSyncColorAccuracyFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppFSCA);
        internal delegate ADLX_RESULT GetColorDepthFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppColorDepth);
        internal delegate ADLX_RESULT GetPixelFormatFn(IntPtr pThis, IntPtr pDisplay, IntPtr* ppPixelFormat);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT BoolSupportedFn(IntPtr pThis, byte* supported);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT BoolEnabledFn(IntPtr pThis, byte* enabled);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT BoolSetEnabledFn(IntPtr pThis, byte enabled);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetScaleModeFn(IntPtr pThis, ADLX_SCALE_MODE* mode);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT SetScaleModeFn(IntPtr pThis, ADLX_SCALE_MODE mode);
        internal delegate ADLX_RESULT BoolSetFn(IntPtr pThis, byte enable);
        internal delegate ADLX_RESULT InvokeFn(IntPtr pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetUIntFn(IntPtr pThis, uint* value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetIntRangeFn(IntPtr pThis, ADLX_IntRange* range);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetIntValueFn(IntPtr pThis, int* value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT SetIntValueFn(IntPtr pThis, int value);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetCustomResolutionValueFn(IntPtr pThis, ADLX_CustomResolution* resolution);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetDPLinkRateFn(IntPtr pThis, ADLX_DP_LINK_RATE* linkRate);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetColorDepthValueFn(IntPtr pThis, ADLX_COLOR_DEPTH* depth);
        internal delegate ADLX_RESULT SetColorDepthValueFn(IntPtr pThis, ADLX_COLOR_DEPTH depth);
        internal delegate ADLX_RESULT IsSupportedColorDepthFn(IntPtr pThis, ADLX_COLOR_DEPTH depth, byte* supported);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        internal delegate ADLX_RESULT GetPixelFormatValueFn(IntPtr pThis, ADLX_PIXEL_FORMAT* format);
        internal delegate ADLX_RESULT SetPixelFormatValueFn(IntPtr pThis, ADLX_PIXEL_FORMAT format);
        internal delegate ADLX_RESULT IsSupportedPixelFormatFn(IntPtr pThis, ADLX_PIXEL_FORMAT format, byte* supported);

        // IADLXPerformanceMonitoringServices vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXPerformanceMonitoringServicesVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXPerformanceMonitoringServices methods
            public IntPtr GetSamplingIntervalRange;          // ADLX_RESULT GetSamplingIntervalRange(ADLX_IntRange* range)
            public IntPtr SetSamplingInterval;                // ADLX_RESULT SetSamplingInterval(adlx_int intervalMs)
            public IntPtr GetSamplingInterval;                // ADLX_RESULT GetSamplingInterval(adlx_int* intervalMs)
            public IntPtr GetMaxPerformanceMetricsHistorySize; // ADLX_RESULT GetMaxPerformanceMetricsHistorySize(adlx_int* sizeSec)
            public IntPtr SetMaxPerformanceMetricsHistorySize; // ADLX_RESULT SetMaxPerformanceMetricsHistorySize(adlx_int sizeSec)
            public IntPtr ClearPerformanceMetricsHistory;     // ADLX_RESULT ClearPerformanceMetricsHistory()
            public IntPtr GetCurrentPerformanceMetricsHistorySize; // ADLX_RESULT GetCurrentPerformanceMetricsHistorySize(adlx_int* sizeSec)
            public IntPtr GetGPUMetricsHistory;               // ADLX_RESULT GetGPUMetricsHistory(IADLXGPU* pGPU, adlx_int startMs, adlx_int stopMs, IADLXGPUMetricsList** ppMetricsList)
            public IntPtr GetCurrentGPUMetrics;               // ADLX_RESULT GetCurrentGPUMetrics(IADLXGPU* pGPU, IADLXGPUMetrics** ppMetrics)
            public IntPtr GetSupportedGPUMetrics;             // ADLX_RESULT GetSupportedGPUMetrics(IADLXGPU* pGPU, IADLXGPUMetricsSupport** ppMetricsSupport)
            public IntPtr GetSystemMetricsHistory;            // ADLX_RESULT GetSystemMetricsHistory(adlx_int startMs, adlx_int stopMs, IADLXSystemMetricsList** ppMetricsList)
            public IntPtr GetCurrentSystemMetrics;            // ADLX_RESULT GetCurrentSystemMetrics(IADLXSystemMetrics** ppMetrics)
            public IntPtr GetSupportedSystemMetrics;          // ADLX_RESULT GetSupportedSystemMetrics(IADLXSystemMetricsSupport** ppMetricsSupport)
            public IntPtr StartPerformanceMetricsTracking;    // ADLX_RESULT StartPerformanceMetricsTracking()
            public IntPtr StopPerformanceMetricsTracking;     // ADLX_RESULT StopPerformanceMetricsTracking()
            public IntPtr GetAllMetricsHistory;               // ADLX_RESULT GetAllMetricsHistory(adlx_int startMs, adlx_int stopMs, IADLXAllMetricsList** ppMetricsList)
            public IntPtr GetCurrentAllMetrics;               // ADLX_RESULT GetCurrentAllMetrics(IADLXAllMetrics** ppMetrics)
        }

        // IADLXGPUMetricsSupport vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXGPUMetricsSupportVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXGPUMetricsSupport methods
            public IntPtr IsSupportedGPUUsage;                // ADLX_RESULT IsSupportedGPUUsage(adlx_bool* supported)
            public IntPtr IsSupportedGPUClockSpeed;           // ADLX_RESULT IsSupportedGPUClockSpeed(adlx_bool* supported)
            public IntPtr IsSupportedGPUVRAMClockSpeed;       // ADLX_RESULT IsSupportedGPUVRAMClockSpeed(adlx_bool* supported)
            public IntPtr IsSupportedGPUTemperature;          // ADLX_RESULT IsSupportedGPUTemperature(adlx_bool* supported)
            public IntPtr IsSupportedGPUHotspotTemperature;   // ADLX_RESULT IsSupportedGPUHotspotTemperature(adlx_bool* supported)
            public IntPtr IsSupportedGPUPower;                // ADLX_RESULT IsSupportedGPUPower(adlx_bool* supported)
            public IntPtr IsSupportedGPUFanSpeed;             // ADLX_RESULT IsSupportedGPUFanSpeed(adlx_bool* supported)
            public IntPtr IsSupportedGPUVRAM;                 // ADLX_RESULT IsSupportedGPUVRAM(adlx_bool* supported)
            public IntPtr IsSupportedGPUVoltage;              // ADLX_RESULT IsSupportedGPUVoltage(adlx_bool* supported)
            public IntPtr IsSupportedGPUTotalBoardPower;      // ADLX_RESULT IsSupportedGPUTotalBoardPower(adlx_bool* supported)
        }

        // IADLXGPUMetrics vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXGPUMetricsVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXGPUMetrics methods
            public IntPtr TimeStamp;                          // ADLX_RESULT TimeStamp(adlx_int64* ms)
            public IntPtr GPUUsage;                           // ADLX_RESULT GPUUsage(adlx_double* data)
            public IntPtr GPUClockSpeed;                      // ADLX_RESULT GPUClockSpeed(adlx_int* data)
            public IntPtr GPUVRAMClockSpeed;                  // ADLX_RESULT GPUVRAMClockSpeed(adlx_int* data)
            public IntPtr GPUTemperature;                     // ADLX_RESULT GPUTemperature(adlx_double* data)
            public IntPtr GPUHotspotTemperature;              // ADLX_RESULT GPUHotspotTemperature(adlx_double* data)
            public IntPtr GPUPower;                           // ADLX_RESULT GPUPower(adlx_double* data)
            public IntPtr GPUFanSpeed;                        // ADLX_RESULT GPUFanSpeed(adlx_int* data)
            public IntPtr GPUVRAM;                            // ADLX_RESULT GPUVRAM(adlx_int* data)
            public IntPtr GPUVoltage;                         // ADLX_RESULT GPUVoltage(adlx_int* data)
            public IntPtr GPUTotalBoardPower;                 // ADLX_RESULT GPUTotalBoardPower(adlx_double* data)
        }

        // IADLXGPUTuningServices vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXGPUTuningServicesVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXGPUTuningServices methods
            public IntPtr GetGPUTuningChangedHandling;   // ADLX_RESULT GetGPUTuningChangedHandling(IADLXGPUTuningChangedHandling** ppGPUTuningChangedHandling)
            public IntPtr IsSupportedAutoTuning;          // ADLX_RESULT IsSupportedAutoTuning(IADLXGPU* pGPU, adlx_bool* supported)
            public IntPtr IsSupportedPresetTuning;        // ADLX_RESULT IsSupportedPresetTuning(IADLXGPU* pGPU, adlx_bool* supported)
            public IntPtr IsSupportedManualGFXTuning;     // ADLX_RESULT IsSupportedManualGFXTuning(IADLXGPU* pGPU, adlx_bool* supported)
            public IntPtr IsSupportedManualVRAMTuning;    // ADLX_RESULT IsSupportedManualVRAMTuning(IADLXGPU* pGPU, adlx_bool* supported)
            public IntPtr IsSupportedManualFanTuning;     // ADLX_RESULT IsSupportedManualFanTuning(IADLXGPU* pGPU, adlx_bool* supported)
            public IntPtr IsSupportedManualPowerTuning;   // ADLX_RESULT IsSupportedManualPowerTuning(IADLXGPU* pGPU, adlx_bool* supported)
            public IntPtr GetAutoTuning;                  // ADLX_RESULT GetAutoTuning(IADLXGPU* pGPU, IADLXGPUAutoTuning** ppAutoTuning)
            public IntPtr GetPresetTuning;                // ADLX_RESULT GetPresetTuning(IADLXGPU* pGPU, IADLXGPUPresetTuning** ppPresetTuning)
            public IntPtr GetManualGFXTuning;             // ADLX_RESULT GetManualGFXTuning(IADLXGPU* pGPU, IADLXGPUManualGFXTuning** ppManualGFXTuning)
            public IntPtr GetManualVRAMTuning;            // ADLX_RESULT GetManualVRAMTuning(IADLXGPU* pGPU, IADLXGPUManualVRAMTuning** ppManualVRAMTuning)
            public IntPtr GetManualFanTuning;             // ADLX_RESULT GetManualFanTuning(IADLXGPU* pGPU, IADLXGPUManualFanTuning** ppManualFanTuning)
            public IntPtr GetManualPowerTuning;           // ADLX_RESULT GetManualPowerTuning(IADLXGPU* pGPU, IADLXGPUManualPowerTuning** ppManualPowerTuning)
        }

        // IADLXDisplayServices vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayServicesVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXDisplayServices methods
            public IntPtr GetNumberOfDisplays;    // ADLX_RESULT GetNumberOfDisplays(adlx_uint* numDisplays)
            public IntPtr GetDisplays;            // ADLX_RESULT GetDisplays(IADLXDisplayList** ppDisplay)
            public IntPtr Get3DLUT;               // ADLX_RESULT Get3DLUT(IADLXDisplay* pDisplay, IADLXDisplay3DLUT** ppDisp3DLUT)
            public IntPtr GetGamut;               // ADLX_RESULT GetGamut(IADLXDisplay* pDisplay, IADLXDisplayGamut** ppDispGamut)
            public IntPtr GetGamma;               // ADLX_RESULT GetGamma(IADLXDisplay* pDisplay, IADLXDisplayGamma** ppDispGamma)
            public IntPtr GetDisplayChangedHandling;  // ADLX_RESULT GetDisplayChangedHandling(IADLXDisplayChangedHandling** ppDisplayChangedHandling)
            public IntPtr GetFreeSync;            // ADLX_RESULT GetFreeSync(IADLXDisplay* pDisplay, IADLXDisplayFreeSync** ppFreeSync)
            public IntPtr GetVirtualSuperResolution; // ADLX_RESULT GetVirtualSuperResolution(IADLXDisplay* pDisplay, IADLXDisplayVSR** ppVSR)
            public IntPtr GetGPUScaling;          // ADLX_RESULT GetGPUScaling(IADLXDisplay* pDisplay, IADLXDisplayGPUScaling** ppGPUScaling)
            public IntPtr GetScalingMode;         // ADLX_RESULT GetScalingMode(IADLXDisplay* pDisplay, IADLXDisplayScalingMode** ppScalingMode)
            public IntPtr GetIntegerScaling;      // ADLX_RESULT GetIntegerScaling(IADLXDisplay* pDisplay, IADLXDisplayIntegerScaling** ppIntegerScaling)
            public IntPtr GetColorDepth;          // ADLX_RESULT GetColorDepth(IADLXDisplay* pDisplay, IADLXDisplayColorDepth** ppColorDepth)
            public IntPtr GetPixelFormat;         // ADLX_RESULT GetPixelFormat(IADLXDisplay* pDisplay, IADLXDisplayPixelFormat** ppPixelFormat)
            public IntPtr GetCustomColor;         // ADLX_RESULT GetCustomColor(IADLXDisplay* pDisplay, IADLXDisplayCustomColor** ppCustomColor)
            public IntPtr GetHDCP;               // ADLX_RESULT GetHDCP(IADLXDisplay* pDisplay, IADLXDisplayHDCP** ppHDCP)
            public IntPtr GetCustomResolution;    // ADLX_RESULT GetCustomResolution(IADLXDisplay* pDisplay, IADLXDisplayCustomResolution** ppCustomResolution)
            public IntPtr GetVariBright;          // ADLX_RESULT GetVariBright(IADLXDisplay* pDisplay, IADLXDisplayVariBright** ppVariBright)
            public IntPtr GetDisplayBlanking;     // ADLX_RESULT GetDisplayBlanking(IADLXDisplay* pDisplay, IADLXDisplayBlanking** ppDisplayBlanking)
            public IntPtr GetDisplayConnectivityExperience; // ADLX_RESULT GetDisplayConnectivityExperience(IADLXDisplay* pDisplay, IADLXDisplayConnectivityExperience** ppDisplayConnectivityExperience)
            public IntPtr GetPowerTuning; // placeholder
            public IntPtr GetRadeonSuperResolution; // placeholder
            public IntPtr GetVirtualResolution; // placeholder
            public IntPtr GetAdaptiveSync; // placeholder
            public IntPtr GetDisplayGamutPixelFormat; // placeholder
            public IntPtr GetCustomColorTemperature; // placeholder
            public IntPtr GetFreeSyncColorAccuracy; // IADLXDisplayFreeSyncColorAccuracy**
            public IntPtr GetDynamicRefreshRateControl; // IADLXDisplayDynamicRefreshRateControl**
        }

        // IADLXDisplayList vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayListVtbl
        {
            // Inherit from IADLXList
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;
            public IntPtr Size;
            public IntPtr Empty;
            public IntPtr Begin;
            public IntPtr End;
            public IntPtr At;              // ADLX_RESULT At(adlx_uint location, IADLXDisplay** ppItem)
            public IntPtr Clear;
            public IntPtr Remove_Back;
            public IntPtr Add_Back;        // ADLX_RESULT Add_Back(IADLXDisplay* pItem)
        }

        // IADLXDisplay vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayVtbl
        {
            // Base interface methods
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXDisplay methods (based on IDisplays.h)
            public IntPtr ManufacturerID;         // ADLX_RESULT ManufacturerID(adlx_uint* manufacturerID)
            public IntPtr DisplayType;            // ADLX_RESULT DisplayType(ADLX_DISPLAY_TYPE* displayType)
            public IntPtr ConnectorType;          // ADLX_RESULT ConnectorType(ADLX_DISPLAY_CONNECTOR_TYPE* connectType)
            public IntPtr Name;                   // ADLX_RESULT Name(const char** displayName)
            public IntPtr EDID;                   // ADLX_RESULT EDID(const char** edid)
            public IntPtr NativeResolution;       // ADLX_RESULT NativeResolution(adlx_int* maxHResolution, adlx_int* maxVResolution)
            public IntPtr RefreshRate;            // ADLX_RESULT RefreshRate(adlx_double* refreshRate)
            public IntPtr PixelClock;             // ADLX_RESULT PixelClock(adlx_uint* pixelClock)
            public IntPtr ScanType;               // ADLX_RESULT ScanType(ADLX_DISPLAY_SCAN_TYPE* scanType)
            public IntPtr GetGPU;                 // ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
            public IntPtr UniqueId;               // ADLX_RESULT UniqueId(adlx_size* uniqueId)
        }

        // IADLXDisplayFreeSync vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayFreeSyncVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsEnabled;    // ADLX_RESULT IsEnabled(adlx_bool* enabled)
            public IntPtr SetEnabled;   // ADLX_RESULT SetEnabled(adlx_bool enabled)
        }

        // IADLXDisplayGPUScaling vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayGPUScalingVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsEnabled;    // ADLX_RESULT IsEnabled(adlx_bool* enabled)
            public IntPtr SetEnabled;   // ADLX_RESULT SetEnabled(adlx_bool enabled)
        }

        // IADLXDisplayScalingMode vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayScalingModeVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr GetMode;      // ADLX_RESULT GetMode(ADLX_SCALE_MODE* currentMode)
            public IntPtr SetMode;      // ADLX_RESULT SetMode(ADLX_SCALE_MODE mode)
        }

        // IADLXDisplayColorDepth vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayColorDepthVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;          // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr GetValue;             // ADLX_RESULT GetValue(ADLX_COLOR_DEPTH* currentColorDepth)
            public IntPtr SetValue;             // ADLX_RESULT SetValue(ADLX_COLOR_DEPTH colorDepth)
            public IntPtr IsSupportedColorDepth;// ADLX_RESULT IsSupportedColorDepth(ADLX_COLOR_DEPTH depth, adlx_bool* supported)
        }

        // IADLXDisplayPixelFormat vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayPixelFormatVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;            // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr GetValue;               // ADLX_RESULT GetValue(ADLX_PIXEL_FORMAT* pixelFormat)
            public IntPtr SetValue;               // ADLX_RESULT SetValue(ADLX_PIXEL_FORMAT pixelFormat)
            public IntPtr IsSupportedPixelFormat; // ADLX_RESULT IsSupportedPixelFormat(ADLX_PIXEL_FORMAT pixelFormat, adlx_bool* supported)
        }

        // IADLXDisplayFreeSyncColorAccuracy vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayFreeSyncColorAccuracyVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsEnabled;    // ADLX_RESULT IsEnabled(adlx_bool* enabled)
            public IntPtr SetEnabled;   // ADLX_RESULT SetEnabled(adlx_bool enabled)
        }

        // IADLXDisplayDynamicRefreshRateControl vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayDynamicRefreshRateControlVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsEnabled;    // ADLX_RESULT IsEnabled(adlx_bool* enabled)
            public IntPtr SetEnabled;   // ADLX_RESULT SetEnabled(adlx_bool enabled)
        }

        // IADLXDisplayVSR vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayVSRVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsEnabled;    // ADLX_RESULT IsEnabled(adlx_bool* enabled)
            public IntPtr SetEnabled;   // ADLX_RESULT SetEnabled(adlx_bool enabled)
        }

        // IADLXDisplayIntegerScaling vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayIntegerScalingVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsEnabled;    // ADLX_RESULT IsEnabled(adlx_bool* enabled)
            public IntPtr SetEnabled;   // ADLX_RESULT SetEnabled(adlx_bool enabled)
        }

        // IADLXDisplayHDCP vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayHDCPVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsEnabled;    // ADLX_RESULT IsEnabled(adlx_bool* enabled)
            public IntPtr SetEnabled;   // ADLX_RESULT SetEnabled(adlx_bool enabled)
        }

        // IADLXDisplayCustomColor vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayCustomColorVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsHueSupported;
            public IntPtr GetHueRange;
            public IntPtr GetHue;
            public IntPtr SetHue;
            public IntPtr IsSaturationSupported;
            public IntPtr GetSaturationRange;
            public IntPtr GetSaturation;
            public IntPtr SetSaturation;
            public IntPtr IsBrightnessSupported;
            public IntPtr GetBrightnessRange;
            public IntPtr GetBrightness;
            public IntPtr SetBrightness;
            public IntPtr IsContrastSupported;
            public IntPtr GetContrastRange;
            public IntPtr GetContrast;
            public IntPtr SetContrast;
            public IntPtr IsTemperatureSupported;
            public IntPtr GetTemperatureRange;
            public IntPtr GetTemperature;
            public IntPtr SetTemperature;
        }

        // IADLXDisplayVariBright vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayVariBrightVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;              // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsEnabled;                // ADLX_RESULT IsEnabled(adlx_bool* enabled)
            public IntPtr SetEnabled;               // ADLX_RESULT SetEnabled(adlx_bool enabled)
            public IntPtr IsCurrentMaximizeBrightness;
            public IntPtr IsCurrentOptimizeBrightness;
            public IntPtr IsCurrentBalanced;
            public IntPtr IsCurrentOptimizeBattery;
            public IntPtr IsCurrentMaximizeBattery;
            public IntPtr SetMaximizeBrightness;
            public IntPtr SetOptimizeBrightness;
            public IntPtr SetBalanced;
            public IntPtr SetOptimizeBattery;
            public IntPtr SetMaximizeBattery;
        }

        // IADLXDisplayVariBright1 vtable (extends VariBright)
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayVariBright1Vtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;
            public IntPtr IsEnabled;
            public IntPtr SetEnabled;
            public IntPtr IsCurrentMaximizeBrightness;
            public IntPtr IsCurrentOptimizeBrightness;
            public IntPtr IsCurrentBalanced;
            public IntPtr IsCurrentOptimizeBattery;
            public IntPtr IsCurrentMaximizeBattery;
            public IntPtr SetMaximizeBrightness;
            public IntPtr SetOptimizeBrightness;
            public IntPtr SetBalanced;
            public IntPtr SetOptimizeBattery;
            public IntPtr SetMaximizeBattery;
            public IntPtr IsBacklightAdaptiveSupported;
            public IntPtr IsBacklightAdaptiveEnabled;
            public IntPtr SetBacklightAdaptiveEnabled;
            public IntPtr IsBatteryLifeSupported;
            public IntPtr IsBatteryLifeEnabled;
            public IntPtr SetBatteryLifeEnabled;
        }

        // IADLXDisplayBlanking vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayBlankingVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;      // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr IsCurrentBlanked; // ADLX_RESULT IsCurrentBlanked(adlx_bool* blanked)
            public IntPtr IsCurrentUnblanked; // ADLX_RESULT IsCurrentUnblanked(adlx_bool* unBlanked)
            public IntPtr SetBlanked;       // ADLX_RESULT SetBlanked()
            public IntPtr SetUnblanked;     // ADLX_RESULT SetUnblanked()
        }

        // IADLXDisplayCustomResolution vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayCustomResolutionVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;                  // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr GetResolutionList;            // ADLX_RESULT GetResolutionList(IADLXDisplayResolutionList** ppResolutionList)
            public IntPtr GetCurrentAppliedResolution;  // ADLX_RESULT GetCurrentAppliedResolution(IADLXDisplayResolution** ppResolution)
            public IntPtr CreateNewResolution;          // ADLX_RESULT CreateNewResolution(IADLXDisplayResolution* pResolution)
            public IntPtr DeleteResolution;             // ADLX_RESULT DeleteResolution(IADLXDisplayResolution* pResolution)
        }

        // IADLXDisplayResolution vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayResolutionVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr GetValue; // ADLX_RESULT GetValue(ADLX_CustomResolution* customResolution)
            public IntPtr SetValue; // ADLX_RESULT SetValue(ADLX_CustomResolution customResolution)
        }

        // IADLXDisplayConnectivityExperience vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDisplayConnectivityExperienceVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupportedHDMIQualityDetection;
            public IntPtr IsSupportedDPLink;
            public IntPtr IsEnabledHDMIQualityDetection;
            public IntPtr SetEnabledHDMIQualityDetection;
            public IntPtr GetDPLinkRate;
            public IntPtr GetNumberOfActiveLanes;
            public IntPtr GetNumberOfTotalLanes;
            public IntPtr GetRelativePreEmphasis;
            public IntPtr SetRelativePreEmphasis;
            public IntPtr GetRelativeVoltageSwing;
            public IntPtr SetRelativeVoltageSwing;
            public IntPtr IsEnabledLinkProtection;
        }

        // IADLXDesktop vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDesktopVtbl
        {
            // IADLXInterface
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            // IADLXDesktop
            public IntPtr Orientation;          // ADLX_RESULT Orientation(ADLX_ORIENTATION* orientation)
            public IntPtr Size;                 // ADLX_RESULT Size(adlx_int* width, adlx_int* height)
            public IntPtr TopLeft;              // ADLX_RESULT TopLeft(ADLX_Point* locationTopLeft)
            public IntPtr Type;                 // ADLX_RESULT Type(ADLX_DESKTOP_TYPE* desktopType)
            public IntPtr GetNumberOfDisplays;  // ADLX_RESULT GetNumberOfDisplays(adlx_uint* numDisplays)
            public IntPtr GetDisplays;          // ADLX_RESULT GetDisplays(IADLXDisplayList** ppDisplays)
        }

        // IADLXDesktopList vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDesktopListVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;
            public IntPtr Size;
            public IntPtr Empty;
            public IntPtr Begin;
            public IntPtr End;
            public IntPtr At;              // ADLX_RESULT At(adlx_uint location, IADLXDesktop** ppItem)
            public IntPtr Clear;
            public IntPtr Remove_Back;
            public IntPtr Add_Back;        // ADLX_RESULT Add_Back(IADLXDesktop* pItem)
        }

        // IADLXEyefinityDesktop vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXEyefinityDesktopVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr GridSize;             // ADLX_RESULT GridSize(adlx_uint* rows, adlx_uint* cols)
            public IntPtr GetDisplay;           // ADLX_RESULT GetDisplay(adlx_uint row, adlx_uint col, IADLXDisplay** ppDisplay)
            public IntPtr DisplayOrientation;   // ADLX_RESULT DisplayOrientation(adlx_uint row, adlx_uint col, ADLX_ORIENTATION* displayOrientation)
            public IntPtr DisplaySize;          // ADLX_RESULT DisplaySize(adlx_uint row, adlx_uint col, adlx_int* displayWidth, adlx_int* displayHeight)
        }

        // IADLXSimpleEyefinity vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXSimpleEyefinityVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr IsSupported;          // ADLX_RESULT IsSupported(adlx_bool* supported)
            public IntPtr Create;               // ADLX_RESULT Create(IADLXEyefinityDesktop** ppEyefinityDesktop)
            public IntPtr DestroyAll;           // ADLX_RESULT DestroyAll()
            public IntPtr Destroy;              // ADLX_RESULT Destroy(IADLXEyefinityDesktop* pDesktop)
        }

        // IADLXDesktopServices vtable
        [StructLayout(LayoutKind.Sequential)]
        internal struct IADLXDesktopServicesVtbl
        {
            public IntPtr QueryInterface;
            public IntPtr AddRef;
            public IntPtr Release;

            public IntPtr GetNumberOfDesktops;      // ADLX_RESULT GetNumberOfDesktops(adlx_uint* pNumDesktops)
            public IntPtr GetDesktops;              // ADLX_RESULT GetDesktops(IADLXDesktopList** ppDesktops)
            public IntPtr GetDesktopChangedHandling;// ADLX_RESULT GetDesktopChangedHandling(IADLXDesktopChangedHandling** ppDesktopChangedHandling)
            public IntPtr GetSimpleEyefinity;       // ADLX_RESULT GetSimpleEyefinity(IADLXSimpleEyefinity** ppSimpleEyefinity)
        }
    }
}
