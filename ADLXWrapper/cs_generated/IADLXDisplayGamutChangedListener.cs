using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayGamutChangedListener.xml' path='doc/member[@name="IADLXDisplayGamutChangedListener"]/*' />
public unsafe partial struct IADLXDisplayGamutChangedListener
{
    public void** lpVtbl;

    /// <include file='IADLXDisplayGamutChangedListener.xml' path='doc/member[@name="IADLXDisplayGamutChangedListener.OnDisplayGamutChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplayGamutChanged([NativeTypeName("adlx::IADLXDisplayGamutChangedEvent *")] IADLXDisplayGamutChangedEvent* pDisplayGamutChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedListener*, IADLXDisplayGamutChangedEvent*, byte>)(lpVtbl[0]))((IADLXDisplayGamutChangedListener*)Unsafe.AsPointer(ref this), pDisplayGamutChangedEvent) != 0;
    }
}
