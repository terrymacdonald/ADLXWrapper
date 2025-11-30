using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualTuningState : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualTuningState
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualTuningState* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualTuningState* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualTuningState* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFrequency(IADLXManualTuningState* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetFrequency(IADLXManualTuningState* pThis, [NativeTypeName("adlx_int")] int value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetVoltage(IADLXManualTuningState* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetVoltage(IADLXManualTuningState* pThis, [NativeTypeName("adlx_int")] int value);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetFrequency([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFrequency>((IntPtr)(lpVtbl[3]))(pThis, value);
        }
    }

    public ADLX_RESULT SetFrequency([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetFrequency>((IntPtr)(lpVtbl[4]))(pThis, value);
        }
    }

    public ADLX_RESULT GetVoltage([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetVoltage>((IntPtr)(lpVtbl[5]))(pThis, value);
        }
    }

    public ADLX_RESULT SetVoltage([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetVoltage>((IntPtr)(lpVtbl[6]))(pThis, value);
        }
    }
}
