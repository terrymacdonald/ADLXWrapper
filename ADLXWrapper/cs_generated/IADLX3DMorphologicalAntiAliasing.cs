using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DMorphologicalAntiAliasing : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DMorphologicalAntiAliasing
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DMorphologicalAntiAliasing* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DMorphologicalAntiAliasing* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DMorphologicalAntiAliasing* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLX3DMorphologicalAntiAliasing* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLX3DMorphologicalAntiAliasing* pThis, [NativeTypeName("adlx_bool *")] bool* isEnabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabled(IADLX3DMorphologicalAntiAliasing* pThis, [NativeTypeName("adlx_bool")] byte enable);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DMorphologicalAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DMorphologicalAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DMorphologicalAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLX3DMorphologicalAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        fixed (IADLX3DMorphologicalAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, isEnabled);
        }
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        fixed (IADLX3DMorphologicalAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabled>((IntPtr)(lpVtbl[5]))(pThis, enable);
        }
    }
}
