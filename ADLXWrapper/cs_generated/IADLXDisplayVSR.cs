using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayVSR.xml' path='doc/member[@name="IADLXDisplayVSR"]/*' />
[NativeTypeName("struct IADLXDisplayVSR : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayVSR
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, int>)(lpVtbl[0]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, int>)(lpVtbl[1]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayVSR.xml' path='doc/member[@name="IADLXDisplayVSR.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayVSR.xml' path='doc/member[@name="IADLXDisplayVSR.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayVSR.xml' path='doc/member[@name="IADLXDisplayVSR.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayVSR*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayVSR*)Unsafe.AsPointer(ref this), enabled);
    }
}
