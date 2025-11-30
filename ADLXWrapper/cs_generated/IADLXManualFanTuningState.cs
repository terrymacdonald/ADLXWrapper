using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualFanTuningState : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualFanTuningState
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualFanTuningState* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualFanTuningState* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualFanTuningState* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFanSpeed(IADLXManualFanTuningState* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetFanSpeed(IADLXManualFanTuningState* pThis, [NativeTypeName("adlx_int")] int value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTemperature(IADLXManualFanTuningState* pThis, [NativeTypeName("adlx_int *")] int* value);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetTemperature(IADLXManualFanTuningState* pThis, [NativeTypeName("adlx_int")] int value);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualFanTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualFanTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualFanTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualFanTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFanSpeed>((IntPtr)(lpVtbl[3]))(pThis, value);
        }
    }

    public ADLX_RESULT SetFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualFanTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetFanSpeed>((IntPtr)(lpVtbl[4]))(pThis, value);
        }
    }

    public ADLX_RESULT GetTemperature([NativeTypeName("adlx_int *")] int* value)
    {
        fixed (IADLXManualFanTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTemperature>((IntPtr)(lpVtbl[5]))(pThis, value);
        }
    }

    public ADLX_RESULT SetTemperature([NativeTypeName("adlx_int")] int value)
    {
        fixed (IADLXManualFanTuningState* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetTemperature>((IntPtr)(lpVtbl[6]))(pThis, value);
        }
    }
}
