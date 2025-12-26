using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSystemMetricsSupport1.xml' path='doc/member[@name="IADLXSystemMetricsSupport1"]/*' />
[NativeTypeName("struct IADLXSystemMetricsSupport1 : adlx::IADLXSystemMetricsSupport")]
public unsafe partial struct IADLXSystemMetricsSupport1
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int>)(lpVtbl[0]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int>)(lpVtbl[1]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <inheritdoc cref="IADLXSystemMetricsSupport.IsSupportedCPUUsage" />
    public ADLX_RESULT IsSupportedCPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXSystemMetricsSupport.IsSupportedSystemRAM" />
    public ADLX_RESULT IsSupportedSystemRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXSystemMetricsSupport.IsSupportedSmartShift" />
    public ADLX_RESULT IsSupportedSmartShift([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }

    /// <inheritdoc cref="IADLXSystemMetricsSupport.GetCPUUsageRange" />
    public ADLX_RESULT GetCPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXSystemMetricsSupport.GetSystemRAMRange" />
    public ADLX_RESULT GetSystemRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <inheritdoc cref="IADLXSystemMetricsSupport.GetSmartShiftRange" />
    public ADLX_RESULT GetSmartShiftRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, int*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXSystemMetricsSupport1.xml' path='doc/member[@name="IADLXSystemMetricsSupport1.IsSupportedPowerDistribution"]/*' />
    public ADLX_RESULT IsSupportedPowerDistribution([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport1*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXSystemMetricsSupport1*)Unsafe.AsPointer(ref this), supported);
    }
}
