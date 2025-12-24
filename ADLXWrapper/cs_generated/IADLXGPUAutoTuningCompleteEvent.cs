using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUAutoTuningCompleteEvent.xml' path='doc/member[@name="IADLXGPUAutoTuningCompleteEvent"]/*' />
[NativeTypeName("struct IADLXGPUAutoTuningCompleteEvent : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAutoTuningCompleteEvent
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, int>)(lpVtbl[0]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, int>)(lpVtbl[1]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPUAutoTuningCompleteEvent.xml' path='doc/member[@name="IADLXGPUAutoTuningCompleteEvent.IsUndervoltGPUCompleted"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsUndervoltGPUCompleted()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, byte>)(lpVtbl[3]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXGPUAutoTuningCompleteEvent.xml' path='doc/member[@name="IADLXGPUAutoTuningCompleteEvent.IsOverclockGPUCompleted"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsOverclockGPUCompleted()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, byte>)(lpVtbl[4]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this)) != 0;
    }

    /// <include file='IADLXGPUAutoTuningCompleteEvent.xml' path='doc/member[@name="IADLXGPUAutoTuningCompleteEvent.IsOverclockVRAMCompleted"]/*' />
    [return: NativeTypeName("adlx_bool")]
    public bool IsOverclockVRAMCompleted()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUAutoTuningCompleteEvent*, byte>)(lpVtbl[5]))((IADLXGPUAutoTuningCompleteEvent*)Unsafe.AsPointer(ref this)) != 0;
    }
}
