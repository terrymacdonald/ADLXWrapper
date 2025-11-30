using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXDisplayCustomColor
{
}

[NativeTypeName("struct IADLXDisplayCustomColor : adlx::IADLXInterface")]
public unsafe partial struct IADLXDisplayCustomColor
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXDisplayCustomColor* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXDisplayCustomColor* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXDisplayCustomColor* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsHueSupported(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetHueRange(IADLXDisplayCustomColor* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetHue(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int *")] int* currentHue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetHue(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int")] int hue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSaturationSupported(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSaturationRange(IADLXDisplayCustomColor* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetSaturation(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int *")] int* currentSaturation);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetSaturation(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int")] int saturation);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsBrightnessSupported(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetBrightnessRange(IADLXDisplayCustomColor* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetBrightness(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int *")] int* currentBrightness);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetBrightness(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int")] int brightness);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsContrastSupported(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetContrastRange(IADLXDisplayCustomColor* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetContrast(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int *")] int* currentContrast);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetContrast(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int")] int contrast);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsTemperatureSupported(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTemperatureRange(IADLXDisplayCustomColor* pThis, ADLX_IntRange* range);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetTemperature(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int *")] int* currentTemperature);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SetTemperature(IADLXDisplayCustomColor* pThis, [NativeTypeName("adlx_int")] int temperature);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsHueSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsHueSupported>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetHueRange(ADLX_IntRange* range)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetHueRange>((IntPtr)(lpVtbl[4]))(pThis, range);
        }
    }

    public ADLX_RESULT GetHue([NativeTypeName("adlx_int *")] int* currentHue)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetHue>((IntPtr)(lpVtbl[5]))(pThis, currentHue);
        }
    }

    public ADLX_RESULT SetHue([NativeTypeName("adlx_int")] int hue)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetHue>((IntPtr)(lpVtbl[6]))(pThis, hue);
        }
    }

    public ADLX_RESULT IsSaturationSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSaturationSupported>((IntPtr)(lpVtbl[7]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetSaturationRange(ADLX_IntRange* range)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSaturationRange>((IntPtr)(lpVtbl[8]))(pThis, range);
        }
    }

    public ADLX_RESULT GetSaturation([NativeTypeName("adlx_int *")] int* currentSaturation)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetSaturation>((IntPtr)(lpVtbl[9]))(pThis, currentSaturation);
        }
    }

    public ADLX_RESULT SetSaturation([NativeTypeName("adlx_int")] int saturation)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetSaturation>((IntPtr)(lpVtbl[10]))(pThis, saturation);
        }
    }

    public ADLX_RESULT IsBrightnessSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsBrightnessSupported>((IntPtr)(lpVtbl[11]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetBrightnessRange(ADLX_IntRange* range)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetBrightnessRange>((IntPtr)(lpVtbl[12]))(pThis, range);
        }
    }

    public ADLX_RESULT GetBrightness([NativeTypeName("adlx_int *")] int* currentBrightness)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetBrightness>((IntPtr)(lpVtbl[13]))(pThis, currentBrightness);
        }
    }

    public ADLX_RESULT SetBrightness([NativeTypeName("adlx_int")] int brightness)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetBrightness>((IntPtr)(lpVtbl[14]))(pThis, brightness);
        }
    }

    public ADLX_RESULT IsContrastSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsContrastSupported>((IntPtr)(lpVtbl[15]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetContrastRange(ADLX_IntRange* range)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetContrastRange>((IntPtr)(lpVtbl[16]))(pThis, range);
        }
    }

    public ADLX_RESULT GetContrast([NativeTypeName("adlx_int *")] int* currentContrast)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetContrast>((IntPtr)(lpVtbl[17]))(pThis, currentContrast);
        }
    }

    public ADLX_RESULT SetContrast([NativeTypeName("adlx_int")] int contrast)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetContrast>((IntPtr)(lpVtbl[18]))(pThis, contrast);
        }
    }

    public ADLX_RESULT IsTemperatureSupported([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsTemperatureSupported>((IntPtr)(lpVtbl[19]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetTemperatureRange(ADLX_IntRange* range)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTemperatureRange>((IntPtr)(lpVtbl[20]))(pThis, range);
        }
    }

    public ADLX_RESULT GetTemperature([NativeTypeName("adlx_int *")] int* currentTemperature)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetTemperature>((IntPtr)(lpVtbl[21]))(pThis, currentTemperature);
        }
    }

    public ADLX_RESULT SetTemperature([NativeTypeName("adlx_int")] int temperature)
    {
        fixed (IADLXDisplayCustomColor* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SetTemperature>((IntPtr)(lpVtbl[22]))(pThis, temperature);
        }
    }
}
