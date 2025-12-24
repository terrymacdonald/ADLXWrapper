using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXChangedEvent.xml' path='doc/member[@name="IADLXChangedEvent"]/*' />
[NativeTypeName("struct IADLXChangedEvent : adlx::IADLXInterface")]
public unsafe partial struct IADLXChangedEvent
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXChangedEvent*, int>)(lpVtbl[0]))((IADLXChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXChangedEvent*, int>)(lpVtbl[1]))((IADLXChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXChangedEvent.xml' path='doc/member[@name="IADLXChangedEvent.GetOrigin"]/*' />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXChangedEvent*)Unsafe.AsPointer(ref this));
    }
}
