using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplaySettingsChangedEvent3 : adlx::IADLXDisplaySettingsChangedEvent2")]
public unsafe partial struct IADLXDisplaySettingsChangedEvent3
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplaySettingsChangedEvent3* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplay(IADLXDisplaySettingsChangedEvent3* pThis, IADLXDisplay** ppDisplay);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsFreeSyncChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsVSRChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsGPUScalingChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsScalingModeChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsIntegerScalingChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsColorDepthChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsPixelFormatChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsHDCPChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorHueChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorSaturationChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorBrightnessChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorTemperatureChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomColorContrastChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustomResolutionChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsVariBrightChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsDisplayBlankingChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsDisplayConnectivityExperienceChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsDisplayDynamicRefreshRateControlChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsFreeSyncColorAccuracyChanged(IADLXDisplaySettingsChangedEvent3* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplay>((IntPtr)(lpVtbl[4]))(pThis, ppDisplay);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsFreeSyncChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVSRChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsVSRChanged>((IntPtr)(lpVtbl[6]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsGPUScalingChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsGPUScalingChanged>((IntPtr)(lpVtbl[7]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsScalingModeChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsScalingModeChanged>((IntPtr)(lpVtbl[8]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsIntegerScalingChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsIntegerScalingChanged>((IntPtr)(lpVtbl[9]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsColorDepthChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsColorDepthChanged>((IntPtr)(lpVtbl[10]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsPixelFormatChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsPixelFormatChanged>((IntPtr)(lpVtbl[11]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsHDCPChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsHDCPChanged>((IntPtr)(lpVtbl[12]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorHueChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorHueChanged>((IntPtr)(lpVtbl[13]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorSaturationChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorSaturationChanged>((IntPtr)(lpVtbl[14]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorBrightnessChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorBrightnessChanged>((IntPtr)(lpVtbl[15]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorTemperatureChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorTemperatureChanged>((IntPtr)(lpVtbl[16]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomColorContrastChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomColorContrastChanged>((IntPtr)(lpVtbl[17]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustomResolutionChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustomResolutionChanged>((IntPtr)(lpVtbl[18]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVariBrightChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsVariBrightChanged>((IntPtr)(lpVtbl[19]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayBlankingChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsDisplayBlankingChanged>((IntPtr)(lpVtbl[20]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayConnectivityExperienceChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsDisplayConnectivityExperienceChanged>((IntPtr)(lpVtbl[21]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsDisplayDynamicRefreshRateControlChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsDisplayDynamicRefreshRateControlChanged>((IntPtr)(lpVtbl[22]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsFreeSyncColorAccuracyChanged()
    {
        fixed (IADLXDisplaySettingsChangedEvent3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsFreeSyncColorAccuracyChanged>((IntPtr)(lpVtbl[23]))(pThis) != 0;
        }
    }
}
