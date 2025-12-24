using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXPowerTuningServices1.xml' path='doc/member[@name="IADLXPowerTuningServices1"]/*' />
[NativeTypeName("struct IADLXPowerTuningServices1 : adlx::IADLXPowerTuningServices")]
public unsafe partial struct IADLXPowerTuningServices1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, int>)(lpVtbl[0]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, int>)(lpVtbl[1]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXPowerTuningServices.GetPowerTuningChangedHandling" />
    public ADLX_RESULT GetPowerTuningChangedHandling(IADLXPowerTuningChangedHandling** ppPowerTuningChangedHandling)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, IADLXPowerTuningChangedHandling**, ADLX_RESULT>)(lpVtbl[3]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), ppPowerTuningChangedHandling);
    }

    /// <inheritdoc cref="IADLXPowerTuningServices.GetSmartShiftMax" />
    public ADLX_RESULT GetSmartShiftMax(IADLXSmartShiftMax** ppSmartShiftMax)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, IADLXSmartShiftMax**, ADLX_RESULT>)(lpVtbl[4]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), ppSmartShiftMax);
    }

    /// <include file='IADLXPowerTuningServices1.xml' path='doc/member[@name="IADLXPowerTuningServices1.GetSmartShiftEco"]/*' />
    public ADLX_RESULT GetSmartShiftEco(IADLXSmartShiftEco** ppSmartShiftEco)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, IADLXSmartShiftEco**, ADLX_RESULT>)(lpVtbl[5]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), ppSmartShiftEco);
    }

    /// <include file='IADLXPowerTuningServices1.xml' path='doc/member[@name="IADLXPowerTuningServices1.IsGPUConnectSupported"]/*' />
    public ADLX_RESULT IsGPUConnectSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXPowerTuningServices1.xml' path='doc/member[@name="IADLXPowerTuningServices1.GetGPUConnectGPUs"]/*' />
    public ADLX_RESULT GetGPUConnectGPUs(IADLXGPU2List** ppGPUs)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXPowerTuningServices1*, IADLXGPU2List**, ADLX_RESULT>)(lpVtbl[7]))((IADLXPowerTuningServices1*)Unsafe.AsPointer(ref this), ppGPUs);
    }
}
