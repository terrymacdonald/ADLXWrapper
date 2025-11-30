using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayChangedHandling
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayChangedHandling* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddDisplayListEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplayListChangedListener *")] IADLXDisplayListChangedListener* pDisplayListChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveDisplayListEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplayListChangedListener *")] IADLXDisplayListChangedListener* pDisplayListChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddDisplayGamutEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplayGamutChangedListener *")] IADLXDisplayGamutChangedListener* pDisplayGamutChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveDisplayGamutEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplayGamutChangedListener *")] IADLXDisplayGamutChangedListener* pDisplayGamutChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddDisplayGammaEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplayGammaChangedListener *")] IADLXDisplayGammaChangedListener* pDisplayGammaChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveDisplayGammaEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplayGammaChangedListener *")] IADLXDisplayGammaChangedListener* pDisplayGammaChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddDisplay3DLUTEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplay3DLUTChangedListener *")] IADLXDisplay3DLUTChangedListener* pDisplay3DLUTChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveDisplay3DLUTEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplay3DLUTChangedListener *")] IADLXDisplay3DLUTChangedListener* pDisplay3DLUTChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddDisplaySettingsEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplaySettingsChangedListener *")] IADLXDisplaySettingsChangedListener* pDisplaySettingsChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveDisplaySettingsEventListener(IADLXDisplayChangedHandling* pThis, [NativeTypeName("adlx::IADLXDisplaySettingsChangedListener *")] IADLXDisplaySettingsChangedListener* pDisplaySettingsChangedListener);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT AddDisplayListEventListener([NativeTypeName("adlx::IADLXDisplayListChangedListener *")] IADLXDisplayListChangedListener* pDisplayListChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddDisplayListEventListener>((IntPtr)(lpVtbl[3]))(pThis, pDisplayListChangedListener);
        }
    }

    public ADLX_RESULT RemoveDisplayListEventListener([NativeTypeName("adlx::IADLXDisplayListChangedListener *")] IADLXDisplayListChangedListener* pDisplayListChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveDisplayListEventListener>((IntPtr)(lpVtbl[4]))(pThis, pDisplayListChangedListener);
        }
    }

    public ADLX_RESULT AddDisplayGamutEventListener([NativeTypeName("adlx::IADLXDisplayGamutChangedListener *")] IADLXDisplayGamutChangedListener* pDisplayGamutChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddDisplayGamutEventListener>((IntPtr)(lpVtbl[5]))(pThis, pDisplayGamutChangedListener);
        }
    }

    public ADLX_RESULT RemoveDisplayGamutEventListener([NativeTypeName("adlx::IADLXDisplayGamutChangedListener *")] IADLXDisplayGamutChangedListener* pDisplayGamutChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveDisplayGamutEventListener>((IntPtr)(lpVtbl[6]))(pThis, pDisplayGamutChangedListener);
        }
    }

    public ADLX_RESULT AddDisplayGammaEventListener([NativeTypeName("adlx::IADLXDisplayGammaChangedListener *")] IADLXDisplayGammaChangedListener* pDisplayGammaChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddDisplayGammaEventListener>((IntPtr)(lpVtbl[7]))(pThis, pDisplayGammaChangedListener);
        }
    }

    public ADLX_RESULT RemoveDisplayGammaEventListener([NativeTypeName("adlx::IADLXDisplayGammaChangedListener *")] IADLXDisplayGammaChangedListener* pDisplayGammaChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveDisplayGammaEventListener>((IntPtr)(lpVtbl[8]))(pThis, pDisplayGammaChangedListener);
        }
    }

    public ADLX_RESULT AddDisplay3DLUTEventListener([NativeTypeName("adlx::IADLXDisplay3DLUTChangedListener *")] IADLXDisplay3DLUTChangedListener* pDisplay3DLUTChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddDisplay3DLUTEventListener>((IntPtr)(lpVtbl[9]))(pThis, pDisplay3DLUTChangedListener);
        }
    }

    public ADLX_RESULT RemoveDisplay3DLUTEventListener([NativeTypeName("adlx::IADLXDisplay3DLUTChangedListener *")] IADLXDisplay3DLUTChangedListener* pDisplay3DLUTChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveDisplay3DLUTEventListener>((IntPtr)(lpVtbl[10]))(pThis, pDisplay3DLUTChangedListener);
        }
    }

    public ADLX_RESULT AddDisplaySettingsEventListener([NativeTypeName("adlx::IADLXDisplaySettingsChangedListener *")] IADLXDisplaySettingsChangedListener* pDisplaySettingsChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddDisplaySettingsEventListener>((IntPtr)(lpVtbl[11]))(pThis, pDisplaySettingsChangedListener);
        }
    }

    public ADLX_RESULT RemoveDisplaySettingsEventListener([NativeTypeName("adlx::IADLXDisplaySettingsChangedListener *")] IADLXDisplaySettingsChangedListener* pDisplaySettingsChangedListener)
    {
        fixed (IADLXDisplayChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveDisplaySettingsEventListener>((IntPtr)(lpVtbl[12]))(pThis, pDisplaySettingsChangedListener);
        }
    }
}
