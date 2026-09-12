using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUMetrics3.xml' path='doc/member[@name="IADLXGPUMetrics3"]/*' />
[NativeTypeName("struct IADLXGPUMetrics3 : adlx::IADLXGPUMetrics2")]
public unsafe partial struct IADLXGPUMetrics3
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int>)(lpVtbl[0]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int>)(lpVtbl[1]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.TimeStamp" />
    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), ms);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUUsage" />
    public ADLX_RESULT GPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, double*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUClockSpeed" />
    public ADLX_RESULT GPUClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUVRAMClockSpeed" />
    public ADLX_RESULT GPUVRAMClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUTemperature" />
    public ADLX_RESULT GPUTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, double*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUHotspotTemperature" />
    public ADLX_RESULT GPUHotspotTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, double*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUPower" />
    public ADLX_RESULT GPUPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, double*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUTotalBoardPower" />
    public ADLX_RESULT GPUTotalBoardPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, double*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUFanSpeed" />
    public ADLX_RESULT GPUFanSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUVRAM" />
    public ADLX_RESULT GPUVRAM([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUVoltage" />
    public ADLX_RESULT GPUVoltage([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUIntakeTemperature" />
    public ADLX_RESULT GPUIntakeTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, double*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics1.GPUMemoryTemperature" />
    public ADLX_RESULT GPUMemoryTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, double*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics1.NPUFrequency" />
    public ADLX_RESULT NPUFrequency([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics1.NPUActivityLevel" />
    public ADLX_RESULT NPUActivityLevel([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics2.GPUSharedMemory" />
    public ADLX_RESULT GPUSharedMemory([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[18]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics3.xml' path='doc/member[@name="IADLXGPUMetrics3.GPUFanDuty"]/*' />
    public ADLX_RESULT GPUFanDuty([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics3*, int*, ADLX_RESULT>)(lpVtbl[19]))((IADLXGPUMetrics3*)Unsafe.AsPointer(ref this), data);
    }
}
