using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSystemMetricsSupport : adlx::IADLXInterface")]
public unsafe partial struct IADLXSystemMetricsSupport
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXSystemMetricsSupport* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXSystemMetricsSupport* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXSystemMetricsSupport* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedCPUUsage(IADLXSystemMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedSystemRAM(IADLXSystemMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedSmartShift(IADLXSystemMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCPUUsageRange(IADLXSystemMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSystemRAMRange(IADLXSystemMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSmartShiftRange(IADLXSystemMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedCPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedCPUUsage>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedSystemRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedSystemRAM>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedSmartShift([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedSmartShift>((IntPtr)(lpVtbl[5]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetCPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCPUUsageRange>((IntPtr)(lpVtbl[6]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetSystemRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSystemRAMRange>((IntPtr)(lpVtbl[7]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetSmartShiftRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXSystemMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSmartShiftRange>((IntPtr)(lpVtbl[8]))(pThis, minValue, maxValue);
        }
    }
}
