using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXVideoSuperResolution.xml' path='doc/member[@name="IADLXVideoSuperResolution"]/*' />
[NativeTypeName("struct IADLXVideoSuperResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLXVideoSuperResolution
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, int>)(lpVtbl[0]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, int>)(lpVtbl[1]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXVideoSuperResolution.xml' path='doc/member[@name="IADLXVideoSuperResolution.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXVideoSuperResolution.xml' path='doc/member[@name="IADLXVideoSuperResolution.IsEnabled"]/*' />
    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this), isEnabled);
    }

    /// <include file='IADLXVideoSuperResolution.xml' path='doc/member[@name="IADLXVideoSuperResolution.SetEnabled"]/*' />
    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXVideoSuperResolution*, byte, ADLX_RESULT>)(lpVtbl[5]))((IADLXVideoSuperResolution*)Unsafe.AsPointer(ref this), enable);
    }
}
