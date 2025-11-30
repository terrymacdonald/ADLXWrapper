using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXMultimediaChangedEvent : adlx::IADLXChangedEvent")]
public unsafe partial struct IADLXMultimediaChangedEvent
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXMultimediaChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXMultimediaChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXMultimediaChangedEvent* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_SYNC_ORIGIN _GetOrigin(IADLXMultimediaChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPU(IADLXMultimediaChangedEvent* pThis, IADLXGPU** ppGPU);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsVideoUpscaleChanged(IADLXMultimediaChangedEvent* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_bool")]
    public delegate byte _IsVideoSuperResolutionChanged(IADLXMultimediaChangedEvent* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXMultimediaChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXMultimediaChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXMultimediaChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_SYNC_ORIGIN GetOrigin()
    {
        fixed (IADLXMultimediaChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetOrigin>((IntPtr)(lpVtbl[3]))(pThis);
        }
    }

    public ADLX_RESULT GetGPU(IADLXGPU** ppGPU)
    {
        fixed (IADLXMultimediaChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPU>((IntPtr)(lpVtbl[4]))(pThis, ppGPU);
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVideoUpscaleChanged()
    {
        fixed (IADLXMultimediaChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsVideoUpscaleChanged>((IntPtr)(lpVtbl[5]))(pThis) != 0;
        }
    }

    [return: NativeTypeName("adlx_bool")]
    public bool IsVideoSuperResolutionChanged()
    {
        fixed (IADLXMultimediaChangedEvent* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsVideoSuperResolutionChanged>((IntPtr)(lpVtbl[6]))(pThis) != 0;
        }
    }
}
