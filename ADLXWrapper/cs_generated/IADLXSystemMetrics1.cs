using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystemMetrics1 : adlx::IADLXSystemMetrics")]
public unsafe partial struct IADLXSystemMetrics1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXSystemMetrics1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXSystemMetrics1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXSystemMetrics1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _TimeStamp(IADLXSystemMetrics1* pThis, [NativeTypeName("adlx_int64 *")] long* ms);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _CPUUsage(IADLXSystemMetrics1* pThis, [NativeTypeName("adlx_double *")] double* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SystemRAM(IADLXSystemMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SmartShift(IADLXSystemMetrics1* pThis, [NativeTypeName("adlx_int *")] int* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PowerDistribution(IADLXSystemMetrics1* pThis, [NativeTypeName("adlx_int *")] int* apuShiftValue, [NativeTypeName("adlx_int *")] int* gpuShiftValue, [NativeTypeName("adlx_int *")] int* apuShiftLimit, [NativeTypeName("adlx_int *")] int* gpuShiftLimit, [NativeTypeName("adlx_int *")] int* totalShiftLimit);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXSystemMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXSystemMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXSystemMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        fixed (IADLXSystemMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_TimeStamp>((IntPtr)(lpVtbl[3]))(pThis, ms);
        }
    }

    public ADLX_RESULT CPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        fixed (IADLXSystemMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_CPUUsage>((IntPtr)(lpVtbl[4]))(pThis, data);
        }
    }

    public ADLX_RESULT SystemRAM([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXSystemMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SystemRAM>((IntPtr)(lpVtbl[5]))(pThis, data);
        }
    }

    public ADLX_RESULT SmartShift([NativeTypeName("adlx_int *")] int* data)
    {
        fixed (IADLXSystemMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SmartShift>((IntPtr)(lpVtbl[6]))(pThis, data);
        }
    }

    public ADLX_RESULT PowerDistribution([NativeTypeName("adlx_int *")] int* apuShiftValue, [NativeTypeName("adlx_int *")] int* gpuShiftValue, [NativeTypeName("adlx_int *")] int* apuShiftLimit, [NativeTypeName("adlx_int *")] int* gpuShiftLimit, [NativeTypeName("adlx_int *")] int* totalShiftLimit)
    {
        fixed (IADLXSystemMetrics1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PowerDistribution>((IntPtr)(lpVtbl[7]))(pThis, apuShiftValue, gpuShiftValue, apuShiftLimit, gpuShiftLimit, totalShiftLimit);
        }
    }
}
