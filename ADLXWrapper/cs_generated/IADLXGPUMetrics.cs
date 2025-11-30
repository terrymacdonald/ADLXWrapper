using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUMetrics : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUMetrics
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUMetrics* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUMetrics* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUMetrics* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _TimeStamp(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_int64 *")] long* ms);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUUsage(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUClockSpeed(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUVRAMClockSpeed(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUTemperature(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUHotspotTemperature(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUPower(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUTotalBoardPower(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUFanSpeed(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUVRAM(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUVoltage(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUIntakeTemperature(IADLXGPUMetrics* pThis, [NativeTypeName("adlx_double *")] double* data);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_TimeStamp>((IntPtr)(lpVtbl[3]))(pThis, ms);
        }
    }

    public ADLX_RESULT GPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUUsage>((IntPtr)(lpVtbl[4]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUClockSpeed>((IntPtr)(lpVtbl[5]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUVRAMClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUVRAMClockSpeed>((IntPtr)(lpVtbl[6]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUTemperature>((IntPtr)(lpVtbl[7]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUHotspotTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUHotspotTemperature>((IntPtr)(lpVtbl[8]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUPower([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUPower>((IntPtr)(lpVtbl[9]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUTotalBoardPower([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUTotalBoardPower>((IntPtr)(lpVtbl[10]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUFanSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUFanSpeed>((IntPtr)(lpVtbl[11]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUVRAM([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUVRAM>((IntPtr)(lpVtbl[12]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUVoltage([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUVoltage>((IntPtr)(lpVtbl[13]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUIntakeTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUIntakeTemperature>((IntPtr)(lpVtbl[14]))(pThis, data);
        }
    }
}
