using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplaySettingsChangedListener.xml' path='doc/member[@name="IADLXDisplaySettingsChangedListener"]/*' />
public unsafe partial struct IADLXDisplaySettingsChangedListener
{
    public void** lpVtbl;

    /// <include file='IADLXDisplaySettingsChangedListener.xml' path='doc/member[@name="IADLXDisplaySettingsChangedListener.OnDisplaySettingsChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplaySettingsChanged([NativeTypeName("adlx::IADLXDisplaySettingsChangedEvent *")] IADLXDisplaySettingsChangedEvent* pDisplaySettingChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplaySettingsChangedListener*, IADLXDisplaySettingsChangedEvent*, byte>)(lpVtbl[0]))((IADLXDisplaySettingsChangedListener*)Unsafe.AsPointer(ref this), pDisplaySettingChangedEvent) != 0;
    }
}
