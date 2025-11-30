using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXGPUTuningChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnGPUTuningChanged(IADLXGPUTuningChangedListener* pThis, [NativeTypeName("adlx::IADLXGPUTuningChangedEvent *")] IADLXGPUTuningChangedEvent* pGPUTuningChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnGPUTuningChanged([NativeTypeName("adlx::IADLXGPUTuningChangedEvent *")] IADLXGPUTuningChangedEvent* pGPUTuningChangedEvent)
    {
        fixed (IADLXGPUTuningChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnGPUTuningChanged>((IntPtr)(lpVtbl[0]))(pThis, pGPUTuningChangedEvent) != 0;
        }
    }
}
