using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices"]/*' />
[NativeTypeName("struct IADLXPerformanceMonitoringServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXPerformanceMonitoringServices
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int>)(lpVtbl[0]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int>)(lpVtbl[1]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetSamplingIntervalRange"]/*' />
    public ADLX_RESULT GetSamplingIntervalRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), range);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.SetSamplingInterval"]/*' />
    public ADLX_RESULT SetSamplingInterval([NativeTypeName("adlx_int")] int askedIntervalMs)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int, ADLX_RESULT>)(lpVtbl[4]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), askedIntervalMs);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetSamplingInterval"]/*' />
    public ADLX_RESULT GetSamplingInterval([NativeTypeName("adlx_int *")] int* intervalMs)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), intervalMs);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetMaxPerformanceMetricsHistorySizeRange"]/*' />
    public ADLX_RESULT GetMaxPerformanceMetricsHistorySizeRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[6]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), range);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.SetMaxPerformanceMetricsHistorySize"]/*' />
    public ADLX_RESULT SetMaxPerformanceMetricsHistorySize([NativeTypeName("adlx_int")] int sizeSec)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int, ADLX_RESULT>)(lpVtbl[7]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), sizeSec);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetMaxPerformanceMetricsHistorySize"]/*' />
    public ADLX_RESULT GetMaxPerformanceMetricsHistorySize([NativeTypeName("adlx_int *")] int* sizeSec)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), sizeSec);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.ClearPerformanceMetricsHistory"]/*' />
    public ADLX_RESULT ClearPerformanceMetricsHistory()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, ADLX_RESULT>)(lpVtbl[9]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetCurrentPerformanceMetricsHistorySize"]/*' />
    public ADLX_RESULT GetCurrentPerformanceMetricsHistorySize([NativeTypeName("adlx_int *")] int* sizeSec)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), sizeSec);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.StartPerformanceMetricsTracking"]/*' />
    public ADLX_RESULT StartPerformanceMetricsTracking()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, ADLX_RESULT>)(lpVtbl[11]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.StopPerformanceMetricsTracking"]/*' />
    public ADLX_RESULT StopPerformanceMetricsTracking()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, ADLX_RESULT>)(lpVtbl[12]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetAllMetricsHistory"]/*' />
    public ADLX_RESULT GetAllMetricsHistory([NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXAllMetricsList** ppMetricsList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int, int, IADLXAllMetricsList**, ADLX_RESULT>)(lpVtbl[13]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), startMs, stopMs, ppMetricsList);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetGPUMetricsHistory"]/*' />
    public ADLX_RESULT GetGPUMetricsHistory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXGPUMetricsList** ppMetricsList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, IADLXGPU*, int, int, IADLXGPUMetricsList**, ADLX_RESULT>)(lpVtbl[14]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), pGPU, startMs, stopMs, ppMetricsList);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetSystemMetricsHistory"]/*' />
    public ADLX_RESULT GetSystemMetricsHistory([NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXSystemMetricsList** ppMetricsList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int, int, IADLXSystemMetricsList**, ADLX_RESULT>)(lpVtbl[15]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), startMs, stopMs, ppMetricsList);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetFPSHistory"]/*' />
    public ADLX_RESULT GetFPSHistory([NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXFPSList** ppMetricsList)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, int, int, IADLXFPSList**, ADLX_RESULT>)(lpVtbl[16]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), startMs, stopMs, ppMetricsList);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetCurrentAllMetrics"]/*' />
    public ADLX_RESULT GetCurrentAllMetrics(IADLXAllMetrics** ppMetrics)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, IADLXAllMetrics**, ADLX_RESULT>)(lpVtbl[17]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), ppMetrics);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetCurrentGPUMetrics"]/*' />
    public ADLX_RESULT GetCurrentGPUMetrics([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetrics** ppMetrics)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, IADLXGPU*, IADLXGPUMetrics**, ADLX_RESULT>)(lpVtbl[18]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), pGPU, ppMetrics);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetCurrentSystemMetrics"]/*' />
    public ADLX_RESULT GetCurrentSystemMetrics(IADLXSystemMetrics** ppMetrics)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, IADLXSystemMetrics**, ADLX_RESULT>)(lpVtbl[19]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), ppMetrics);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetCurrentFPS"]/*' />
    public ADLX_RESULT GetCurrentFPS(IADLXFPS** ppMetrics)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, IADLXFPS**, ADLX_RESULT>)(lpVtbl[20]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), ppMetrics);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetSupportedGPUMetrics"]/*' />
    public ADLX_RESULT GetSupportedGPUMetrics([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetricsSupport** ppMetricsSupported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, IADLXGPU*, IADLXGPUMetricsSupport**, ADLX_RESULT>)(lpVtbl[21]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), pGPU, ppMetricsSupported);
    }

    /// <include file='IADLXPerformanceMonitoringServices.xml' path='doc/member[@name="IADLXPerformanceMonitoringServices.GetSupportedSystemMetrics"]/*' />
    public ADLX_RESULT GetSupportedSystemMetrics(IADLXSystemMetricsSupport** ppMetricsSupported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPerformanceMonitoringServices*, IADLXSystemMetricsSupport**, ADLX_RESULT>)(lpVtbl[22]))((IADLXPerformanceMonitoringServices*)Unsafe.AsPointer(ref this), ppMetricsSupported);
    }
}
