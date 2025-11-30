using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXSystem
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _HybridGraphicsType(IADLXSystem* pThis, ADLX_HG_TYPE* hgType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUs(IADLXSystem* pThis, IADLXGPUList** ppGPUs);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXSystem* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplaysServices(IADLXSystem* pThis, IADLXDisplayServices** ppDispServices);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDesktopsServices(IADLXSystem* pThis, IADLXDesktopServices** ppDeskServices);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUsChangedHandling(IADLXSystem* pThis, IADLXGPUsChangedHandling** ppGPUsChangedHandling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _EnableLog(IADLXSystem* pThis, ADLX_LOG_DESTINATION mode, ADLX_LOG_SEVERITY severity, [NativeTypeName("adlx::IADLXLog *")] IADLXLog* pLogger, [NativeTypeName("const wchar_t *")] ushort* fileName);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Get3DSettingsServices(IADLXSystem* pThis, IADLX3DSettingsServices** pp3DSettingsServices);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUTuningServices(IADLXSystem* pThis, IADLXGPUTuningServices** ppGPUTuningServices);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetPerformanceMonitoringServices(IADLXSystem* pThis, IADLXPerformanceMonitoringServices** ppPerformanceMonitoringServices);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _TotalSystemRAM(IADLXSystem* pThis, [NativeTypeName("adlx_uint *")] uint* ramMB);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetI2C(IADLXSystem* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXI2C** ppI2C);

    public ADLX_RESULT HybridGraphicsType(ADLX_HG_TYPE* hgType)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_HybridGraphicsType>((IntPtr)(lpVtbl[0]))(pThis, hgType);
        }
    }

    public ADLX_RESULT GetGPUs(IADLXGPUList** ppGPUs)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUs>((IntPtr)(lpVtbl[1]))(pThis, ppGPUs);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetDisplaysServices(IADLXDisplayServices** ppDispServices)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplaysServices>((IntPtr)(lpVtbl[3]))(pThis, ppDispServices);
        }
    }

    public ADLX_RESULT GetDesktopsServices(IADLXDesktopServices** ppDeskServices)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDesktopsServices>((IntPtr)(lpVtbl[4]))(pThis, ppDeskServices);
        }
    }

    public ADLX_RESULT GetGPUsChangedHandling(IADLXGPUsChangedHandling** ppGPUsChangedHandling)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUsChangedHandling>((IntPtr)(lpVtbl[5]))(pThis, ppGPUsChangedHandling);
        }
    }

    public ADLX_RESULT EnableLog(ADLX_LOG_DESTINATION mode, ADLX_LOG_SEVERITY severity, [NativeTypeName("adlx::IADLXLog *")] IADLXLog* pLogger, [NativeTypeName("const wchar_t *")] ushort* fileName)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_EnableLog>((IntPtr)(lpVtbl[6]))(pThis, mode, severity, pLogger, fileName);
        }
    }

    public ADLX_RESULT Get3DSettingsServices(IADLX3DSettingsServices** pp3DSettingsServices)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Get3DSettingsServices>((IntPtr)(lpVtbl[7]))(pThis, pp3DSettingsServices);
        }
    }

    public ADLX_RESULT GetGPUTuningServices(IADLXGPUTuningServices** ppGPUTuningServices)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUTuningServices>((IntPtr)(lpVtbl[8]))(pThis, ppGPUTuningServices);
        }
    }

    public ADLX_RESULT GetPerformanceMonitoringServices(IADLXPerformanceMonitoringServices** ppPerformanceMonitoringServices)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetPerformanceMonitoringServices>((IntPtr)(lpVtbl[9]))(pThis, ppPerformanceMonitoringServices);
        }
    }

    public ADLX_RESULT TotalSystemRAM([NativeTypeName("adlx_uint *")] uint* ramMB)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_TotalSystemRAM>((IntPtr)(lpVtbl[10]))(pThis, ramMB);
        }
    }

    public ADLX_RESULT GetI2C([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXI2C** ppI2C)
    {
        fixed (IADLXSystem* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetI2C>((IntPtr)(lpVtbl[11]))(pThis, pGPU, ppI2C);
        }
    }
}
