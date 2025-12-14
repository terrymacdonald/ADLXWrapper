using System;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    /// <summary>
    /// Facade for GPU tuning services bound to a specific GPU.
    /// </summary>
    public sealed unsafe class AdlxGpuTuning : IDisposable
    {
        private readonly ADLXApi _owner;
        private ComPtr<IADLXGPUTuningServices> _services;
        private ComPtr<IADLXGPU> _gpu;
        private bool _disposed;

        internal AdlxGpuTuning(ADLXApi owner, IADLXGPUTuningServices* services, IADLXGPU* gpu)
        {
            _owner = owner ?? throw new ArgumentNullException(nameof(owner));
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (gpu == null) throw new ArgumentNullException(nameof(gpu));
            _services = new ComPtr<IADLXGPUTuningServices>(services);
            _gpu = new ComPtr<IADLXGPU>(gpu);
        }

        public GpuTuningProfile GetProfile()
        {
            ThrowIfDisposed();
            var capabilities = new GpuTuningCapabilitiesInfo(_services.Get(), _gpu.Get());
            var auto = ADLXGPUTuningHelpers.GetAutoTuning(_services.Get(), _gpu.Get());
            var preset = ADLXGPUTuningHelpers.GetPresetTuning(_services.Get(), _gpu.Get());
            var manualFan = ADLXGPUTuningHelpers.GetManualFanTuning(_services.Get(), _gpu.Get());
            var manualVram = ADLXGPUTuningHelpers.GetManualVramTuning(_services.Get(), _gpu.Get());
            var manualGfx = ADLXGPUTuningHelpers.GetManualGfxTuning(_services.Get(), _gpu.Get());
            var manualPower = ADLXPowerTuningHelpers.GetManualPowerTuning(_services.Get(), _gpu.Get());
            return new GpuTuningProfile(capabilities, auto, preset, manualFan, manualVram, manualGfx, manualPower);
        }

        public void ApplyProfile(GpuTuningProfile profile)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();

            // Apply in order: preset then manual knobs as provided. Auto tuning apply is not exposed in helpers; capture only.
            if (profile.PresetTuning != null)
            {
                IADLXInterface* pPreset = null;
                var res = _services.Get()->GetPresetTuning(_gpu.Get(), &pPreset);
                if (res == ADLX_RESULT.ADLX_OK && pPreset != null)
                {
                    using var preset = new ComPtr<IADLXGPUPresetTuning>((IADLXGPUPresetTuning*)pPreset);
                    ADLXGPUTuningHelpers.ApplyPresetTuning(preset.Get(), profile.PresetTuning.Value);
                }
            }

            if (profile.ManualFan != null)
            {
                IADLXInterface* pFan = null;
                var res = _services.Get()->GetManualFanTuning(_gpu.Get(), &pFan);
                if (res == ADLX_RESULT.ADLX_OK && pFan != null)
                {
                    using var fan = new ComPtr<IADLXManualFanTuning>((IADLXManualFanTuning*)pFan);
                    ADLXGPUTuningHelpers.ApplyManualFanTuning(fan.Get(), profile.ManualFan.Value);
                }
            }

            if (profile.ManualVram != null)
            {
                IADLXInterface* pVram = null;
                var res = _services.Get()->GetManualVRAMTuning(_gpu.Get(), &pVram);
                if (res == ADLX_RESULT.ADLX_OK && pVram != null)
                {
                    using var vramBase = new ComPtr<IADLXInterface>(pVram);
                    using var vram = ADLXHelpers.RequireInterface<IADLXManualVRAMTuning1>((IntPtr)vramBase.Get(), nameof(IADLXManualVRAMTuning1));
                    ADLXGPUTuningHelpers.ApplyManualVramTuning(vram.Get(), profile.ManualVram.Value);
                }
            }

            if (profile.ManualGfx != null)
            {
                IADLXInterface* pGfx = null;
                var res = _services.Get()->GetManualGFXTuning(_gpu.Get(), &pGfx);
                if (res == ADLX_RESULT.ADLX_OK && pGfx != null)
                {
                    using var gfxBase = new ComPtr<IADLXInterface>(pGfx);
                    using var gfx = ADLXHelpers.RequireInterface<IADLXManualGraphicsTuning2>((IntPtr)gfxBase.Get(), nameof(IADLXManualGraphicsTuning2));
                    ADLXGPUTuningHelpers.ApplyManualGfxTuning(gfx.Get(), profile.ManualGfx.Value);
                }
            }

            if (profile.ManualPower != null)
            {
                IADLXInterface* pPower = null;
                var res = _services.Get()->GetManualPowerTuning(_gpu.Get(), &pPower);
                if (res == ADLX_RESULT.ADLX_OK && pPower != null)
                {
                    using var power = new ComPtr<IADLXManualPowerTuning>((IADLXManualPowerTuning*)pPower);
                    ADLXPowerTuningHelpers.ApplyManualPowerTuning(power.Get(), profile.ManualPower.Value);
                }
            }
        }

        public AutoTuningInfo GetAutoTuning() => ADLXGPUTuningHelpers.GetAutoTuning(_services.Get(), _gpu.Get());
        public PresetTuningInfo GetPresetTuning() => ADLXGPUTuningHelpers.GetPresetTuning(_services.Get(), _gpu.Get());
        public ManualFanTuningInfo GetManualFanTuning() => ADLXGPUTuningHelpers.GetManualFanTuning(_services.Get(), _gpu.Get());
        public ManualVramTuningInfo GetManualVramTuning() => ADLXGPUTuningHelpers.GetManualVramTuning(_services.Get(), _gpu.Get());
        public ManualGfxTuningInfo GetManualGfxTuning() => ADLXGPUTuningHelpers.GetManualGfxTuning(_services.Get(), _gpu.Get());
        public ManualPowerTuningInfo GetManualPowerTuning() => ADLXPowerTuningHelpers.GetManualPowerTuning(_services.Get(), _gpu.Get());

        public AdlxInterfaceHandle GetGpuTuningServicesHandle()
        {
            ThrowIfDisposed();
            ADLXHelpers.AddRefInterface((IntPtr)_services.Get());
            return AdlxInterfaceHandle.From(_services.Get(), addRef: false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (_disposed) return;
            _services.Dispose();
            _gpu.Dispose();
            _services = default;
            _gpu = default;
            _disposed = true;
        }

        ~AdlxGpuTuning()
        {
            Dispose(false);
        }

        private void ThrowIfDisposed()
        {
            if (_disposed)
                throw new ObjectDisposedException(nameof(AdlxGpuTuning));
        }
    }

    /// <summary>
    /// JSON-serializable GPU tuning profile.
    /// </summary>
    public sealed class GpuTuningProfile
    {
        public GpuTuningCapabilitiesInfo Capabilities { get; set; }
        public AutoTuningInfo? AutoTuning { get; set; }
        public PresetTuningInfo? PresetTuning { get; set; }
        public ManualFanTuningInfo? ManualFan { get; set; }
        public ManualVramTuningInfo? ManualVram { get; set; }
        public ManualGfxTuningInfo? ManualGfx { get; set; }
        public ManualPowerTuningInfo? ManualPower { get; set; }

        [JsonConstructor]
        public GpuTuningProfile(
            GpuTuningCapabilitiesInfo capabilities,
            AutoTuningInfo? autoTuning,
            PresetTuningInfo? presetTuning,
            ManualFanTuningInfo? manualFan,
            ManualVramTuningInfo? manualVram,
            ManualGfxTuningInfo? manualGfx,
            ManualPowerTuningInfo? manualPower)
        {
            Capabilities = capabilities;
            AutoTuning = autoTuning;
            PresetTuning = presetTuning;
            ManualFan = manualFan;
            ManualVram = manualVram;
            ManualGfx = manualGfx;
            ManualPower = manualPower;
        }

        internal GpuTuningProfile(
            GpuTuningCapabilitiesInfo capabilities,
            AutoTuningInfo auto,
            PresetTuningInfo preset,
            ManualFanTuningInfo fan,
            ManualVramTuningInfo vram,
            ManualGfxTuningInfo gfx,
            ManualPowerTuningInfo manualPower)
        {
            Capabilities = capabilities;
            AutoTuning = auto;
            PresetTuning = preset;
            ManualFan = fan;
            ManualVram = vram;
            ManualGfx = gfx;
            ManualPower = manualPower;
        }
    }
}
