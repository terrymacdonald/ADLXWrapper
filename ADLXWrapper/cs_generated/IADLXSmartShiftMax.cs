using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSmartShiftMax.xml' path='doc/member[@name="IADLXSmartShiftMax"]/*' />
[NativeTypeName("struct IADLXSmartShiftMax : adlx::IADLXInterface")]
public unsafe partial struct IADLXSmartShiftMax
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int>)(lpVtbl[0]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int>)(lpVtbl[1]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXSmartShiftMax.xml' path='doc/member[@name="IADLXSmartShiftMax.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXSmartShiftMax.xml' path='doc/member[@name="IADLXSmartShiftMax.GetBiasMode"]/*' />
    public ADLX_RESULT GetBiasMode(ADLX_SSM_BIAS_MODE* mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ADLX_SSM_BIAS_MODE*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLXSmartShiftMax.xml' path='doc/member[@name="IADLXSmartShiftMax.SetBiasMode"]/*' />
    public ADLX_RESULT SetBiasMode(ADLX_SSM_BIAS_MODE mode)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ADLX_SSM_BIAS_MODE, ADLX_RESULT>)(lpVtbl[5]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), mode);
    }

    /// <include file='IADLXSmartShiftMax.xml' path='doc/member[@name="IADLXSmartShiftMax.GetBiasRange"]/*' />
    public ADLX_RESULT GetBiasRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), range);
    }

    /// <include file='IADLXSmartShiftMax.xml' path='doc/member[@name="IADLXSmartShiftMax.GetBias"]/*' />
    public ADLX_RESULT GetBias([NativeTypeName("adlx_int *")] int* bias)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), bias);
    }

    /// <include file='IADLXSmartShiftMax.xml' path='doc/member[@name="IADLXSmartShiftMax.SetBias"]/*' />
    public ADLX_RESULT SetBias([NativeTypeName("adlx_int")] int bias)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSmartShiftMax*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLXSmartShiftMax*)Unsafe.AsPointer(ref this), bias);
    }
}
