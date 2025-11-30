using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXAllMetrics : adlx::IADLXInterface")]
public unsafe partial struct IADLXAllMetrics
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXAllMetrics* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXAllMetrics* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXAllMetrics* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _TimeStamp(IADLXAllMetrics* pThis, [NativeTypeName("adlx_int64 *")] long* ms);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSystemMetrics(IADLXAllMetrics* pThis, IADLXSystemMetrics** ppSystemMetrics);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetFPS(IADLXAllMetrics* pThis, IADLXFPS** ppFPS);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUMetrics(IADLXAllMetrics* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetrics** ppGPUMetrics);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXAllMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXAllMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXAllMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT TimeStamp([NativeTypeName("adlx_int64 *")] long* ms)
    {
        fixed (IADLXAllMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_TimeStamp>((IntPtr)(lpVtbl[3]))(pThis, ms);
        }
    }

    public ADLX_RESULT GetSystemMetrics(IADLXSystemMetrics** ppSystemMetrics)
    {
        fixed (IADLXAllMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSystemMetrics>((IntPtr)(lpVtbl[4]))(pThis, ppSystemMetrics);
        }
    }

    public ADLX_RESULT GetFPS(IADLXFPS** ppFPS)
    {
        fixed (IADLXAllMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetFPS>((IntPtr)(lpVtbl[5]))(pThis, ppFPS);
        }
    }

    public ADLX_RESULT GetGPUMetrics([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXGPUMetrics** ppGPUMetrics)
    {
        fixed (IADLXAllMetrics* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUMetrics>((IntPtr)(lpVtbl[6]))(pThis, pGPU, ppGPUMetrics);
        }
    }
}
