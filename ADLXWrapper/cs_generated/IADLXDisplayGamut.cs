using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayGamut : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayGamut
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayGamut* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayGamut* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayGamut* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedCCIR709ColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedCCIR601ColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedAdobeRgbColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedCIERgbColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedCCIR2020ColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedCustomColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported5000kWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported6500kWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported7500kWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupported9300kWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedCustomWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrent5000kWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrent6500kWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrent7500kWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrent9300kWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentCustomWhitePoint(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetWhitePoint(IADLXDisplayGamut* pThis, ADLX_Point* point);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentCCIR709ColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentCCIR601ColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentAdobeRgbColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentCIERgbColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentCCIR2020ColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentCustomColorSpace(IADLXDisplayGamut* pThis, [NativeTypeName("adlx_bool *")] bool* isSet);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGamutColorSpace(IADLXDisplayGamut* pThis, ADLX_GamutColorSpace* gamutColorSpace);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetGamut(IADLXDisplayGamut* pThis, ADLX_RGB customWhitePoint, ADLX_GamutColorSpace customGamut);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetGamut1(IADLXDisplayGamut* pThis, ADLX_WHITE_POINT predefinedWhitePoint, ADLX_GamutColorSpace customGamut);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetGamut2(IADLXDisplayGamut* pThis, ADLX_RGB customWhitePoint, ADLX_GAMUT_SPACE predefinedGamutSpace);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetGamut3(IADLXDisplayGamut* pThis, ADLX_WHITE_POINT predefinedWhitePoint, ADLX_GAMUT_SPACE predefinedGamutSpace);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public readonly ADLX_RESULT IsSupportedCCIR709ColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedCCIR709ColorSpace>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupportedCCIR601ColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedCCIR601ColorSpace>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupportedAdobeRgbColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedAdobeRgbColorSpace>((IntPtr)(lpVtbl[5]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupportedCIERgbColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedCIERgbColorSpace>((IntPtr)(lpVtbl[6]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupportedCCIR2020ColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedCCIR2020ColorSpace>((IntPtr)(lpVtbl[7]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupportedCustomColorSpace([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedCustomColorSpace>((IntPtr)(lpVtbl[8]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupported5000kWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported5000kWhitePoint>((IntPtr)(lpVtbl[9]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupported6500kWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported6500kWhitePoint>((IntPtr)(lpVtbl[10]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupported7500kWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported7500kWhitePoint>((IntPtr)(lpVtbl[11]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupported9300kWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupported9300kWhitePoint>((IntPtr)(lpVtbl[12]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsSupportedCustomWhitePoint([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedCustomWhitePoint>((IntPtr)(lpVtbl[13]))(pThis, supported);
        }
    }

    public readonly ADLX_RESULT IsCurrent5000kWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrent5000kWhitePoint>((IntPtr)(lpVtbl[14]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrent6500kWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrent6500kWhitePoint>((IntPtr)(lpVtbl[15]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrent7500kWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrent7500kWhitePoint>((IntPtr)(lpVtbl[16]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrent9300kWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrent9300kWhitePoint>((IntPtr)(lpVtbl[17]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrentCustomWhitePoint([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentCustomWhitePoint>((IntPtr)(lpVtbl[18]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT GetWhitePoint(ADLX_Point* point)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetWhitePoint>((IntPtr)(lpVtbl[19]))(pThis, point);
        }
    }

    public readonly ADLX_RESULT IsCurrentCCIR709ColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentCCIR709ColorSpace>((IntPtr)(lpVtbl[20]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrentCCIR601ColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentCCIR601ColorSpace>((IntPtr)(lpVtbl[21]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrentAdobeRgbColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentAdobeRgbColorSpace>((IntPtr)(lpVtbl[22]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrentCIERgbColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentCIERgbColorSpace>((IntPtr)(lpVtbl[23]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrentCCIR2020ColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentCCIR2020ColorSpace>((IntPtr)(lpVtbl[24]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT IsCurrentCustomColorSpace([NativeTypeName("adlx_bool *")] bool* isSet)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentCustomColorSpace>((IntPtr)(lpVtbl[25]))(pThis, isSet);
        }
    }

    public readonly ADLX_RESULT GetGamutColorSpace(ADLX_GamutColorSpace* gamutColorSpace)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGamutColorSpace>((IntPtr)(lpVtbl[26]))(pThis, gamutColorSpace);
        }
    }

    public ADLX_RESULT SetGamut(ADLX_RGB customWhitePoint, ADLX_GamutColorSpace customGamut)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetGamut>((IntPtr)(lpVtbl[27]))(pThis, customWhitePoint, customGamut);
        }
    }

    public ADLX_RESULT SetGamut(ADLX_WHITE_POINT predefinedWhitePoint, ADLX_GamutColorSpace customGamut)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetGamut1>((IntPtr)(lpVtbl[28]))(pThis, predefinedWhitePoint, customGamut);
        }
    }

    public ADLX_RESULT SetGamut(ADLX_RGB customWhitePoint, ADLX_GAMUT_SPACE predefinedGamutSpace)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetGamut2>((IntPtr)(lpVtbl[29]))(pThis, customWhitePoint, predefinedGamutSpace);
        }
    }

    public ADLX_RESULT SetGamut(ADLX_WHITE_POINT predefinedWhitePoint, ADLX_GAMUT_SPACE predefinedGamutSpace)
    {
        fixed (IADLXDisplayGamut* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetGamut3>((IntPtr)(lpVtbl[30]))(pThis, predefinedWhitePoint, predefinedGamutSpace);
        }
    }
}

public partial struct IADLXDisplayGamut
{
}
