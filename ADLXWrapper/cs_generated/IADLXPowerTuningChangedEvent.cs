using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXPowerTuningChangedEvent.xml' path='doc/member[@name="IADLXPowerTuningChangedEvent"]/*' />
[NativeTypeName("struct IADLXPowerTuningChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXPowerTuningChangedEvent
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, int>)(lpVtbl[0]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, int>)(lpVtbl[1]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXPowerTuningChangedEvent.xml' path='doc/member[@name="IADLXPowerTuningChangedEvent.IsSmartShiftMaxChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartShiftMaxChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent*, byte>)(lpVtbl[4]))((IADLXPowerTuningChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
