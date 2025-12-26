using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLX3DImageSharpening.xml' path='doc/member[@name="IADLX3DImageSharpening"]/*' />
[NativeTypeName("struct IADLX3DImageSharpening : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DImageSharpening
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, int>)(lpVtbl[0]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, int>)(lpVtbl[1]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLX3DImageSharpening.xml' path='doc/member[@name="IADLX3DImageSharpening.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLX3DImageSharpening.xml' path='doc/member[@name="IADLX3DImageSharpening.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLX3DImageSharpening.xml' path='doc/member[@name="IADLX3DImageSharpening.GetSharpnessRange"]/*' />
    public ADLX_RESULT GetSharpnessRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this), range);
    }

    /// <include file='IADLX3DImageSharpening.xml' path='doc/member[@name="IADLX3DImageSharpening.GetSharpness"]/*' />
    public ADLX_RESULT GetSharpness([NativeTypeName("adlx_int *")] int* currentSharpness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this), currentSharpness);
    }

    /// <include file='IADLX3DImageSharpening.xml' path='doc/member[@name="IADLX3DImageSharpening.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, byte, ADLX_RESULT>)(lpVtbl[7]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLX3DImageSharpening.xml' path='doc/member[@name="IADLX3DImageSharpening.SetSharpness"]/*' />
    public ADLX_RESULT SetSharpness([NativeTypeName("adlx_int")] int sharpness)
    {
        return ((delegate* unmanaged[Stdcall]<IADLX3DImageSharpening*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLX3DImageSharpening*)Unsafe.AsPointer(ref this), sharpness);
    }
}
