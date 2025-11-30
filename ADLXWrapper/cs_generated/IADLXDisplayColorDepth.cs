using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplayColorDepth
{
}

[NativeTypeName("struct IADLXDisplayColorDepth : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayColorDepth
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayColorDepth* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayColorDepth* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayColorDepth* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXDisplayColorDepth* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetValue(IADLXDisplayColorDepth* pThis, ADLX_COLOR_DEPTH* currentColorDepth);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetValue(IADLXDisplayColorDepth* pThis, ADLX_COLOR_DEPTH colorDepth);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedColorDepth(IADLXDisplayColorDepth* pThis, ADLX_COLOR_DEPTH colorDepth, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedBPC_6(IADLXDisplayColorDepth* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedBPC_8(IADLXDisplayColorDepth* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedBPC_10(IADLXDisplayColorDepth* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedBPC_12(IADLXDisplayColorDepth* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedBPC_14(IADLXDisplayColorDepth* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedBPC_16(IADLXDisplayColorDepth* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetValue(ADLX_COLOR_DEPTH* currentColorDepth)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetValue>((IntPtr)(lpVtbl[4]))(pThis, currentColorDepth);
        }
    }

    public ADLX_RESULT SetValue(ADLX_COLOR_DEPTH colorDepth)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetValue>((IntPtr)(lpVtbl[5]))(pThis, colorDepth);
        }
    }

    public ADLX_RESULT IsSupportedColorDepth(ADLX_COLOR_DEPTH colorDepth, [NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedColorDepth>((IntPtr)(lpVtbl[6]))(pThis, colorDepth, supported);
        }
    }

    public ADLX_RESULT IsSupportedBPC_6([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedBPC_6>((IntPtr)(lpVtbl[7]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedBPC_8([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedBPC_8>((IntPtr)(lpVtbl[8]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedBPC_10([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedBPC_10>((IntPtr)(lpVtbl[9]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedBPC_12([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedBPC_12>((IntPtr)(lpVtbl[10]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedBPC_14([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedBPC_14>((IntPtr)(lpVtbl[11]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedBPC_16([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayColorDepth* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedBPC_16>((IntPtr)(lpVtbl[12]))(pThis, supported);
        }
    }
}
