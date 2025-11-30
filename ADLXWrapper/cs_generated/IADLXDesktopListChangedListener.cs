using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public unsafe partial struct IADLXDesktopListChangedListener
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _OnDesktopListChanged(IADLXDesktopListChangedListener* pThis, [NativeTypeName("adlx::IADLXDesktopList *")] IADLXDesktopList* pNewDesktop);

    [return: NativeTypeName("adlx_bool")]
    public bool OnDesktopListChanged([NativeTypeName("adlx::IADLXDesktopList *")] IADLXDesktopList* pNewDesktop)
    {
        fixed (IADLXDesktopListChangedListener* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_OnDesktopListChanged>((IntPtr)(lpVtbl[0]))(pThis, pNewDesktop) != 0;
        }
    }
}
