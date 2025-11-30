using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLX3DRadeonSuperResolution : adlx::IADLXInterface")]
public unsafe partial struct IADLX3DRadeonSuperResolution
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLX3DRadeonSuperResolution* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLX3DRadeonSuperResolution* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLX3DRadeonSuperResolution* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLX3DRadeonSuperResolution* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsEnabled(IADLX3DRadeonSuperResolution* pThis, [NativeTypeName("adlx_bool *")] bool* enabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetEnabled(IADLX3DRadeonSuperResolution* pThis, [NativeTypeName("adlx_bool")] byte enable);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSharpnessRange(IADLX3DRadeonSuperResolution* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSharpness(IADLX3DRadeonSuperResolution* pThis, [NativeTypeName("adlx_int *")] int* currentSharpness);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetSharpness(IADLX3DRadeonSuperResolution* pThis, [NativeTypeName("adlx_int")] int sharpness);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsEnabled([NativeTypeName("adlx_bool *")] bool* enabled)
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsEnabled>((IntPtr)(lpVtbl[4]))(pThis, enabled);
        }
    }

    public ADLX_RESULT SetEnabled([NativeTypeName("adlx_bool")] byte enable)
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetEnabled>((IntPtr)(lpVtbl[5]))(pThis, enable);
        }
    }

    public ADLX_RESULT GetSharpnessRange(ADLX_IntRange* range)
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSharpnessRange>((IntPtr)(lpVtbl[6]))(pThis, range);
        }
    }

    public ADLX_RESULT GetSharpness([NativeTypeName("adlx_int *")] int* currentSharpness)
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSharpness>((IntPtr)(lpVtbl[7]))(pThis, currentSharpness);
        }
    }

    public ADLX_RESULT SetSharpness([NativeTypeName("adlx_int")] int sharpness)
    {
        fixed (IADLX3DRadeonSuperResolution* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetSharpness>((IntPtr)(lpVtbl[8]))(pThis, sharpness);
        }
    }
}
