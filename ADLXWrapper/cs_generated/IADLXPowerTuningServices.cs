using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXPowerTuningServices.xml' path='doc/member[@name="IADLXPowerTuningServices"]/*' />
[NativeTypeName("struct IADLXPowerTuningServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXPowerTuningServices
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices*, int>)(lpVtbl[0]))((IADLXPowerTuningServices*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices*, int>)(lpVtbl[1]))((IADLXPowerTuningServices*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningServices*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXPowerTuningServices.xml' path='doc/member[@name="IADLXPowerTuningServices.GetPowerTuningChangedHandling"]/*' />
    public ADLX_RESULT GetPowerTuningChangedHandling(IADLXPowerTuningChangedHandling** ppPowerTuningChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices*, IADLXPowerTuningChangedHandling**, ADLX_RESULT>)(lpVtbl[3]))((IADLXPowerTuningServices*)Unsafe.AsPointer(ref this), ppPowerTuningChangedHandling);
    }

    /// <include file='IADLXPowerTuningServices.xml' path='doc/member[@name="IADLXPowerTuningServices.GetSmartShiftMax"]/*' />
    public ADLX_RESULT GetSmartShiftMax(IADLXSmartShiftMax** ppSmartShiftMax)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices*, IADLXSmartShiftMax**, ADLX_RESULT>)(lpVtbl[4]))((IADLXPowerTuningServices*)Unsafe.AsPointer(ref this), ppSmartShiftMax);
    }
}
