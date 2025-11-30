using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayVariBright1 : adlx::IADLXDisplayVariBright")]
public unsafe partial struct IADLXDisplayVariBright1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayVariBright1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayVariBright1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayVariBright1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool")] byte enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentMaximizeBrightness(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* maximizeBrightness);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentOptimizeBrightness(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* optimizeBrightness);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentBalanced(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* balanced);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentOptimizeBattery(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* optimizeBattery);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentMaximizeBattery(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* maximizeBattery);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMaximizeBrightness(IADLXDisplayVariBright1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetOptimizeBrightness(IADLXDisplayVariBright1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBalanced(IADLXDisplayVariBright1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetOptimizeBattery(IADLXDisplayVariBright1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMaximizeBattery(IADLXDisplayVariBright1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsBacklightAdaptiveSupported(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsBacklightAdaptiveEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBacklightAdaptiveEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool")] byte enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsBatteryLifeSupported(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsBatteryLifeEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBatteryLifeEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool")] byte enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsWindowsPowerModeSupported(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsWindowsPowerModeEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetWindowsPowerModeEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool")] byte enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsFullScreenVideoSupported(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsFullScreenVideoEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetFullScreenVideoEnabled(IADLXDisplayVariBright1* pThis, [NativeTypeName("adlx_bool")] byte enabled);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabled>((IntPtr)(lpVtbl[5]))(pThis, enabled);
        }
    }

    public ADLX_RESULT IsCurrentMaximizeBrightness([NativeTypeName("adlx_bool *")] bool* maximizeBrightness)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentMaximizeBrightness>((IntPtr)(lpVtbl[6]))(pThis, maximizeBrightness);
        }
    }

    public ADLX_RESULT IsCurrentOptimizeBrightness([NativeTypeName("adlx_bool *")] bool* optimizeBrightness)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentOptimizeBrightness>((IntPtr)(lpVtbl[7]))(pThis, optimizeBrightness);
        }
    }

    public ADLX_RESULT IsCurrentBalanced([NativeTypeName("adlx_bool *")] bool* balanced)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentBalanced>((IntPtr)(lpVtbl[8]))(pThis, balanced);
        }
    }

    public ADLX_RESULT IsCurrentOptimizeBattery([NativeTypeName("adlx_bool *")] bool* optimizeBattery)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentOptimizeBattery>((IntPtr)(lpVtbl[9]))(pThis, optimizeBattery);
        }
    }

    public ADLX_RESULT IsCurrentMaximizeBattery([NativeTypeName("adlx_bool *")] bool* maximizeBattery)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentMaximizeBattery>((IntPtr)(lpVtbl[10]))(pThis, maximizeBattery);
        }
    }

    public ADLX_RESULT SetMaximizeBrightness()
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMaximizeBrightness>((IntPtr)(lpVtbl[11]))(pThis);
        }
    }

    public ADLX_RESULT SetOptimizeBrightness()
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetOptimizeBrightness>((IntPtr)(lpVtbl[12]))(pThis);
        }
    }

    public ADLX_RESULT SetBalanced()
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBalanced>((IntPtr)(lpVtbl[13]))(pThis);
        }
    }

    public ADLX_RESULT SetOptimizeBattery()
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetOptimizeBattery>((IntPtr)(lpVtbl[14]))(pThis);
        }
    }

    public ADLX_RESULT SetMaximizeBattery()
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMaximizeBattery>((IntPtr)(lpVtbl[15]))(pThis);
        }
    }

    public ADLX_RESULT IsBacklightAdaptiveSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsBacklightAdaptiveSupported>((IntPtr)(lpVtbl[16]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsBacklightAdaptiveEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsBacklightAdaptiveEnabled>((IntPtr)(lpVtbl[17]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetBacklightAdaptiveEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBacklightAdaptiveEnabled>((IntPtr)(lpVtbl[18]))(pThis, enabled);
        }
    }

    public ADLX_RESULT IsBatteryLifeSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsBatteryLifeSupported>((IntPtr)(lpVtbl[19]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsBatteryLifeEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsBatteryLifeEnabled>((IntPtr)(lpVtbl[20]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetBatteryLifeEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBatteryLifeEnabled>((IntPtr)(lpVtbl[21]))(pThis, enabled);
        }
    }

    public ADLX_RESULT IsWindowsPowerModeSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsWindowsPowerModeSupported>((IntPtr)(lpVtbl[22]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsWindowsPowerModeEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsWindowsPowerModeEnabled>((IntPtr)(lpVtbl[23]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetWindowsPowerModeEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetWindowsPowerModeEnabled>((IntPtr)(lpVtbl[24]))(pThis, enabled);
        }
    }

    public ADLX_RESULT IsFullScreenVideoSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsFullScreenVideoSupported>((IntPtr)(lpVtbl[25]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsFullScreenVideoEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsFullScreenVideoEnabled>((IntPtr)(lpVtbl[26]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetFullScreenVideoEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        fixed (IADLXDisplayVariBright1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetFullScreenVideoEnabled>((IntPtr)(lpVtbl[27]))(pThis, enabled);
        }
    }
}
