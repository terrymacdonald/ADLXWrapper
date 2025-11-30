using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayServices3 : adlx::IADLXDisplayServices2")]
public unsafe partial struct IADLXDisplayServices3
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayServices3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayServices3* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayServices3* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetNumberOfDisplays(IADLXDisplayServices3* pThis, [NativeTypeName("adlx_uint *")] uint* numDisplays);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplays(IADLXDisplayServices3* pThis, IADLXDisplayList** ppDisplay);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Get3DLUT(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplay3DLUT** ppDisp3DLUT);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGamut(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGamut** ppDispGamut);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGamma(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGamma** ppDispGamma);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplayChangedHandling(IADLXDisplayServices3* pThis, IADLXDisplayChangedHandling** ppDisplayChangedHandling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFreeSync(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayFreeSync** ppFreeSync);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetVirtualSuperResolution(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayVSR** ppVSR);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUScaling(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGPUScaling** ppGPUScaling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetScalingMode(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayScalingMode** ppScalingMode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetIntegerScaling(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayIntegerScaling** ppIntegerScaling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetColorDepth(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayColorDepth** ppColorDepth);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetPixelFormat(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayPixelFormat** ppPixelFormat);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCustomColor(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayCustomColor** ppCustomColor);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetHDCP(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayHDCP** ppHDCP);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCustomResolution(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayCustomResolution** ppCustomResolution);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetVariBright(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayVariBright** ppVariBright);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplayBlanking(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayBlanking** ppDisplayBlanking);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplayConnectivityExperience(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayConnectivityExperience** ppDisplayConnectivityExperience);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDynamicRefreshRateControl(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayDynamicRefreshRateControl** ppDRRC);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFreeSyncColorAccuracy(IADLXDisplayServices3* pThis, [NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayFreeSyncColorAccuracy** ppFSCA);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetNumberOfDisplays([NativeTypeName("adlx_uint *")] uint* numDisplays)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetNumberOfDisplays>((IntPtr)(lpVtbl[3]))(pThis, numDisplays);
        }
    }

    public ADLX_RESULT GetDisplays(IADLXDisplayList** ppDisplay)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplays>((IntPtr)(lpVtbl[4]))(pThis, ppDisplay);
        }
    }

    public ADLX_RESULT Get3DLUT([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplay3DLUT** ppDisp3DLUT)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Get3DLUT>((IntPtr)(lpVtbl[5]))(pThis, pDisplay, ppDisp3DLUT);
        }
    }

    public ADLX_RESULT GetGamut([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGamut** ppDispGamut)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGamut>((IntPtr)(lpVtbl[6]))(pThis, pDisplay, ppDispGamut);
        }
    }

    public ADLX_RESULT GetGamma([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGamma** ppDispGamma)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGamma>((IntPtr)(lpVtbl[7]))(pThis, pDisplay, ppDispGamma);
        }
    }

    public ADLX_RESULT GetDisplayChangedHandling(IADLXDisplayChangedHandling** ppDisplayChangedHandling)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplayChangedHandling>((IntPtr)(lpVtbl[8]))(pThis, ppDisplayChangedHandling);
        }
    }

    public ADLX_RESULT GetFreeSync([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayFreeSync** ppFreeSync)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFreeSync>((IntPtr)(lpVtbl[9]))(pThis, pDisplay, ppFreeSync);
        }
    }

    public ADLX_RESULT GetVirtualSuperResolution([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayVSR** ppVSR)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetVirtualSuperResolution>((IntPtr)(lpVtbl[10]))(pThis, pDisplay, ppVSR);
        }
    }

    public ADLX_RESULT GetGPUScaling([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayGPUScaling** ppGPUScaling)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUScaling>((IntPtr)(lpVtbl[11]))(pThis, pDisplay, ppGPUScaling);
        }
    }

    public ADLX_RESULT GetScalingMode([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayScalingMode** ppScalingMode)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetScalingMode>((IntPtr)(lpVtbl[12]))(pThis, pDisplay, ppScalingMode);
        }
    }

    public ADLX_RESULT GetIntegerScaling([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayIntegerScaling** ppIntegerScaling)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetIntegerScaling>((IntPtr)(lpVtbl[13]))(pThis, pDisplay, ppIntegerScaling);
        }
    }

    public ADLX_RESULT GetColorDepth([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayColorDepth** ppColorDepth)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetColorDepth>((IntPtr)(lpVtbl[14]))(pThis, pDisplay, ppColorDepth);
        }
    }

    public ADLX_RESULT GetPixelFormat([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayPixelFormat** ppPixelFormat)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetPixelFormat>((IntPtr)(lpVtbl[15]))(pThis, pDisplay, ppPixelFormat);
        }
    }

    public ADLX_RESULT GetCustomColor([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayCustomColor** ppCustomColor)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCustomColor>((IntPtr)(lpVtbl[16]))(pThis, pDisplay, ppCustomColor);
        }
    }

    public ADLX_RESULT GetHDCP([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayHDCP** ppHDCP)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetHDCP>((IntPtr)(lpVtbl[17]))(pThis, pDisplay, ppHDCP);
        }
    }

    public ADLX_RESULT GetCustomResolution([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayCustomResolution** ppCustomResolution)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCustomResolution>((IntPtr)(lpVtbl[18]))(pThis, pDisplay, ppCustomResolution);
        }
    }

    public ADLX_RESULT GetVariBright([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayVariBright** ppVariBright)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetVariBright>((IntPtr)(lpVtbl[19]))(pThis, pDisplay, ppVariBright);
        }
    }

    public ADLX_RESULT GetDisplayBlanking([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayBlanking** ppDisplayBlanking)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplayBlanking>((IntPtr)(lpVtbl[20]))(pThis, pDisplay, ppDisplayBlanking);
        }
    }

    public ADLX_RESULT GetDisplayConnectivityExperience([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayConnectivityExperience** ppDisplayConnectivityExperience)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplayConnectivityExperience>((IntPtr)(lpVtbl[21]))(pThis, pDisplay, ppDisplayConnectivityExperience);
        }
    }

    public ADLX_RESULT GetDynamicRefreshRateControl([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayDynamicRefreshRateControl** ppDRRC)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDynamicRefreshRateControl>((IntPtr)(lpVtbl[22]))(pThis, pDisplay, ppDRRC);
        }
    }

    public ADLX_RESULT GetFreeSyncColorAccuracy([NativeTypeName("adlx::IADLXDisplay *")] IADLXDisplay* pDisplay, IADLXDisplayFreeSyncColorAccuracy** ppFSCA)
    {
        fixed (IADLXDisplayServices3* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFreeSyncColorAccuracy>((IntPtr)(lpVtbl[23]))(pThis, pDisplay, ppFSCA);
        }
    }
}
