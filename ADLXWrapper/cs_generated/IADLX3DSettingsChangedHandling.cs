using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DSettingsChangedHandling : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DSettingsChangedHandling
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DSettingsChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DSettingsChangedHandling* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DSettingsChangedHandling* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add3DSettingsEventListener(IADLX3DSettingsChangedHandling* pThis, [NativeTypeName("adlx::IADLX3DSettingsChangedListener *")] IADLX3DSettingsChangedListener* p3DSettingsChangedListener);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Remove3DSettingsEventListener(IADLX3DSettingsChangedHandling* pThis, [NativeTypeName("adlx::IADLX3DSettingsChangedListener *")] IADLX3DSettingsChangedListener* p3DSettingsChangedListener);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DSettingsChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DSettingsChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DSettingsChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT Add3DSettingsEventListener([NativeTypeName("adlx::IADLX3DSettingsChangedListener *")] IADLX3DSettingsChangedListener* p3DSettingsChangedListener)
    {
        fixed (IADLX3DSettingsChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add3DSettingsEventListener>((IntPtr)(lpVtbl[3]))(pThis, p3DSettingsChangedListener);
        }
    }

    public ADLX_RESULT Remove3DSettingsEventListener([NativeTypeName("adlx::IADLX3DSettingsChangedListener *")] IADLX3DSettingsChangedListener* p3DSettingsChangedListener)
    {
        fixed (IADLX3DSettingsChangedHandling* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Remove3DSettingsEventListener>((IntPtr)(lpVtbl[4]))(pThis, p3DSettingsChangedListener);
        }
    }
}
