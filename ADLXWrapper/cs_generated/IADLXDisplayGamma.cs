using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXDisplayGamma : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayGamma
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayGamma* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayGamma* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayGamma* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentReGammaRamp(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isReGammaRamp);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentDeGammaRamp(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isDeGammaRamp);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentRegammaCoefficient(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isRegammaCoeff);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGammaRamp(IADLXDisplayGamma* pThis, ADLX_GammaRamp* lut);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGammaCoefficient(IADLXDisplayGamma* pThis, ADLX_RegammaCoeff* coeff);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedReGammaSRGB(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isSupportedRegammaSRGB);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedReGammaBT709(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isSupportedReGammaBT709);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedReGammaPQ(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isSupportedReGammaPQ);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedReGammaPQ2084Interim(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isSupportedReGammaPQ2084Interim);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedReGamma36(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isSupportedReGamma36);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentReGammaSRGB(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isCurrentReGammaSRGB);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentReGammaBT709(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isCurrentReGammaBT709);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentReGammaPQ(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isCurrentReGammaPQ);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentReGammaPQ2084Interim(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isCurrentReGammaPQ2084Interim);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsCurrentReGamma36(IADLXDisplayGamma* pThis, [NativeTypeName("adlx_bool *")] bool* isCurrentReGamma36);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetReGammaSRGB(IADLXDisplayGamma* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetReGammaBT709(IADLXDisplayGamma* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetReGammaPQ(IADLXDisplayGamma* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetReGammaPQ2084Interim(IADLXDisplayGamma* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetReGamma36(IADLXDisplayGamma* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetReGammaCoefficient(IADLXDisplayGamma* pThis, ADLX_RegammaCoeff coeff);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetDeGammaRamp(IADLXDisplayGamma* pThis, ADLX_GammaRamp gammaRamp);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetDeGammaRamp1(IADLXDisplayGamma* pThis, [NativeTypeName("const char *")] sbyte* path);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetReGammaRamp(IADLXDisplayGamma* pThis, ADLX_GammaRamp gammaRamp);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetReGammaRamp1(IADLXDisplayGamma* pThis, [NativeTypeName("const char *")] sbyte* path);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ResetGammaRamp(IADLXDisplayGamma* pThis);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsCurrentReGammaRamp([NativeTypeName("adlx_bool *")] bool* isReGammaRamp)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentReGammaRamp>((IntPtr)(lpVtbl[3]))(pThis, isReGammaRamp);
        }
    }

    public ADLX_RESULT IsCurrentDeGammaRamp([NativeTypeName("adlx_bool *")] bool* isDeGammaRamp)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentDeGammaRamp>((IntPtr)(lpVtbl[4]))(pThis, isDeGammaRamp);
        }
    }

    public ADLX_RESULT IsCurrentRegammaCoefficient([NativeTypeName("adlx_bool *")] bool* isRegammaCoeff)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentRegammaCoefficient>((IntPtr)(lpVtbl[5]))(pThis, isRegammaCoeff);
        }
    }

    public ADLX_RESULT GetGammaRamp(ADLX_GammaRamp* lut)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGammaRamp>((IntPtr)(lpVtbl[6]))(pThis, lut);
        }
    }

    public ADLX_RESULT GetGammaCoefficient(ADLX_RegammaCoeff* coeff)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGammaCoefficient>((IntPtr)(lpVtbl[7]))(pThis, coeff);
        }
    }

    public ADLX_RESULT IsSupportedReGammaSRGB([NativeTypeName("adlx_bool *")] bool* isSupportedRegammaSRGB)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedReGammaSRGB>((IntPtr)(lpVtbl[8]))(pThis, isSupportedRegammaSRGB);
        }
    }

    public ADLX_RESULT IsSupportedReGammaBT709([NativeTypeName("adlx_bool *")] bool* isSupportedReGammaBT709)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedReGammaBT709>((IntPtr)(lpVtbl[9]))(pThis, isSupportedReGammaBT709);
        }
    }

    public ADLX_RESULT IsSupportedReGammaPQ([NativeTypeName("adlx_bool *")] bool* isSupportedReGammaPQ)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedReGammaPQ>((IntPtr)(lpVtbl[10]))(pThis, isSupportedReGammaPQ);
        }
    }

    public ADLX_RESULT IsSupportedReGammaPQ2084Interim([NativeTypeName("adlx_bool *")] bool* isSupportedReGammaPQ2084Interim)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedReGammaPQ2084Interim>((IntPtr)(lpVtbl[11]))(pThis, isSupportedReGammaPQ2084Interim);
        }
    }

    public ADLX_RESULT IsSupportedReGamma36([NativeTypeName("adlx_bool *")] bool* isSupportedReGamma36)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedReGamma36>((IntPtr)(lpVtbl[12]))(pThis, isSupportedReGamma36);
        }
    }

    public ADLX_RESULT IsCurrentReGammaSRGB([NativeTypeName("adlx_bool *")] bool* isCurrentReGammaSRGB)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentReGammaSRGB>((IntPtr)(lpVtbl[13]))(pThis, isCurrentReGammaSRGB);
        }
    }

    public ADLX_RESULT IsCurrentReGammaBT709([NativeTypeName("adlx_bool *")] bool* isCurrentReGammaBT709)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentReGammaBT709>((IntPtr)(lpVtbl[14]))(pThis, isCurrentReGammaBT709);
        }
    }

    public ADLX_RESULT IsCurrentReGammaPQ([NativeTypeName("adlx_bool *")] bool* isCurrentReGammaPQ)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentReGammaPQ>((IntPtr)(lpVtbl[15]))(pThis, isCurrentReGammaPQ);
        }
    }

    public ADLX_RESULT IsCurrentReGammaPQ2084Interim([NativeTypeName("adlx_bool *")] bool* isCurrentReGammaPQ2084Interim)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentReGammaPQ2084Interim>((IntPtr)(lpVtbl[16]))(pThis, isCurrentReGammaPQ2084Interim);
        }
    }

    public ADLX_RESULT IsCurrentReGamma36([NativeTypeName("adlx_bool *")] bool* isCurrentReGamma36)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsCurrentReGamma36>((IntPtr)(lpVtbl[17]))(pThis, isCurrentReGamma36);
        }
    }

    public ADLX_RESULT SetReGammaSRGB()
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetReGammaSRGB>((IntPtr)(lpVtbl[18]))(pThis);
        }
    }

    public ADLX_RESULT SetReGammaBT709()
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetReGammaBT709>((IntPtr)(lpVtbl[19]))(pThis);
        }
    }

    public ADLX_RESULT SetReGammaPQ()
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetReGammaPQ>((IntPtr)(lpVtbl[20]))(pThis);
        }
    }

    public ADLX_RESULT SetReGammaPQ2084Interim()
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetReGammaPQ2084Interim>((IntPtr)(lpVtbl[21]))(pThis);
        }
    }

    public ADLX_RESULT SetReGamma36()
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetReGamma36>((IntPtr)(lpVtbl[22]))(pThis);
        }
    }

    public ADLX_RESULT SetReGammaCoefficient(ADLX_RegammaCoeff coeff)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetReGammaCoefficient>((IntPtr)(lpVtbl[23]))(pThis, coeff);
        }
    }

    public ADLX_RESULT SetDeGammaRamp(ADLX_GammaRamp gammaRamp)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetDeGammaRamp>((IntPtr)(lpVtbl[24]))(pThis, gammaRamp);
        }
    }

    public ADLX_RESULT SetDeGammaRamp([NativeTypeName("const char *")] sbyte* path)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetDeGammaRamp1>((IntPtr)(lpVtbl[25]))(pThis, path);
        }
    }

    public ADLX_RESULT SetReGammaRamp(ADLX_GammaRamp gammaRamp)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetReGammaRamp>((IntPtr)(lpVtbl[26]))(pThis, gammaRamp);
        }
    }

    public ADLX_RESULT SetReGammaRamp([NativeTypeName("const char *")] sbyte* path)
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetReGammaRamp1>((IntPtr)(lpVtbl[27]))(pThis, path);
        }
    }

    public ADLX_RESULT ResetGammaRamp()
    {
        fixed (IADLXDisplayGamma* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ResetGammaRamp>((IntPtr)(lpVtbl[28]))(pThis);
        }
    }
}

public partial struct IADLXDisplayGamma
{
}
