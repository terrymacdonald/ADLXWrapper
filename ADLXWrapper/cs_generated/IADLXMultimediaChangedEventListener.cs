using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXMultimediaChangedEventListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnMultimediaChanged(IADLXMultimediaChangedEventListener* pThis, [NativeTypeName("adlx::IADLXMultimediaChangedEvent *")] IADLXMultimediaChangedEvent* pMultimediaChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnMultimediaChanged([NativeTypeName("adlx::IADLXMultimediaChangedEvent *")] IADLXMultimediaChangedEvent* pMultimediaChangedEvent)
    {
        fixed (IADLXMultimediaChangedEventListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnMultimediaChanged>((IntPtr)(lpVtbl[0]))(pThis, pMultimediaChangedEvent) != 0;
        }
    }
}
