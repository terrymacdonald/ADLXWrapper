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
        /// Acquire display services and wrap in a SafeHandle.
        /// </summary>
        public static AdlxInterfaceHandle GetDisplayServicesHandle(IntPtr pSystem)
        {
            if (pSystem == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSystem));

            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)pSystem;
            var getDisplayServicesFn = Marshal.GetDelegateForFunctionPointer<GetDisplayServicesFn>(
                systemVtbl->GetDisplaysServices);

            IntPtr pDisplayServices;
            var result = getDisplayServicesFn(pSystem, &pDisplayServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display services");
            }

            return AdlxInterfaceHandle.From(pDisplayServices);
        }

        /// <summary>
        /// Enumerate displays for a GPU
        /// Returns array of display interface pointers
        /// </summary>
        public static IntPtr[] EnumerateDisplays(IntPtr pGPU)
        {
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            // We need the system services to get display services
            // This will be called from tests that have access to system services
            // For now, return empty array - will be implemented when we have display services access
            return Array.Empty<IntPtr>();
        }

        /// <summary>
        /// Enumerate all displays from system display services
        /// Returns array of display interface pointers
        /// </summary>
        public static IntPtr[] EnumerateAllDisplays(IntPtr pSystem)
        {
            if (pSystem == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSystem));

            // Get display services from system
            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)pSystem;
            var getDisplayServicesFn = Marshal.GetDelegateForFunctionPointer<GetDisplayServicesFn>(
                systemVtbl->GetDisplaysServices);

            IntPtr pDisplayServices;
            var result = getDisplayServicesFn(pSystem, &pDisplayServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display services");
            }

            if (pDisplayServices == IntPtr.Zero)
            {
                return Array.Empty<IntPtr>();
            }

            try
            {
                // Get displays from display services
                var displayServicesVtbl = *(ADLXVTables.IADLXDisplayServicesVtbl**)pDisplayServices;
                var getDisplaysFn = (ADLXVTables.GetDisplaysFn)Marshal.GetDelegateForFunctionPointer(
                    displayServicesVtbl->GetDisplays, typeof(ADLXVTables.GetDisplaysFn));

                IntPtr pDisplayList;
                result = getDisplaysFn(pDisplayServices, &pDisplayList);

                if (result != ADLX_RESULT.ADLX_OK)
                {
                    throw new ADLXException(result, "Failed to get display list");
                }

                if (pDisplayList == IntPtr.Zero)
                {
                    return Array.Empty<IntPtr>();
                }

                try
                {
                    // Get list size
                    var listVtbl = *(ADLXVTables.IADLXDisplayListVtbl**)pDisplayList;
                    var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(
                        listVtbl->Size, typeof(ADLXVTables.SizeFn));
                    var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(
                        listVtbl->At, typeof(ADLXVTables.AtFn));

                    uint count = sizeFn(pDisplayList);

                    if (count == 0)
                    {
                        return Array.Empty<IntPtr>();
                    }

                    // Get each display from the list
                    var displays = new IntPtr[count];
                    for (uint i = 0; i < count; i++)
                    {
                        IntPtr pDisplay;
                        result = atFn(pDisplayList, i, &pDisplay);

                        if (result != ADLX_RESULT.ADLX_OK)
                        {
                            throw new ADLXException(result, $"Failed to get display at index {i}");
                        }

                        displays[i] = pDisplay;
                    }

                    return displays;
                }
                finally
                {
                    // Release the display list interface
                    ADLXHelpers.ReleaseInterface(pDisplayList);
                }
            }
            finally
            {
                // Release the display services interface
                ADLXHelpers.ReleaseInterface(pDisplayServices);
            }
        }

        /// <summary>
        /// Enumerate all displays from system display services, returning SafeHandles for automatic release.
        /// </summary>
        public static AdlxInterfaceHandle[] EnumerateAllDisplayHandles(IntPtr pSystem)
        {
            var raw = EnumerateAllDisplays(pSystem);
            var handles = new AdlxInterfaceHandle[raw.Length];
            for (int i = 0; i < raw.Length; i++)
            {
                handles[i] = AdlxInterfaceHandle.From(raw[i]);
            }
            return handles;
        }

        // Delegate for GetDisplaysServices
        [UnmanagedFunctionPointer(CallingConvention.StdCall)]
        private delegate ADLX_RESULT GetDisplayServicesFn(IntPtr pThis, IntPtr* ppDisplayServices);

        /// <summary>
        /// Get display name
        /// </summary>
        public static string GetDisplayName(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var nameFn = (ADLXVTables.DisplayNameFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->Name, typeof(ADLXVTables.DisplayNameFn));

            byte* pName;
            var result = nameFn(pDisplay, &pName);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display name");
            }

            return MarshalString(pName);
        }

        /// <summary>
        /// Get display native resolution
        /// </summary>
        public static (int width, int height) GetDisplayNativeResolution(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var resolutionFn = (ADLXVTables.NativeResolutionFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->NativeResolution, typeof(ADLXVTables.NativeResolutionFn));

            int width, height;
            var result = resolutionFn(pDisplay, &width, &height);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display resolution");
            }

            return (width, height);
        }

        /// <summary>
        /// Get display refresh rate
        /// </summary>
        public static double GetDisplayRefreshRate(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var refreshRateFn = (ADLXVTables.RefreshRateFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->RefreshRate, typeof(ADLXVTables.RefreshRateFn));

            double refreshRate;
            var result = refreshRateFn(pDisplay, &refreshRate);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display refresh rate");
            }

            return refreshRate;
        }

        /// <summary>
        /// Get display manufacturer ID
        /// </summary>
        public static uint GetDisplayManufacturerID(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var manufacturerIDFn = (ADLXVTables.ManufacturerIDFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->ManufacturerID, typeof(ADLXVTables.ManufacturerIDFn));

            uint manufacturerID;
            var result = manufacturerIDFn(pDisplay, &manufacturerID);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display manufacturer ID");
            }

            return manufacturerID;
        }

        /// <summary>
        /// Get display pixel clock
        /// </summary>
        public static uint GetDisplayPixelClock(IntPtr pDisplay)
        {
            if (pDisplay == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDisplay));

            var vtbl = *(ADLXVTables.IADLXDisplayVtbl**)pDisplay;
            var pixelClockFn = (ADLXVTables.PixelClockFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->PixelClock, typeof(ADLXVTables.PixelClockFn));

            uint pixelClock;
            var result = pixelClockFn(pDisplay, &pixelClock);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display pixel clock");
            }

            return pixelClock;
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

    /// <summary>
    /// Helper methods for GPU tuning services
    /// </summary>
    public static unsafe class ADLXGPUTuningHelpers
    {
        /// <summary>
        /// Check if auto tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedAutoTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedAutoTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check auto tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if preset tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedPresetTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedPresetTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check preset tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual GFX tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedManualGFXTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedManualGFXTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual GFX tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual VRAM tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedManualVRAMTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedManualVRAMTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual VRAM tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual fan tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedManualFanTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedManualFanTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual fan tuning support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if manual power tuning is supported for a GPU
        /// </summary>
        public static bool IsSupportedManualPowerTuning(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            if (pGPUTuningServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPUTuningServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXGPUTuningServicesVtbl**)pGPUTuningServices;
            var isSupportedFn = (ADLXVTables.IsSupportedTuningFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedManualPowerTuning, typeof(ADLXVTables.IsSupportedTuningFn));

            byte supported;
            var result = isSupportedFn(pGPUTuningServices, pGPU, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check manual power tuning support");
            }

            return supported != 0;
        }
    }

    /// <summary>
    /// Helper methods for GPU performance monitoring information retrieval
    /// </summary>
    public static class ADLXPerformanceMonitoringInfo
    {
        /// <summary>
        /// GPU metrics support capabilities
        /// </summary>
        public struct GPUMetricsSupport
        {
            public bool UsageSupported;
            public bool ClockSpeedSupported;
            public bool TemperatureSupported;
            public bool PowerSupported;
            public bool FanSpeedSupported;
            public bool VRAMSupported;
        }

        /// <summary>
        /// Current GPU metrics values
        /// </summary>
        public struct GPUMetricsSnapshot
        {
            public double Temperature;
            public double Usage;
            public int ClockSpeed;
            public int VRAMClockSpeed;
            public int VRAMUsage;
            public int FanSpeed;
            public double Power;
        }

        /// <summary>
        /// Get metrics support for a GPU
        /// </summary>
        public static GPUMetricsSupport GetMetricsSupport(IntPtr pPerfMonServices, IntPtr pGPU)
        {
            var pMetricsSupport = ADLXPerformanceMonitoringHelpers.GetSupportedGPUMetrics(pPerfMonServices, pGPU);
            try
            {
                var support = new GPUMetricsSupport();

                try { support.UsageSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUUsage(pMetricsSupport); }
                catch { support.UsageSupported = false; }

                try { support.ClockSpeedSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUClockSpeed(pMetricsSupport); }
                catch { support.ClockSpeedSupported = false; }

                try { support.TemperatureSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUTemperature(pMetricsSupport); }
                catch { support.TemperatureSupported = false; }

                try { support.PowerSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUPower(pMetricsSupport); }
                catch { support.PowerSupported = false; }

                try { support.FanSpeedSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUFanSpeed(pMetricsSupport); }
                catch { support.FanSpeedSupported = false; }

                try { support.VRAMSupported = ADLXPerformanceMonitoringHelpers.IsSupportedGPUVRAM(pMetricsSupport); }
                catch { support.VRAMSupported = false; }

                return support;
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetricsSupport);
            }
        }

        /// <summary>
        /// Get current metrics snapshot for a GPU
        /// </summary>
        public static GPUMetricsSnapshot GetCurrentMetrics(IntPtr pPerfMonServices, IntPtr pGPU)
        {
            var pMetrics = ADLXPerformanceMonitoringHelpers.GetCurrentGPUMetrics(pPerfMonServices, pGPU);
            try
            {
                var snapshot = new GPUMetricsSnapshot();

                try { snapshot.Temperature = ADLXPerformanceMonitoringHelpers.GetGPUTemperature(pMetrics); }
                catch { snapshot.Temperature = 0; }

                try { snapshot.Usage = ADLXPerformanceMonitoringHelpers.GetGPUUsage(pMetrics); }
                catch { snapshot.Usage = 0; }

                try { snapshot.ClockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUClockSpeed(pMetrics); }
                catch { snapshot.ClockSpeed = 0; }

                try { snapshot.VRAMClockSpeed = ADLXPerformanceMonitoringHelpers.GetGPUVRAMClockSpeed(pMetrics); }
                catch { snapshot.VRAMClockSpeed = 0; }

                try { snapshot.VRAMUsage = ADLXPerformanceMonitoringHelpers.GetGPUVRAM(pMetrics); }
                catch { snapshot.VRAMUsage = 0; }

                try { snapshot.FanSpeed = ADLXPerformanceMonitoringHelpers.GetGPUFanSpeed(pMetrics); }
                catch { snapshot.FanSpeed = 0; }

                try { snapshot.Power = ADLXPerformanceMonitoringHelpers.GetGPUPower(pMetrics); }
                catch { snapshot.Power = 0; }

                return snapshot;
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pMetrics);
            }
        }
    }

    /// <summary>
    /// Helper methods for GPU tuning information retrieval
    /// </summary>
    public static class ADLXGPUTuningInfo
    {
        /// <summary>
        /// GPU tuning capabilities
        /// </summary>
        public struct GPUTuningCapabilities
        {
            public bool AutoTuningSupported;
            public bool PresetTuningSupported;
            public bool ManualGFXTuningSupported;
            public bool ManualVRAMTuningSupported;
            public bool ManualFanTuningSupported;
            public bool ManualPowerTuningSupported;
        }

        /// <summary>
        /// Get comprehensive tuning capabilities for a GPU
        /// </summary>
        public static GPUTuningCapabilities GetTuningCapabilities(IntPtr pGPUTuningServices, IntPtr pGPU)
        {
            var capabilities = new GPUTuningCapabilities();

            try
            {
                capabilities.AutoTuningSupported = ADLXGPUTuningHelpers.IsSupportedAutoTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.AutoTuningSupported = false;
            }

            try
            {
                capabilities.PresetTuningSupported = ADLXGPUTuningHelpers.IsSupportedPresetTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.PresetTuningSupported = false;
            }

            try
            {
                capabilities.ManualGFXTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualGFXTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.ManualGFXTuningSupported = false;
            }

            try
            {
                capabilities.ManualVRAMTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualVRAMTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.ManualVRAMTuningSupported = false;
            }

            try
            {
                capabilities.ManualFanTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualFanTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.ManualFanTuningSupported = false;
            }

            try
            {
                capabilities.ManualPowerTuningSupported = ADLXGPUTuningHelpers.IsSupportedManualPowerTuning(pGPUTuningServices, pGPU);
            }
            catch
            {
                capabilities.ManualPowerTuningSupported = false;
            }

            return capabilities;
        }
    }

    /// <summary>
    /// Helper methods for display information retrieval
    /// Combines multiple property calls into convenient structs
    /// </summary>
    public static class ADLXDisplayInfo
    {
        /// <summary>
        /// Basic display information
        /// </summary>
        public struct DisplayBasicInfo
        {
            public string Name;
            public int Width;
            public int Height;
            public double RefreshRate;
            public uint ManufacturerID;
            public uint PixelClock;
        }

        /// <summary>
        /// Get comprehensive basic information about a display
        /// </summary>
        public static DisplayBasicInfo GetBasicInfo(IntPtr pDisplay)
        {
            var resolution = ADLXDisplayHelpers.GetDisplayNativeResolution(pDisplay);

            return new DisplayBasicInfo
            {
                Name = ADLXDisplayHelpers.GetDisplayName(pDisplay),
                Width = resolution.width,
                Height = resolution.height,
                RefreshRate = ADLXDisplayHelpers.GetDisplayRefreshRate(pDisplay),
                ManufacturerID = ADLXDisplayHelpers.GetDisplayManufacturerID(pDisplay),
                PixelClock = ADLXDisplayHelpers.GetDisplayPixelClock(pDisplay)
            };
        }
    }

    /// <summary>
    /// Helper methods for performance monitoring services
    /// </summary>
    public static unsafe class ADLXPerformanceMonitoringHelpers
    {
        /// <summary>
        /// Get supported GPU metrics for a GPU
        /// </summary>
        public static IntPtr GetSupportedGPUMetrics(IntPtr pPerfMonServices, IntPtr pGPU)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getSupportedMetricsFn = (ADLXVTables.GetSupportedGPUMetricsFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetSupportedGPUMetrics, typeof(ADLXVTables.GetSupportedGPUMetricsFn));

            IntPtr pMetricsSupport;
            var result = getSupportedMetricsFn(pPerfMonServices, pGPU, &pMetricsSupport);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get supported GPU metrics");
            }

            return pMetricsSupport;
        }

        /// <summary>
        /// Get current GPU metrics for a GPU
        /// </summary>
        public static IntPtr GetCurrentGPUMetrics(IntPtr pPerfMonServices, IntPtr pGPU)
        {
            if (pPerfMonServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pPerfMonServices));
            if (pGPU == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pGPU));

            var vtbl = *(ADLXVTables.IADLXPerformanceMonitoringServicesVtbl**)pPerfMonServices;
            var getCurrentMetricsFn = (ADLXVTables.GetCurrentGPUMetricsFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GetCurrentGPUMetrics, typeof(ADLXVTables.GetCurrentGPUMetricsFn));

            IntPtr pMetrics;
            var result = getCurrentMetricsFn(pPerfMonServices, pGPU, &pMetrics);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get current GPU metrics");
            }

            return pMetrics;
        }

        /// <summary>
        /// Check if GPU usage metric is supported
        /// </summary>
        public static bool IsSupportedGPUUsage(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUUsage, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU usage support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU clock speed metric is supported
        /// </summary>
        public static bool IsSupportedGPUClockSpeed(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUClockSpeed, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU clock speed support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU temperature metric is supported
        /// </summary>
        public static bool IsSupportedGPUTemperature(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUTemperature, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU temperature support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU power metric is supported
        /// </summary>
        public static bool IsSupportedGPUPower(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUPower, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU power support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU fan speed metric is supported
        /// </summary>
        public static bool IsSupportedGPUFanSpeed(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUFanSpeed, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU fan speed support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Check if GPU VRAM metric is supported
        /// </summary>
        public static bool IsSupportedGPUVRAM(IntPtr pMetricsSupport)
        {
            if (pMetricsSupport == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetricsSupport));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsSupportVtbl**)pMetricsSupport;
            var isSupportedFn = (ADLXVTables.IsSupportedMetricFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->IsSupportedGPUVRAM, typeof(ADLXVTables.IsSupportedMetricFn));

            byte supported;
            var result = isSupportedFn(pMetricsSupport, &supported);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to check GPU VRAM support");
            }

            return supported != 0;
        }

        /// <summary>
        /// Get GPU temperature from metrics
        /// </summary>
        public static double GetGPUTemperature(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var temperatureFn = (ADLXVTables.GPUTemperatureFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUTemperature, typeof(ADLXVTables.GPUTemperatureFn));

            double temperature;
            var result = temperatureFn(pMetrics, &temperature);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU temperature");
            }

            return temperature;
        }

        /// <summary>
        /// Get GPU usage from metrics
        /// </summary>
        public static double GetGPUUsage(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var usageFn = (ADLXVTables.GPUUsageFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUUsage, typeof(ADLXVTables.GPUUsageFn));

            double usage;
            var result = usageFn(pMetrics, &usage);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU usage");
            }

            return usage;
        }

        /// <summary>
        /// Get GPU clock speed from metrics
        /// </summary>
        public static int GetGPUClockSpeed(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var clockSpeedFn = (ADLXVTables.GPUClockSpeedFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUClockSpeed, typeof(ADLXVTables.GPUClockSpeedFn));

            int clockSpeed;
            var result = clockSpeedFn(pMetrics, &clockSpeed);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU clock speed");
            }

            return clockSpeed;
        }

        /// <summary>
        /// Get GPU VRAM clock speed from metrics
        /// </summary>
        public static int GetGPUVRAMClockSpeed(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var vramClockSpeedFn = (ADLXVTables.GPUClockSpeedFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUVRAMClockSpeed, typeof(ADLXVTables.GPUClockSpeedFn));

            int vramClockSpeed;
            var result = vramClockSpeedFn(pMetrics, &vramClockSpeed);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU VRAM clock speed");
            }

            return vramClockSpeed;
        }

        /// <summary>
        /// Get GPU VRAM usage from metrics
        /// </summary>
        public static int GetGPUVRAM(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var vramFn = (ADLXVTables.GPUVRAMFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUVRAM, typeof(ADLXVTables.GPUVRAMFn));

            int vram;
            var result = vramFn(pMetrics, &vram);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU VRAM");
            }

            return vram;
        }

        /// <summary>
        /// Get GPU fan speed from metrics
        /// </summary>
        public static int GetGPUFanSpeed(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var fanSpeedFn = (ADLXVTables.GPUClockSpeedFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUFanSpeed, typeof(ADLXVTables.GPUClockSpeedFn));

            int fanSpeed;
            var result = fanSpeedFn(pMetrics, &fanSpeed);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU fan speed");
            }

            return fanSpeed;
        }

        /// <summary>
        /// Get GPU power from metrics
        /// </summary>
        public static double GetGPUPower(IntPtr pMetrics)
        {
            if (pMetrics == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pMetrics));

            var vtbl = *(ADLXVTables.IADLXGPUMetricsVtbl**)pMetrics;
            var powerFn = (ADLXVTables.GPUPowerFn)Marshal.GetDelegateForFunctionPointer(
                vtbl->GPUPower, typeof(ADLXVTables.GPUPowerFn));

            double power;
            var result = powerFn(pMetrics, &power);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get GPU power");
            }

            return power;
        }
    }

    /// <summary>
    /// Helper methods for desktop and Eyefinity services
    /// </summary>
    public static unsafe class ADLXDesktopHelpers
    {
        public static AdlxInterfaceHandle GetDesktopServicesHandle(IntPtr pSystem)
        {
            if (pSystem == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSystem));

            var systemVtbl = *(ADLXVTables.IADLXSystemVtbl**)pSystem;
            var getDesktopServicesFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDesktopsServicesFn>(
                systemVtbl->GetDesktopsServices);

            IntPtr pDesktopServices;
            var result = getDesktopServicesFn(pSystem, &pDesktopServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop services");
            }

            return AdlxInterfaceHandle.From(pDesktopServices);
        }

        public static IntPtr[] EnumerateAllDesktops(IntPtr pDesktopServices)
        {
            if (pDesktopServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktopServices));

            var svcVtbl = *(ADLXVTables.IADLXDesktopServicesVtbl**)pDesktopServices;
            var getDesktopsFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetDesktopsFn>(
                svcVtbl->GetDesktops);

            IntPtr pDesktopList;
            var result = getDesktopsFn(pDesktopServices, &pDesktopList);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop list");
            }

            if (pDesktopList == IntPtr.Zero)
            {
                return Array.Empty<IntPtr>();
            }

            try
            {
                var listVtbl = *(ADLXVTables.IADLXDesktopListVtbl**)pDesktopList;
                var sizeFn = (ADLXVTables.SizeFn)Marshal.GetDelegateForFunctionPointer(listVtbl->Size, typeof(ADLXVTables.SizeFn));
                var atFn = (ADLXVTables.AtFn)Marshal.GetDelegateForFunctionPointer(listVtbl->At, typeof(ADLXVTables.AtFn));

                uint count = sizeFn(pDesktopList);
                if (count == 0)
                {
                    return Array.Empty<IntPtr>();
                }

                var desktops = new IntPtr[count];
                for (uint i = 0; i < count; i++)
                {
                    IntPtr pDesktop;
                    result = atFn(pDesktopList, i, &pDesktop);
                    if (result != ADLX_RESULT.ADLX_OK)
                    {
                        throw new ADLXException(result, $"Failed to get desktop at index {i}");
                    }
                    desktops[i] = pDesktop;
                }

                return desktops;
            }
            finally
            {
                ADLXHelpers.ReleaseInterface(pDesktopList);
            }
        }

        public static AdlxInterfaceHandle[] EnumerateAllDesktopHandles(IntPtr pDesktopServices)
        {
            var raw = EnumerateAllDesktops(pDesktopServices);
            var handles = new AdlxInterfaceHandle[raw.Length];
            for (int i = 0; i < raw.Length; i++)
            {
                handles[i] = AdlxInterfaceHandle.From(raw[i]);
            }
            return handles;
        }

        public static ADLX_DESKTOP_TYPE GetDesktopType(IntPtr pDesktop)
        {
            if (pDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktop));

            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)pDesktop;
            var typeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopTypeFn>(vtbl->Type);

            ADLX_DESKTOP_TYPE type;
            var result = typeFn(pDesktop, &type);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop type");
            }

            return type;
        }

        public static (int Width, int Height) GetDesktopSize(IntPtr pDesktop)
        {
            if (pDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktop));

            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)pDesktop;
            var sizeFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopSizeFn>(vtbl->Size);

            int w, h;
            var result = sizeFn(pDesktop, &w, &h);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop size");
            }

            return (w, h);
        }

        public static (int X, int Y) GetDesktopTopLeft(IntPtr pDesktop)
        {
            if (pDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktop));

            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)pDesktop;
            var topLeftFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopTopLeftFn>(vtbl->TopLeft);

            ADLX_Point pt;
            var result = topLeftFn(pDesktop, &pt);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop top-left");
            }

            return (pt.x, pt.y);
        }

        public static ADLX_ORIENTATION GetDesktopOrientation(IntPtr pDesktop)
        {
            if (pDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktop));

            var vtbl = *(ADLXVTables.IADLXDesktopVtbl**)pDesktop;
            var orientFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.DesktopOrientationFn>(vtbl->Orientation);

            ADLX_ORIENTATION orientation;
            var result = orientFn(pDesktop, &orientation);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get desktop orientation");
            }

            return orientation;
        }

        public static AdlxInterfaceHandle GetSimpleEyefinityHandle(IntPtr pDesktopServices)
        {
            if (pDesktopServices == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pDesktopServices));

            var vtbl = *(ADLXVTables.IADLXDesktopServicesVtbl**)pDesktopServices;
            var getFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.GetSimpleEyefinityFn>(vtbl->GetSimpleEyefinity);

            IntPtr pSimple;
            var result = getFn(pDesktopServices, &pSimple);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get simple Eyefinity interface");
            }

            return AdlxInterfaceHandle.From(pSimple);
        }

        public static bool IsSimpleEyefinitySupported(IntPtr pSimpleEyefinity)
        {
            if (pSimpleEyefinity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var vtbl = *(ADLXVTables.IADLXSimpleEyefinityVtbl**)pSimpleEyefinity;
            var supportedFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityIsSupportedFn>(vtbl->IsSupported);

            byte supported;
            var result = supportedFn(pSimpleEyefinity, &supported);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to query Eyefinity support");
            }

            return supported != 0;
        }

        public static AdlxInterfaceHandle CreateEyefinityDesktop(IntPtr pSimpleEyefinity)
        {
            if (pSimpleEyefinity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var vtbl = *(ADLXVTables.IADLXSimpleEyefinityVtbl**)pSimpleEyefinity;
            var createFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityCreateFn>(vtbl->Create);

            IntPtr pDesktop;
            var result = createFn(pSimpleEyefinity, &pDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to create Eyefinity desktop");
            }

            return AdlxInterfaceHandle.From(pDesktop);
        }

        public static void DestroyEyefinityDesktop(IntPtr pSimpleEyefinity, IntPtr pEyefinityDesktop)
        {
            if (pSimpleEyefinity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSimpleEyefinity));
            if (pEyefinityDesktop == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pEyefinityDesktop));

            var vtbl = *(ADLXVTables.IADLXSimpleEyefinityVtbl**)pSimpleEyefinity;
            var destroyFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityDestroyFn>(vtbl->Destroy);

            var result = destroyFn(pSimpleEyefinity, pEyefinityDesktop);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy Eyefinity desktop");
            }
        }

        public static void DestroyAllEyefinityDesktops(IntPtr pSimpleEyefinity)
        {
            if (pSimpleEyefinity == IntPtr.Zero)
                throw new ArgumentNullException(nameof(pSimpleEyefinity));

            var vtbl = *(ADLXVTables.IADLXSimpleEyefinityVtbl**)pSimpleEyefinity;
            var destroyAllFn = Marshal.GetDelegateForFunctionPointer<ADLXVTables.EyefinityDestroyAllFn>(vtbl->DestroyAll);

            var result = destroyAllFn(pSimpleEyefinity);
            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to destroy all Eyefinity desktops");
            }
        }
    }
}
