using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut"]/*' />
[NativeTypeName("struct IADLXDisplayGamut : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayGamut
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, int>)(lpVtbl[0]))((IADLXDisplayGamut*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, int>)(lpVtbl[1]))((IADLXDisplayGamut*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayGamut*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupportedCCIR709ColorSpace"]/*' />
    public readonly ADLX_RESULT IsSupportedCCIR709ColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupportedCCIR601ColorSpace"]/*' />
    public readonly ADLX_RESULT IsSupportedCCIR601ColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupportedAdobeRgbColorSpace"]/*' />
    public readonly ADLX_RESULT IsSupportedAdobeRgbColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupportedCIERgbColorSpace"]/*' />
    public readonly ADLX_RESULT IsSupportedCIERgbColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupportedCCIR2020ColorSpace"]/*' />
    public readonly ADLX_RESULT IsSupportedCCIR2020ColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupportedCustomColorSpace"]/*' />
    public readonly ADLX_RESULT IsSupportedCustomColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupported5000kWhitePoint"]/*' />
    public readonly ADLX_RESULT IsSupported5000kWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupported6500kWhitePoint"]/*' />
    public readonly ADLX_RESULT IsSupported6500kWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupported7500kWhitePoint"]/*' />
    public readonly ADLX_RESULT IsSupported7500kWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupported9300kWhitePoint"]/*' />
    public readonly ADLX_RESULT IsSupported9300kWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsSupportedCustomWhitePoint"]/*' />
    public readonly ADLX_RESULT IsSupportedCustomWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), supported);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrent5000kWhitePoint"]/*' />
    public readonly ADLX_RESULT IsCurrent5000kWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrent6500kWhitePoint"]/*' />
    public readonly ADLX_RESULT IsCurrent6500kWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[15]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrent7500kWhitePoint"]/*' />
    public readonly ADLX_RESULT IsCurrent7500kWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[16]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrent9300kWhitePoint"]/*' />
    public readonly ADLX_RESULT IsCurrent9300kWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[17]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrentCustomWhitePoint"]/*' />
    public readonly ADLX_RESULT IsCurrentCustomWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[18]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.GetWhitePoint"]/*' />
    public readonly ADLX_RESULT GetWhitePoint(ADLX_Point* point)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, ADLX_Point*, ADLX_RESULT>)(lpVtbl[19]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), point);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrentCCIR709ColorSpace"]/*' />
    public readonly ADLX_RESULT IsCurrentCCIR709ColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[20]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrentCCIR601ColorSpace"]/*' />
    public readonly ADLX_RESULT IsCurrentCCIR601ColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[21]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrentAdobeRgbColorSpace"]/*' />
    public readonly ADLX_RESULT IsCurrentAdobeRgbColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[22]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrentCIERgbColorSpace"]/*' />
    public readonly ADLX_RESULT IsCurrentCIERgbColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[23]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrentCCIR2020ColorSpace"]/*' />
    public readonly ADLX_RESULT IsCurrentCCIR2020ColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[24]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.IsCurrentCustomColorSpace"]/*' />
    public readonly ADLX_RESULT IsCurrentCustomColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, bool*, ADLX_RESULT>)(lpVtbl[25]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), isSet);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.GetGamutColorSpace"]/*' />
    public readonly ADLX_RESULT GetGamutColorSpace(ADLX_GamutColorSpace* gamutColorSpace)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, ADLX_GamutColorSpace*, ADLX_RESULT>)(lpVtbl[26]))((IADLXDisplayGamut*)Unsafe.AsPointer(in this), gamutColorSpace);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.SetGamut"]/*' />
    public ADLX_RESULT SetGamut(ADLX_RGB customWhitePoint, ADLX_GamutColorSpace customGamut)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, ADLX_RGB, ADLX_GamutColorSpace, ADLX_RESULT>)(lpVtbl[27]))((IADLXDisplayGamut*)Unsafe.AsPointer(ref this), customWhitePoint, customGamut);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.SetGamut"]/*' />
    public ADLX_RESULT SetGamut(ADLX_WHITE_POINT predefinedWhitePoint, ADLX_GamutColorSpace customGamut)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, ADLX_WHITE_POINT, ADLX_GamutColorSpace, ADLX_RESULT>)(lpVtbl[28]))((IADLXDisplayGamut*)Unsafe.AsPointer(ref this), predefinedWhitePoint, customGamut);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.SetGamut"]/*' />
    public ADLX_RESULT SetGamut(ADLX_RGB customWhitePoint, ADLX_GAMUT_SPACE predefinedGamutSpace)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, ADLX_RGB, ADLX_GAMUT_SPACE, ADLX_RESULT>)(lpVtbl[29]))((IADLXDisplayGamut*)Unsafe.AsPointer(ref this), customWhitePoint, predefinedGamutSpace);
    }

    /// <include file='IADLXDisplayGamut.xml' path='doc/member[@name="IADLXDisplayGamut.SetGamut"]/*' />
    public ADLX_RESULT SetGamut(ADLX_WHITE_POINT predefinedWhitePoint, ADLX_GAMUT_SPACE predefinedGamutSpace)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamut*, ADLX_WHITE_POINT, ADLX_GAMUT_SPACE, ADLX_RESULT>)(lpVtbl[30]))((IADLXDisplayGamut*)Unsafe.AsPointer(ref this), predefinedWhitePoint, predefinedGamutSpace);
    }
}
