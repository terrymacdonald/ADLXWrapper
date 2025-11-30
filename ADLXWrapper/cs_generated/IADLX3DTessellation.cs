using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DTessellation : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DTessellation
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DTessellation* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DTessellation* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DTessellation* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLX3DTessellation* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMode(IADLX3DTessellation* pThis, ADLX_TESSELLATION_MODE* currentMode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetLevel(IADLX3DTessellation* pThis, ADLX_TESSELLATION_LEVEL* currentLevel);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMode(IADLX3DTessellation* pThis, ADLX_TESSELLATION_MODE mode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetLevel(IADLX3DTessellation* pThis, ADLX_TESSELLATION_LEVEL level);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DTessellation* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DTessellation* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DTessellation* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLX3DTessellation* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetMode(ADLX_TESSELLATION_MODE* currentMode)
    {
        fixed (IADLX3DTessellation* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMode>((IntPtr)(lpVtbl[4]))(pThis, currentMode);
        }
    }

    public ADLX_RESULT GetLevel(ADLX_TESSELLATION_LEVEL* currentLevel)
    {
        fixed (IADLX3DTessellation* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetLevel>((IntPtr)(lpVtbl[5]))(pThis, currentLevel);
        }
    }

    public ADLX_RESULT SetMode(ADLX_TESSELLATION_MODE mode)
    {
        fixed (IADLX3DTessellation* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMode>((IntPtr)(lpVtbl[6]))(pThis, mode);
        }
    }

    public ADLX_RESULT SetLevel(ADLX_TESSELLATION_LEVEL level)
    {
        fixed (IADLX3DTessellation* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetLevel>((IntPtr)(lpVtbl[7]))(pThis, level);
        }
    }
}
