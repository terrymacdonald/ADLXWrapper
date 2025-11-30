using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUAutoTuningCompleteEvent : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUAutoTuningCompleteEvent
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUAutoTuningCompleteEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUAutoTuningCompleteEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUAutoTuningCompleteEvent* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsUndervoltGPUCompleted(IADLXGPUAutoTuningCompleteEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsOverclockGPUCompleted(IADLXGPUAutoTuningCompleteEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsOverclockVRAMCompleted(IADLXGPUAutoTuningCompleteEvent* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUAutoTuningCompleteEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUAutoTuningCompleteEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUAutoTuningCompleteEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsUndervoltGPUCompleted()
    {
        fixed (IADLXGPUAutoTuningCompleteEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsUndervoltGPUCompleted>((IntPtr)(lpVtbl[3]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsOverclockGPUCompleted()
    {
        fixed (IADLXGPUAutoTuningCompleteEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsOverclockGPUCompleted>((IntPtr)(lpVtbl[4]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsOverclockVRAMCompleted()
    {
        fixed (IADLXGPUAutoTuningCompleteEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsOverclockVRAMCompleted>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }
}
