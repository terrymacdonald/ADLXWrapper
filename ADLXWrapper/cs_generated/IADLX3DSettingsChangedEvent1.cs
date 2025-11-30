using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DSettingsChangedEvent1 : adlx::IADLX3DSettingsChangedEvent")]
public unsafe partial struct IADLX3DSettingsChangedEvent1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DSettingsChangedEvent1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPU(IADLX3DSettingsChangedEvent1* pThis, IADLXGPU** ppGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsAntiLagChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsChillChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsBoostChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsImageSharpeningChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsEnhancedSyncChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsWaitForVerticalRefreshChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsFrameRateTargetControlChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsAntiAliasingChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsMorphologicalAntiAliasingChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsAnisotropicFilteringChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsTessellationModeChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsRadeonSuperResolutionChanged(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsResetShaderCache(IADLX3DSettingsChangedEvent1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsAMDFluidMotionFramesChanged(IADLX3DSettingsChangedEvent1* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPU>((IntPtr)(lpVtbl[4]))(pThis, ppGPU);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsAntiLagChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsAntiLagChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsChillChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsChillChanged>((IntPtr)(lpVtbl[6]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsBoostChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsBoostChanged>((IntPtr)(lpVtbl[7]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsImageSharpeningChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsImageSharpeningChanged>((IntPtr)(lpVtbl[8]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsEnhancedSyncChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnhancedSyncChanged>((IntPtr)(lpVtbl[9]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsWaitForVerticalRefreshChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsWaitForVerticalRefreshChanged>((IntPtr)(lpVtbl[10]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsFrameRateTargetControlChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsFrameRateTargetControlChanged>((IntPtr)(lpVtbl[11]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsAntiAliasingChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsAntiAliasingChanged>((IntPtr)(lpVtbl[12]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsMorphologicalAntiAliasingChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsMorphologicalAntiAliasingChanged>((IntPtr)(lpVtbl[13]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsAnisotropicFilteringChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsAnisotropicFilteringChanged>((IntPtr)(lpVtbl[14]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsTessellationModeChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsTessellationModeChanged>((IntPtr)(lpVtbl[15]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsRadeonSuperResolutionChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsRadeonSuperResolutionChanged>((IntPtr)(lpVtbl[16]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsResetShaderCache()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsResetShaderCache>((IntPtr)(lpVtbl[17]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsAMDFluidMotionFramesChanged()
    {
        fixed (IADLX3DSettingsChangedEvent1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsAMDFluidMotionFramesChanged>((IntPtr)(lpVtbl[18]))(pThis) != 0;
        }
    }
}
