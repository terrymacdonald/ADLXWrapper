using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary> 
    /// Helper methods for ADLX GPU operations.
    /// </summary>
    public static unsafe class ADLXGpuHelpers
    {
        /// <summary>
        /// Enumerates all available GPUs.
        /// </summary>
        public static IEnumerable<GpuInfo> EnumerateAllGpus(IADLXSystem* pSystem)
        {
            if (pSystem == null) yield break;

            pSystem->GetGPUs(out var pGpuList);
            using var gpuList = new ComPtr<IADLXGPUList>(pGpuList);

            for (uint i = 0; i < gpuList.Get()->Size(); i++)
            {
                gpuList.Get()->At(i, out var pGpu);
                using var gpu = new ComPtr<IADLXGPU>(pGpu);
                yield return new GpuInfo(gpu.Get());
            }
        }
    }

    /// <summary>
    /// Represents the collected information for a GPU.
    /// </summary>
    public readonly struct GpuInfo
    {
        public string Name { get; init; }
        public string VendorId { get; init; }
        public int UniqueId { get; init; }
        public uint TotalVRAM { get; init; }
        public string VRAMType { get; init; }
        public bool IsExternal { get; init; }
        public bool HasDesktops { get; init; }
        public string DeviceId { get; init; }
        public string PNPString { get; init; }
        public string DriverPath { get; init; }

        [JsonConstructor]
        public GpuInfo(string name, string vendorId, int uniqueId, uint totalVRAM, string vramType, bool isExternal, bool hasDesktops, string deviceId, string pnpString, string driverPath)
        {
            Name = name;
            VendorId = vendorId;
            UniqueId = uniqueId;
            TotalVRAM = totalVRAM;
            VRAMType = vramType;
            IsExternal = isExternal;
            HasDesktops = hasDesktops;
            DeviceId = deviceId;
            PNPString = pnpString;
            DriverPath = driverPath;
        }

        internal unsafe GpuInfo(IADLXGPU* pGpu)
        {
            pGpu->Name(out var namePtr);
            Name = ADLXHelpers.MarshalString(namePtr);

            pGpu->VendorId(out var vendorIdPtr);
            VendorId = ADLXHelpers.MarshalString(vendorIdPtr);

            pGpu->UniqueId(out var uid);
            UniqueId = uid;

            pGpu->TotalVRAM(out var vram);
            TotalVRAM = vram;

            pGpu->VRAMType(out var vramTypePtr);
            VRAMType = ADLXHelpers.MarshalString(vramTypePtr);

            pGpu->IsExternal(out var isExt);
            IsExternal = isExt != 0;

            pGpu->HasDesktops(out var hasDesk);
            HasDesktops = hasDesk != 0;

            pGpu->DeviceId(out var devIdPtr);
            DeviceId = ADLXHelpers.MarshalString(devIdPtr);

            pGpu->PNPString(out var pnpPtr);
            PNPString = ADLXHelpers.MarshalString(pnpPtr);

            pGpu->DriverPath(out var driverPathPtr);
            DriverPath = ADLXHelpers.MarshalString(driverPathPtr);
        }
    }
}