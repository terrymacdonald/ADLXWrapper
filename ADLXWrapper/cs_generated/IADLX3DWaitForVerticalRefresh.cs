using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DWaitForVerticalRefresh : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DWaitForVerticalRefresh
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DWaitForVerticalRefresh* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DWaitForVerticalRefresh* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DWaitForVerticalRefresh* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLX3DWaitForVerticalRefresh* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLX3DWaitForVerticalRefresh* pThis, [NativeTypeName("adlx_bool *")] bool* isEnabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMode(IADLX3DWaitForVerticalRefresh* pThis, ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE* currentMode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMode(IADLX3DWaitForVerticalRefresh* pThis, ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DWaitForVerticalRefresh* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DWaitForVerticalRefresh* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DWaitForVerticalRefresh* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLX3DWaitForVerticalRefresh* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* isEnabled)
    {
        fixed (IADLX3DWaitForVerticalRefresh* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, isEnabled);
        }
    }

    public ADLX_RESULT GetMode(ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE* currentMode)
    {
        fixed (IADLX3DWaitForVerticalRefresh* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMode>((IntPtr)(lpVtbl[5]))(pThis, currentMode);
        }
    }

    public ADLX_RESULT SetMode(ADLX_WAIT_FOR_VERTICAL_REFRESH_MODE mode)
    {
        fixed (IADLX3DWaitForVerticalRefresh* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMode>((IntPtr)(lpVtbl[6]))(pThis, mode);
        }
    }
}
