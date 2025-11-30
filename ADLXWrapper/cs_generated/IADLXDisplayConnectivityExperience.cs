using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplayConnectivityExperience
{
}

public partial struct IADLXDisplayConnectivityExperience
{
}

[NativeTypeName("struct IADLXDisplayConnectivityExperience : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayConnectivityExperience
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayConnectivityExperience* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayConnectivityExperience* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedHDMIQualityDetection(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedDPLink(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabledHDMIQualityDetection(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabledHDMIQualityDetection(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_bool")] byte enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDPLinkRate(IADLXDisplayConnectivityExperience* pThis, ADLX_DP_LINK_RATE* linkRate);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetNumberOfActiveLanes(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_uint *")] uint* numActiveLanes);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetNumberOfTotalLanes(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_uint *")] uint* numTotalLanes);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetRelativePreEmphasis(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_int *")] int* relativePreEmphasis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetRelativePreEmphasis(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_int")] int relativePreEmphasis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetRelativeVoltageSwing(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_int *")] int* relativeVoltageSwing);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetRelativeVoltageSwing(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_int")] int relativeVoltageSwing);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabledLinkProtection(IADLXDisplayConnectivityExperience* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedHDMIQualityDetection([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedHDMIQualityDetection>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedDPLink([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedDPLink>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabledHDMIQualityDetection([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabledHDMIQualityDetection>((IntPtr)(lpVtbl[5]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetEnabledHDMIQualityDetection([NativeTypeName("adlx_bool")] byte enabled)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabledHDMIQualityDetection>((IntPtr)(lpVtbl[6]))(pThis, enabled);
        }
    }

    public ADLX_RESULT GetDPLinkRate(ADLX_DP_LINK_RATE* linkRate)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDPLinkRate>((IntPtr)(lpVtbl[7]))(pThis, linkRate);
        }
    }

    public ADLX_RESULT GetNumberOfActiveLanes([NativeTypeName("adlx_uint *")] uint* numActiveLanes)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetNumberOfActiveLanes>((IntPtr)(lpVtbl[8]))(pThis, numActiveLanes);
        }
    }

    public ADLX_RESULT GetNumberOfTotalLanes([NativeTypeName("adlx_uint *")] uint* numTotalLanes)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetNumberOfTotalLanes>((IntPtr)(lpVtbl[9]))(pThis, numTotalLanes);
        }
    }

    public ADLX_RESULT GetRelativePreEmphasis([NativeTypeName("adlx_int *")] int* relativePreEmphasis)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetRelativePreEmphasis>((IntPtr)(lpVtbl[10]))(pThis, relativePreEmphasis);
        }
    }

    public ADLX_RESULT SetRelativePreEmphasis([NativeTypeName("adlx_int")] int relativePreEmphasis)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetRelativePreEmphasis>((IntPtr)(lpVtbl[11]))(pThis, relativePreEmphasis);
        }
    }

    public ADLX_RESULT GetRelativeVoltageSwing([NativeTypeName("adlx_int *")] int* relativeVoltageSwing)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetRelativeVoltageSwing>((IntPtr)(lpVtbl[12]))(pThis, relativeVoltageSwing);
        }
    }

    public ADLX_RESULT SetRelativeVoltageSwing([NativeTypeName("adlx_int")] int relativeVoltageSwing)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetRelativeVoltageSwing>((IntPtr)(lpVtbl[13]))(pThis, relativeVoltageSwing);
        }
    }

    public ADLX_RESULT IsEnabledLinkProtection([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXDisplayConnectivityExperience* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabledLinkProtection>((IntPtr)(lpVtbl[14]))(pThis, enabled);
        }
    }
}
