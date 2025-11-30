using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplay3DLUTChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXDisplay3DLUTChangedEvent
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplay3DLUTChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplay3DLUTChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplay3DLUTChangedEvent* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLXDisplay3DLUTChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetDisplay(IADLXDisplay3DLUTChangedEvent* pThis, IADLXDisplay** ppDisplay);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsSCEChanged(IADLXDisplay3DLUTChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsCustom3DLUTChanged(IADLXDisplay3DLUTChangedEvent* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplay3DLUTChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplay3DLUTChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplay3DLUTChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLXDisplay3DLUTChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    public ADLX_RESULT GetDisplay(IADLXDisplay** ppDisplay)
    {
        fixed (IADLXDisplay3DLUTChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetDisplay>((IntPtr)(lpVtbl[4]))(pThis, ppDisplay);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsSCEChanged()
    {
        fixed (IADLXDisplay3DLUTChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSCEChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsCustom3DLUTChanged()
    {
        fixed (IADLXDisplay3DLUTChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCustom3DLUTChanged>((IntPtr)(lpVtbl[6]))(pThis) != 0;
        }
    }
}
