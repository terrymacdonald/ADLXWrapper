using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUConnectChangedEvent.xml' path='doc/member[@name="IADLXGPUConnectChangedEvent"]/*' />
[NativeTypeName("struct IADLXGPUConnectChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXGPUConnectChangedEvent
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedEvent*, int>)(lpVtbl[0]))((IADLXGPUConnectChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedEvent*, int>)(lpVtbl[1]))((IADLXGPUConnectChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUConnectChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXGPUConnectChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXGPUConnectChangedEvent.xml' path='doc/member[@name="IADLXGPUConnectChangedEvent.GetGPU"]/*' />
    public ADLX_RESULT GetGPU(IADLXGPU2** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedEvent*, IADLXGPU2**, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUConnectChangedEvent*)Unsafe.AsPointer(ref this), ppGPU);
    }

    /// <include file='IADLXGPUConnectChangedEvent.xml' path='doc/member[@name="IADLXGPUConnectChangedEvent.IsGPUAppsListChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUAppsListChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedEvent*, byte>)(lpVtbl[5]))((IADLXGPUConnectChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXGPUConnectChangedEvent.xml' path='doc/member[@name="IADLXGPUConnectChangedEvent.IsGPUPowerChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUPowerChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedEvent*, byte>)(lpVtbl[6]))((IADLXGPUConnectChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXGPUConnectChangedEvent.xml' path='doc/member[@name="IADLXGPUConnectChangedEvent.IsGPUPowerChangeError"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUPowerChangeError(ADLX_RESULT* pPowerChangeError)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUConnectChangedEvent*, ADLX_RESULT*, byte>)(lpVtbl[7]))((IADLXGPUConnectChangedEvent*)Unsafe.AsPointer(ref this), pPowerChangeError) != 0;
    }
}
