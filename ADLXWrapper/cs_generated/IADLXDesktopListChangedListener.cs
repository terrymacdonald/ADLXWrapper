using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDesktopListChangedListener.xml' path='doc/member[@name="IADLXDesktopListChangedListener"]/*' />
public unsafe partial struct IADLXDesktopListChangedListener
{
    public void** lpVtbl;

    /// <include file='IADLXDesktopListChangedListener.xml' path='doc/member[@name="IADLXDesktopListChangedListener.OnDesktopListChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool OnDesktopListChanged([NativeTypeName("adlx::IADLXDesktopList *")] IADLXDesktopList* pNewDesktop)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktopListChangedListener*, IADLXDesktopList*, byte>)(lpVtbl[0]))((IADLXDesktopListChangedListener*)Unsafe.AsPointer(ref this), pNewDesktop) != 0;
    }
}
