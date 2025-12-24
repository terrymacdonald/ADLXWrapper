using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXMultimediaChangedEventListener.xml' path='doc/member[@name="IADLXMultimediaChangedEventListener"]/*' />
public unsafe partial struct IADLXMultimediaChangedEventListener
{
    public void** lpVtbl;

    /// <include file='IADLXMultimediaChangedEventListener.xml' path='doc/member[@name="IADLXMultimediaChangedEventListener.OnMultimediaChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnMultimediaChanged([NativeTypeName("adlx::IADLXMultimediaChangedEvent *")] IADLXMultimediaChangedEvent* pMultimediaChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEventListener*, IADLXMultimediaChangedEvent*, byte>)(lpVtbl[0]))((IADLXMultimediaChangedEventListener*)Unsafe.AsPointer(ref this), pMultimediaChangedEvent) != 0;
    }
}
