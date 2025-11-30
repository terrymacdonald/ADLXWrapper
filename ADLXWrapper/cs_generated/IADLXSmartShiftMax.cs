using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXSmartShiftMax : adlx::IADLXInterface")]
public unsafe partial struct IADLXSmartShiftMax
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXSmartShiftMax* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXSmartShiftMax* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXSmartShiftMax* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXSmartShiftMax* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetBiasMode(IADLXSmartShiftMax* pThis, ADLX_SSM_BIAS_MODE* mode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBiasMode(IADLXSmartShiftMax* pThis, ADLX_SSM_BIAS_MODE mode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetBiasRange(IADLXSmartShiftMax* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetBias(IADLXSmartShiftMax* pThis, [NativeTypeName("adlx_int *")] int* bias);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBias(IADLXSmartShiftMax* pThis, [NativeTypeName("adlx_int")] int bias);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetBiasMode(ADLX_SSM_BIAS_MODE* mode)
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetBiasMode>((IntPtr)(lpVtbl[4]))(pThis, mode);
        }
    }

    public ADLX_RESULT SetBiasMode(ADLX_SSM_BIAS_MODE mode)
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBiasMode>((IntPtr)(lpVtbl[5]))(pThis, mode);
        }
    }

    public ADLX_RESULT GetBiasRange(ADLX_IntRange* range)
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetBiasRange>((IntPtr)(lpVtbl[6]))(pThis, range);
        }
    }

    public ADLX_RESULT GetBias([NativeTypeName("adlx_int *")] int* bias)
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetBias>((IntPtr)(lpVtbl[7]))(pThis, bias);
        }
    }

    public ADLX_RESULT SetBias([NativeTypeName("adlx_int")] int bias)
    {
        fixed (IADLXSmartShiftMax* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBias>((IntPtr)(lpVtbl[8]))(pThis, bias);
        }
    }
}
