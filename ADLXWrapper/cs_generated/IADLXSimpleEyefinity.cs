using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSimpleEyefinity : adlx::IADLXInterface")]
public unsafe partial struct IADLXSimpleEyefinity
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXSimpleEyefinity* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXSimpleEyefinity* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXSimpleEyefinity* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXSimpleEyefinity* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Create(IADLXSimpleEyefinity* pThis, IADLXEyefinityDesktop** ppEyefinityDesktop);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DestroyAll(IADLXSimpleEyefinity* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Destroy(IADLXSimpleEyefinity* pThis, [NativeTypeName("adlx::IADLXEyefinityDesktop *")] IADLXEyefinityDesktop* pDesktop);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXSimpleEyefinity* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXSimpleEyefinity* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXSimpleEyefinity* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSimpleEyefinity* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT Create(IADLXEyefinityDesktop** ppEyefinityDesktop)
    {
        fixed (IADLXSimpleEyefinity* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Create>((IntPtr)(lpVtbl[4]))(pThis, ppEyefinityDesktop);
        }
    }

    public ADLX_RESULT DestroyAll()
    {
        fixed (IADLXSimpleEyefinity* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DestroyAll>((IntPtr)(lpVtbl[5]))(pThis);
        }
    }

    public ADLX_RESULT Destroy([NativeTypeName("adlx::IADLXEyefinityDesktop *")] IADLXEyefinityDesktop* pDesktop)
    {
        fixed (IADLXSimpleEyefinity* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Destroy>((IntPtr)(lpVtbl[6]))(pThis, pDesktop);
        }
    }
}
