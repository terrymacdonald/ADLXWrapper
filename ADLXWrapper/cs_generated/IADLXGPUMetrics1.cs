using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUMetrics1 : adlx::IADLXGPUMetrics")]
public unsafe partial struct IADLXGPUMetrics1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUMetrics1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUMetrics1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUMetrics1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _TimeStamp(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_int64 *")] long* ms);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUUsage(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUClockSpeed(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUVRAMClockSpeed(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUTemperature(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUHotspotTemperature(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUPower(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUTotalBoardPower(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUFanSpeed(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUVRAM(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUVoltage(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUIntakeTemperature(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUMemoryTemperature(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _NPUFrequency(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _NPUActivityLevel(IADLXGPUMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_TimeStamp>((IntPtr)(lpVtbl[3]))(pThis, ms);
        }
    }

    public ADLX_RESULT GPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUUsage>((IntPtr)(lpVtbl[4]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUClockSpeed>((IntPtr)(lpVtbl[5]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUVRAMClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUVRAMClockSpeed>((IntPtr)(lpVtbl[6]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUTemperature>((IntPtr)(lpVtbl[7]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUHotspotTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUHotspotTemperature>((IntPtr)(lpVtbl[8]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUPower([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUPower>((IntPtr)(lpVtbl[9]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUTotalBoardPower([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUTotalBoardPower>((IntPtr)(lpVtbl[10]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUFanSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUFanSpeed>((IntPtr)(lpVtbl[11]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUVRAM([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUVRAM>((IntPtr)(lpVtbl[12]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUVoltage([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUVoltage>((IntPtr)(lpVtbl[13]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUIntakeTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUIntakeTemperature>((IntPtr)(lpVtbl[14]))(pThis, data);
        }
    }

    public ADLX_RESULT GPUMemoryTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUMemoryTemperature>((IntPtr)(lpVtbl[15]))(pThis, data);
        }
    }

    public ADLX_RESULT NPUFrequency([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_NPUFrequency>((IntPtr)(lpVtbl[16]))(pThis, data);
        }
    }

    public ADLX_RESULT NPUActivityLevel([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXGPUMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_NPUActivityLevel>((IntPtr)(lpVtbl[17]))(pThis, data);
        }
    }
}
