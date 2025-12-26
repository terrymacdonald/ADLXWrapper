using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience"]/*' />
[NativeTypeName("struct IADLXDisplayConnectivityExperience : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayConnectivityExperience
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int>)(lpVtbl[0]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int>)(lpVtbl[1]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.IsSupportedHDMIQualityDetection"]/*' />
    public ADLX_RESULT IsSupportedHDMIQualityDetection([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.IsSupportedDPLink"]/*' />
    public ADLX_RESULT IsSupportedDPLink([NativeTypeName("adlx_bool *")] bool* supported)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), supported);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.IsEnabledHDMIQualityDetection"]/*' />
    public ADLX_RESULT IsEnabledHDMIQualityDetection([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.SetEnabledHDMIQualityDetection"]/*' />
    public ADLX_RESULT SetEnabledHDMIQualityDetection([NativeTypeName("adlx_bool")] byte enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, byte, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), enabled);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.GetDPLinkRate"]/*' />
    public ADLX_RESULT GetDPLinkRate(ADLX_DP_LINK_RATE* linkRate)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, ADLX_DP_LINK_RATE*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), linkRate);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.GetNumberOfActiveLanes"]/*' />
    public ADLX_RESULT GetNumberOfActiveLanes([NativeTypeName("adlx_uint *")] uint* numActiveLanes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, uint*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), numActiveLanes);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.GetNumberOfTotalLanes"]/*' />
    public ADLX_RESULT GetNumberOfTotalLanes([NativeTypeName("adlx_uint *")] uint* numTotalLanes)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, uint*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), numTotalLanes);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.GetRelativePreEmphasis"]/*' />
    public ADLX_RESULT GetRelativePreEmphasis([NativeTypeName("adlx_int *")] int* relativePreEmphasis)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), relativePreEmphasis);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.SetRelativePreEmphasis"]/*' />
    public ADLX_RESULT SetRelativePreEmphasis([NativeTypeName("adlx_int")] int relativePreEmphasis)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), relativePreEmphasis);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.GetRelativeVoltageSwing"]/*' />
    public ADLX_RESULT GetRelativeVoltageSwing([NativeTypeName("adlx_int *")] int* relativeVoltageSwing)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), relativeVoltageSwing);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.SetRelativeVoltageSwing"]/*' />
    public ADLX_RESULT SetRelativeVoltageSwing([NativeTypeName("adlx_int")] int relativeVoltageSwing)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, int, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), relativeVoltageSwing);
    }

    /// <include file='IADLXDisplayConnectivityExperience.xml' path='doc/member[@name="IADLXDisplayConnectivityExperience.IsEnabledLinkProtection"]/*' />
    public ADLX_RESULT IsEnabledLinkProtection([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayConnectivityExperience*, bool*, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayConnectivityExperience*)Unsafe.AsPointer(ref this), enabled);
    }
}
