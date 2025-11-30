using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXPowerTuningChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnPowerTuningChanged(IADLXPowerTuningChangedListener* pThis, [NativeTypeName("adlx::IADLXPowerTuningChangedEvent *")] IADLXPowerTuningChangedEvent* pPowerTuningChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnPowerTuningChanged([NativeTypeName("adlx::IADLXPowerTuningChangedEvent *")] IADLXPowerTuningChangedEvent* pPowerTuningChangedEvent)
    {
        fixed (IADLXPowerTuningChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnPowerTuningChanged>((IntPtr)(lpVtbl[0]))(pThis, pPowerTuningChangedEvent) != 0;
        }
    }
}
