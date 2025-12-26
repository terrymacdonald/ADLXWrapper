using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXManualTuningState.xml' path='doc/member[@name="IADLXManualTuningState"]/*' />
[NativeTypeName("struct IADLXManualTuningState : adlx::IADLXInterface")]
public unsafe partial struct IADLXManualTuningState
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int>)(lpVtbl[0]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int>)(lpVtbl[1]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXManualTuningState.xml' path='doc/member[@name="IADLXManualTuningState.GetFrequency"]/*' />
    public ADLX_RESULT GetFrequency([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int*, ADLX_RESULT>)(lpVtbl[3]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualTuningState.xml' path='doc/member[@name="IADLXManualTuningState.SetFrequency"]/*' />
    public ADLX_RESULT SetFrequency([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int, ADLX_RESULT>)(lpVtbl[4]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualTuningState.xml' path='doc/member[@name="IADLXManualTuningState.GetVoltage"]/*' />
    public ADLX_RESULT GetVoltage([NativeTypeName("adlx_int *")] int* value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), value);
    }

    /// <include file='IADLXManualTuningState.xml' path='doc/member[@name="IADLXManualTuningState.SetVoltage"]/*' />
    public ADLX_RESULT SetVoltage([NativeTypeName("adlx_int")] int value)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXManualTuningState*, int, ADLX_RESULT>)(lpVtbl[6]))((IADLXManualTuningState*)Unsafe.AsPointer(ref this), value);
    }
}
