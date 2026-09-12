using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUStressTestFinishedListener.xml' path='doc/member[@name="IADLXGPUStressTestFinishedListener"]/*' />
public unsafe partial struct IADLXGPUStressTestFinishedListener
{
    public void** lpVtbl;

    /// <include file='IADLXGPUStressTestFinishedListener.xml' path='doc/member[@name="IADLXGPUStressTestFinishedListener.OnGPUStressTestFinished"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUStressTestFinished([NativeTypeName("adlx::IADLXGPU3 *")] IADLXGPU3* pGPU, [NativeTypeName("adlx_bool")] byte result)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUStressTestFinishedListener*, IADLXGPU3*, byte, byte>)(lpVtbl[0]))((IADLXGPUStressTestFinishedListener*)Unsafe.AsPointer(ref this), pGPU, result) != 0;
    }
}
