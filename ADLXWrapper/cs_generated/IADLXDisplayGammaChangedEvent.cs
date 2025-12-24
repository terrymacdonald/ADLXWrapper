using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayGammaChangedEvent.xml' path='doc/member[@name="IADLXDisplayGammaChangedEvent"]/*' />
[NativeTypeName("struct IADLXDisplayGammaChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXDisplayGammaChangedEvent
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, int>)(lpVtbl[0]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, int>)(lpVtbl[1]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayGammaChangedEvent.xml' path='doc/member[@name="IADLXDisplayGammaChangedEvent.GetDisplay"]/*' />
    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    /// <include file='IADLXDisplayGammaChangedEvent.xml' path='doc/member[@name="IADLXDisplayGammaChangedEvent.IsGammaRampChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsGammaRampChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, byte>)(lpVtbl[5]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplayGammaChangedEvent.xml' path='doc/member[@name="IADLXDisplayGammaChangedEvent.IsGammaCoefficientChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsGammaCoefficientChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, byte>)(lpVtbl[6]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplayGammaChangedEvent.xml' path='doc/member[@name="IADLXDisplayGammaChangedEvent.IsReGammaChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsReGammaChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, byte>)(lpVtbl[7]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplayGammaChangedEvent.xml' path='doc/member[@name="IADLXDisplayGammaChangedEvent.IsDeGammaChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsDeGammaChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGammaChangedEvent*, byte>)(lpVtbl[8]))((IADLXDisplayGammaChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
