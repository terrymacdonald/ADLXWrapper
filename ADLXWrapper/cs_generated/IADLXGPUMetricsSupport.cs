using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport"]/*' />
[NativeTypeName("struct IADLXGPUMetricsSupport : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUMetricsSupport
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int>)(lpVtbl[0]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int>)(lpVtbl[1]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUUsage"]/*' />
    public ADLX_RESULT IsSupportedGPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUClockSpeed"]/*' />
    public ADLX_RESULT IsSupportedGPUClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUVRAMClockSpeed"]/*' />
    public ADLX_RESULT IsSupportedGPUVRAMClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUTemperature"]/*' />
    public ADLX_RESULT IsSupportedGPUTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUHotspotTemperature"]/*' />
    public ADLX_RESULT IsSupportedGPUHotspotTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUPower"]/*' />
    public ADLX_RESULT IsSupportedGPUPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUTotalBoardPower"]/*' />
    public ADLX_RESULT IsSupportedGPUTotalBoardPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUFanSpeed"]/*' />
    public ADLX_RESULT IsSupportedGPUFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUVRAM"]/*' />
    public ADLX_RESULT IsSupportedGPUVRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUVoltage"]/*' />
    public ADLX_RESULT IsSupportedGPUVoltage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUUsageRange"]/*' />
    public ADLX_RESULT GetGPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUClockSpeedRange"]/*' />
    public ADLX_RESULT GetGPUClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUVRAMClockSpeedRange"]/*' />
    public ADLX_RESULT GetGPUVRAMClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUTemperatureRange"]/*' />
    public ADLX_RESULT GetGPUTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUHotspotTemperatureRange"]/*' />
    public ADLX_RESULT GetGPUHotspotTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUPowerRange"]/*' />
    public ADLX_RESULT GetGPUPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUFanSpeedRange"]/*' />
    public ADLX_RESULT GetGPUFanSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[19]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUVRAMRange"]/*' />
    public ADLX_RESULT GetGPUVRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[20]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUVoltageRange"]/*' />
    public ADLX_RESULT GetGPUVoltageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[21]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUTotalBoardPowerRange"]/*' />
    public ADLX_RESULT GetGPUTotalBoardPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[22]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.GetGPUIntakeTemperatureRange"]/*' />
    public ADLX_RESULT GetGPUIntakeTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[23]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport.xml' path='doc/member[@name="IADLXGPUMetricsSupport.IsSupportedGPUIntakeTemperature"]/*' />
    public ADLX_RESULT IsSupportedGPUIntakeTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[24]))((IADLXGPUMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }
}
