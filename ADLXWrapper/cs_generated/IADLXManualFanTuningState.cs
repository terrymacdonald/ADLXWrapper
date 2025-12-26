using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualFanTuningState.xml' path='doc/member[@name="IADLXManualFanTuningState"]/*' />
[NativeTypeName("struct IADLXManualFanTuningState : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualFanTuningState
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int>)(lpVtbl[0]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int>)(lpVtbl[1]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXManualFanTuningState.xml' path='doc/member[@name="IADLXManualFanTuningState.GetFanSpeed"]/*' />
    public ADLX_RESULT GetFanSpeed([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuningState.xml' path='doc/member[@name="IADLXManualFanTuningState.SetFanSpeed"]/*' />
    public ADLX_RESULT SetFanSpeed([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuningState.xml' path='doc/member[@name="IADLXManualFanTuningState.GetTemperature"]/*' />
    public ADLX_RESULT GetTemperature([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualFanTuningState.xml' path='doc/member[@name="IADLXManualFanTuningState.SetTemperature"]/*' />
    public ADLX_RESULT SetTemperature([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualFanTuningState*, int, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualFanTuningState*)Unsafe.AsPointer(ref this), value);
    }
}
