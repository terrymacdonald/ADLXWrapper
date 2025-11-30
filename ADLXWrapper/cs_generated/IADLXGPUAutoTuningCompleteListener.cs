using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXGPUAutoTuningCompleteListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnGPUAutoTuningComplete(IADLXGPUAutoTuningCompleteListener* pThis, [NativeTypeName("adlx::IADLXGPUAutoTuningCompleteEvent *")] IADLXGPUAutoTuningCompleteEvent* pGPUAutoTuningCompleteEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUAutoTuningComplete([NativeTypeName("adlx::IADLXGPUAutoTuningCompleteEvent *")] IADLXGPUAutoTuningCompleteEvent* pGPUAutoTuningCompleteEvent)
    {
        fixed (IADLXGPUAutoTuningCompleteListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnGPUAutoTuningComplete>((IntPtr)(lpVtbl[0]))(pThis, pGPUAutoTuningCompleteEvent) != 0;
        }
    }
}
