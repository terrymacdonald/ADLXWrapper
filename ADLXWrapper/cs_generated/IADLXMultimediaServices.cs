using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXMultimediaServices : adlx::IADLXInterface")]
public unsafe partial struct IADLXMultimediaServices
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXMultimediaServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXMultimediaServices* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXMultimediaServices* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMultimediaChangedHandling(IADLXMultimediaServices* pThis, IADLXMultimediaChangedHandling** ppMultimediaChangedHandling);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetVideoUpscale(IADLXMultimediaServices* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXVideoUpscale** ppVideoupscale);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetVideoSuperResolution(IADLXMultimediaServices* pThis, [NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXVideoSuperResolution** ppVideoSuperResolution);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXMultimediaServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXMultimediaServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXMultimediaServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT GetMultimediaChangedHandling(IADLXMultimediaChangedHandling** ppMultimediaChangedHandling)
    {
        fixed (IADLXMultimediaServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMultimediaChangedHandling>((IntPtr)(lpVtbl[3]))(pThis, ppMultimediaChangedHandling);
        }
    }

    public ADLX_RESULT GetVideoUpscale([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXVideoUpscale** ppVideoupscale)
    {
        fixed (IADLXMultimediaServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetVideoUpscale>((IntPtr)(lpVtbl[4]))(pThis, pGPU, ppVideoupscale);
        }
    }

    public ADLX_RESULT GetVideoSuperResolution([NativeTypeName("adlx::IADLXGPU *")] IADLXGPU* pGPU, IADLXVideoSuperResolution** ppVideoSuperResolution)
    {
        fixed (IADLXMultimediaServices* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetVideoSuperResolution>((IntPtr)(lpVtbl[5]))(pThis, pGPU, ppVideoSuperResolution);
        }
    }
}

public partial struct IADLXMultimediaServices
{
}
