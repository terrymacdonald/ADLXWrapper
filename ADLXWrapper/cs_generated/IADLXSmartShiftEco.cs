using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSmartShiftEco.xml' path='doc/member[@name="IADLXSmartShiftEco"]/*' />
[NativeTypeName("struct IADLXSmartShiftEco : adlx::IADLXInterface")]
public unsafe partial struct IADLXSmartShiftEco
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, int>)(lpVtbl[0]))((IADLXSmartShiftEco*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, int>)(lpVtbl[1]))((IADLXSmartShiftEco*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSmartShiftEco*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXSmartShiftEco.xml' path='doc/member[@name="IADLXSmartShiftEco.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSmartShiftEco*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXSmartShiftEco.xml' path='doc/member[@name="IADLXSmartShiftEco.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSmartShiftEco*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXSmartShiftEco.xml' path='doc/member[@name="IADLXSmartShiftEco.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXSmartShiftEco*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXSmartShiftEco.xml' path='doc/member[@name="IADLXSmartShiftEco.IsInactive"]/*' />
    public ADLX_RESULT IsInactive([NativeTypeName("adlx_bool *")] bool* inactive)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSmartShiftEco*)Unsafe.AsPointer(ref this), inactive);
    }

    /// <include file='IADLXSmartShiftEco.xml' path='doc/member[@name="IADLXSmartShiftEco.GetInactiveReason"]/*' />
    public ADLX_RESULT GetInactiveReason(ADLX_SMARTSHIFT_ECO_INACTIVE_REASON* reason)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftEco*, ADLX_SMARTSHIFT_ECO_INACTIVE_REASON*, ADLX_RESULT>)(lpVtbl[7]))((IADLXSmartShiftEco*)Unsafe.AsPointer(ref this), reason);
    }
}
