using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth"]/*' />
[NativeTypeName("struct IADLXDisplayColorDepth : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayColorDepth
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, int>)(lpVtbl[0]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, int>)(lpVtbl[1]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.GetValue"]/*' />
    public ADLX_RESULT GetValue(ADLX_COLOR_DEPTH* currentColorDepth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, ADLX_COLOR_DEPTH*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), currentColorDepth);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.SetValue"]/*' />
    public ADLX_RESULT SetValue(ADLX_COLOR_DEPTH colorDepth)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, ADLX_COLOR_DEPTH, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), colorDepth);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.IsSupportedColorDepth"]/*' />
    public ADLX_RESULT IsSupportedColorDepth(ADLX_COLOR_DEPTH colorDepth, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, ADLX_COLOR_DEPTH, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), colorDepth, supported);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.IsSupportedBPC_6"]/*' />
    public ADLX_RESULT IsSupportedBPC_6([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.IsSupportedBPC_8"]/*' />
    public ADLX_RESULT IsSupportedBPC_8([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.IsSupportedBPC_10"]/*' />
    public ADLX_RESULT IsSupportedBPC_10([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.IsSupportedBPC_12"]/*' />
    public ADLX_RESULT IsSupportedBPC_12([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.IsSupportedBPC_14"]/*' />
    public ADLX_RESULT IsSupportedBPC_14([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayColorDepth.xml' path='doc/member[@name="IADLXDisplayColorDepth.IsSupportedBPC_16"]/*' />
    public ADLX_RESULT IsSupportedBPC_16([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayColorDepth*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayColorDepth*)Unsafe.AsPointer(ref this), supported);
    }
}
