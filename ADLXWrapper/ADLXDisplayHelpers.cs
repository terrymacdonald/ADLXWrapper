using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Helper methods for ADLX Display operations.
    /// </summary>
    public static unsafe class ADLXDisplayHelpers
    {
        /// <summary>
        /// Gets the IADLXDisplayServices interface from the system services. Callers must dispose the returned pointer.
        /// </summary>
        public static IADLXDisplayServices* GetDisplayServices(IADLXSystem* pSystem)
        {
            if (pSystem == null) throw new ArgumentNullException(nameof(pSystem));

            var getDisplayServicesFn = (delegate* unmanaged[Stdcall]<IADLXSystem*, out IADLXDisplayServices*, ADLX_RESULT>)pSystem->Vtbl->GetDisplaysServices;

            IADLXDisplayServices* pDisplayServices;
            var result = getDisplayServicesFn(pSystem, &pDisplayServices);

            if (result != ADLX_RESULT.ADLX_OK)
            {
                throw new ADLXException(result, "Failed to get display services");
            }

            // Caller must wrap this in a ComPtr and dispose it.
            return (IADLXDisplayServices*)pDisplayServices;
        }

        /// <summary>
        /// Enumerate all displays from system display services
        /// </summary>
        public static IEnumerable<DisplayInfo> EnumerateAllDisplays(IADLXSystem* pSystem)
        {
            if (pSystem == null) yield break;

            using var displayServices = new ComPtr<IADLXDisplayServices>(GetDisplayServices(pSystem));
            if (displayServices.Get() == null) yield break;

            displayServices.Get()->GetDisplays(out var pDisplayList);
            using var displayList = new ComPtr<IADLXDisplayList>(pDisplayList);

            for (uint i = 0; i < displayList.Get()->Size(); i++)
            {
                displayList.Get()->At(i, out var pDisplay);
                using var display = new ComPtr<IADLXDisplay>(pDisplay);
                yield return new DisplayInfo(display.Get());
            }
        }
    }

    /// <summary>
    /// Represents the collected information for a display.
    /// </summary>
    public readonly struct DisplayInfo
    {
        public string Name { get; init; }
        public int Width { get; init; }
        public int Height { get; init; }
        public double RefreshRate { get; init; }
        public uint ManufacturerID { get; init; }
        public uint PixelClock { get; init; }
        public ADLX_DISPLAY_TYPE Type { get; init; }
        public ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType { get; init; }
        public ADLX_DISPLAY_SCAN_TYPE ScanType { get; init; }
        public ulong UniqueId { get; init; }
        public string Edid { get; init; }
        public int GpuUniqueId { get; init; }

        [JsonConstructor]
        public DisplayInfo(string name, int width, int height, double refreshRate, uint manufacturerID, uint pixelClock, ADLX_DISPLAY_TYPE type, ADLX_DISPLAY_CONNECTOR_TYPE connectorType, ADLX_DISPLAY_SCAN_TYPE scanType, ulong uniqueId, string edid, int gpuUniqueId)
        {
            Name = name; Width = width; Height = height; RefreshRate = refreshRate;
            ManufacturerID = manufacturerID; PixelClock = pixelClock; Type = type; ConnectorType = connectorType;
            ScanType = scanType;
            UniqueId = uniqueId;
            Edid = edid;
            GpuUniqueId = gpuUniqueId;
        }

        internal unsafe DisplayInfo(IADLXDisplay* pDisplay)
        {
            pDisplay->Name(out var namePtr); Name = ADLXHelpers.MarshalString(namePtr);
            pDisplay->EDID(out var edidPtr); Edid = ADLXHelpers.MarshalString(edidPtr);
            pDisplay->NativeResolution(out var w, out var h); Width = w; Height = h;
            pDisplay->RefreshRate(out var rr); RefreshRate = rr;
            pDisplay->ManufacturerID(out var mid); ManufacturerID = mid;
            pDisplay->PixelClock(out var pc); PixelClock = pc;
            pDisplay->DisplayType(out var dt); Type = dt;
            pDisplay->ConnectorType(out var ct); ConnectorType = ct;
            pDisplay->ScanType(out var st); ScanType = st;
            pDisplay->UniqueId(out var uid); UniqueId = uid;

            pDisplay->GetGPU(out var pGpu);
            using var gpu = new ComPtr<IADLXGPU>(pGpu);
            gpu.Get()->UniqueId(out var gpuId);
            GpuUniqueId = gpuId;
        }
    }
}