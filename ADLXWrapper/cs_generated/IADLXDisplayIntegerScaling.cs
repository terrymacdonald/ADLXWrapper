using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayIntegerScaling.xml' path='doc/member[@name="IADLXDisplayIntegerScaling"]/*' />
[NativeTypeName("struct IADLXDisplayIntegerScaling : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayIntegerScaling
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, int>)(lpVtbl[0]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, int>)(lpVtbl[1]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayIntegerScaling.xml' path='doc/member[@name="IADLXDisplayIntegerScaling.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayIntegerScaling.xml' path='doc/member[@name="IADLXDisplayIntegerScaling.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayIntegerScaling.xml' path='doc/member[@name="IADLXDisplayIntegerScaling.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayIntegerScaling*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayIntegerScaling*)Unsafe.AsPointer(ref this), enabled);
    }
}
