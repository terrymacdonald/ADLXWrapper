using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayHDCP.xml' path='doc/member[@name="IADLXDisplayHDCP"]/*' />
[NativeTypeName("struct IADLXDisplayHDCP : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayHDCP
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, int>)(lpVtbl[0]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, int>)(lpVtbl[1]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayHDCP.xml' path='doc/member[@name="IADLXDisplayHDCP.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayHDCP.xml' path='doc/member[@name="IADLXDisplayHDCP.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayHDCP.xml' path='doc/member[@name="IADLXDisplayHDCP.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayHDCP*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayHDCP*)Unsafe.AsPointer(ref this), enabled);
    }
}
