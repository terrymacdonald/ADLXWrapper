using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualPowerTuning1.xml' path='doc/member[@name="IADLXManualPowerTuning1"]/*' />
[NativeTypeName("struct IADLXManualPowerTuning1 : adlx::IADLXManualPowerTuning")]
public unsafe partial struct IADLXManualPowerTuning1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int>)(lpVtbl[0]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int>)(lpVtbl[1]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXManualPowerTuning.GetPowerLimitRange" />
    public ADLX_RESULT GetPowerLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <inheritdoc cref="IADLXManualPowerTuning.GetPowerLimit" />
    public ADLX_RESULT GetPowerLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int*, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), curVal);
    }

    /// <inheritdoc cref="IADLXManualPowerTuning.SetPowerLimit" />
    public ADLX_RESULT SetPowerLimit([NativeTypeName("adlx_int")] int curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), curVal);
    }

    /// <inheritdoc cref="IADLXManualPowerTuning.IsSupportedTDCLimit" />
    public ADLX_RESULT IsSupportedTDCLimit([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXManualPowerTuning.GetTDCLimitRange" />
    public ADLX_RESULT GetTDCLimitRange(ADLX_IntRange* tuningRange)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[7]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), tuningRange);
    }

    /// <inheritdoc cref="IADLXManualPowerTuning.GetTDCLimit" />
    public ADLX_RESULT GetTDCLimit([NativeTypeName("adlx_int *")] int* curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), curVal);
    }

    /// <inheritdoc cref="IADLXManualPowerTuning.SetTDCLimit" />
    public ADLX_RESULT SetTDCLimit([NativeTypeName("adlx_int")] int curVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int, ADLX_RESULT>)(lpVtbl[9]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), curVal);
    }

    /// <include file='IADLXManualPowerTuning1.xml' path='doc/member[@name="IADLXManualPowerTuning1.GetPowerLimitDefault"]/*' />
    public ADLX_RESULT GetPowerLimitDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), defaultVal);
    }

    /// <include file='IADLXManualPowerTuning1.xml' path='doc/member[@name="IADLXManualPowerTuning1.GetTDCLimitDefault"]/*' />
    public ADLX_RESULT GetTDCLimitDefault([NativeTypeName("adlx_int *")] int* defaultVal)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualPowerTuning1*, int*, ADLX_RESULT>)(lpVtbl[11]))((IADLXManualPowerTuning1*)Unsafe.AsPointer(ref this), defaultVal);
    }
}
