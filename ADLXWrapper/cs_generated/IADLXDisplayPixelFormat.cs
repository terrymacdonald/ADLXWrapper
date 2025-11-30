using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplayPixelFormat
{
}

[NativeTypeName("struct IADLXDisplayPixelFormat : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayPixelFormat
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayPixelFormat* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayPixelFormat* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayPixelFormat* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported(IADLXDisplayPixelFormat* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetValue(IADLXDisplayPixelFormat* pThis, ADLX_PIXEL_FORMAT* pixelFormat);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetValue(IADLXDisplayPixelFormat* pThis, ADLX_PIXEL_FORMAT pixelFormat);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedPixelFormat(IADLXDisplayPixelFormat* pThis, ADLX_PIXEL_FORMAT pixelFormat, [NativeTypeName("adlx_bool *")] bool* supportd);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedRGB444Full(IADLXDisplayPixelFormat* pThis, [NativeTypeName("adlx_bool *")] bool* supportd);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedYCbCr444(IADLXDisplayPixelFormat* pThis, [NativeTypeName("adlx_bool *")] bool* supportd);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedYCbCr422(IADLXDisplayPixelFormat* pThis, [NativeTypeName("adlx_bool *")] bool* supportd);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedRGB444Limited(IADLXDisplayPixelFormat* pThis, [NativeTypeName("adlx_bool *")] bool* supportd);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedYCbCr420(IADLXDisplayPixelFormat* pThis, [NativeTypeName("adlx_bool *")] bool* supportd);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetValue(ADLX_PIXEL_FORMAT* pixelFormat)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetValue>((IntPtr)(lpVtbl[4]))(pThis, pixelFormat);
        }
    }

    public ADLX_RESULT SetValue(ADLX_PIXEL_FORMAT pixelFormat)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetValue>((IntPtr)(lpVtbl[5]))(pThis, pixelFormat);
        }
    }

    public ADLX_RESULT IsSupportedPixelFormat(ADLX_PIXEL_FORMAT pixelFormat, [NativeTypeName("adlx_bool *")] bool* supportd)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedPixelFormat>((IntPtr)(lpVtbl[6]))(pThis, pixelFormat, supportd);
        }
    }

    public ADLX_RESULT IsSupportedRGB444Full([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedRGB444Full>((IntPtr)(lpVtbl[7]))(pThis, supportd);
        }
    }

    public ADLX_RESULT IsSupportedYCbCr444([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedYCbCr444>((IntPtr)(lpVtbl[8]))(pThis, supportd);
        }
    }

    public ADLX_RESULT IsSupportedYCbCr422([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedYCbCr422>((IntPtr)(lpVtbl[9]))(pThis, supportd);
        }
    }

    public ADLX_RESULT IsSupportedRGB444Limited([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedRGB444Limited>((IntPtr)(lpVtbl[10]))(pThis, supportd);
        }
    }

    public ADLX_RESULT IsSupportedYCbCr420([NativeTypeName("adlx_bool *")] bool* supportd)
    {
        fixed (IADLXDisplayPixelFormat* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedYCbCr420>((IntPtr)(lpVtbl[11]))(pThis, supportd);
        }
    }
}
