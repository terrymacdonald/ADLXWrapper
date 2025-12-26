using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat"]/*' />
[NativeTypeName("struct IADLXDisplayPixelFormat : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayPixelFormat
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, int>)(lpVtbl[0]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, int>)(lpVtbl[1]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.GetValue"]/*' />
    public ADLX_RESULT GetValue(ADLX_PIXEL_FORMAT* pixelFormat)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, ADLX_PIXEL_FORMAT*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), pixelFormat);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.SetValue"]/*' />
    public ADLX_RESULT SetValue(ADLX_PIXEL_FORMAT pixelFormat)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, ADLX_PIXEL_FORMAT, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), pixelFormat);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.IsSupportedPixelFormat"]/*' />
    public ADLX_RESULT IsSupportedPixelFormat(ADLX_PIXEL_FORMAT pixelFormat, [NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, ADLX_PIXEL_FORMAT, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), pixelFormat, supportd);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.IsSupportedRGB444Full"]/*' />
    public ADLX_RESULT IsSupportedRGB444Full([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.IsSupportedYCbCr444"]/*' />
    public ADLX_RESULT IsSupportedYCbCr444([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.IsSupportedYCbCr422"]/*' />
    public ADLX_RESULT IsSupportedYCbCr422([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.IsSupportedRGB444Limited"]/*' />
    public ADLX_RESULT IsSupportedRGB444Limited([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }

    /// <include file='IADLXDisplayPixelFormat.xml' path='doc/member[@name="IADLXDisplayPixelFormat.IsSupportedYCbCr420"]/*' />
    public ADLX_RESULT IsSupportedYCbCr420([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayPixelFormat*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayPixelFormat*)Unsafe.AsPointer(ref this), supportd);
    }
}
