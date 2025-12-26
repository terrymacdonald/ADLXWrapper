using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualPowerTuning.xml' path='doc/member[@name="IADLXManualPowerTuning"]/*' />
[NativeTypeName("struct IADLXManualPowerTuning : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualPowerTuning
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int>)(lpVtbl[0]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int>)(lpVtbl[1]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXManualPowerTuning.xml' path='doc/member[@name="IADLXManualPowerTuning.GetPowerLimitRange"]/*' />
    public ADLX_RESULT GetPowerLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualPowerTuning.xml' path='doc/member[@name="IADLXManualPowerTuning.GetPowerLimit"]/*' />
    public ADLX_RESULT GetPowerLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), curVal);
    }

    /// <include file='IADLXManualPowerTuning.xml' path='doc/member[@name="IADLXManualPowerTuning.SetPowerLimit"]/*' />
    public ADLX_RESULT SetPowerLimit([NativeTypeName("adlx_int")] int curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), curVal);
    }

    /// <include file='IADLXManualPowerTuning.xml' path='doc/member[@name="IADLXManualPowerTuning.IsSupportedTDCLimit"]/*' />
    public ADLX_RESULT IsSupportedTDCLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXManualPowerTuning.xml' path='doc/member[@name="IADLXManualPowerTuning.GetTDCLimitRange"]/*' />
    public ADLX_RESULT GetTDCLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <include file='IADLXManualPowerTuning.xml' path='doc/member[@name="IADLXManualPowerTuning.GetTDCLimit"]/*' />
    public ADLX_RESULT GetTDCLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), curVal);
    }

    /// <include file='IADLXManualPowerTuning.xml' path='doc/member[@name="IADLXManualPowerTuning.SetTDCLimit"]/*' />
    public ADLX_RESULT SetTDCLimit([NativeTypeName("adlx_int")] int curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning*, int, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualPowerTuning*)Unsafe.AsPointer(ref this), curVal);
    }
}
