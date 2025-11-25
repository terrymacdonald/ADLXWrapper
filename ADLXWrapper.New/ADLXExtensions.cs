using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for ADLX GPU operations
    /// Provides convenient access to GPU properties via VTable
    /// </summary>
    public static unsafe class ADLXHelpers
    {
        /// <summary>
        /// Get GPU name
        /// </summary>
        public static string GetGPUName(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var nameFn = (ADLXVTables.NameFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Name, typeof(ADLXVTables.NameFn));

            byte* pName;
            var result = nameFn(pGPU, &pName);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU name");
            }

            return MarshalString(pName);
        }

        /// <summary>
        /// Get GPU vendor ID
        /// </summary>
        public static string GetGPUVendorId(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var vendorIdFn = (ADLXVTables.VendorIdFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->VendorId, typeof(ADLXVTables.VendorIdFn));

            byte* pVendorId;
            var result = vendorIdFn(pGPU, &pVendorId);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU vendor ID");
            }

            return MarshalString(pVendorId);
        }

        /// <summary>
        /// Get GPU driver path
        /// </summary>
        public static string GetGPUDriverPath(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var driverPathFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.NameFn>(vtbl->DriverPath);

            byte* pDriverPath;
            var result = driverPathFn(pGPU, &pDriverPath);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU driver path");
            }

            return MarshalString(pDriverPath);
        }

        /// <summary>
        /// Get GPU PNP string
        /// </summary>
        public static string GetGPUPNPString(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var pnpStringFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.NameFn>(vtbl->PNPString);

            byte* pPNPString;
            var result = pnpStringFn(pGPU, &pPNPString);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU PNP string");
            }

            return MarshalString(pPNPString);
        }

        /// <summary>
        /// Get GPU total VRAM in MB
        /// </summary>
        public static uint GetGPUTotalVRAM(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var totalVRAMFn = (ADLXVTables.TotalVRAMFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->TotalVRAM, typeof(ADLXVTables.TotalVRAMFn));

            uint vramMB;
            var result = totalVRAMFn(pGPU, &vramMB);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU total VRAM");
            }

            return vramMB;
        }

        /// <summary>
        /// Get GPU VRAM type
        /// </summary>
        public static string GetGPUVRAMType(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var vramTypeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.NameFn>(vtbl->VRAMType);

            byte* pVRAMType;
            var result = vramTypeFn(pGPU, &pVRAMType);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU VRAM type");
            }

            return MarshalString(pVRAMType);
        }

        /// <summary>
        /// Get GPU unique ID
        /// </summary>
        public static int GetGPUUniqueId(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var uniqueIdFn = (ADLXVTables.UniqueIdFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->UniqueId, typeof(ADLXVTables.UniqueIdFn));

            int uniqueId;
            var result = uniqueIdFn(pGPU, &uniqueId);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU unique ID");
            }

            return uniqueId;
        }

        /// <summary>
        /// Get GPU device ID
        /// </summary>
        public static string GetGPUDeviceId(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var deviceIdFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.NameFn>(vtbl->DeviceId);

            byte* pDeviceId;
            var result = deviceIdFn(pGPU, &pDeviceId);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU device ID");
            }

            return MarshalString(pDeviceId);
        }

        /// <summary>
        /// Check if GPU is external
        /// </summary>
        public static bool IsGPUExternal(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var isExternalFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.IsExternalFn>(vtbl->IsExternal);

            byte isExternal;
            var result = isExternalFn(pGPU, &isExternal);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check if GPU is external");
            }

            return isExternal != 0;
        }

        /// <summary>
        /// Check if GPU has desktops
        /// </summary>
        public static bool HasGPUDesktops(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUVtbl**)pGPU;
            var hasDesktopsFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.HasDesktopsFn>(vtbl->HasDesktops);

            byte hasDesktops;
            var result = hasDesktopsFn(pGPU, &hasDesktops);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check if GPU has desktops");
            }

            return hasDesktops != 0;
        }

        /// <summary>
        /// Release an ADLX interface
        /// Decrements the reference count
        /// </summary>
        public static void ReleaseInterface(IntPtr pInterface)
        {
            if (pInterface == IntPtr.Zero)
                return;

            var vtbl = *(ADLXVTables.IADLXInterfaceVtbl**)pInterface;
            var releaseFn = (ADLXVTables.ReleaseFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Release, typeof(ADLXVTables.ReleaseFn));

            releaseFn(pInterface);
        }

        /// <summary>
        /// Add reference to an ADLX interface
        /// Increments the reference count
        /// </summary>
        public static void AddRefInterface(IntPtr pInterface)
        {
            if (pInterface == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pInterface));

            var vtbl = *(ADLXVTables.IADLXInterfaceVtbl**)pInterface;
            var addRefFn = (ADLXVTables.AddRefFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->AddRef, typeof(ADLXVTables.AddRefFn));

            addRefFn(pInterface);
        }

        /// <summary>
        /// Helper to marshal ANSI string pointer to managed string
        /// </summary>
        private static string MarshalString(byte* pStr)
        {
            if (pStr == null)
                return string.Empty;

            return Marshal.PtrToStringAnsi((IntPtr)pStr) ?? string.Empty;
        }
    }

    /// <summary>
    /// Helper methods for working with ADLX display services
    /// </summary>
    public static unsafe class ADLXDisplayHelpers
    {
        /// <summary>
        /// Enumerate displays for a GPU
        /// Returns array of display interface pointers
        /// </summary>
        public static IntPtr[] EnumerateDisplays(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            // This will be implemented when we add display services support
            // For now, return empty array as placeholder
            // Will be completed in Stage 5
            return Array.Empty<IntPtr>();
        }
    }

    /// <summary>
    /// Helper methods for ADLX list operations
    /// </summary>
    public static unsafe class ADLXListHelpers
    {
        /// <summary>
        /// Get the size of a list
        /// </summary>
        public static uint GetListSize(IntPtr pList)
        {
            if (pList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pList;
            var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Size, typeof(ADLXVTables.SizeFn));

            return sizeFn(pList);
        }

        /// <summary>
        /// Check if a list is empty
        /// </summary>
        public static bool IsListEmpty(IntPtr pList)
        {
            if (pList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pList;
            var emptyFn = (ADLXVTables.EmptyFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Empty, typeof(ADLXVTables.EmptyFn));

            return emptyFn(pList) != 0;
        }

        /// <summary>
        /// Get item at specific index from list
        /// </summary>
        public static IntPtr GetListItemAt(IntPtr pList, uint index)
        {
            if (pList == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pList));

            var vtbl = *(ADLXVTables.IADLXListVtbl**)pList;
            var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->At, typeof(ADLXVTables.AtFn));

            IntPtr pItem;
            var result = atFn(pList, index, &pItem);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, $"Failed to get item at index {index}");
            }

            return pItem;
        }

        /// <summary>
        /// Convert a list to an array of interface pointers
        /// </summary>
        public static IntPtr[] ListToArray(IntPtr pList)
        {
            if (pList == IntPtr.Zero)
                return Array.Empty<IntPtr>();

            uint count = GetListSize(pList);
            if (count == 0)
                return Array.Empty<IntPtr>();

            var items = new IntPtr[count];
            for (uint i = 0; i < count; i++)
            {
                items[i] = GetListItemAt(pList, i);
            }

            return items;
        }
    }

    /// <summary>
    /// Helper methods for GPU information retrieval
    /// Combines multiple property calls into convenient structs
    /// </summary>
    public static class ADLXGPUInfo
    {
        /// <summary>
        /// Basic GPU information
        /// </summary>
        public struct GPUBasicInfo
        {
            public string Name;
            public string VendorId;
            public int UniqueId;
            public uint TotalVRAM;
            public string VRAMType;
            public bool IsExternal;
            public bool HasDesktops;
        }

        /// <summary>
        /// Get comprehensive basic information about a GPU
        /// </summary>
        public static GPUBasicInfo GetBasicInfo(IntPtr pGPU)
        {
            return new GPUBasicInfo
            {
                Name = ADLXHelpers.GetGPUName(pGPU),
                VendorId = ADLXHelpers.GetGPUVendorId(pGPU),
                UniqueId = ADLXHelpers.GetGPUUniqueId(pGPU),
                TotalVRAM = ADLXHelpers.GetGPUTotalVRAM(pGPU),
                VRAMType = ADLXHelpers.GetGPUVRAMType(pGPU),
                IsExternal = ADLXHelpers.IsGPUExternal(pGPU),
                HasDesktops = ADLXHelpers.HasGPUDesktops(pGPU)
            };
        }

        /// <summary>
        /// GPU identification information
        /// </summary>
        public struct GPUIdentification
        {
            public string DeviceId;
            public string PNPString;
            public string DriverPath;
            public int UniqueId;
        }

        /// <summary>
        /// Get GPU identification information
        /// </summary>
        public static GPUIdentification GetIdentification(IntPtr pGPU)
        {
            return new GPUIdentification
            {
                DeviceId = ADLXHelpers.GetGPUDeviceId(pGPU),
                PNPString = ADLXHelpers.GetGPUPNPString(pGPU),
                DriverPath = ADLXHelpers.GetGPUDriverPath(pGPU),
                UniqueId = ADLXHelpers.GetGPUUniqueId(pGPU)
            };
        }
    }
}
