using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper;

[NativeTypeName("struct IADLXGPU1 : adlx::IADLXGPU")]
public unsafe partial struct IADLXGPU1
{
    public void** lpVtbl;

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Acquire(IADLXGPU1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    [return: NativeTypeName("adlx_long")]
    public delegate int _Release(IADLXGPU1* pThis);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _QueryInterface(IADLXGPU1* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _VendorId(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** vendorId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ASICFamilyType(IADLXGPU1* pThis, ADLX_ASIC_FAMILY_TYPE* asicFamilyType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Type(IADLXGPU1* pThis, ADLX_GPU_TYPE* gpuType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _IsExternal(IADLXGPU1* pThis, [NativeTypeName("adlx_bool *")] bool* isExternal);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _Name(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** name);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DriverPath(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** driverPath);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PNPString(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** pnpString);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _HasDesktops(IADLXGPU1* pThis, [NativeTypeName("adlx_bool *")] bool* hasDesktops);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _TotalVRAM(IADLXGPU1* pThis, [NativeTypeName("adlx_uint *")] uint* vramMB);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _VRAMType(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** type);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _BIOSInfo(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _DeviceId(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** deviceId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _RevisionId(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** revisionId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SubSystemId(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** subSystemId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _SubSystemVendorId(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** subSystemVendorId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _UniqueId(IADLXGPU1* pThis, [NativeTypeName("adlx_int *")] int* uniqueId);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PCIBusType(IADLXGPU1* pThis, ADLX_PCI_BUS_TYPE* busType);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _PCIBusLaneWidth(IADLXGPU1* pThis, [NativeTypeName("adlx_uint *")] uint* laneWidth);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _MultiGPUMode(IADLXGPU1* pThis, ADLX_MGPU_MODE* mode);

    [UnmanagedFunctionPointer(CallingConvention.StdCall)]
    public delegate ADLX_RESULT _ProductName(IADLXGPU1* pThis, [NativeTypeName("const char **")] sbyte** productName);

    [return: NativeTypeName("adlx_long")]
    public int Acquire()
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
        }
    }

    [return: NativeTypeName("adlx_long")]
    public int Release()
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
        }
    }

    public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
        }
    }

    public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_VendorId>((IntPtr)(lpVtbl[3]))(pThis, vendorId);
        }
    }

    public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ASICFamilyType>((IntPtr)(lpVtbl[4]))(pThis, asicFamilyType);
        }
    }

    public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Type>((IntPtr)(lpVtbl[5]))(pThis, gpuType);
        }
    }

    public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_IsExternal>((IntPtr)(lpVtbl[6]))(pThis, isExternal);
        }
    }

    public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_Name>((IntPtr)(lpVtbl[7]))(pThis, name);
        }
    }

    public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DriverPath>((IntPtr)(lpVtbl[8]))(pThis, driverPath);
        }
    }

    public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PNPString>((IntPtr)(lpVtbl[9]))(pThis, pnpString);
        }
    }

    public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_HasDesktops>((IntPtr)(lpVtbl[10]))(pThis, hasDesktops);
        }
    }

    public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_TotalVRAM>((IntPtr)(lpVtbl[11]))(pThis, vramMB);
        }
    }

    public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_VRAMType>((IntPtr)(lpVtbl[12]))(pThis, type);
        }
    }

    public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_BIOSInfo>((IntPtr)(lpVtbl[13]))(pThis, partNumber, version, date);
        }
    }

    public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_DeviceId>((IntPtr)(lpVtbl[14]))(pThis, deviceId);
        }
    }

    public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_RevisionId>((IntPtr)(lpVtbl[15]))(pThis, revisionId);
        }
    }

    public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SubSystemId>((IntPtr)(lpVtbl[16]))(pThis, subSystemId);
        }
    }

    public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_SubSystemVendorId>((IntPtr)(lpVtbl[17]))(pThis, subSystemVendorId);
        }
    }

    public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_UniqueId>((IntPtr)(lpVtbl[18]))(pThis, uniqueId);
        }
    }

    public readonly ADLX_RESULT PCIBusType(ADLX_PCI_BUS_TYPE* busType)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PCIBusType>((IntPtr)(lpVtbl[19]))(pThis, busType);
        }
    }

    public readonly ADLX_RESULT PCIBusLaneWidth([NativeTypeName("adlx_uint *")] uint* laneWidth)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_PCIBusLaneWidth>((IntPtr)(lpVtbl[20]))(pThis, laneWidth);
        }
    }

    public ADLX_RESULT MultiGPUMode(ADLX_MGPU_MODE* mode)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_MultiGPUMode>((IntPtr)(lpVtbl[21]))(pThis, mode);
        }
    }

    public readonly ADLX_RESULT ProductName([NativeTypeName("const char **")] sbyte** productName)
    {
        fixed (IADLXGPU1* pThis = &this)
        {
            return Marshal.GetDelegateForFunctionPointer<_ProductName>((IntPtr)(lpVtbl[22]))(pThis, productName);
        }
    }
}
