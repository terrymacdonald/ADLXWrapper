using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXMultimediaChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLXMultimediaChangedHandling
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXMultimediaChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXMultimediaChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXMultimediaChangedHandling* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AddMultimediaEventListener(IADLXMultimediaChangedHandling* pThis, [NativeTypeName("adlx::IADLXMultimediaChangedEventListener *")] IADLXMultimediaChangedEventListener* pMultimediaChangedEventListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RemoveMultimediaEventListener(IADLXMultimediaChangedHandling* pThis, [NativeTypeName("adlx::IADLXMultimediaChangedEventListener *")] IADLXMultimediaChangedEventListener* pMultimediaChangedEventListener);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXMultimediaChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXMultimediaChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXMultimediaChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT AddMultimediaEventListener([NativeTypeName("adlx::IADLXMultimediaChangedEventListener *")] IADLXMultimediaChangedEventListener* pMultimediaChangedEventListener)
    {
        fixed (IADLXMultimediaChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AddMultimediaEventListener>((IntPtr)(lpVtbl[3]))(pThis, pMultimediaChangedEventListener);
        }
    }

    public ADLX_RESULT RemoveMultimediaEventListener([NativeTypeName("adlx::IADLXMultimediaChangedEventListener *")] IADLXMultimediaChangedEventListener* pMultimediaChangedEventListener)
    {
        fixed (IADLXMultimediaChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RemoveMultimediaEventListener>((IntPtr)(lpVtbl[4]))(pThis, pMultimediaChangedEventListener);
        }
    }
}
