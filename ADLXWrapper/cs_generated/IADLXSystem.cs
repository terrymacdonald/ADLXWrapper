using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem"]/*' />
public unsafe partial struct IADLXSystem
{
    public void** lpVtbl;

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.HybridGraphicsType"]/*' />
    public ADLX_RESULT HybridGraphicsType(ADLX_HG_TYPE* hgType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, ADLX_HG_TYPE*, ADLX_RESULT>)(lpVtbl[0]))((IADLXSystem*)Unsafe.AsPointer(ref this), hgType);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.GetGPUs"]/*' />
    public ADLX_RESULT GetGPUs(IADLXGPUList** ppGPUs)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXGPUList**, ADLX_RESULT>)(lpVtbl[1]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppGPUs);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.QueryInterface"]/*' />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystem*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.GetDisplaysServices"]/*' />
    public ADLX_RESULT GetDisplaysServices(IADLXDisplayServices** ppDispServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXDisplayServices**, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppDispServices);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.GetDesktopsServices"]/*' />
    public ADLX_RESULT GetDesktopsServices(IADLXDesktopServices** ppDeskServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXDesktopServices**, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppDeskServices);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.GetGPUsChangedHandling"]/*' />
    public ADLX_RESULT GetGPUsChangedHandling(IADLXGPUsChangedHandling** ppGPUsChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXGPUsChangedHandling**, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppGPUsChangedHandling);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.EnableLog"]/*' />
    public ADLX_RESULT EnableLog(ADLX_LOG_DESTINATION mode, ADLX_LOG_SEVERITY severity, [NativeTypeName("adlx::IADLXLog *")] IADLXLog* pLogger, [NativeTypeName("const wchar_t *")] ushort* fileName)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, ADLX_LOG_DESTINATION, ADLX_LOG_SEVERITY, IADLXLog*, ushort*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSystem*)Unsafe.AsPointer(ref this), mode, severity, pLogger, fileName);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.Get3DSettingsServices"]/*' />
    public ADLX_RESULT Get3DSettingsServices(IADLX3DSettingsServices** pp3DSettingsServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLX3DSettingsServices**, ADLX_RESULT>)(lpVtbl[7]))((IADLXSystem*)Unsafe.AsPointer(ref this), pp3DSettingsServices);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.GetGPUTuningServices"]/*' />
    public ADLX_RESULT GetGPUTuningServices(IADLXGPUTuningServices** ppGPUTuningServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXGPUTuningServices**, ADLX_RESULT>)(lpVtbl[8]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppGPUTuningServices);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.GetPerformanceMonitoringServices"]/*' />
    public ADLX_RESULT GetPerformanceMonitoringServices(IADLXPerformanceMonitoringServices** ppPerformanceMonitoringServices)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXPerformanceMonitoringServices**, ADLX_RESULT>)(lpVtbl[9]))((IADLXSystem*)Unsafe.AsPointer(ref this), ppPerformanceMonitoringServices);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.TotalSystemRAM"]/*' />
    public ADLX_RESULT TotalSystemRAM([NativeTypeName("adlx_uint *")] uint* ramMB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, uint*, ADLX_RESULT>)(lpVtbl[10]))((IADLXSystem*)Unsafe.AsPointer(ref this), ramMB);
    }

    /// <include file='IADLXSystem.xml' path='doc/member[@name="IADLXSystem.GetI2C"]/*' />
    public ADLX_RESULT GetI2C([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXI2C** ppI2C)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystem*, IADLXGPU*, IADLXI2C**, ADLX_RESULT>)(lpVtbl[11]))((IADLXSystem*)Unsafe.AsPointer(ref this), pGPU, ppI2C);
    }
}
