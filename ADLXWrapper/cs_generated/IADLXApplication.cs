using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXApplication : adlx::IADLXInterface")]
public unsafe partial struct IADLXApplication
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXApplication* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXApplication* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXApplication* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ProcessID(IADLXApplication* pThis, [NativeTypeName("adlx_ulong *")] uint* pid);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Name(IADLXApplication* pThis, [NativeTypeName("const wchar_t **")] ushort** ppAppName);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _FullPath(IADLXApplication* pThis, [NativeTypeName("const wchar_t **")] ushort** ppAppPath);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GPUDependencyType(IADLXApplication* pThis, ADLX_APP_GPU_DEPENDENCY* gpuDependency);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXApplication* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXApplication* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXApplication* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT ProcessID([NativeTypeName("adlx_ulong *")] uint* pid)
    {
        fixed (IADLXApplication* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ProcessID>((IntPtr)(lpVtbl[3]))(pThis, pid);
        }
    }

    public ADLX_RESULT Name([NativeTypeName("const wchar_t **")] ushort** ppAppName)
    {
        fixed (IADLXApplication* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Name>((IntPtr)(lpVtbl[4]))(pThis, ppAppName);
        }
    }

    public ADLX_RESULT FullPath([NativeTypeName("const wchar_t **")] ushort** ppAppPath)
    {
        fixed (IADLXApplication* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_FullPath>((IntPtr)(lpVtbl[5]))(pThis, ppAppPath);
        }
    }

    public ADLX_RESULT GPUDependencyType(ADLX_APP_GPU_DEPENDENCY* gpuDependency)
    {
        fixed (IADLXApplication* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GPUDependencyType>((IntPtr)(lpVtbl[6]))(pThis, gpuDependency);
        }
    }
}
