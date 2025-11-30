using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDesktopList : adlx::IADLXList")]
public unsafe partial struct IADLXDesktopList
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDesktopList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDesktopList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDesktopList* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Size(IADLXDesktopList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _Empty(IADLXDesktopList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Begin(IADLXDesktopList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _End(IADLXDesktopList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At(IADLXDesktopList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Clear(IADLXDesktopList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Remove_Back(IADLXDesktopList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back(IADLXDesktopList* pThis, [NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At1(IADLXDesktopList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXDesktop** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back1(IADLXDesktopList* pThis, [NativeTypeName("adlx::IADLXDesktop *")] IADLXDesktop* pItem);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Size>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Empty>((IntPtr)(lpVtbl[4]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Begin>((IntPtr)(lpVtbl[5]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_End>((IntPtr)(lpVtbl[6]))(pThis);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At>((IntPtr)(lpVtbl[7]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Clear()
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Clear>((IntPtr)(lpVtbl[8]))(pThis);
        }
    }

    public ADLX_RESULT Remove_Back()
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Remove_Back>((IntPtr)(lpVtbl[9]))(pThis);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back>((IntPtr)(lpVtbl[10]))(pThis, pItem);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXDesktop** ppItem)
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At1>((IntPtr)(lpVtbl[11]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXDesktop *")] IADLXDesktop* pItem)
    {
        fixed (IADLXDesktopList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back1>((IntPtr)(lpVtbl[12]))(pThis, pItem);
        }
    }
}
