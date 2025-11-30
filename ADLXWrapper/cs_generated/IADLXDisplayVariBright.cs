using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplayVariBright
{
}

[NativeTypeName("struct IADLXDisplayVariBright : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayVariBright
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayVariBright* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayVariBright* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayVariBright* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXDisplayVariBright* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLXDisplayVariBright* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabled(IADLXDisplayVariBright* pThis, [NativeTypeName("adlx_bool")] byte enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentMaximizeBrightness(IADLXDisplayVariBright* pThis, [NativeTypeName("adlx_bool *")] bool* maximizeBrightness);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentOptimizeBrightness(IADLXDisplayVariBright* pThis, [NativeTypeName("adlx_bool *")] bool* optimizeBrightness);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentBalanced(IADLXDisplayVariBright* pThis, [NativeTypeName("adlx_bool *")] bool* balanced);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentOptimizeBattery(IADLXDisplayVariBright* pThis, [NativeTypeName("adlx_bool *")] bool* optimizeBattery);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentMaximizeBattery(IADLXDisplayVariBright* pThis, [NativeTypeName("adlx_bool *")] bool* maximizeBattery);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMaximizeBrightness(IADLXDisplayVariBright* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetOptimizeBrightness(IADLXDisplayVariBright* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBalanced(IADLXDisplayVariBright* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetOptimizeBattery(IADLXDisplayVariBright* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMaximizeBattery(IADLXDisplayVariBright* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enabled)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabled>((IntPtr)(lpVtbl[5]))(pThis, enabled);
        }
    }

    public ADLX_RESULT IsCurrentMaximizeBrightness([NativeTypeName("adlx_bool *")] bool* maximizeBrightness)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentMaximizeBrightness>((IntPtr)(lpVtbl[6]))(pThis, maximizeBrightness);
        }
    }

    public ADLX_RESULT IsCurrentOptimizeBrightness([NativeTypeName("adlx_bool *")] bool* optimizeBrightness)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentOptimizeBrightness>((IntPtr)(lpVtbl[7]))(pThis, optimizeBrightness);
        }
    }

    public ADLX_RESULT IsCurrentBalanced([NativeTypeName("adlx_bool *")] bool* balanced)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentBalanced>((IntPtr)(lpVtbl[8]))(pThis, balanced);
        }
    }

    public ADLX_RESULT IsCurrentOptimizeBattery([NativeTypeName("adlx_bool *")] bool* optimizeBattery)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentOptimizeBattery>((IntPtr)(lpVtbl[9]))(pThis, optimizeBattery);
        }
    }

    public ADLX_RESULT IsCurrentMaximizeBattery([NativeTypeName("adlx_bool *")] bool* maximizeBattery)
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentMaximizeBattery>((IntPtr)(lpVtbl[10]))(pThis, maximizeBattery);
        }
    }

    public ADLX_RESULT SetMaximizeBrightness()
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMaximizeBrightness>((IntPtr)(lpVtbl[11]))(pThis);
        }
    }

    public ADLX_RESULT SetOptimizeBrightness()
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetOptimizeBrightness>((IntPtr)(lpVtbl[12]))(pThis);
        }
    }

    public ADLX_RESULT SetBalanced()
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBalanced>((IntPtr)(lpVtbl[13]))(pThis);
        }
    }

    public ADLX_RESULT SetOptimizeBattery()
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetOptimizeBattery>((IntPtr)(lpVtbl[14]))(pThis);
        }
    }

    public ADLX_RESULT SetMaximizeBattery()
    {
        fixed (IADLXDisplayVariBright* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMaximizeBattery>((IntPtr)(lpVtbl[15]))(pThis);
        }
    }
}
