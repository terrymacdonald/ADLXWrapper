using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPerformanceMonitoringServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXPerformanceMonitoringServices
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXPerformanceMonitoringServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXPerformanceMonitoringServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSamplingIntervalRange(IADLXPerformanceMonitoringServices* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetSamplingInterval(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx_int")] int askedIntervalMs);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSamplingInterval(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx_int *")] int* intervalMs);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMaxPerformanceMetricsHistorySizeRange(IADLXPerformanceMonitoringServices* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMaxPerformanceMetricsHistorySize(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx_int")] int sizeSec);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMaxPerformanceMetricsHistorySize(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx_int *")] int* sizeSec);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ClearPerformanceMetricsHistory(IADLXPerformanceMonitoringServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCurrentPerformanceMetricsHistorySize(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx_int *")] int* sizeSec);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _StartPerformanceMetricsTracking(IADLXPerformanceMonitoringServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _StopPerformanceMetricsTracking(IADLXPerformanceMonitoringServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetAllMetricsHistory(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXAllMetricsList** ppMetricsList);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUMetricsHistory(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXGPUMetricsList** ppMetricsList);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSystemMetricsHistory(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXSystemMetricsList** ppMetricsList);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFPSHistory(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXFPSList** ppMetricsList);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCurrentAllMetrics(IADLXPerformanceMonitoringServices* pThis, IADLXAllMetrics** ppMetrics);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCurrentGPUMetrics(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetrics** ppMetrics);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCurrentSystemMetrics(IADLXPerformanceMonitoringServices* pThis, IADLXSystemMetrics** ppMetrics);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCurrentFPS(IADLXPerformanceMonitoringServices* pThis, IADLXFPS** ppMetrics);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSupportedGPUMetrics(IADLXPerformanceMonitoringServices* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetricsSupport** ppMetricsSupported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSupportedSystemMetrics(IADLXPerformanceMonitoringServices* pThis, IADLXSystemMetricsSupport** ppMetricsSupported);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetSamplingIntervalRange(ADLX_IntRange* range)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSamplingIntervalRange>((IntPtr)(lpVtbl[3]))(pThis, range);
        }
    }

    public ADLX_RESULT SetSamplingInterval([NativeTypeName("adlx_int")] int askedIntervalMs)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetSamplingInterval>((IntPtr)(lpVtbl[4]))(pThis, askedIntervalMs);
        }
    }

    public ADLX_RESULT GetSamplingInterval([NativeTypeName("adlx_int *")] int* intervalMs)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSamplingInterval>((IntPtr)(lpVtbl[5]))(pThis, intervalMs);
        }
    }

    public ADLX_RESULT GetMaxPerformanceMetricsHistorySizeRange(ADLX_IntRange* range)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMaxPerformanceMetricsHistorySizeRange>((IntPtr)(lpVtbl[6]))(pThis, range);
        }
    }

    public ADLX_RESULT SetMaxPerformanceMetricsHistorySize([NativeTypeName("adlx_int")] int sizeSec)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMaxPerformanceMetricsHistorySize>((IntPtr)(lpVtbl[7]))(pThis, sizeSec);
        }
    }

    public ADLX_RESULT GetMaxPerformanceMetricsHistorySize([NativeTypeName("adlx_int *")] int* sizeSec)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMaxPerformanceMetricsHistorySize>((IntPtr)(lpVtbl[8]))(pThis, sizeSec);
        }
    }

    public ADLX_RESULT ClearPerformanceMetricsHistory()
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ClearPerformanceMetricsHistory>((IntPtr)(lpVtbl[9]))(pThis);
        }
    }

    public ADLX_RESULT GetCurrentPerformanceMetricsHistorySize([NativeTypeName("adlx_int *")] int* sizeSec)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCurrentPerformanceMetricsHistorySize>((IntPtr)(lpVtbl[10]))(pThis, sizeSec);
        }
    }

    public ADLX_RESULT StartPerformanceMetricsTracking()
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_StartPerformanceMetricsTracking>((IntPtr)(lpVtbl[11]))(pThis);
        }
    }

    public ADLX_RESULT StopPerformanceMetricsTracking()
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_StopPerformanceMetricsTracking>((IntPtr)(lpVtbl[12]))(pThis);
        }
    }

    public ADLX_RESULT GetAllMetricsHistory([NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXAllMetricsList** ppMetricsList)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetAllMetricsHistory>((IntPtr)(lpVtbl[13]))(pThis, startMs, stopMs, ppMetricsList);
        }
    }

    public ADLX_RESULT GetGPUMetricsHistory([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, [NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXGPUMetricsList** ppMetricsList)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUMetricsHistory>((IntPtr)(lpVtbl[14]))(pThis, pGPU, startMs, stopMs, ppMetricsList);
        }
    }

    public ADLX_RESULT GetSystemMetricsHistory([NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXSystemMetricsList** ppMetricsList)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSystemMetricsHistory>((IntPtr)(lpVtbl[15]))(pThis, startMs, stopMs, ppMetricsList);
        }
    }

    public ADLX_RESULT GetFPSHistory([NativeTypeName("adlx_int")] int startMs, [NativeTypeName("adlx_int")] int stopMs, IADLXFPSList** ppMetricsList)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFPSHistory>((IntPtr)(lpVtbl[16]))(pThis, startMs, stopMs, ppMetricsList);
        }
    }

    public ADLX_RESULT GetCurrentAllMetrics(IADLXAllMetrics** ppMetrics)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCurrentAllMetrics>((IntPtr)(lpVtbl[17]))(pThis, ppMetrics);
        }
    }

    public ADLX_RESULT GetCurrentGPUMetrics([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetrics** ppMetrics)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCurrentGPUMetrics>((IntPtr)(lpVtbl[18]))(pThis, pGPU, ppMetrics);
        }
    }

    public ADLX_RESULT GetCurrentSystemMetrics(IADLXSystemMetrics** ppMetrics)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCurrentSystemMetrics>((IntPtr)(lpVtbl[19]))(pThis, ppMetrics);
        }
    }

    public ADLX_RESULT GetCurrentFPS(IADLXFPS** ppMetrics)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCurrentFPS>((IntPtr)(lpVtbl[20]))(pThis, ppMetrics);
        }
    }

    public ADLX_RESULT GetSupportedGPUMetrics([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetricsSupport** ppMetricsSupported)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSupportedGPUMetrics>((IntPtr)(lpVtbl[21]))(pThis, pGPU, ppMetricsSupported);
        }
    }

    public ADLX_RESULT GetSupportedSystemMetrics(IADLXSystemMetricsSupport** ppMetricsSupported)
    {
        fixed (IADLXPerformanceMonitoringServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSupportedSystemMetrics>((IntPtr)(lpVtbl[22]))(pThis, ppMetricsSupported);
        }
    }
}

public partial struct IADLXPerformanceMonitoringServices
{
}
