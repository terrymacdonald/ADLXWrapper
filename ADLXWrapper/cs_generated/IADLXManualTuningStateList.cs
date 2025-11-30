using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXManualTuningStateList
{
}

public partial struct IADLXManualTuningStateList
{
}

[NativeTypeName("struct IADLXManualTuningStateList : adlx::IADLXList")]
public unsafe partial struct IADLXManualTuningStateList
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualTuningStateList* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Size(IADLXManualTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _Empty(IADLXManualTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Begin(IADLXManualTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _End(IADLXManualTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At(IADLXManualTuningStateList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Clear(IADLXManualTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Remove_Back(IADLXManualTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back(IADLXManualTuningStateList* pThis, [NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At1(IADLXManualTuningStateList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXManualTuningState** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back1(IADLXManualTuningStateList* pThis, [NativeTypeName("adlx::IADLXManualTuningState *")] IADLXManualTuningState* pItem);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Size>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Empty>((IntPtr)(lpVtbl[4]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Begin>((IntPtr)(lpVtbl[5]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_End>((IntPtr)(lpVtbl[6]))(pThis);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At>((IntPtr)(lpVtbl[7]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Clear()
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Clear>((IntPtr)(lpVtbl[8]))(pThis);
        }
    }

    public ADLX_RESULT Remove_Back()
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Remove_Back>((IntPtr)(lpVtbl[9]))(pThis);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back>((IntPtr)(lpVtbl[10]))(pThis, pItem);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXManualTuningState** ppItem)
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At1>((IntPtr)(lpVtbl[11]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXManualTuningState *")] IADLXManualTuningState* pItem)
    {
        fixed (IADLXManualTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back1>((IntPtr)(lpVtbl[12]))(pThis, pItem);
        }
    }
}
