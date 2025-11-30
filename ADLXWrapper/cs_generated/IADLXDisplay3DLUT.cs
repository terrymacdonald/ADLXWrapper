using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplay3DLUT : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplay3DLUT
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplay3DLUT* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplay3DLUT* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplay3DLUT* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedSCE(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedSCEVividGaming(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentSCEDisabled(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_bool *")] bool* sceDisabled);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentSCEVividGaming(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_bool *")] bool* vividGaming);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetSCEDisabled(IADLXDisplay3DLUT* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetSCEVividGaming(IADLXDisplay3DLUT* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedSCEDynamicContrast(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentSCEDynamicContrast(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_bool *")] bool* dynamicContrast);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSCEDynamicContrastRange(IADLXDisplay3DLUT* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSCEDynamicContrast(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_int *")] int* contrast);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetSCEDynamicContrast(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_int")] int contrast);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedUser3DLUT(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ClearUser3DLUT(IADLXDisplay3DLUT* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSDRUser3DLUT(IADLXDisplay3DLUT* pThis, ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetSDRUser3DLUT(IADLXDisplay3DLUT* pThis, ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetHDRUser3DLUT(IADLXDisplay3DLUT* pThis, ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetHDRUser3DLUT(IADLXDisplay3DLUT* pThis, ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetAllUser3DLUT(IADLXDisplay3DLUT* pThis, ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetAllUser3DLUT(IADLXDisplay3DLUT* pThis, ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetUser3DLUTIndex(IADLXDisplay3DLUT* pThis, [NativeTypeName("adlx_int")] int lutSize, [NativeTypeName("const ADLX_UINT16_RGB *")] ADLX_UINT16_RGB* rgbCoordinate, [NativeTypeName("adlx_int *")] int* index);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedSCE([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedSCE>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedSCEVividGaming([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedSCEVividGaming>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsCurrentSCEDisabled([NativeTypeName("adlx_bool *")] bool* sceDisabled)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentSCEDisabled>((IntPtr)(lpVtbl[5]))(pThis, sceDisabled);
        }
    }

    public ADLX_RESULT IsCurrentSCEVividGaming([NativeTypeName("adlx_bool *")] bool* vividGaming)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentSCEVividGaming>((IntPtr)(lpVtbl[6]))(pThis, vividGaming);
        }
    }

    public ADLX_RESULT SetSCEDisabled()
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetSCEDisabled>((IntPtr)(lpVtbl[7]))(pThis);
        }
    }

    public ADLX_RESULT SetSCEVividGaming()
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetSCEVividGaming>((IntPtr)(lpVtbl[8]))(pThis);
        }
    }

    public ADLX_RESULT IsSupportedSCEDynamicContrast([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedSCEDynamicContrast>((IntPtr)(lpVtbl[9]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsCurrentSCEDynamicContrast([NativeTypeName("adlx_bool *")] bool* dynamicContrast)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentSCEDynamicContrast>((IntPtr)(lpVtbl[10]))(pThis, dynamicContrast);
        }
    }

    public ADLX_RESULT GetSCEDynamicContrastRange(ADLX_IntRange* range)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSCEDynamicContrastRange>((IntPtr)(lpVtbl[11]))(pThis, range);
        }
    }

    public ADLX_RESULT GetSCEDynamicContrast([NativeTypeName("adlx_int *")] int* contrast)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSCEDynamicContrast>((IntPtr)(lpVtbl[12]))(pThis, contrast);
        }
    }

    public ADLX_RESULT SetSCEDynamicContrast([NativeTypeName("adlx_int")] int contrast)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetSCEDynamicContrast>((IntPtr)(lpVtbl[13]))(pThis, contrast);
        }
    }

    public ADLX_RESULT IsSupportedUser3DLUT([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedUser3DLUT>((IntPtr)(lpVtbl[14]))(pThis, supported);
        }
    }

    public ADLX_RESULT ClearUser3DLUT()
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ClearUser3DLUT>((IntPtr)(lpVtbl[15]))(pThis);
        }
    }

    public ADLX_RESULT GetSDRUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSDRUser3DLUT>((IntPtr)(lpVtbl[16]))(pThis, transferFunction, colorSpace, pointsNumber, data);
        }
    }

    public ADLX_RESULT SetSDRUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetSDRUser3DLUT>((IntPtr)(lpVtbl[17]))(pThis, transferFunction, colorSpace, pointsNumber, data);
        }
    }

    public ADLX_RESULT GetHDRUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetHDRUser3DLUT>((IntPtr)(lpVtbl[18]))(pThis, transferFunction, colorSpace, pointsNumber, data);
        }
    }

    public ADLX_RESULT SetHDRUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetHDRUser3DLUT>((IntPtr)(lpVtbl[19]))(pThis, transferFunction, colorSpace, pointsNumber, data);
        }
    }

    public ADLX_RESULT GetAllUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION* transferFunction, ADLX_3DLUT_COLORSPACE* colorSpace, [NativeTypeName("adlx_int *")] int* pointsNumber, ADLX_3DLUT_Data* data)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetAllUser3DLUT>((IntPtr)(lpVtbl[20]))(pThis, transferFunction, colorSpace, pointsNumber, data);
        }
    }

    public ADLX_RESULT SetAllUser3DLUT(ADLX_3DLUT_TRANSFER_FUNCTION transferFunction, ADLX_3DLUT_COLORSPACE colorSpace, [NativeTypeName("adlx_int")] int pointsNumber, [NativeTypeName("const ADLX_3DLUT_Data *")] ADLX_3DLUT_Data* data)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetAllUser3DLUT>((IntPtr)(lpVtbl[21]))(pThis, transferFunction, colorSpace, pointsNumber, data);
        }
    }

    public ADLX_RESULT GetUser3DLUTIndex([NativeTypeName("adlx_int")] int lutSize, [NativeTypeName("const ADLX_UINT16_RGB *")] ADLX_UINT16_RGB* rgbCoordinate, [NativeTypeName("adlx_int *")] int* index)
    {
        fixed (IADLXDisplay3DLUT* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetUser3DLUTIndex>((IntPtr)(lpVtbl[22]))(pThis, lutSize, rgbCoordinate, index);
        }
    }
}

public partial struct IADLXDisplay3DLUT
{
}
