using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPowerTuningChangedEvent1 : adlx::IADLXPowerTuningChangedEvent")]
public unsafe partial struct IADLXPowerTuningChangedEvent1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXPowerTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXPowerTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXPowerTuningChangedEvent1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLXPowerTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsSmartShiftMaxChanged(IADLXPowerTuningChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsSmartShiftEcoChanged(IADLXPowerTuningChangedEvent1* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXPowerTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXPowerTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXPowerTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLXPowerTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartShiftMaxChanged()
    {
        fixed (IADLXPowerTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSmartShiftMaxChanged>((IntPtr)(lpVtbl[4]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartShiftEcoChanged()
    {
        fixed (IADLXPowerTuningChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSmartShiftEcoChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }
}
