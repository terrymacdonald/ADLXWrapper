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
    }
}
