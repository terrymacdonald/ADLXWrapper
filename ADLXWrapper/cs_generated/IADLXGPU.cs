using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    [NativeTypeName("struct IADLXGPU : adlx::IADLXInterface")]
    public unsafe partial struct IADLXGPU
    {
        public void** lpVtbl;

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_long")]
        public delegate int _Acquire(IADLXGPU* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        [return: NativeTypeName("adlx_long")]
        public delegate int _Release(IADLXGPU* pThis);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _QueryInterface(IADLXGPU* pThis, [NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _VendorId(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** vendorId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _ASICFamilyType(IADLXGPU* pThis, ADLX_ASIC_FAMILY_TYPE* asicFamilyType);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _Type(IADLXGPU* pThis, ADLX_GPU_TYPE* gpuType);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _IsExternal(IADLXGPU* pThis, [NativeTypeName("adlx_bool *")] bool* isExternal);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _Name(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** name);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _DriverPath(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** driverPath);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _PNPString(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** pnpString);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _HasDesktops(IADLXGPU* pThis, [NativeTypeName("adlx_bool *")] bool* hasDesktops);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _TotalVRAM(IADLXGPU* pThis, [NativeTypeName("adlx_uint *")] uint* vramMB);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _VRAMType(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** type);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _BIOSInfo(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _DeviceId(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** deviceId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _RevisionId(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** revisionId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _SubSystemId(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** subSystemId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _SubSystemVendorId(IADLXGPU* pThis, [NativeTypeName("const char **")] sbyte** subSystemVendorId);

        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        public delegate ADLX_RESULT _UniqueId(IADLXGPU* pThis, [NativeTypeName("adlx_int *")] int* uniqueId);

        [return: NativeTypeName("const wchar_t *")]
        public static ushort* IID()
        {
            return "IADLXInterface";
        }

        [return: NativeTypeName("adlx_long")]
        public int Acquire()
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Acquire>((IntPtr)(lpVtbl[0]))(pThis);
            }
        }

        [return: NativeTypeName("adlx_long")]
        public int Release()
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Release>((IntPtr)(lpVtbl[1]))(pThis);
            }
        }

        public ADLX_RESULT QueryInterface([NativeTypeName("const wchar_t *")] ushort* interfaceId, void** ppInterface)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_QueryInterface>((IntPtr)(lpVtbl[2]))(pThis, interfaceId, ppInterface);
            }
        }

        public ADLX_RESULT VendorId([NativeTypeName("const char **")] sbyte** vendorId)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_VendorId>((IntPtr)(lpVtbl[3]))(pThis, vendorId);
            }
        }

        public readonly ADLX_RESULT ASICFamilyType(ADLX_ASIC_FAMILY_TYPE* asicFamilyType)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_ASICFamilyType>((IntPtr)(lpVtbl[4]))(pThis, asicFamilyType);
            }
        }

        public readonly ADLX_RESULT Type(ADLX_GPU_TYPE* gpuType)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Type>((IntPtr)(lpVtbl[5]))(pThis, gpuType);
            }
        }

        public readonly ADLX_RESULT IsExternal([NativeTypeName("adlx_bool *")] bool* isExternal)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_IsExternal>((IntPtr)(lpVtbl[6]))(pThis, isExternal);
            }
        }

        public readonly ADLX_RESULT Name([NativeTypeName("const char **")] sbyte** name)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_Name>((IntPtr)(lpVtbl[7]))(pThis, name);
            }
        }

        public readonly ADLX_RESULT DriverPath([NativeTypeName("const char **")] sbyte** driverPath)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_DriverPath>((IntPtr)(lpVtbl[8]))(pThis, driverPath);
            }
        }

        public readonly ADLX_RESULT PNPString([NativeTypeName("const char **")] sbyte** pnpString)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_PNPString>((IntPtr)(lpVtbl[9]))(pThis, pnpString);
            }
        }

        public readonly ADLX_RESULT HasDesktops([NativeTypeName("adlx_bool *")] bool* hasDesktops)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_HasDesktops>((IntPtr)(lpVtbl[10]))(pThis, hasDesktops);
            }
        }

        public ADLX_RESULT TotalVRAM([NativeTypeName("adlx_uint *")] uint* vramMB)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_TotalVRAM>((IntPtr)(lpVtbl[11]))(pThis, vramMB);
            }
        }

        public ADLX_RESULT VRAMType([NativeTypeName("const char **")] sbyte** type)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_VRAMType>((IntPtr)(lpVtbl[12]))(pThis, type);
            }
        }

        public ADLX_RESULT BIOSInfo([NativeTypeName("const char **")] sbyte** partNumber, [NativeTypeName("const char **")] sbyte** version, [NativeTypeName("const char **")] sbyte** date)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_BIOSInfo>((IntPtr)(lpVtbl[13]))(pThis, partNumber, version, date);
            }
        }

        public ADLX_RESULT DeviceId([NativeTypeName("const char **")] sbyte** deviceId)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_DeviceId>((IntPtr)(lpVtbl[14]))(pThis, deviceId);
            }
        }

        public ADLX_RESULT RevisionId([NativeTypeName("const char **")] sbyte** revisionId)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_RevisionId>((IntPtr)(lpVtbl[15]))(pThis, revisionId);
            }
        }

        public ADLX_RESULT SubSystemId([NativeTypeName("const char **")] sbyte** subSystemId)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_SubSystemId>((IntPtr)(lpVtbl[16]))(pThis, subSystemId);
            }
        }

        public ADLX_RESULT SubSystemVendorId([NativeTypeName("const char **")] sbyte** subSystemVendorId)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_SubSystemVendorId>((IntPtr)(lpVtbl[17]))(pThis, subSystemVendorId);
            }
        }

        public ADLX_RESULT UniqueId([NativeTypeName("adlx_int *")] int* uniqueId)
        {
            fixed (IADLXGPU* pThis = &this)
            {
                return Marshal.GetDelegateForFunctionPointer<_UniqueId>((IntPtr)(lpVtbl[18]))(pThis, uniqueId);
            }
        }
    }
}
