using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDesktop.xml' path='doc/member[@name="IADLXDesktop"]/*' />
[NativeTypeName("struct IADLXDesktop : adlx::IADLXInterface")]
public unsafe partial struct IADLXDesktop
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, int>)(lpVtbl[0]))((IADLXDesktop*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, int>)(lpVtbl[1]))((IADLXDesktop*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDesktop*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDesktop.xml' path='doc/member[@name="IADLXDesktop.Orientation"]/*' />
    public ADLX_RESULT Orientation(ADLX_ORIENTATION* orientation)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, ADLX_ORIENTATION*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDesktop*)Unsafe.AsPointer(ref this), orientation);
    }

    /// <include file='IADLXDesktop.xml' path='doc/member[@name="IADLXDesktop.Size"]/*' />
    public ADLX_RESULT Size([NativeTypeName("adlx_int *")] int* width, [NativeTypeName("adlx_int *")] int* height)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, int*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDesktop*)Unsafe.AsPointer(ref this), width, height);
    }

    /// <include file='IADLXDesktop.xml' path='doc/member[@name="IADLXDesktop.TopLeft"]/*' />
    public ADLX_RESULT TopLeft(ADLX_Point* locationTopLeft)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, ADLX_Point*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDesktop*)Unsafe.AsPointer(ref this), locationTopLeft);
    }

    /// <include file='IADLXDesktop.xml' path='doc/member[@name="IADLXDesktop.Type"]/*' />
    public ADLX_RESULT Type(ADLX_DESKTOP_TYPE* desktopType)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, ADLX_DESKTOP_TYPE*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDesktop*)Unsafe.AsPointer(ref this), desktopType);
    }

    /// <include file='IADLXDesktop.xml' path='doc/member[@name="IADLXDesktop.GetNumberOfDisplays"]/*' />
    public ADLX_RESULT GetNumberOfDisplays([NativeTypeName("adlx_uint *")] uint* numDisplays)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, uint*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDesktop*)Unsafe.AsPointer(ref this), numDisplays);
    }

    /// <include file='IADLXDesktop.xml' path='doc/member[@name="IADLXDesktop.GetDisplays"]/*' />
    public ADLX_RESULT GetDisplays(IADLXDisplayList** ppDisplays)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDesktop*, IADLXDisplayList**, ADLX_RESULT>)(lpVtbl[8]))((IADLXDesktop*)Unsafe.AsPointer(ref this), ppDisplays);
    }
}
