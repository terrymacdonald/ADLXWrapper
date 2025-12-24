using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUTuningChangedListener.xml' path='doc/member[@name="IADLXGPUTuningChangedListener"]/*' />
public unsafe partial struct IADLXGPUTuningChangedListener
{
    public void** lpVtbl;

    /// <include file='IADLXGPUTuningChangedListener.xml' path='doc/member[@name="IADLXGPUTuningChangedListener.OnGPUTuningChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUTuningChanged([NativeTypeName("adlx::IADLXGPUTuningChangedEvent *")] IADLXGPUTuningChangedEvent* pGPUTuningChangedEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedListener*, IADLXGPUTuningChangedEvent*, byte>)(lpVtbl[0]))((IADLXGPUTuningChangedListener*)Unsafe.AsPointer(ref this), pGPUTuningChangedEvent) != 0;
    }
}
