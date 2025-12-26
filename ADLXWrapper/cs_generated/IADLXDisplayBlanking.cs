using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayBlanking.xml' path='doc/member[@name="IADLXDisplayBlanking"]/*' />
[NativeTypeName("struct IADLXDisplayBlanking : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayBlanking
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, int>)(lpVtbl[0]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, int>)(lpVtbl[1]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayBlanking.xml' path='doc/member[@name="IADLXDisplayBlanking.IsSupported"]/*' />
    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayBlanking.xml' path='doc/member[@name="IADLXDisplayBlanking.IsCurrentBlanked"]/*' />
    public ADLX_RESULT IsCurrentBlanked([NativeTypeName("adlx_bool *")] bool* blanked)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this), blanked);
    }

    /// <include file='IADLXDisplayBlanking.xml' path='doc/member[@name="IADLXDisplayBlanking.IsCurrentUnblanked"]/*' />
    public ADLX_RESULT IsCurrentUnblanked([NativeTypeName("adlx_bool *")] bool* unBlanked)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this), unBlanked);
    }

    /// <include file='IADLXDisplayBlanking.xml' path='doc/member[@name="IADLXDisplayBlanking.SetBlanked"]/*' />
    public ADLX_RESULT SetBlanked()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayBlanking.xml' path='doc/member[@name="IADLXDisplayBlanking.SetUnblanked"]/*' />
    public ADLX_RESULT SetUnblanked()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayBlanking*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayBlanking*)Unsafe.AsPointer(ref this));
    }
}
