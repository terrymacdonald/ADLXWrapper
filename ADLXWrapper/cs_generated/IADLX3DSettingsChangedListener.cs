using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DSettingsChangedListener.xml' path='doc/member[@name="IADLX3DSettingsChangedListener"]/*' />
public unsafe partial struct IADLX3DSettingsChangedListener
{
    public void** lpVtbl;

    /// <include file='IADLX3DSettingsChangedListener.xml' path='doc/member[@name="IADLX3DSettingsChangedListener.On3DSettingsChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool On3DSettingsChanged([NativeTypeName("adlx::IADLX3DSettingsChangedEvent *")] IADLX3DSettingsChangedEvent* p3DSettingsChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DSettingsChangedListener*, IADLX3DSettingsChangedEvent*, byte>)(lpVtbl[0]))((IADLX3DSettingsChangedListener*)Unsafe.AsPointer(ref this), p3DSettingsChangedEvent) != 0;
    }
}
