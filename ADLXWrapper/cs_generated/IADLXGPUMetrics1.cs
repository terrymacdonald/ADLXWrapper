using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUMetrics1.xml' path='doc/member[@name="IADLXGPUMetrics1"]/*' />
[NativeTypeName("struct IADLXGPUMetrics1 : adlx::IADLXGPUMetrics")]
public unsafe partial struct IADLXGPUMetrics1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int>)(lpVtbl[0]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int>)(lpVtbl[1]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.TimeStamp" />
    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), ms);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUUsage" />
    public ADLX_RESULT GPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUClockSpeed" />
    public ADLX_RESULT GPUClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUVRAMClockSpeed" />
    public ADLX_RESULT GPUVRAMClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUTemperature" />
    public ADLX_RESULT GPUTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUHotspotTemperature" />
    public ADLX_RESULT GPUHotspotTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUPower" />
    public ADLX_RESULT GPUPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUTotalBoardPower" />
    public ADLX_RESULT GPUTotalBoardPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUFanSpeed" />
    public ADLX_RESULT GPUFanSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUVRAM" />
    public ADLX_RESULT GPUVRAM([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUVoltage" />
    public ADLX_RESULT GPUVoltage([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <inheritdoc cref="IADLXGPUMetrics.GPUIntakeTemperature" />
    public ADLX_RESULT GPUIntakeTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics1.xml' path='doc/member[@name="IADLXGPUMetrics1.GPUMemoryTemperature"]/*' />
    public ADLX_RESULT GPUMemoryTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, double*, ADLX_RESULT>)(lpVtbl[15]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics1.xml' path='doc/member[@name="IADLXGPUMetrics1.NPUFrequency"]/*' />
    public ADLX_RESULT NPUFrequency([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[16]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics1.xml' path='doc/member[@name="IADLXGPUMetrics1.NPUActivityLevel"]/*' />
    public ADLX_RESULT NPUActivityLevel([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics1*, int*, ADLX_RESULT>)(lpVtbl[17]))((IADLXGPUMetrics1*)Unsafe.AsPointer(ref this), data);
    }
}
