using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXPowerTuningChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXPowerTuningChangedHandling
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXPowerTuningChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXPowerTuningChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXPowerTuningChangedHandling* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddPowerTuningEventListener(IADLXPowerTuningChangedHandling* pThis, [NativeTypeName("adlx::IADLXPowerTuningChangedListener *")] IADLXPowerTuningChangedListener* pPowerTuningChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemovePowerTuningEventListener(IADLXPowerTuningChangedHandling* pThis, [NativeTypeName("adlx::IADLXPowerTuningChangedListener *")] IADLXPowerTuningChangedListener* pPowerTuningChangedListener);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXPowerTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXPowerTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXPowerTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT AddPowerTuningEventListener([NativeTypeName("adlx::IADLXPowerTuningChangedListener *")] IADLXPowerTuningChangedListener* pPowerTuningChangedListener)
    {
        fixed (IADLXPowerTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddPowerTuningEventListener>((IntPtr)(lpVtbl[3]))(pThis, pPowerTuningChangedListener);
        }
    }

    public ADLX_RESULT RemovePowerTuningEventListener([NativeTypeName("adlx::IADLXPowerTuningChangedListener *")] IADLXPowerTuningChangedListener* pPowerTuningChangedListener)
    {
        fixed (IADLXPowerTuningChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemovePowerTuningEventListener>((IntPtr)(lpVtbl[4]))(pThis, pPowerTuningChangedListener);
        }
    }
}
