using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXVideoUpscale.xml' path='doc/member[@name="IADLXVideoUpscale"]/*' />
[NativeTypeName("struct IADLXVideoUpscale : adlx::IADLXInterface")]
public unsafe partial struct IADLXVideoUpscale
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int>)(lpVtbl[0]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int>)(lpVtbl[1]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXVideoUpscale.xml' path='doc/member[@name="IADLXVideoUpscale.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXVideoUpscale.xml' path='doc/member[@name="IADLXVideoUpscale.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLXVideoUpscale.xml' path='doc/member[@name="IADLXVideoUpscale.GetSharpnessRange"]/*' />
    public ADLX_RESULT GetSharpnessRange(ADLX_IntRange* range)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, ADLX_IntRange*, ADLX_RESULT>)(lpVtbl[5]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), range);
    }

    /// <include file='IADLXVideoUpscale.xml' path='doc/member[@name="IADLXVideoUpscale.GetSharpness"]/*' />
    public ADLX_RESULT GetSharpness([NativeTypeName("adlx_int *")] int* currentMinRes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), currentMinRes);
    }

    /// <include file='IADLXVideoUpscale.xml' path='doc/member[@name="IADLXVideoUpscale.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, byte, ADLX_RESULT>)(lpVtbl[7]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), enable);
    }

    /// <include file='IADLXVideoUpscale.xml' path='doc/member[@name="IADLXVideoUpscale.SetSharpness"]/*' />
    public ADLX_RESULT SetSharpness([NativeTypeName("adlx_int")] int minSharp)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoUpscale*, int, ADLX_RESULT>)(lpVtbl[8]))((IADLXVideoUpscale*)Unsafe.AsPointer(ref this), minSharp);
    }
}
