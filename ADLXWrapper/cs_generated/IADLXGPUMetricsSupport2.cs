using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPUMetricsSupport2 : adlx::IADLXGPUMetricsSupport1")]
public unsafe partial struct IADLXGPUMetricsSupport2
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPUMetricsSupport2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPUMetricsSupport2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUUsage(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUClockSpeed(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUVRAMClockSpeed(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUTemperature(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUHotspotTemperature(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUPower(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUTotalBoardPower(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUFanSpeed(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUVRAM(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUVoltage(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUUsageRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUClockSpeedRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUVRAMClockSpeedRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUTemperatureRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUHotspotTemperatureRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUPowerRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUFanSpeedRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUVRAMRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUVoltageRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUTotalBoardPowerRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUIntakeTemperatureRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUIntakeTemperature(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUMemoryTemperature(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUMemoryTemperatureRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedNPUFrequency(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetNPUFrequencyRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedNPUActivityLevel(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetNPUActivityLevelRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedGPUSharedMemory(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetGPUSharedMemoryRange(IADLXGPUMetricsSupport2* pThis, [NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT IsSupportedGPUUsage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUUsage>((IntPtr)(lpVtbl[3]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUClockSpeed>((IntPtr)(lpVtbl[4]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUVRAMClockSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUVRAMClockSpeed>((IntPtr)(lpVtbl[5]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUTemperature>((IntPtr)(lpVtbl[6]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUHotspotTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUHotspotTemperature>((IntPtr)(lpVtbl[7]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUPower>((IntPtr)(lpVtbl[8]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUTotalBoardPower([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUTotalBoardPower>((IntPtr)(lpVtbl[9]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUFanSpeed([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUFanSpeed>((IntPtr)(lpVtbl[10]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUVRAM([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUVRAM>((IntPtr)(lpVtbl[11]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUVoltage([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUVoltage>((IntPtr)(lpVtbl[12]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetGPUUsageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUUsageRange>((IntPtr)(lpVtbl[13]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUClockSpeedRange>((IntPtr)(lpVtbl[14]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUVRAMClockSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUVRAMClockSpeedRange>((IntPtr)(lpVtbl[15]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUTemperatureRange>((IntPtr)(lpVtbl[16]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUHotspotTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUHotspotTemperatureRange>((IntPtr)(lpVtbl[17]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUPowerRange>((IntPtr)(lpVtbl[18]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUFanSpeedRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUFanSpeedRange>((IntPtr)(lpVtbl[19]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUVRAMRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUVRAMRange>((IntPtr)(lpVtbl[20]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUVoltageRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUVoltageRange>((IntPtr)(lpVtbl[21]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUTotalBoardPowerRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUTotalBoardPowerRange>((IntPtr)(lpVtbl[22]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT GetGPUIntakeTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUIntakeTemperatureRange>((IntPtr)(lpVtbl[23]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT IsSupportedGPUIntakeTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUIntakeTemperature>((IntPtr)(lpVtbl[24]))(pThis, supported);
        }
    }

    public ADLX_RESULT IsSupportedGPUMemoryTemperature([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUMemoryTemperature>((IntPtr)(lpVtbl[25]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetGPUMemoryTemperatureRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUMemoryTemperatureRange>((IntPtr)(lpVtbl[26]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT IsSupportedNPUFrequency([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedNPUFrequency>((IntPtr)(lpVtbl[27]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetNPUFrequencyRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetNPUFrequencyRange>((IntPtr)(lpVtbl[28]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT IsSupportedNPUActivityLevel([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedNPUActivityLevel>((IntPtr)(lpVtbl[29]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetNPUActivityLevelRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetNPUActivityLevelRange>((IntPtr)(lpVtbl[30]))(pThis, minValue, maxValue);
        }
    }

    public ADLX_RESULT IsSupportedGPUSharedMemory([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedGPUSharedMemory>((IntPtr)(lpVtbl[31]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetGPUSharedMemoryRange([NativeTypeName("adlx_int *")] int* minValue, [NativeTypeName("adlx_int *")] int* maxValue)
    {
        fixed (IADLXGPUMetricsSupport2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetGPUSharedMemoryRange>((IntPtr)(lpVtbl[32]))(pThis, minValue, maxValue);
        }
    }
}
