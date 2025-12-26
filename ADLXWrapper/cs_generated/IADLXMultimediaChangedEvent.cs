using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXMultimediaChangedEvent.xml' path='doc/member[@name="IADLXMultimediaChangedEvent"]/*' />
[NativeTypeName("struct IADLXMultimediaChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXMultimediaChangedEvent
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, int>)(lpVtbl[0]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, int>)(lpVtbl[1]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXChangedEvent.GetOrigin" />
    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, ADLX_SYNC_ORIGIN>)(lpVtbl[3]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXMultimediaChangedEvent.xml' path='doc/member[@name="IADLXMultimediaChangedEvent.GetGPU"]/*' />
    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, IADLXGPU**, ADLX_RESULT>)(lpVtbl[4]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this), ppGPU);
    }

    /// <include file='IADLXMultimediaChangedEvent.xml' path='doc/member[@name="IADLXMultimediaChangedEvent.IsVideoUpscaleChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsVideoUpscaleChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, byte>)(lpVtbl[5]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXMultimediaChangedEvent.xml' path='doc/member[@name="IADLXMultimediaChangedEvent.IsVideoSuperResolutionChanged"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsVideoSuperResolutionChanged()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXMultimediaChangedEvent*, byte>)(lpVtbl[6]))((IADLXMultimediaChangedEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
