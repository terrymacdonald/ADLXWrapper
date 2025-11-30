using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDesktopServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXDesktopServices
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDesktopServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDesktopServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDesktopServices* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetNumberOfDesktops(IADLXDesktopServices* pThis, [NativeTypeName("adlx_uint *")] uint* numDesktops);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDesktops(IADLXDesktopServices* pThis, IADLXDesktopList** ppDesktops);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDesktopChangedHandling(IADLXDesktopServices* pThis, IADLXDesktopChangedHandling** ppDesktopChangedHandling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSimpleEyefinity(IADLXDesktopServices* pThis, IADLXSimpleEyefinity** ppSimpleEyefinity);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDesktopServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDesktopServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDesktopServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetNumberOfDesktops([NativeTypeName("adlx_uint *")] uint* numDesktops)
    {
        fixed (IADLXDesktopServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetNumberOfDesktops>((IntPtr)(lpVtbl[3]))(pThis, numDesktops);
        }
    }

    public ADLX_RESULT GetDesktops(IADLXDesktopList** ppDesktops)
    {
        fixed (IADLXDesktopServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDesktops>((IntPtr)(lpVtbl[4]))(pThis, ppDesktops);
        }
    }

    public ADLX_RESULT GetDesktopChangedHandling(IADLXDesktopChangedHandling** ppDesktopChangedHandling)
    {
        fixed (IADLXDesktopServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDesktopChangedHandling>((IntPtr)(lpVtbl[5]))(pThis, ppDesktopChangedHandling);
        }
    }

    public ADLX_RESULT GetSimpleEyefinity(IADLXSimpleEyefinity** ppSimpleEyefinity)
    {
        fixed (IADLXDesktopServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSimpleEyefinity>((IntPtr)(lpVtbl[6]))(pThis, ppSimpleEyefinity);
        }
    }
}

public partial struct IADLXDesktopServices
{
}
