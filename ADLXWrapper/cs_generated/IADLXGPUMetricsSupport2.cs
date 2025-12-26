using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUMetricsSupport2.xml' path='doc/member[@name="IADLXGPUMetricsSupport2"]/*' />
[NativeTypeName("struct IADLXGPUMetricsSupport2 : adlx::IADLXGPUMetricsSupport1")]
public unsafe partial struct IADLXGPUMetricsSupport2
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int>)(lpVtbl[0]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int>)(lpVtbl[1]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUUsage" />
    public ADLX_RESULT IsSupportedGPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUClockSpeed" />
    public ADLX_RESULT IsSupportedGPUClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUVRAMClockSpeed" />
    public ADLX_RESULT IsSupportedGPUVRAMClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUTemperature" />
    public ADLX_RESULT IsSupportedGPUTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUHotspotTemperature" />
    public ADLX_RESULT IsSupportedGPUHotspotTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUPower" />
    public ADLX_RESULT IsSupportedGPUPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUTotalBoardPower" />
    public ADLX_RESULT IsSupportedGPUTotalBoardPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUFanSpeed" />
    public ADLX_RESULT IsSupportedGPUFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUVRAM" />
    public ADLX_RESULT IsSupportedGPUVRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUVoltage" />
    public ADLX_RESULT IsSupportedGPUVoltage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUUsageRange" />
    public ADLX_RESULT GetGPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUClockSpeedRange" />
    public ADLX_RESULT GetGPUClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUVRAMClockSpeedRange" />
    public ADLX_RESULT GetGPUVRAMClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUTemperatureRange" />
    public ADLX_RESULT GetGPUTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUHotspotTemperatureRange" />
    public ADLX_RESULT GetGPUHotspotTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUPowerRange" />
    public ADLX_RESULT GetGPUPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUFanSpeedRange" />
    public ADLX_RESULT GetGPUFanSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[19]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUVRAMRange" />
    public ADLX_RESULT GetGPUVRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[20]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUVoltageRange" />
    public ADLX_RESULT GetGPUVoltageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[21]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUTotalBoardPowerRange" />
    public ADLX_RESULT GetGPUTotalBoardPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[22]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.GetGPUIntakeTemperatureRange" />
    public ADLX_RESULT GetGPUIntakeTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[23]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport.IsSupportedGPUIntakeTemperature" />
    public ADLX_RESULT IsSupportedGPUIntakeTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[24]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport1.IsSupportedGPUMemoryTemperature" />
    public ADLX_RESULT IsSupportedGPUMemoryTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[25]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport1.GetGPUMemoryTemperatureRange" />
    public ADLX_RESULT GetGPUMemoryTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[26]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport1.IsSupportedNPUFrequency" />
    public ADLX_RESULT IsSupportedNPUFrequency([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[27]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport1.GetNPUFrequencyRange" />
    public ADLX_RESULT GetNPUFrequencyRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[28]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport1.IsSupportedNPUActivityLevel" />
    public ADLX_RESULT IsSupportedNPUActivityLevel([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[29]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXGPUMetricsSupport1.GetNPUActivityLevelRange" />
    public ADLX_RESULT GetNPUActivityLevelRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[30]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXGPUMetricsSupport2.xml' path='doc/member[@name="IADLXGPUMetricsSupport2.IsSupportedGPUSharedMemory"]/*' />
    public ADLX_RESULT IsSupportedGPUSharedMemory([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, bool*, ADLX_RESULT>)(lpVtbl[31]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXGPUMetricsSupport2.xml' path='doc/member[@name="IADLXGPUMetricsSupport2.GetGPUSharedMemoryRange"]/*' />
    public ADLX_RESULT GetGPUSharedMemoryRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetricsSupport2*, int*, int*, ADLX_RESULT>)(lpVtbl[32]))((IADLXGPUMetricsSupport2*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }
}
