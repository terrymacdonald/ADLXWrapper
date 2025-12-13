using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace ADLXWrapper
{
    public sealed unsafe class AdlxDisplay : IDisposable
    {
        private readonly ADLXSystemServices _system;
        private ComPtr<IADLXDisplay> _display;
        private ComPtr<IADLXDisplayServices> _displayServices;
        private AdlxDesktop? _desktop;
        private bool _disposed;

        internal AdlxDisplay(ADLXSystemServices system, ComPtr<IADLXDisplay> display, ComPtr<IADLXDisplayServices> services, AdlxDesktop? parent)
        {
            _system = system ?? throw new ArgumentNullException(nameof(system));
            _display = display;
            _displayServices = services;
            _desktop = parent;
        }

        public string Name
        {
            get
            {
                ThrowIfDisposed();
                sbyte* namePtr = null;
                _display.Get()->Name(&namePtr);
                return ADLXHelpers.MarshalString(&namePtr);
            }
        }

        public int Width
        {
            get
            {
                ThrowIfDisposed();
                int w = 0, h = 0;
                _display.Get()->NativeResolution(&w, &h);
                return w;
            }
        }

        public int Height
        {
            get
            {
                ThrowIfDisposed();
                int w = 0, h = 0;
                _display.Get()->NativeResolution(&w, &h);
                return h;
            }
        }

        public double RefreshRate
        {
            get
            {
                ThrowIfDisposed();
                double rr = 0;
                _display.Get()->RefreshRate(&rr);
                return rr;
            }
        }

        public uint ManufacturerId
        {
            get
            {
                ThrowIfDisposed();
                uint mid = 0;
                _display.Get()->ManufacturerID(&mid);
                return mid;
            }
        }

        public uint PixelClock
        {
            get
            {
                ThrowIfDisposed();
                uint pc = 0;
                _display.Get()->PixelClock(&pc);
                return pc;
            }
        }

        public DisplayResolutionState GetResolution()
        {
            ThrowIfDisposed();
            return new DisplayResolutionState
            {
                Width = Width,
                Height = Height,
                RefreshRate = (int)Math.Round(RefreshRate),
                ScanType = ScanType,
                PixelClock = (int)PixelClock
            };
        }

        public void SetResolution(DisplayResolutionState state)
        {
            ThrowIfDisposed();
            if (state == null) return;

            // ADLX does not expose a direct "set active resolution" API; ensure the mode exists via custom resolutions.
            var custom = GetCustomResolution();
            if (!custom.Supported) return;

            var desired = new CustomResolutionState { Supported = true };
            foreach (var existing in custom.Resolutions)
            {
                desired.Resolutions.Add(existing);
            }

            var exists = false;
            foreach (var res in desired.Resolutions)
            {
                if (res.Width == state.Width && res.Height == state.Height && res.RefreshRate == state.RefreshRate)
                {
                    exists = true;
                    break;
                }
            }

            if (!exists)
            {
                desired.Resolutions.Add(state);
            }

            SetCustomResolution(desired);
        }

        public ADLX_DISPLAY_TYPE Type
        {
            get
            {
                ThrowIfDisposed();
                ADLX_DISPLAY_TYPE t = default;
                _display.Get()->DisplayType(&t);
                return t;
            }
        }

        public ADLX_DISPLAY_CONNECTOR_TYPE ConnectorType
        {
            get
            {
                ThrowIfDisposed();
                ADLX_DISPLAY_CONNECTOR_TYPE ct = default;
                _display.Get()->ConnectorType(&ct);
                return ct;
            }
        }

        public ADLX_DISPLAY_SCAN_TYPE ScanType
        {
            get
            {
                ThrowIfDisposed();
                ADLX_DISPLAY_SCAN_TYPE st = default;
                _display.Get()->ScanType(&st);
                return st;
            }
        }

        public ulong UniqueId
        {
            get
            {
                ThrowIfDisposed();
                nuint uid = 0;
                _display.Get()->UniqueId(&uid);
                return (ulong)uid;
            }
        }

        public int GpuUniqueId
        {
            get
            {
                ThrowIfDisposed();
                IADLXGPU* pGpu = null;
                _display.Get()->GetGPU(&pGpu);
                using var gpu = new ComPtr<IADLXGPU>(pGpu);
                int id = 0;
                gpu.Get()->UniqueId(&id);
                return id;
            }
        }

        public string Edid
        {
            get
            {
                ThrowIfDisposed();
                sbyte* edidPtr = null;
                _display.Get()->EDID(&edidPtr);
                return ADLXHelpers.MarshalString(&edidPtr);
            }
        }

        public AdlxDesktop GetDesktop()
        {
            ThrowIfDisposed();
            if (_desktop != null) return _desktop;
            // Lazy resolve by scanning desktops and matching UniqueId
            var desktops = _system.EnumerateAllDesktops();
            foreach (var d in desktops)
            {
                foreach (var disp in d.GetDisplays())
                {
                    if (disp.UniqueId == UniqueId)
                    {
                        _desktop = d;
                        return d;
                    }
                }
            }
            throw new InvalidOperationException("Desktop for display could not be resolved");
        }

        public DisplayProfile GetProfile()
        {
            ThrowIfDisposed();
            var profile = new DisplayProfile
            {
                UniqueId = UniqueId,
                GpuUniqueId = GpuUniqueId,
                Name = Name,
                Edid = Edid,
                Resolution = GetResolution()
            };

            profile.Gamma = GetGamma();
            profile.Gamut = GetGamut();
            profile.ThreeDLUT = GetThreeDLUT();
            profile.CustomColor = GetCustomColor();
            profile.Connectivity = GetConnectivity();
            profile.CustomResolution = GetCustomResolution();
            profile.ColorDepth = GetColorDepth();
            profile.PixelFormat = GetPixelFormat();
            profile.FreeSync = GetFreeSyncState();
            profile.Vsr = GetVsrState();
            profile.IntegerScaling = GetIntegerScalingState();
            profile.GpuScaling = GetGpuScalingState();
            profile.ScalingMode = GetScalingMode();
            profile.Hdcp = GetHdcpState();
            profile.VariBright = GetVariBright();
            profile.Blanking = GetBlanking();
            return profile;
        }

        public string ToJson() => JsonConvert.SerializeObject(GetProfile(), Formatting.Indented);

        public AdlxCapabilities Capabilities => _system.Capabilities;

        public void ApplyProfile(DisplayProfile profile) => ApplyProfile(profile, null);

        public void ApplyProfile(DisplayProfile profile, Action<string>? onSkip)
        {
            if (profile == null) throw new ArgumentNullException(nameof(profile));
            ThrowIfDisposed();

            var ds3Available = _system.Capabilities.SupportsDisplayServices3;

            if (profile.Gamma != null) SetGamma(profile.Gamma);
            if (profile.Gamut != null) SetGamut(profile.Gamut);
            if (profile.ThreeDLUT != null) SetThreeDLUT(profile.ThreeDLUT);
            if (profile.CustomColor != null) SetCustomColor(profile.CustomColor);
            if (profile.Connectivity != null)
            {
                if (ds3Available) SetConnectivity(profile.Connectivity);
                else onSkip?.Invoke("Connectivity skipped (DisplayServices3 unavailable)");
            }

            if (profile.CustomResolution != null) SetCustomResolution(profile.CustomResolution);
            if (profile.Resolution != null) SetResolution(profile.Resolution);

            if (profile.ColorDepth != null)
            {
                if (ds3Available) SetColorDepth(profile.ColorDepth);
                else onSkip?.Invoke("ColorDepth skipped (DisplayServices3 unavailable)");
            }

            if (profile.PixelFormat != null)
            {
                if (ds3Available) SetPixelFormat(profile.PixelFormat);
                else onSkip?.Invoke("PixelFormat skipped (DisplayServices3 unavailable)");
            }

            if (profile.FreeSync?.Enabled != null)
            {
                if (ds3Available) SetFreeSyncEnabled(profile.FreeSync.Enabled.Value);
                else onSkip?.Invoke("FreeSync skipped (DisplayServices3 unavailable)");
            }

            if (profile.Vsr?.Enabled != null)
            {
                if (ds3Available) SetVsrEnabled(profile.Vsr.Enabled.Value);
                else onSkip?.Invoke("VSR skipped (DisplayServices3 unavailable)");
            }

            if (profile.IntegerScaling?.Enabled != null)
            {
                if (ds3Available) SetIntegerScalingEnabled(profile.IntegerScaling.Enabled.Value);
                else onSkip?.Invoke("IntegerScaling skipped (DisplayServices3 unavailable)");
            }

            if (profile.GpuScaling?.Enabled != null)
            {
                if (ds3Available) SetGpuScalingEnabled(profile.GpuScaling.Enabled.Value);
                else onSkip?.Invoke("GPUScaling skipped (DisplayServices3 unavailable)");
            }

            if (profile.ScalingMode?.Mode != null)
            {
                if (ds3Available) SetScalingMode(profile.ScalingMode.Mode.Value);
                else onSkip?.Invoke("ScalingMode skipped (DisplayServices3 unavailable)");
            }

            if (profile.Hdcp?.Enabled != null)
            {
                if (ds3Available) SetHdcpEnabled(profile.Hdcp.Enabled.Value);
                else onSkip?.Invoke("HDCP skipped (DisplayServices3 unavailable)");
            }

            if (profile.VariBright?.Enabled != null)
            {
                if (ds3Available) SetVariBright(profile.VariBright);
                else onSkip?.Invoke("VariBright skipped (DisplayServices3 unavailable)");
            }

            if (profile.Blanking?.Blanked != null)
            {
                if (ds3Available) SetBlanked(profile.Blanking.Blanked.Value);
                else onSkip?.Invoke("Blanking skipped (DisplayServices3 unavailable)");
            }
        }

        public GammaState GetGamma()
        {
            ThrowIfDisposed();
            var info = ADLXDisplaySettingsHelpers.GetGamma(_displayServices.Get(), _display.Get());
            return new GammaState { Supported = info.IsSupported, Preset = info.Preset, Coeff = info.Coefficient, Ramp = info.Ramp };
        }

        public void SetGamma(GammaState state)
        {
            ThrowIfDisposed();
            if (state == null || !state.Supported) return;
            IADLXDisplayGamma* pGamma;
            var result = _displayServices.Get()->GetGamma(_display.Get(), &pGamma);
            if (result != ADLX_RESULT.ADLX_OK) throw new ADLXException(result, "Failed to get Gamma interface");
            using var gamma = new ComPtr<IADLXDisplayGamma>(pGamma);
            var info = new GammaInfo(state.Supported, state.Preset, state.Coeff, state.Ramp);
            ADLXDisplaySettingsHelpers.ApplyGamma(gamma.Get(), info);
        }

        public GamutState GetGamut()
        {
            ThrowIfDisposed();
            var info = ADLXDisplaySettingsHelpers.GetGamut(_displayServices.Get(), _display.Get());
            return new GamutState
            {
                Supported = info.IsGamutSupported || info.IsWhitePointSupported,
                ColorSpace = info.ColorSpace,
                WhitePoint = info.WhitePoint,
                CustomWhitePoint = info.CustomWhitePoint
            };
        }

        public void SetGamut(GamutState state)
        {
            ThrowIfDisposed();
            if (state == null || !state.Supported) return;
            IADLXDisplayGamut* pGamut;
            var result = _displayServices.Get()->GetGamut(_display.Get(), &pGamut);
            if (result != ADLX_RESULT.ADLX_OK) throw new ADLXException(result, "Failed to get Gamut interface");
            using var gamut = new ComPtr<IADLXDisplayGamut>(pGamut);

            ADLX_GamutColorSpace currentSpace = default;
            gamut.Get()->GetGamutColorSpace(&currentSpace);

            ADLX_RESULT applyResult;
            if (state.ColorSpace.HasValue)
            {
                if (state.WhitePoint.HasValue)
                {
                    applyResult = gamut.Get()->SetGamut(state.WhitePoint.Value, state.ColorSpace.Value);
                }
                else if (state.CustomWhitePoint.HasValue)
                {
                    applyResult = gamut.Get()->SetGamut(state.CustomWhitePoint.Value, state.ColorSpace.Value);
                }
                else
                {
                    applyResult = gamut.Get()->SetGamut(ADLX_WHITE_POINT.WHITE_POINT_6500K, state.ColorSpace.Value);
                }
            }
            else if (state.WhitePoint.HasValue)
            {
                applyResult = gamut.Get()->SetGamut(state.WhitePoint.Value, currentSpace);
            }
            else if (state.CustomWhitePoint.HasValue)
            {
                applyResult = gamut.Get()->SetGamut(state.CustomWhitePoint.Value, currentSpace);
            }
            else
            {
                return; // nothing to apply
            }

            if (applyResult != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(applyResult, "Failed to apply gamut configuration");
        }

        public ThreeDLUTState GetThreeDLUT()
        {
            ThrowIfDisposed();
            var info = ADLXDisplaySettingsHelpers.Get3DLUT(_displayServices.Get(), _display.Get());
            return new ThreeDLUTState
            {
                Supported = info.IsSceSupported || info.IsSceVividGamingSupported,
                DynamicContrastSupported = info.IsSceDynamicContrastSupported,
                Mode = info.CurrentMode,
                DynamicContrast = info.DynamicContrast
            };
        }

        public void SetThreeDLUT(ThreeDLUTState state)
        {
            ThrowIfDisposed();
            if (state == null || !state.Supported) return;
            IADLXDisplay3DLUT* pLut;
            var result = _displayServices.Get()->Get3DLUT(_display.Get(), &pLut);
            if (result != ADLX_RESULT.ADLX_OK) throw new ADLXException(result, "Failed to get 3DLUT interface");
            using var lut = new ComPtr<IADLXDisplay3DLUT>(pLut);

            ADLX_RESULT apply = ADLX_RESULT.ADLX_OK;
            switch (state.Mode)
            {
                case ThreeDLUTMode.Disabled:
                    apply = lut.Get()->SetSCEDisabled();
                    break;
                case ThreeDLUTMode.VividGaming:
                    apply = lut.Get()->SetSCEVividGaming();
                    break;
            }

            if (apply != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(apply, "Failed to apply 3DLUT mode");

            if (state.DynamicContrastSupported && state.DynamicContrast.HasValue)
            {
                var range = default(ADLX_IntRange);
                lut.Get()->GetSCEDynamicContrastRange(&range);
                var clamped = Math.Clamp(state.DynamicContrast.Value, range.minValue, range.maxValue);
                lut.Get()->SetSCEDynamicContrast(clamped);
            }
        }

        public CustomColorState GetCustomColor()
        {
            ThrowIfDisposed();
            var info = ADLXDisplaySettingsHelpers.GetCustomColor(_displayServices.Get(), _display.Get());
            return new CustomColorState
            {
                IsHueSupported = info.IsHueSupported,
                Hue = info.IsHueSupported ? info.Hue : null,
                IsSaturationSupported = info.IsSaturationSupported,
                Saturation = info.IsSaturationSupported ? info.Saturation : null,
                IsBrightnessSupported = info.IsBrightnessSupported,
                Brightness = info.IsBrightnessSupported ? info.Brightness : null,
                IsContrastSupported = info.IsContrastSupported,
                Contrast = info.IsContrastSupported ? info.Contrast : null,
                IsTemperatureSupported = info.IsTemperatureSupported,
                Temperature = info.IsTemperatureSupported ? info.Temperature : null
            };
        }

        public void SetCustomColor(CustomColorState state)
        {
            ThrowIfDisposed();
            if (state == null) return;
            IADLXDisplayCustomColor* pCustom;
            var result = _displayServices.Get()->GetCustomColor(_display.Get(), &pCustom);
            if (result != ADLX_RESULT.ADLX_OK) throw new ADLXException(result, "Failed to get Custom Color interface");
            using var custom = new ComPtr<IADLXDisplayCustomColor>(pCustom);
            var info = new CustomColorInfo(state.IsHueSupported, state.IsHueSupported, state.Hue ?? 0, state.IsSaturationSupported, state.Saturation ?? 0, state.IsBrightnessSupported, state.Brightness ?? 0, state.IsContrastSupported, state.Contrast ?? 0, state.IsTemperatureSupported, state.Temperature ?? 0);
            ADLXDisplaySettingsHelpers.ApplyCustomColor(custom.Get(), info);
        }

        public ConnectivityState GetConnectivity()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null)
            {
                return new ConnectivityState { HdmiQualityDetectionSupported = false, DpLinkRateSupported = false, RelativePreEmphasisSupported = false, RelativeVoltageSwingSupported = false };
            }
            var info = ADLXDisplaySettingsHelpers.GetDisplayConnectivityExperience((IADLXDisplayServices*)svc3.Get(), _display.Get());
            return new ConnectivityState
            {
                HdmiQualityDetectionSupported = info.IsHdmiQualityDetectionSupported,
                HdmiQualityDetectionEnabled = info.IsHdmiQualityDetectionSupported ? info.IsHdmiQualityDetectionEnabled : null,
                DpLinkRateSupported = info.IsDpLinkRateSupported,
                DpLinkRate = info.IsDpLinkRateSupported ? info.DpLinkRate : null,
                RelativePreEmphasisSupported = info.IsRelativePreEmphasisSupported,
                RelativePreEmphasis = info.IsRelativePreEmphasisSupported ? info.RelativePreEmphasis : null,
                RelativeVoltageSwingSupported = info.IsRelativeVoltageSwingSupported,
                RelativeVoltageSwing = info.IsRelativeVoltageSwingSupported ? info.RelativeVoltageSwing : null
            };
        }

        public void SetConnectivity(ConnectivityState state)
        {
            ThrowIfDisposed();
            if (state == null) return;
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var services3 = svc3.Get();
            IADLXDisplayConnectivityExperience* pConn;
            var result = services3->GetDisplayConnectivityExperience(_display.Get(), &pConn);
            if (result != ADLX_RESULT.ADLX_OK) return; // unsupported
            using var conn = new ComPtr<IADLXDisplayConnectivityExperience>(pConn);
            ADLXDisplaySettingsHelpers.ApplyDisplayConnectivityExperience(conn.Get(), new ConnectivityExperienceInfo(state.HdmiQualityDetectionSupported, state.HdmiQualityDetectionEnabled ?? false, state.DpLinkRateSupported, state.DpLinkRate ?? default, state.RelativePreEmphasisSupported, state.RelativePreEmphasis ?? 0, state.RelativeVoltageSwingSupported, state.RelativeVoltageSwing ?? 0));
        }

        public CustomResolutionState GetCustomResolution()
        {
            ThrowIfDisposed();
            var info = ADLXDisplaySettingsHelpers.GetCustomResolution(_displayServices.Get(), _display.Get());
            var state = new CustomResolutionState { Supported = info.IsSupported };
            foreach (var res in info.Resolutions)
            {
                state.Resolutions.Add(new DisplayResolutionState
                {
                    Width = res.ResWidth,
                    Height = res.ResHeight,
                    RefreshRate = res.RefreshRate,
                    ScanType = res.Presentation,
                    TimingStandard = res.TimingStandard,
                    PixelClock = res.PixelClock,
                    DetailedTiming = res.DetailedTiming
                });
            }
            return state;
        }

        public void SetCustomResolution(CustomResolutionState state)
        {
            ThrowIfDisposed();
            if (state == null || !state.Supported || state.Resolutions.Count == 0)
                return;

            IADLXDisplayCustomResolution* pCustomRes;
            var result = _displayServices.Get()->GetCustomResolution(_display.Get(), &pCustomRes);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get Custom Resolution interface");

            using var customRes = new ComPtr<IADLXDisplayCustomResolution>(pCustomRes);
            bool supported = false;
            customRes.Get()->IsSupported(&supported);
            if (!supported)
                return;

            IADLXDisplayResolutionList* pResList = null;
            result = customRes.Get()->GetResolutionList(&pResList);
            if (result != ADLX_RESULT.ADLX_OK)
                throw new ADLXException(result, "Failed to get custom resolution list");

            using var resList = new ComPtr<IADLXDisplayResolutionList>(pResList);
            var desiredKeys = new HashSet<(int, int, int)>();
            foreach (var res in state.Resolutions)
            {
                desiredKeys.Add((res.Width, res.Height, res.RefreshRate));
            }

            var existingKeys = new HashSet<(int, int, int)>();
            for (uint i = 0; i < resList.Get()->Size(); i++)
            {
                IADLXDisplayResolution* pRes = null;
                resList.Get()->At(i, &pRes);
                using var res = new ComPtr<IADLXDisplayResolution>(pRes);
                var key = ExtractResolutionKey(res.Get());
                existingKeys.Add(key);
                if (!desiredKeys.Contains(key))
                {
                    customRes.Get()->DeleteResolution(res.Get());
                }
            }

            using var scratch = AcquireScratchResolution(customRes.Get(), resList.Get());
            if (scratch.Get() == null)
                return;

            foreach (var res in state.Resolutions)
            {
                var key = (res.Width, res.Height, res.RefreshRate);
                if (existingKeys.Contains(key))
                    continue;

                var custom = BuildCustomResolution(res);
                var setResult = scratch.Get()->SetValue(custom);
                if (setResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(setResult, "Failed to set custom resolution values");

                var createResult = customRes.Get()->CreateNewResolution(scratch.Get());
                if (createResult != ADLX_RESULT.ADLX_OK)
                    throw new ADLXException(createResult, "Failed to create custom resolution");
            }
        }

        public ColorDepthState GetColorDepth()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new ColorDepthState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetColorDepthHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var depth = new ComPtr<IADLXDisplayColorDepth>((IADLXDisplayColorDepth*)handle);
            var (supported, current) = ADLXDisplaySettingsHelpers.GetColorDepthState(handle);
            return new ColorDepthState { Supported = supported, Value = supported ? current : null };
        }

        public void SetColorDepth(ColorDepthState state)
        {
            ThrowIfDisposed();
            if (state == null || !state.Supported || state.Value == null) return;
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetColorDepthHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var depth = new ComPtr<IADLXDisplayColorDepth>((IADLXDisplayColorDepth*)handle);
            ADLXDisplaySettingsHelpers.SetColorDepth(handle, state.Value.Value);
        }

        public PixelFormatState GetPixelFormat()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new PixelFormatState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetPixelFormatHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var pf = new ComPtr<IADLXDisplayPixelFormat>((IADLXDisplayPixelFormat*)handle);
            var (supported, current) = ADLXDisplaySettingsHelpers.GetPixelFormatState(handle);
            return new PixelFormatState { Supported = supported, Value = supported ? current : null };
        }

        public void SetPixelFormat(PixelFormatState state)
        {
            ThrowIfDisposed();
            if (state == null || !state.Supported || state.Value == null) return;
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetPixelFormatHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var pf = new ComPtr<IADLXDisplayPixelFormat>((IADLXDisplayPixelFormat*)handle);
            ADLXDisplaySettingsHelpers.SetPixelFormat(handle, state.Value.Value);
        }

        public FreeSyncState GetFreeSyncState()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new FreeSyncState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetFreeSyncHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var fs = new ComPtr<IADLXDisplayFreeSync>((IADLXDisplayFreeSync*)handle);
            var (supported, enabled) = ADLXDisplaySettingsHelpers.GetFreeSyncState(handle);
            return new FreeSyncState { Supported = supported, Enabled = supported ? enabled : null };
        }

        public void SetFreeSyncEnabled(bool enable)
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetFreeSyncHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var fs = new ComPtr<IADLXDisplayFreeSync>((IADLXDisplayFreeSync*)handle);
            ADLXDisplaySettingsHelpers.SetFreeSyncEnabled(handle, enable);
        }

        public VirtualSuperResolutionState GetVsrState()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new VirtualSuperResolutionState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetVirtualSuperResolutionHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var vsr = new ComPtr<IADLXDisplayVSR>((IADLXDisplayVSR*)handle);
            var (supported, enabled) = ADLXDisplaySettingsHelpers.GetVirtualSuperResolutionState(handle);
            return new VirtualSuperResolutionState { Supported = supported, Enabled = supported ? enabled : null };
        }

        public void SetVsrEnabled(bool enable)
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetVirtualSuperResolutionHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var vsr = new ComPtr<IADLXDisplayVSR>((IADLXDisplayVSR*)handle);
            ADLXDisplaySettingsHelpers.SetVirtualSuperResolutionEnabled(handle, enable);
        }

        public IntegerScalingState GetIntegerScalingState()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new IntegerScalingState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetIntegerScalingHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var iscale = new ComPtr<IADLXDisplayIntegerScaling>((IADLXDisplayIntegerScaling*)handle);
            var (supported, enabled) = ADLXDisplaySettingsHelpers.GetIntegerScalingState(handle);
            return new IntegerScalingState { Supported = supported, Enabled = supported ? enabled : null };
        }

        public void SetIntegerScalingEnabled(bool enable)
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetIntegerScalingHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var iscale = new ComPtr<IADLXDisplayIntegerScaling>((IADLXDisplayIntegerScaling*)handle);
            ADLXDisplaySettingsHelpers.SetIntegerScalingEnabled(handle, enable);
        }

        public GPUScalingState GetGpuScalingState()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new GPUScalingState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetGPUScalingHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var gs = new ComPtr<IADLXDisplayGPUScaling>((IADLXDisplayGPUScaling*)handle);
            var (supported, enabled) = ADLXDisplaySettingsHelpers.GetGPUScalingState(handle);
            return new GPUScalingState { Supported = supported, Enabled = supported ? enabled : null };
        }

        public void SetGpuScalingEnabled(bool enable)
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetGPUScalingHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var gs = new ComPtr<IADLXDisplayGPUScaling>((IADLXDisplayGPUScaling*)handle);
            ADLXDisplaySettingsHelpers.SetGPUScalingEnabled(handle, enable);
        }

        public ScalingModeState GetScalingMode()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new ScalingModeState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetScalingModeHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var sm = new ComPtr<IADLXDisplayScalingMode>((IADLXDisplayScalingMode*)handle);
            var (supported, mode) = ADLXDisplaySettingsHelpers.GetScalingMode(handle);
            return new ScalingModeState { Supported = supported, Mode = supported ? mode : null };
        }

        public void SetScalingMode(ADLX_SCALE_MODE mode)
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetScalingModeHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var sm = new ComPtr<IADLXDisplayScalingMode>((IADLXDisplayScalingMode*)handle);
            ADLXDisplaySettingsHelpers.SetScalingMode(handle, mode);
        }

        public HdcpState GetHdcpState()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new HdcpState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetHDCPHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var hd = new ComPtr<IADLXDisplayHDCP>((IADLXDisplayHDCP*)handle);
            var (supported, enabled) = ADLXDisplaySettingsHelpers.GetHDCPState(handle);
            return new HdcpState { Supported = supported, Enabled = supported ? enabled : null };
        }

        public void SetHdcpEnabled(bool enable)
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetHDCPHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var hd = new ComPtr<IADLXDisplayHDCP>((IADLXDisplayHDCP*)handle);
            ADLXDisplaySettingsHelpers.SetHDCPEnabled(handle, enable);
        }

        public VariBrightState GetVariBright()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new VariBrightState { Supported = false, Mode = ADLXDisplaySettingsHelpers.VariBrightMode.Unknown };
            var handle = ADLXDisplaySettingsHelpers.GetVariBrightHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var vb = new ComPtr<IADLXDisplayVariBright>((IADLXDisplayVariBright*)handle);
            var (supported, enabled, mode) = ADLXDisplaySettingsHelpers.GetVariBrightState(handle);
            return new VariBrightState { Supported = supported, Enabled = supported ? enabled : null, Mode = mode };
        }

        public void SetVariBright(VariBrightState state)
        {
            ThrowIfDisposed();
            if (state == null || state.Enabled == null) return;
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetVariBrightHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var vb = new ComPtr<IADLXDisplayVariBright>((IADLXDisplayVariBright*)handle);
            ADLXDisplaySettingsHelpers.SetVariBright(handle, state.Enabled.Value, state.Mode);
        }

        public BlankingState GetBlanking()
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return new BlankingState { Supported = false };
            var handle = ADLXDisplaySettingsHelpers.GetDisplayBlankingHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var blank = new ComPtr<IADLXDisplayBlanking>((IADLXDisplayBlanking*)handle);
            var (supported, blanked) = ADLXDisplaySettingsHelpers.GetDisplayBlankingState(handle);
            return new BlankingState { Supported = supported, Blanked = supported ? blanked : null };
        }

        public void SetBlanked(bool blanked)
        {
            ThrowIfDisposed();
            using var svc3 = AcquireServices3OrNull();
            if (svc3.Get() == null) return;
            var handle = ADLXDisplaySettingsHelpers.GetDisplayBlankingHandle((IntPtr)svc3.Get(), (IntPtr)_display.Get());
            using var blank = new ComPtr<IADLXDisplayBlanking>((IADLXDisplayBlanking*)handle);
            ADLXDisplaySettingsHelpers.SetDisplayBlanked(handle, blanked);
        }

        internal IADLXDisplay* GetRaw() => _display.Get();

        private void ThrowIfDisposed()
        {
            _system.ThrowIfDisposed();
            if (_disposed) throw new ObjectDisposedException(nameof(AdlxDisplay));
        }

        private ComPtr<IADLXDisplayServices3> AcquireServices3OrNull()
        {
            return _system.TryGetDisplayServices3(_displayServices);
        }

        private static ADLX_CustomResolution BuildCustomResolution(DisplayResolutionState res)
        {
            return new ADLX_CustomResolution
            {
                resWidth = res.Width,
                resHeight = res.Height,
                refreshRate = res.RefreshRate,
                presentation = res.ScanType ?? ADLX_DISPLAY_SCAN_TYPE.PROGRESSIVE,
                timingStandard = res.TimingStandard ?? ADLX_TIMING_STANDARD.CVT,
                GPixelClock = res.PixelClock ?? 0,
                detailedTiming = res.DetailedTiming ?? default
            };
        }

        private static (int Width, int Height, int RefreshRate) ExtractResolutionKey(IADLXDisplayResolution* pRes)
        {
            ADLX_CustomResolution res = default;
            pRes->GetValue(&res);
            return (res.resWidth, res.resHeight, res.refreshRate);
        }

        private static ComPtr<IADLXDisplayResolution> AcquireScratchResolution(IADLXDisplayCustomResolution* customRes, IADLXDisplayResolutionList* resList)
        {
            if (customRes != null)
            {
                IADLXDisplayResolution* pCurrent = null;
                if (customRes->GetCurrentAppliedResolution(&pCurrent) == ADLX_RESULT.ADLX_OK && pCurrent != null)
                {
                    return new ComPtr<IADLXDisplayResolution>(pCurrent);
                }
            }

            if (resList != null && resList->Size() > 0)
            {
                IADLXDisplayResolution* pRes = null;
                resList->At(0, &pRes);
                return new ComPtr<IADLXDisplayResolution>(pRes);
            }

            return default;
        }

        public void Dispose()
        {
            if (_disposed) return;
            _display.Dispose();
            _displayServices.Dispose();
            _display = default;
            _displayServices = default;
            _disposed = true;
        }
    }
}
