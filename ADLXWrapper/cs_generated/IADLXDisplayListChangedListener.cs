using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDisplayListChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnDisplayListChanged(IADLXDisplayListChangedListener* pThis, [NativeTypeName("adlx::IADLXDisplayList *")] IADLXDisplayList* pNewDisplay);

    [return: NativeTypeName("adlx_bool")]
    public bool OnDisplayListChanged([NativeTypeName("adlx::IADLXDisplayList *")] IADLXDisplayList* pNewDisplay)
    {
        fixed (IADLXDisplayListChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnDisplayListChanged>((IntPtr)(lpVtbl[0]))(pThis, pNewDisplay) != 0;
        }
    }
}
