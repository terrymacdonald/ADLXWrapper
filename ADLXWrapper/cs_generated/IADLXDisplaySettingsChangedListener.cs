using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplaySettingsChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnDisplaySettingsChanged(IADLXDisplaySettingsChangedListener* pThis, [NativeTypeName("adlx::IADLXDisplaySettingsChangedEvent *")] IADLXDisplaySettingsChangedEvent* pDisplaySettingChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplaySettingsChanged([NativeTypeName("adlx::IADLXDisplaySettingsChangedEvent *")] IADLXDisplaySettingsChangedEvent* pDisplaySettingChangedEvent)
    {
        fixed (IADLXDisplaySettingsChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnDisplaySettingsChanged>((IntPtr)(lpVtbl[0]))(pThis, pDisplaySettingChangedEvent) != 0;
        }
    }
}
