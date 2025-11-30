using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplayScalingMode
{
}

[NativeTypeName("struct IADLXDisplayScalingMode : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayScalingMode
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayScalingMode* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayScalingMode* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayScalingMode* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXDisplayScalingMode* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetMode(IADLXDisplayScalingMode* pThis, ADLX_SCALE_MODE* currentMode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetMode(IADLXDisplayScalingMode* pThis, ADLX_SCALE_MODE mode);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayScalingMode* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayScalingMode* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayScalingMode* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayScalingMode* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetMode(ADLX_SCALE_MODE* currentMode)
    {
        fixed (IADLXDisplayScalingMode* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetMode>((IntPtr)(lpVtbl[4]))(pThis, currentMode);
        }
    }

    public ADLX_RESULT SetMode(ADLX_SCALE_MODE mode)
    {
        fixed (IADLXDisplayScalingMode* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetMode>((IntPtr)(lpVtbl[5]))(pThis, mode);
        }
    }
}
