using System.Runtime.CompilerServices;

namespace ADLXWrapper;

/// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma"]/*' />
[NativeTypeName("struct IADLXDisplayGamma : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayGamma
{
    public void** lpVtbl;

    /// <inheritdoc cref="IADLXInterface.Acquire" />
    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, int>)(lpVtbl[0]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.Release" />
    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, int>)(lpVtbl[1]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this));
    }

    /// <inheritdoc cref="IADLXInterface.QueryInterface" />
    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ushort*, void**, ADLX_RESULT>)(lpVtbl[2]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), interfaceId, ppInterface);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsCurrentReGammaRamp"]/*' />
    public ADLX_RESULT IsCurrentReGammaRamp([NativeTypeName("adlx_bool *")] bool* isReGammaRamp)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[3]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isReGammaRamp);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsCurrentDeGammaRamp"]/*' />
    public ADLX_RESULT IsCurrentDeGammaRamp([NativeTypeName("adlx_bool *")] bool* isDeGammaRamp)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[4]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isDeGammaRamp);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsCurrentRegammaCoefficient"]/*' />
    public ADLX_RESULT IsCurrentRegammaCoefficient([NativeTypeName("adlx_bool *")] bool* isRegammaCoeff)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[5]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isRegammaCoeff);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.GetGammaRamp"]/*' />
    public ADLX_RESULT GetGammaRamp(ADLX_GammaRamp* lut)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_GammaRamp*, ADLX_RESULT>)(lpVtbl[6]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), lut);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.GetGammaCoefficient"]/*' />
    public ADLX_RESULT GetGammaCoefficient(ADLX_RegammaCoeff* coeff)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_RegammaCoeff*, ADLX_RESULT>)(lpVtbl[7]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), coeff);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsSupportedReGammaSRGB"]/*' />
    public ADLX_RESULT IsSupportedReGammaSRGB([NativeTypeName("adlx_bool *")] bool* isSupportedRegammaSRGB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[8]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isSupportedRegammaSRGB);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsSupportedReGammaBT709"]/*' />
    public ADLX_RESULT IsSupportedReGammaBT709([NativeTypeName("adlx_bool *")] bool* isSupportedReGammaBT709)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[9]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isSupportedReGammaBT709);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsSupportedReGammaPQ"]/*' />
    public ADLX_RESULT IsSupportedReGammaPQ([NativeTypeName("adlx_bool *")] bool* isSupportedReGammaPQ)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[10]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isSupportedReGammaPQ);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsSupportedReGammaPQ2084Interim"]/*' />
    public ADLX_RESULT IsSupportedReGammaPQ2084Interim([NativeTypeName("adlx_bool *")] bool* isSupportedReGammaPQ2084Interim)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[11]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isSupportedReGammaPQ2084Interim);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsSupportedReGamma36"]/*' />
    public ADLX_RESULT IsSupportedReGamma36([NativeTypeName("adlx_bool *")] bool* isSupportedReGamma36)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[12]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isSupportedReGamma36);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsCurrentReGammaSRGB"]/*' />
    public ADLX_RESULT IsCurrentReGammaSRGB([NativeTypeName("adlx_bool *")] bool* isCurrentReGammaSRGB)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[13]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isCurrentReGammaSRGB);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsCurrentReGammaBT709"]/*' />
    public ADLX_RESULT IsCurrentReGammaBT709([NativeTypeName("adlx_bool *")] bool* isCurrentReGammaBT709)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[14]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isCurrentReGammaBT709);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsCurrentReGammaPQ"]/*' />
    public ADLX_RESULT IsCurrentReGammaPQ([NativeTypeName("adlx_bool *")] bool* isCurrentReGammaPQ)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[15]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isCurrentReGammaPQ);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsCurrentReGammaPQ2084Interim"]/*' />
    public ADLX_RESULT IsCurrentReGammaPQ2084Interim([NativeTypeName("adlx_bool *")] bool* isCurrentReGammaPQ2084Interim)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[16]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isCurrentReGammaPQ2084Interim);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.IsCurrentReGamma36"]/*' />
    public ADLX_RESULT IsCurrentReGamma36([NativeTypeName("adlx_bool *")] bool* isCurrentReGamma36)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, bool*, ADLX_RESULT>)(lpVtbl[17]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), isCurrentReGamma36);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetReGammaSRGB"]/*' />
    public ADLX_RESULT SetReGammaSRGB()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_RESULT>)(lpVtbl[18]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetReGammaBT709"]/*' />
    public ADLX_RESULT SetReGammaBT709()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_RESULT>)(lpVtbl[19]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetReGammaPQ"]/*' />
    public ADLX_RESULT SetReGammaPQ()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_RESULT>)(lpVtbl[20]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetReGammaPQ2084Interim"]/*' />
    public ADLX_RESULT SetReGammaPQ2084Interim()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_RESULT>)(lpVtbl[21]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetReGamma36"]/*' />
    public ADLX_RESULT SetReGamma36()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_RESULT>)(lpVtbl[22]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this));
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetReGammaCoefficient"]/*' />
    public ADLX_RESULT SetReGammaCoefficient(ADLX_RegammaCoeff coeff)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_RegammaCoeff, ADLX_RESULT>)(lpVtbl[23]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), coeff);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetDeGammaRamp"]/*' />
    public ADLX_RESULT SetDeGammaRamp(ADLX_GammaRamp gammaRamp)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_GammaRamp, ADLX_RESULT>)(lpVtbl[24]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), gammaRamp);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetDeGammaRamp"]/*' />
    public ADLX_RESULT SetDeGammaRamp([NativeTypeName("const char *")] sbyte* path)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, sbyte*, ADLX_RESULT>)(lpVtbl[25]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), path);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetReGammaRamp"]/*' />
    public ADLX_RESULT SetReGammaRamp(ADLX_GammaRamp gammaRamp)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_GammaRamp, ADLX_RESULT>)(lpVtbl[26]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), gammaRamp);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.SetReGammaRamp"]/*' />
    public ADLX_RESULT SetReGammaRamp([NativeTypeName("const char *")] sbyte* path)
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, sbyte*, ADLX_RESULT>)(lpVtbl[27]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this), path);
    }

    /// <include file='IADLXDisplayGamma.xml' path='doc/member[@name="IADLXDisplayGamma.ResetGammaRamp"]/*' />
    public ADLX_RESULT ResetGammaRamp()
    {
        return ((delegate* unmanaged[Stdcall]<IADLXDisplayGamma*, ADLX_RESULT>)(lpVtbl[28]))((IADLXDisplayGamma*)Unsafe.AsPointer(ref this));
    }
}
