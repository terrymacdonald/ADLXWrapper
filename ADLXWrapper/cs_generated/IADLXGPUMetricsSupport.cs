using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUMetricsSupport : adlx::IADLXInterface")]
public unsafe partial struct IADLXGPUMetricsSupport
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUMetricsSupport* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUMetricsSupport* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUMetricsSupport* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUUsage(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUClockSpeed(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUVRAMClockSpeed(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUTemperature(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUHotspotTemperature(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUPower(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUTotalBoardPower(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUFanSpeed(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUVRAM(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUVoltage(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUUsageRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUClockSpeedRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUVRAMClockSpeedRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUTemperatureRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUHotspotTemperatureRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUPowerRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUFanSpeedRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUVRAMRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUVoltageRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUTotalBoardPowerRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUIntakeTemperatureRange(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUIntakeTemperature(IADLXGPUMetricsSupport* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedGPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUUsage>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUClockSpeed>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUVRAMClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUVRAMClockSpeed>((IntPtr)(lpVtbl[5]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUTemperature>((IntPtr)(lpVtbl[6]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUHotspotTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUHotspotTemperature>((IntPtr)(lpVtbl[7]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUPower>((IntPtr)(lpVtbl[8]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUTotalBoardPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUTotalBoardPower>((IntPtr)(lpVtbl[9]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUFanSpeed>((IntPtr)(lpVtbl[10]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUVRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUVRAM>((IntPtr)(lpVtbl[11]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUVoltage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUVoltage>((IntPtr)(lpVtbl[12]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetGPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUUsageRange>((IntPtr)(lpVtbl[13]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUClockSpeedRange>((IntPtr)(lpVtbl[14]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUVRAMClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUVRAMClockSpeedRange>((IntPtr)(lpVtbl[15]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUTemperatureRange>((IntPtr)(lpVtbl[16]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUHotspotTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUHotspotTemperatureRange>((IntPtr)(lpVtbl[17]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUPowerRange>((IntPtr)(lpVtbl[18]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUFanSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUFanSpeedRange>((IntPtr)(lpVtbl[19]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUVRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUVRAMRange>((IntPtr)(lpVtbl[20]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUVoltageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUVoltageRange>((IntPtr)(lpVtbl[21]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUTotalBoardPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUTotalBoardPowerRange>((IntPtr)(lpVtbl[22]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUIntakeTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUIntakeTemperatureRange>((IntPtr)(lpVtbl[23]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT IsSupportedGPUIntakeTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUIntakeTemperature>((IntPtr)(lpVtbl[24]))(pThis, supported);
        }
    }
}
