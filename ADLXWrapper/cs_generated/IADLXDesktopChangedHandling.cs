using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDesktopChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXDesktopChangedHandling
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDesktopChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDesktopChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDesktopChangedHandling* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddDesktopListEventListener(IADLXDesktopChangedHandling* pThis, [NativeTypeName("adlx::IADLXDesktopListChangedListener *")] IADLXDesktopListChangedListener* pDesktopListChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveDesktopListEventListener(IADLXDesktopChangedHandling* pThis, [NativeTypeName("adlx::IADLXDesktopListChangedListener *")] IADLXDesktopListChangedListener* pDesktopListChangedListener);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDesktopChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDesktopChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDesktopChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT AddDesktopListEventListener([NativeTypeName("adlx::IADLXDesktopListChangedListener *")] IADLXDesktopListChangedListener* pDesktopListChangedListener)
    {
        fixed (IADLXDesktopChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddDesktopListEventListener>((IntPtr)(lpVtbl[3]))(pThis, pDesktopListChangedListener);
        }
    }

    public ADLX_RESULT RemoveDesktopListEventListener([NativeTypeName("adlx::IADLXDesktopListChangedListener *")] IADLXDesktopListChangedListener* pDesktopListChangedListener)
    {
        fixed (IADLXDesktopChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveDesktopListEventListener>((IntPtr)(lpVtbl[4]))(pThis, pDesktopListChangedListener);
        }
    }
}
