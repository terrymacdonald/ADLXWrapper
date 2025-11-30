using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplayGammaChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnDisplayGammaChanged(IADLXDisplayGammaChangedListener* pThis, [NativeTypeName("adlx::IADLXDisplayGammaChangedEvent *")] IADLXDisplayGammaChangedEvent* pDisplayGammaChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplayGammaChanged([NativeTypeName("adlx::IADLXDisplayGammaChangedEvent *")] IADLXDisplayGammaChangedEvent* pDisplayGammaChangedEvent)
    {
        fixed (IADLXDisplayGammaChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnDisplayGammaChanged>((IntPtr)(lpVtbl[0]))(pThis, pDisplayGammaChangedEvent) != 0;
        }
    }
}
