using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXGPU2List
{
}

[NativeTypeName("struct IADLXGPU2List : adlx::IADLXList")]
public unsafe partial struct IADLXGPU2List
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPU2List* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPU2List* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPU2List* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Size(IADLXGPU2List* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _Empty(IADLXGPU2List* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Begin(IADLXGPU2List* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _End(IADLXGPU2List* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At(IADLXGPU2List* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Clear(IADLXGPU2List* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Remove_Back(IADLXGPU2List* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back(IADLXGPU2List* pThis, [NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At1(IADLXGPU2List* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXGPU2** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back1(IADLXGPU2List* pThis, [NativeTypeName("adlx::IADLXGPU2 *")] IADLXGPU2* pItem);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Size>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Empty>((IntPtr)(lpVtbl[4]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Begin>((IntPtr)(lpVtbl[5]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_End>((IntPtr)(lpVtbl[6]))(pThis);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At>((IntPtr)(lpVtbl[7]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Clear()
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Clear>((IntPtr)(lpVtbl[8]))(pThis);
        }
    }

    public ADLX_RESULT Remove_Back()
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Remove_Back>((IntPtr)(lpVtbl[9]))(pThis);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back>((IntPtr)(lpVtbl[10]))(pThis, pItem);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXGPU2** ppItem)
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At1>((IntPtr)(lpVtbl[11]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXGPU2 *")] IADLXGPU2* pItem)
    {
        fixed (IADLXGPU2List* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back1>((IntPtr)(lpVtbl[12]))(pThis, pItem);
        }
    }
}
