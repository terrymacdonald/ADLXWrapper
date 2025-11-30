using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplay3DLUTChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnDisplay3DLUTChanged(IADLXDisplay3DLUTChangedListener* pThis, [NativeTypeName("adlx::IADLXDisplay3DLUTChangedEvent *")] IADLXDisplay3DLUTChangedEvent* pDisplay3DLUTChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplay3DLUTChanged([NativeTypeName("adlx::IADLXDisplay3DLUTChangedEvent *")] IADLXDisplay3DLUTChangedEvent* pDisplay3DLUTChangedEvent)
    {
        fixed (IADLXDisplay3DLUTChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnDisplay3DLUTChanged>((IntPtr)(lpVtbl[0]))(pThis, pDisplay3DLUTChangedEvent) != 0;
        }
    }
}
