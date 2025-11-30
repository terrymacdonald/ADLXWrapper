using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystemMetricsSupport1 : adlx::IADLXSystemMetricsSupport")]
public unsafe partial struct IADLXSystemMetricsSupport1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXSystemMetricsSupport1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXSystemMetricsSupport1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXSystemMetricsSupport1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedCPUUsage(IADLXSystemMetricsSupport1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedSystemRAM(IADLXSystemMetricsSupport1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedSmartShift(IADLXSystemMetricsSupport1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCPUUsageRange(IADLXSystemMetricsSupport1* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSystemRAMRange(IADLXSystemMetricsSupport1* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSmartShiftRange(IADLXSystemMetricsSupport1* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedPowerDistribution(IADLXSystemMetricsSupport1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedCPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedCPUUsage>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedSystemRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedSystemRAM>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedSmartShift([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedSmartShift>((IntPtr)(lpVtbl[5]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetCPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCPUUsageRange>((IntPtr)(lpVtbl[6]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetSystemRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSystemRAMRange>((IntPtr)(lpVtbl[7]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetSmartShiftRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSmartShiftRange>((IntPtr)(lpVtbl[8]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT IsSupportedPowerDistribution([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSystemMetricsSupport1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedPowerDistribution>((IntPtr)(lpVtbl[9]))(pThis, supported);
        }
    }
}
