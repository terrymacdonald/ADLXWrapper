using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplayCustomResolution
{
}

[NativeTypeName("struct IADLXDisplayCustomResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayCustomResolution
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayCustomResolution* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayCustomResolution* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayCustomResolution* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXDisplayCustomResolution* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetResolutionList(IADLXDisplayCustomResolution* pThis, IADLXDisplayResolutionList** ppResolutionList);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetCurrentAppliedResolution(IADLXDisplayCustomResolution* pThis, IADLXDisplayResolution** ppResolution);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _CreateNewResolution(IADLXDisplayCustomResolution* pThis, [NativeTypeName("adlx::IADLXDisplayResolution *")] IADLXDisplayResolution* pResolution);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DeleteResolution(IADLXDisplayCustomResolution* pThis, [NativeTypeName("adlx::IADLXDisplayResolution *")] IADLXDisplayResolution* pResolution);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayCustomResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayCustomResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayCustomResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayCustomResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetResolutionList(IADLXDisplayResolutionList** ppResolutionList)
    {
        fixed (IADLXDisplayCustomResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetResolutionList>((IntPtr)(lpVtbl[4]))(pThis, ppResolutionList);
        }
    }

    public ADLX_RESULT GetCurrentAppliedResolution(IADLXDisplayResolution** ppResolution)
    {
        fixed (IADLXDisplayCustomResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetCurrentAppliedResolution>((IntPtr)(lpVtbl[5]))(pThis, ppResolution);
        }
    }

    public ADLX_RESULT CreateNewResolution([NativeTypeName("adlx::IADLXDisplayResolution *")] IADLXDisplayResolution* pResolution)
    {
        fixed (IADLXDisplayCustomResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_CreateNewResolution>((IntPtr)(lpVtbl[6]))(pThis, pResolution);
        }
    }

    public ADLX_RESULT DeleteResolution([NativeTypeName("adlx::IADLXDisplayResolution *")] IADLXDisplayResolution* pResolution)
    {
        fixed (IADLXDisplayCustomResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DeleteResolution>((IntPtr)(lpVtbl[7]))(pThis, pResolution);
        }
    }
}
