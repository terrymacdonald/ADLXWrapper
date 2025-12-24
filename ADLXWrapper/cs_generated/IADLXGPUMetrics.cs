using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics"]/*' />
[NativeTypeName("struct IADLXGPUMetrics : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUMetrics
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, int>)(lpVtbl[0]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, int>)(lpVtbl[1]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.TimeStamp"]/*' />
    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, long*, ADLX_RESULT>)(lpVtbl[3]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), ms);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUUsage"]/*' />
    public ADLX_RESULT GPUUsage([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, double*, ADLX_RESULT>)(lpVtbl[4]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUClockSpeed"]/*' />
    public ADLX_RESULT GPUClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, int*, ADLX_RESULT>)(lpVtbl[5]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUVRAMClockSpeed"]/*' />
    public ADLX_RESULT GPUVRAMClockSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUTemperature"]/*' />
    public ADLX_RESULT GPUTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, double*, ADLX_RESULT>)(lpVtbl[7]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUHotspotTemperature"]/*' />
    public ADLX_RESULT GPUHotspotTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, double*, ADLX_RESULT>)(lpVtbl[8]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUPower"]/*' />
    public ADLX_RESULT GPUPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, double*, ADLX_RESULT>)(lpVtbl[9]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUTotalBoardPower"]/*' />
    public ADLX_RESULT GPUTotalBoardPower([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, double*, ADLX_RESULT>)(lpVtbl[10]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUFanSpeed"]/*' />
    public ADLX_RESULT GPUFanSpeed([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, int*, ADLX_RESULT>)(lpVtbl[11]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUVRAM"]/*' />
    public ADLX_RESULT GPUVRAM([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUVoltage"]/*' />
    public ADLX_RESULT GPUVoltage([NativeTypeName("adlx_int *")] int* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, int*, ADLX_RESULT>)(lpVtbl[13]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }

    /// <include file='IADLXGPUMetrics.xml' path='doc/member[@name="IADLXGPUMetrics.GPUIntakeTemperature"]/*' />
    public ADLX_RESULT GPUIntakeTemperature([NativeTypeName("adlx_double *")] double* data)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXGPUMetrics*, double*, ADLX_RESULT>)(lpVtbl[14]))((IADLXGPUMetrics*)Unsafe.AsPointer(ref this), data);
    }
}
