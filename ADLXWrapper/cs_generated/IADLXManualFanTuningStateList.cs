using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXManualFanTuningStateList : adlx::IADLXList")]
public unsafe partial struct IADLXManualFanTuningStateList
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXManualFanTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXManualFanTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXManualFanTuningStateList* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Size(IADLXManualFanTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _Empty(IADLXManualFanTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _Begin(IADLXManualFanTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_uint")]
    public delegate uint _End(IADLXManualFanTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At(IADLXManualFanTuningStateList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Clear(IADLXManualFanTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Remove_Back(IADLXManualFanTuningStateList* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back(IADLXManualFanTuningStateList* pThis, [NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _At1(IADLXManualFanTuningStateList* pThis, [NativeTypeName("const adlx_uint")] uint location, IADLXManualFanTuningState** ppItem);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Add_Back1(IADLXManualFanTuningStateList* pThis, [NativeTypeName("adlx::IADLXManualFanTuningState *")] IADLXManualFanTuningState* pItem);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Size()
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Size>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool Empty()
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Empty>((IntPtr)(lpVtbl[4]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint Begin()
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Begin>((IntPtr)(lpVtbl[5]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_uint")]
    public uint End()
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_End>((IntPtr)(lpVtbl[6]))(pThis);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXInterface** ppItem)
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At>((IntPtr)(lpVtbl[7]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Clear()
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Clear>((IntPtr)(lpVtbl[8]))(pThis);
        }
    }

    public ADLX_RESULT Remove_Back()
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Remove_Back>((IntPtr)(lpVtbl[9]))(pThis);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXInterface *")] IADLXInterface* pItem)
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back>((IntPtr)(lpVtbl[10]))(pThis, pItem);
        }
    }

    public ADLX_RESULT At([NativeTypeName("const adlx_uint")] uint location, IADLXManualFanTuningState** ppItem)
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_At1>((IntPtr)(lpVtbl[11]))(pThis, location, ppItem);
        }
    }

    public ADLX_RESULT Add_Back([NativeTypeName("adlx::IADLXManualFanTuningState *")] IADLXManualFanTuningState* pItem)
    {
        fixed (IADLXManualFanTuningStateList* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Add_Back1>((IntPtr)(lpVtbl[12]))(pThis, pItem);
        }
    }
}
