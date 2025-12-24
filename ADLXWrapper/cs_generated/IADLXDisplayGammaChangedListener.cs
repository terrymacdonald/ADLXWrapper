using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayGammaChangedListener.xml' path='doc/member[@name="IADLXDisplayGammaChangedListener"]/*' />
public unsafe partial struct IADLXDisplayGammaChangedListener
{
    public void** lpVtbl;

    /// <include file='IADLXDisplayGammaChangedListener.xml' path='doc/member[@name="IADLXDisplayGammaChangedListener.OnDisplayGammaChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplayGammaChanged([NativeTypeName("adlx::IADLXDisplayGammaChangedEvent *")] IADLXDisplayGammaChangedEvent* pDisplayGammaChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedListener*, IADLXDisplayGammaChangedEvent*, byte>)(lpVtbl[0]))((IADLXDisplayGammaChangedListener*)Unsafe.AsPointer(ref this), pDisplayGammaChangedEvent) != 0;
    }
}
