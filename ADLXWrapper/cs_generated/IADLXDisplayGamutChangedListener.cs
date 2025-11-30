using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplayGamutChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnDisplayGamutChanged(IADLXDisplayGamutChangedListener* pThis, [NativeTypeName("adlx::IADLXDisplayGamutChangedEvent *")] IADLXDisplayGamutChangedEvent* pDisplayGamutChangedEvent);

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplayGamutChanged([NativeTypeName("adlx::IADLXDisplayGamutChangedEvent *")] IADLXDisplayGamutChangedEvent* pDisplayGamutChangedEvent)
    {
        fixed (IADLXDisplayGamutChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnDisplayGamutChanged>((IntPtr)(lpVtbl[0]))(pThis, pDisplayGamutChangedEvent) != 0;
        }
    }
}
