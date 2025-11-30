using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplaySettingsChangedEvent2 : adlx::IADLXDisplaySettingsChangedEvent1")]
public unsafe partial struct IADLXDisplaySettingsChangedEvent2
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplaySettingsChangedEvent2* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplay(IADLXDisplaySettingsChangedEvent2* pThis, IADLXDisplay** ppDisplay);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsFreeSyncChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsVSRChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsGPUScalingChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsScalingModeChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsIntegerScalingChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsColorDepthChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsPixelFormatChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsHDCPChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorHueChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorSaturationChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorBrightnessChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorTemperatureChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorContrastChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomResolutionChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsVariBrightChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsDisplayBlankingChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsDisplayConnectivityExperienceChanged(IADLXDisplaySettingsChangedEvent2* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplay>((IntPtr)(lpVtbl[4]))(pThis, ppDisplay);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsFreeSyncChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVSRChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsVSRChanged>((IntPtr)(lpVtbl[6]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUScalingChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsGPUScalingChanged>((IntPtr)(lpVtbl[7]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsScalingModeChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsScalingModeChanged>((IntPtr)(lpVtbl[8]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsIntegerScalingChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsIntegerScalingChanged>((IntPtr)(lpVtbl[9]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsColorDepthChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsColorDepthChanged>((IntPtr)(lpVtbl[10]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPixelFormatChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsPixelFormatChanged>((IntPtr)(lpVtbl[11]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsHDCPChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsHDCPChanged>((IntPtr)(lpVtbl[12]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorHueChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorHueChanged>((IntPtr)(lpVtbl[13]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorSaturationChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorSaturationChanged>((IntPtr)(lpVtbl[14]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorBrightnessChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorBrightnessChanged>((IntPtr)(lpVtbl[15]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorTemperatureChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorTemperatureChanged>((IntPtr)(lpVtbl[16]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorContrastChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorContrastChanged>((IntPtr)(lpVtbl[17]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomResolutionChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomResolutionChanged>((IntPtr)(lpVtbl[18]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVariBrightChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsVariBrightChanged>((IntPtr)(lpVtbl[19]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayBlankingChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsDisplayBlankingChanged>((IntPtr)(lpVtbl[20]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayConnectivityExperienceChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsDisplayConnectivityExperienceChanged>((IntPtr)(lpVtbl[21]))(pThis) != 0;
        }
    }
}
