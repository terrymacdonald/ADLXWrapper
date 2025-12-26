using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayGamutChangedEvent.xml' path='doc/member[@name="IADLXDisplayGamutChangedEvent"]/*' />
[NativeTypeName("struct IADLXDisplayGamutChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXDisplayGamutChangedEvent
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, int>)(lpVtbl[0]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, int>)(lpVtbl[1]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayGamutChangedEvent.xml' path='doc/member[@name="IADLXDisplayGamutChangedEvent.GetDisplay"]/*' />
    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, IADLXDisplay**, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this), ppDisplay);
    }

    /// <include file='IADLXDisplayGamutChangedEvent.xml' path='doc/member[@name="IADLXDisplayGamutChangedEvent.IsWhitePointChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsWhitePointChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, byte>)(lpVtbl[5]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXDisplayGamutChangedEvent.xml' path='doc/member[@name="IADLXDisplayGamutChangedEvent.IsColorSpaceChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsColorSpaceChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamutChangedEvent*, byte>)(lpVtbl[6]))((IADLXDisplayGamutChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
