using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

public partial struct IADLXGPU2
{
}

public partial struct IADLXGPU2
{
}

[NativeTypeName("struct IADLXGPU2 : adlx::IADLXGPU1")]
public unsafe partial struct IADLXGPU2
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPU2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPU2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPU2* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _VendorId(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** vendorId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ASICFamilyType(IADLXGPU2* pThis, ADLX_ASIC_FAMILY_TYPE* asicFamilyType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Type(IADLXGPU2* pThis, ADLX_GPU_TYPE* gpuType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsExternal(IADLXGPU2* pThis, [NativeTypeName("adlx_bool *")] bool* isExternal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Name(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** name);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DriverPath(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** driverPath);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PNPString(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** pnpString);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _HasDesktops(IADLXGPU2* pThis, [NativeTypeName("adlx_bool *")] bool* hasDesktops);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _TotalVRAM(IADLXGPU2* pThis, [NativeTypeName("adlx_uint *")] uint* vramMB);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _VRAMType(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** type);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _BIOSInfo(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DeviceId(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** deviceId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RevisionId(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** revisionId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SubSystemId(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** subSystemId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SubSystemVendorId(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** subSystemVendorId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _UniqueId(IADLXGPU2* pThis, [NativeTypeName("adlx_int *")] int* uniqueId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PCIBusType(IADLXGPU2* pThis, ADLX_PCI_BUS_TYPE* busType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PCIBusLaneWidth(IADLXGPU2* pThis, [NativeTypeName("adlx_uint *")] uint* laneWidth);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _MultiGPUMode(IADLXGPU2* pThis, ADLX_MGPU_MODE* mode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ProductName(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** productName);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsPowerOff(IADLXGPU2* pThis, [NativeTypeName("adlx_bool *")] bool* state);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PowerOn(IADLXGPU2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _StartPowerOff(IADLXGPU2* pThis, [NativeTypeName("adlx::IADLXGPUConnectChangedListener *")] IADLXGPUConnectChangedListener* pGPUConnectChangedListener, [NativeTypeName("adlx_int")] int timeout);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AbortPowerOff(IADLXGPU2* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsSupportedApplicationList(IADLXGPU2* pThis, [NativeTypeName("adlx_bool *")] bool* supported);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _GetApplications(IADLXGPU2* pThis, IADLXApplicationList** ppApplications);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AMDSoftwareReleaseDate(IADLXGPU2* pThis, [NativeTypeName("adlx_uint *")] uint* year, [NativeTypeName("adlx_uint *")] uint* month, [NativeTypeName("adlx_uint *")] uint* day);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AMDSoftwareEdition(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** edition);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AMDSoftwareVersion(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** version);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DriverVersion(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** version);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _AMDWindowsDriverVersion(IADLXGPU2* pThis, [NativeTypeName("const char **")] sbyte** version);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _LUID(IADLXGPU2* pThis, ADLX_LUID* luid);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_VendorId>((IntPtr)(lpVtbl[3]))(pThis, vendorId);
        }
    }

    public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ASICFamilyType>((IntPtr)(lpVtbl[4]))(pThis, asicFamilyType);
        }
    }

    public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Type>((IntPtr)(lpVtbl[5]))(pThis, gpuType);
        }
    }

    public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsExternal>((IntPtr)(lpVtbl[6]))(pThis, isExternal);
        }
    }

    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Name>((IntPtr)(lpVtbl[7]))(pThis, name);
        }
    }

    public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DriverPath>((IntPtr)(lpVtbl[8]))(pThis, driverPath);
        }
    }

    public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PNPString>((IntPtr)(lpVtbl[9]))(pThis, pnpString);
        }
    }

    public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_HasDesktops>((IntPtr)(lpVtbl[10]))(pThis, hasDesktops);
        }
    }

    public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_TotalVRAM>((IntPtr)(lpVtbl[11]))(pThis, vramMB);
        }
    }

    public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_VRAMType>((IntPtr)(lpVtbl[12]))(pThis, type);
        }
    }

    public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_BIOSInfo>((IntPtr)(lpVtbl[13]))(pThis, partNumber, version, date);
        }
    }

    public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DeviceId>((IntPtr)(lpVtbl[14]))(pThis, deviceId);
        }
    }

    public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RevisionId>((IntPtr)(lpVtbl[15]))(pThis, revisionId);
        }
    }

    public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SubSystemId>((IntPtr)(lpVtbl[16]))(pThis, subSystemId);
        }
    }

    public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SubSystemVendorId>((IntPtr)(lpVtbl[17]))(pThis, subSystemVendorId);
        }
    }

    public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_UniqueId>((IntPtr)(lpVtbl[18]))(pThis, uniqueId);
        }
    }

    public readonly ADLX_RESULT PCIBusType(ADLX_PCI_BUS_TYPE* busType)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PCIBusType>((IntPtr)(lpVtbl[19]))(pThis, busType);
        }
    }

    public readonly ADLX_RESULT PCIBusLaneWidth([NativeTypeName("adlx_uint *")] uint* laneWidth)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PCIBusLaneWidth>((IntPtr)(lpVtbl[20]))(pThis, laneWidth);
        }
    }

    public ADLX_RESULT MultiGPUMode(ADLX_MGPU_MODE* mode)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_MultiGPUMode>((IntPtr)(lpVtbl[21]))(pThis, mode);
        }
    }

    public readonly ADLX_RESULT ProductName([NativeTypeName("const char **")] sbyte** productName)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ProductName>((IntPtr)(lpVtbl[22]))(pThis, productName);
        }
    }

    public ADLX_RESULT IsPowerOff([NativeTypeName("adlx_bool *")] bool* state)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsPowerOff>((IntPtr)(lpVtbl[23]))(pThis, state);
        }
    }

    public ADLX_RESULT PowerOn()
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PowerOn>((IntPtr)(lpVtbl[24]))(pThis);
        }
    }

    public ADLX_RESULT StartPowerOff([NativeTypeName("adlx::IADLXGPUConnectChangedListener *")] IADLXGPUConnectChangedListener* pGPUConnectChangedListener, [NativeTypeName("adlx_int")] int timeout)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_StartPowerOff>((IntPtr)(lpVtbl[25]))(pThis, pGPUConnectChangedListener, timeout);
        }
    }

    public ADLX_RESULT AbortPowerOff()
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AbortPowerOff>((IntPtr)(lpVtbl[26]))(pThis);
        }
    }

    public ADLX_RESULT IsSupportedApplicationList([NativeTypeName("adlx_bool *")] bool* supported)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsSupportedApplicationList>((IntPtr)(lpVtbl[27]))(pThis, supported);
        }
    }

    public ADLX_RESULT GetApplications(IADLXApplicationList** ppApplications)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_GetApplications>((IntPtr)(lpVtbl[28]))(pThis, ppApplications);
        }
    }

    public ADLX_RESULT AMDSoftwareReleaseDate([NativeTypeName("adlx_uint *")] uint* year, [NativeTypeName("adlx_uint *")] uint* month, [NativeTypeName("adlx_uint *")] uint* day)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AMDSoftwareReleaseDate>((IntPtr)(lpVtbl[29]))(pThis, year, month, day);
        }
    }

    public ADLX_RESULT AMDSoftwareEdition([NativeTypeName("const char **")] sbyte** edition)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AMDSoftwareEdition>((IntPtr)(lpVtbl[30]))(pThis, edition);
        }
    }

    public ADLX_RESULT AMDSoftwareVersion([NativeTypeName("const char **")] sbyte** version)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AMDSoftwareVersion>((IntPtr)(lpVtbl[31]))(pThis, version);
        }
    }

    public ADLX_RESULT DriverVersion([NativeTypeName("const char **")] sbyte** version)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DriverVersion>((IntPtr)(lpVtbl[32]))(pThis, version);
        }
    }

    public ADLX_RESULT AMDWindowsDriverVersion([NativeTypeName("const char **")] sbyte** version)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_AMDWindowsDriverVersion>((IntPtr)(lpVtbl[33]))(pThis, version);
        }
    }

    public ADLX_RESULT LUID(ADLX_LUID* luid)
    {
        fixed (IADLXGPU2* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_LUID>((IntPtr)(lpVtbl[34]))(pThis, luid);
        }
    }
}
