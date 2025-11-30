using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DAntiAliasing : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DAntiAliasing
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DAntiAliasing* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DAntiAliasing* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DAntiAliasing* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLX3DAntiAliasing* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMode(IADLX3DAntiAliasing* pThis, ADLX_ANTI_ALIASING_MODE* currentMode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetLevel(IADLX3DAntiAliasing* pThis, ADLX_ANTI_ALIASING_LEVEL* currentLevel);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMethod(IADLX3DAntiAliasing* pThis, ADLX_ANTI_ALIASING_METHOD* currentMethod);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMode(IADLX3DAntiAliasing* pThis, ADLX_ANTI_ALIASING_MODE mode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetLevel(IADLX3DAntiAliasing* pThis, ADLX_ANTI_ALIASING_LEVEL level);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMethod(IADLX3DAntiAliasing* pThis, ADLX_ANTI_ALIASING_METHOD method);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetMode(ADLX_ANTI_ALIASING_MODE* currentMode)
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMode>((IntPtr)(lpVtbl[4]))(pThis, currentMode);
        }
    }

    public ADLX_RESULT GetLevel(ADLX_ANTI_ALIASING_LEVEL* currentLevel)
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetLevel>((IntPtr)(lpVtbl[5]))(pThis, currentLevel);
        }
    }

    public ADLX_RESULT GetMethod(ADLX_ANTI_ALIASING_METHOD* currentMethod)
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMethod>((IntPtr)(lpVtbl[6]))(pThis, currentMethod);
        }
    }

    public ADLX_RESULT SetMode(ADLX_ANTI_ALIASING_MODE mode)
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMode>((IntPtr)(lpVtbl[7]))(pThis, mode);
        }
    }

    public ADLX_RESULT SetLevel(ADLX_ANTI_ALIASING_LEVEL level)
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetLevel>((IntPtr)(lpVtbl[8]))(pThis, level);
        }
    }

    public ADLX_RESULT SetMethod(ADLX_ANTI_ALIASING_METHOD method)
    {
        fixed (IADLX3DAntiAliasing* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMethod>((IntPtr)(lpVtbl[9]))(pThis, method);
        }
    }
}
