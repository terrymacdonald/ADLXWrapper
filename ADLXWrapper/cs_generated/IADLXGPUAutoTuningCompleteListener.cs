using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUAutoTuningCompleteListener.xml' path='doc/member[@name="IADLXGPUAutoTuningCompleteListener"]/*' />
public unsafe partial struct IADLXGPUAutoTuningCompleteListener
{
    public void** lpVtbl;

    /// <include file='IADLXGPUAutoTuningCompleteListener.xml' path='doc/member[@name="IADLXGPUAutoTuningCompleteListener.OnGPUAutoTuningComplete"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUAutoTuningComplete([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteEvent *")] IADLXGPUAutoTuningCompleteEvent* pGPUAutoTuningCompleteEvent)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteListener*, IADLXGPUAutoTuningCompleteEvent*, byte>)(lpVtbl[0]))((IADLXGPUAutoTuningCompleteListener*)Unsafe.AsPointer(ref this), pGPUAutoTuningCompleteEvent) != 0;
    }
}
