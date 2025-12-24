using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXSystemMetricsSupport.xml' path='doc/member[@name="IADLXSystemMetricsSupport"]/*' />
[NativeTypeName("struct IADLXSystemMetricsSupport : adlx::IADLXInterface")]
public unsafe partial struct IADLXSystemMetricsSupport
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, int>)(lpVtbl[0]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, int>)(lpVtbl[1]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXSystemMetricsSupport.xml' path='doc/member[@name="IADLXSystemMetricsSupport.IsSupportedCPUUsage"]/*' />
    public ADLX_RESULT IsSupportedCPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXSystemMetricsSupport.xml' path='doc/member[@name="IADLXSystemMetricsSupport.IsSupportedSystemRAM"]/*' />
    public ADLX_RESULT IsSupportedSystemRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXSystemMetricsSupport.xml' path='doc/member[@name="IADLXSystemMetricsSupport.IsSupportedSmartShift"]/*' />
    public ADLX_RESULT IsSupportedSmartShift([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXSystemMetricsSupport.xml' path='doc/member[@name="IADLXSystemMetricsSupport.GetCPUUsageRange"]/*' />
    public ADLX_RESULT GetCPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[6]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXSystemMetricsSupport.xml' path='doc/member[@name="IADLXSystemMetricsSupport.GetSystemRAMRange"]/*' />
    public ADLX_RESULT GetSystemRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[7]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }

    /// <include file='IADLXSystemMetricsSupport.xml' path='doc/member[@name="IADLXSystemMetricsSupport.GetSmartShiftRange"]/*' />
    public ADLX_RESULT GetSmartShiftRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXSystemMetricsSupport*, int*, int*, ADLX_RESULT>)(lpVtbl[8]))((IADLXSystemMetricsSupport*)Unsafe.AsPointer(ref this), minValue, maxValue);
    }
}
