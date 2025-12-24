using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXPowerTuningChangedEvent1.xml' path='doc/member[@name="IADLXPowerTuningChangedEvent1"]/*' />
[NativeTypeName("struct IADLXPowerTuningChangedEvent1 : adlx::IADLXPowerTuningChangedEvent")]
public unsafe partial struct IADLXPowerTuningChangedEvent1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, int>)(lpVtbl[0]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, int>)(lpVtbl[1]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXPowerTuningChangedEvent.IsSmartShiftMaxChanged" />
    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartShiftMaxChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, byte>)(lpVtbl[4]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXPowerTuningChangedEvent1.xml' path='doc/member[@name="IADLXPowerTuningChangedEvent1.IsSmartShiftEcoChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsSmartShiftEcoChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningChangedEvent1*, byte>)(lpVtbl[5]))((IADLXPowerTuningChangedEvent1*)Unsafe.AsPointer(ref this)) != 0;
    }
}
