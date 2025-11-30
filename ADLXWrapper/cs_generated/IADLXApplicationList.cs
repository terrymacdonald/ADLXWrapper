using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXApplicationList : adlx::IADLXList")]
public unsafe partial struct IADLXApplicationList
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXApplicationList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXApplicationList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXApplicationList* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Size(IADLXApplicationList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _Empty(IADLXApplicationList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Begin(IADLXApplicationList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _End(IADLXApplicationList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At(IADLXApplicationList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Clear(IADLXApplicationList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Remove_Back(IADLXApplicationList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back(IADLXApplicationList* pThis, [NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At1(IADLXApplicationList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXApplication** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back1(IADLXApplicationList* pThis, [NativeTypeName("adlx::IADLXApplication *")] IADLXApplication* pItem);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Size>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Empty>((IntPtr)(lpVtbl[4]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Begin>((IntPtr)(lpVtbl[5]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_End>((IntPtr)(lpVtbl[6]))(pThis);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At>((IntPtr)(lpVtbl[7]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Clear()
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Clear>((IntPtr)(lpVtbl[8]))(pThis);
        }
    }

    public ADLX_RESULT Remove_Back()
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Remove_Back>((IntPtr)(lpVtbl[9]))(pThis);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back>((IntPtr)(lpVtbl[10]))(pThis, pItem);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXApplication** ppItem)
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At1>((IntPtr)(lpVtbl[11]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXApplication *")] IADLXApplication* pItem)
    {
        fixed (IADLXApplicationList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back1>((IntPtr)(lpVtbl[12]))(pThis, pItem);
        }
    }
}

public partial struct IADLXApplicationList
{
}
