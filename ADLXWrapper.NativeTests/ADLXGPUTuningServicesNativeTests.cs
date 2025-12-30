using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using ADLXWrapper;
using Xunit;

namespace ADLXWrapper.NativeTests;

[Collection("NativeSessionCollection")]
[SupportedOSPlatform("windows")]
public unsafe class ADLXGPUTuningServicesNativeTests
{
    private readonly NativeSession _session;

    public ADLXGPUTuningServicesNativeTests(NativeSessionFixture fixture)
    {
        _session = fixture.Session;
    }

    private static void AssertRangeHasSpan(ADLX_IntRange range, string name)
    {
        Assert.True(range.maxValue >= range.minValue, $"{name} range has inverted bounds.");
    }

    [SkippableFact]
    public void Gpu_tuning_services_acquire_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out _);
    }

    [SkippableFact]
    public void Gpu_tuning_services_query_interface_v1_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);
        QueryInterfaceOrSkip((IADLXInterface*)services, nameof(IADLXGPUTuningServices1));
    }

    [SkippableFact]
    public void Gpu_tuning_support_flags_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            bool isFactory = false;
            AssertResultOrContinue(services->IsAtFactory(gpu, &isFactory));

            bool autoSupported = false;
            AssertResultOrContinue(services->IsSupportedAutoTuning(gpu, &autoSupported));

            bool presetSupported = false;
            AssertResultOrContinue(services->IsSupportedPresetTuning(gpu, &presetSupported));

            bool gfxSupported = false;
            AssertResultOrContinue(services->IsSupportedManualGFXTuning(gpu, &gfxSupported));

            bool vramSupported = false;
            AssertResultOrContinue(services->IsSupportedManualVRAMTuning(gpu, &vramSupported));

            bool fanSupported = false;
            AssertResultOrContinue(services->IsSupportedManualFanTuning(gpu, &fanSupported));

            bool powerSupported = false;
            AssertResultOrContinue(services->IsSupportedManualPowerTuning(gpu, &powerSupported));
        });
    }

    [SkippableFact]
    public void Gpu_tuning_preset_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            bool presetSupported = false;
            if (!AssertResultOrContinue(services->IsSupportedPresetTuning(gpu, &presetSupported)) || !presetSupported)
                return;

            IADLXInterface* presetIface = null;
            var result = services->GetPresetTuning(gpu, &presetIface);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_INVALID_OBJECT || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE || result == ADLX_RESULT.ADLX_PENDING_OPERATION)
                return;
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            if (presetIface == null)
                return;
            using var presetPtr = new ComPtr<IADLXInterface>(presetIface);

            IADLXGPUPresetTuning* preset = null;
            var qi = QueryInterface((IADLXInterface*)presetIface, nameof(IADLXGPUPresetTuning), (void**)&preset);
            if (qi == ADLX_RESULT.ADLX_OK && preset != null)
            {
                using var typedPtr = new ComPtr<IADLXGPUPresetTuning>(preset);
                bool supportPowerSaver = false;
                AssertResultOrContinue(preset->IsSupportedPowerSaver(&supportPowerSaver));
                bool supportQuiet = false;
                AssertResultOrContinue(preset->IsSupportedQuiet(&supportQuiet));
                bool supportBalanced = false;
                AssertResultOrContinue(preset->IsSupportedBalanced(&supportBalanced));
                bool supportTurbo = false;
                AssertResultOrContinue(preset->IsSupportedTurbo(&supportTurbo));
                bool supportRage = false;
                AssertResultOrContinue(preset->IsSupportedRage(&supportRage));

                bool currentPowerSaver = false;
                AssertResultOrContinue(preset->IsCurrentPowerSaver(&currentPowerSaver));
                bool currentQuiet = false;
                AssertResultOrContinue(preset->IsCurrentQuiet(&currentQuiet));
                bool currentBalanced = false;
                AssertResultOrContinue(preset->IsCurrentBalanced(&currentBalanced));
                bool currentTurbo = false;
                AssertResultOrContinue(preset->IsCurrentTurbo(&currentTurbo));
                bool currentRage = false;
                AssertResultOrContinue(preset->IsCurrentRage(&currentRage));
            }
        });
    }

    [SkippableFact]
    public void Gpu_tuning_auto_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            bool autoSupported = false;
            if (!AssertResultOrContinue(services->IsSupportedAutoTuning(gpu, &autoSupported)) || !autoSupported)
                return;

            IADLXInterface* autoIface = null;
            var result = services->GetAutoTuning(gpu, &autoIface);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_INVALID_OBJECT || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE || result == ADLX_RESULT.ADLX_PENDING_OPERATION)
                return;
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            if (autoIface == null)
                return;
            using var autoPtr = new ComPtr<IADLXInterface>(autoIface);

            IADLXGPUAutoTuning* autoTuning = null;
            var qi = QueryInterface((IADLXInterface*)autoIface, nameof(IADLXGPUAutoTuning), (void**)&autoTuning);
            if (qi == ADLX_RESULT.ADLX_OK && autoTuning != null)
            {
                using var typedPtr = new ComPtr<IADLXGPUAutoTuning>(autoTuning);
                bool supportUndervolt = false;
                AssertResultOrContinue(autoTuning->IsSupportedUndervoltGPU(&supportUndervolt));
                bool supportOc = false;
                AssertResultOrContinue(autoTuning->IsSupportedOverclockGPU(&supportOc));
                bool supportVramOc = false;
                AssertResultOrContinue(autoTuning->IsSupportedOverclockVRAM(&supportVramOc));

                bool currentUndervolt = false;
                AssertResultOrContinue(autoTuning->IsCurrentUndervoltGPU(&currentUndervolt));
                bool currentOc = false;
                AssertResultOrContinue(autoTuning->IsCurrentOverclockGPU(&currentOc));
                bool currentVramOc = false;
                AssertResultOrContinue(autoTuning->IsCurrentOverclockVRAM(&currentVramOc));
            }
        });
    }

    [SkippableFact]
    public void Gpu_tuning_manual_power_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            bool powerSupported = false;
            if (!AssertResultOrContinue(services->IsSupportedManualPowerTuning(gpu, &powerSupported)) || !powerSupported)
                return;

            IADLXInterface* powerIface = null;
            var result = services->GetManualPowerTuning(gpu, &powerIface);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_INVALID_OBJECT || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE || result == ADLX_RESULT.ADLX_PENDING_OPERATION)
                return;
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            if (powerIface == null)
                return;
            using var powerPtr = new ComPtr<IADLXInterface>(powerIface);

            IADLXManualPowerTuning* power = null;
            var qi = QueryInterface((IADLXInterface*)powerIface, nameof(IADLXManualPowerTuning1), (void**)&power);
            if (qi != ADLX_RESULT.ADLX_OK || power == null)
            {
                qi = QueryInterface((IADLXInterface*)powerIface, nameof(IADLXManualPowerTuning), (void**)&power);
            }

            if (power != null)
            {
                using var powerTyped = new ComPtr<IADLXManualPowerTuning>(power);

                ADLX_IntRange limitRange = default;
                if (AssertResultOrContinue(power->GetPowerLimitRange(&limitRange)))
                {
                    AssertRangeHasSpan(limitRange, "Power limit");
                }

                int limit = 0;
                AssertResultOrContinue(power->GetPowerLimit(&limit));

                bool tdcSupported = false;
                AssertResultOrContinue(power->IsSupportedTDCLimit(&tdcSupported));
                if (tdcSupported)
                {
                    ADLX_IntRange tdcRange = default;
                    if (AssertResultOrContinue(power->GetTDCLimitRange(&tdcRange)))
                    {
                        AssertRangeHasSpan(tdcRange, "TDC");
                    }
                    int tdc = 0;
                    AssertResultOrContinue(power->GetTDCLimit(&tdc));
                }
            }
        });
    }

    [SkippableFact]
    public void Gpu_tuning_manual_fan_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            bool fanSupported = false;
            if (!AssertResultOrContinue(services->IsSupportedManualFanTuning(gpu, &fanSupported)) || !fanSupported)
                return;

            IADLXInterface* fanIface = null;
            var result = services->GetManualFanTuning(gpu, &fanIface);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_INVALID_OBJECT || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE || result == ADLX_RESULT.ADLX_PENDING_OPERATION)
                return;
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            if (fanIface == null)
                return;
            using var fanPtr = new ComPtr<IADLXInterface>(fanIface);

            IADLXManualFanTuning1* fan = null;
            var qi = QueryInterface((IADLXInterface*)fanIface, nameof(IADLXManualFanTuning1), (void**)&fan);
            if (qi == ADLX_RESULT.ADLX_OK && fan != null)
            {
                using var fanTyped = new ComPtr<IADLXManualFanTuning1>(fan);
                ADLX_IntRange speedRange = default;
                ADLX_IntRange tempRange = default;
                if (AssertResultOrContinue(fan->GetFanTuningRanges(&speedRange, &tempRange)))
                {
                    AssertRangeHasSpan(speedRange, "Fan speed");
                    AssertRangeHasSpan(tempRange, "Fan temperature");
                }

                bool zeroRpm = false;
                AssertResultOrContinue(fan->GetZeroRPMState(&zeroRpm));

                bool targetSupported = false;
                AssertResultOrContinue(fan->IsSupportedTargetFanSpeed(&targetSupported));

                bool minFanSupported = false;
                AssertResultOrContinue(fan->IsSupportedMinFanSpeed(&minFanSupported));
            }
        });
    }

    [SkippableFact]
    public void Gpu_tuning_manual_gfx_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            bool gfxSupported = false;
            if (!AssertResultOrContinue(services->IsSupportedManualGFXTuning(gpu, &gfxSupported)) || !gfxSupported)
                return;

            IADLXInterface* gfxIface = null;
            var result = services->GetManualGFXTuning(gpu, &gfxIface);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_INVALID_OBJECT || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE || result == ADLX_RESULT.ADLX_PENDING_OPERATION)
                return;
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            if (gfxIface == null)
                return;
            using var gfxPtr = new ComPtr<IADLXInterface>(gfxIface);

            IADLXManualGraphicsTuning1* gfx = null;
            var qi = QueryInterface((IADLXInterface*)gfxIface, nameof(IADLXManualGraphicsTuning2_1), (void**)&gfx);
            if (qi != ADLX_RESULT.ADLX_OK || gfx == null)
            {
                qi = QueryInterface((IADLXInterface*)gfxIface, nameof(IADLXManualGraphicsTuning2), (void**)&gfx);
            }
            if (qi != ADLX_RESULT.ADLX_OK || gfx == null)
            {
                qi = QueryInterface((IADLXInterface*)gfxIface, nameof(IADLXManualGraphicsTuning1), (void**)&gfx);
            }

            if (gfx != null)
            {
                using var gfxTyped = new ComPtr<IADLXManualGraphicsTuning1>(gfx);
                ADLX_IntRange freqRange = default;
                ADLX_IntRange voltRange = default;
                if (AssertResultOrContinue(gfx->GetGPUTuningRanges(&freqRange, &voltRange)))
                {
                    AssertRangeHasSpan(freqRange, "GFX frequency");
                    AssertRangeHasSpan(voltRange, "GFX voltage");
                }

                IADLXManualTuningStateList* states = null;
                if (AssertResultOrContinue(gfx->GetGPUTuningStates(&states)) && states != null)
                {
                    using var statesPtr = new ComPtr<IADLXManualTuningStateList>(states);
                    Assert.True(states->Size() > 0, "GFX tuning state list returned empty.");
                }
            }
        });
    }

    [SkippableFact]
    public void Gpu_tuning_manual_vram_all_gpus_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);
        using var gpuListPtr = GetGpuListOrSkip(out var gpuList);

        ForEachGpu(gpuList, gpu =>
        {
            bool vramSupported = false;
            if (!AssertResultOrContinue(services->IsSupportedManualVRAMTuning(gpu, &vramSupported)) || !vramSupported)
                return;

            IADLXInterface* vramIface = null;
            var result = services->GetManualVRAMTuning(gpu, &vramIface);
            if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED || result == ADLX_RESULT.ADLX_INVALID_OBJECT || result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE || result == ADLX_RESULT.ADLX_PENDING_OPERATION)
                return;
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            if (vramIface == null)
                return;
            using var vramPtr = new ComPtr<IADLXInterface>(vramIface);

            IADLXManualVRAMTuning1* vram = null;
            var qi = QueryInterface((IADLXInterface*)vramIface, nameof(IADLXManualVRAMTuning2_1), (void**)&vram);
            if (qi != ADLX_RESULT.ADLX_OK || vram == null)
            {
                qi = QueryInterface((IADLXInterface*)vramIface, nameof(IADLXManualVRAMTuning2), (void**)&vram);
            }
            if (qi != ADLX_RESULT.ADLX_OK || vram == null)
            {
                qi = QueryInterface((IADLXInterface*)vramIface, nameof(IADLXManualVRAMTuning1), (void**)&vram);
            }

            if (vram != null)
            {
                using var vramTyped = new ComPtr<IADLXManualVRAMTuning1>(vram);
                ADLX_IntRange freqRange = default;
                ADLX_IntRange voltRange = default;
                if (AssertResultOrContinue(vram->GetVRAMTuningRanges(&freqRange, &voltRange)))
                {
                    AssertRangeHasSpan(freqRange, "VRAM frequency");
                    AssertRangeHasSpan(voltRange, "VRAM voltage");
                }

                IADLXMemoryTimingDescriptionList* timings = null;
                if (AssertResultOrContinue(vram->GetSupportedMemoryTimingDescriptionList(&timings)) && timings != null)
                {
                    using var timingPtr = new ComPtr<IADLXMemoryTimingDescriptionList>(timings);
                    Assert.True(timings->Size() > 0, "VRAM memory timing description list returned empty.");
                }
            }
        });
    }

    [SkippableFact]
    public void Gpu_tuning_change_listener_native()
    {
        SkipIfNoAdlxSupport();
        using var servicesPtr = GetTuningServicesComPtrOrSkip(out var services);

        IADLXGPUTuningChangedHandling* handling = null;
        var result = services->GetGPUTuningChangedHandling(&handling);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU tuning change handling not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);

        using var handlingPtr = new ComPtr<IADLXGPUTuningChangedHandling>(handling);
        using var listener = new DummyGPUTuningChangedListener();

        var addResult = handling->AddGPUTuningEventListener(listener.Pointer);
        Skip.If(addResult == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU tuning listeners not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, addResult);

        var removeResult = handling->RemoveGPUTuningEventListener(listener.Pointer);
        Assert.Equal(ADLX_RESULT.ADLX_OK, removeResult);
    }

    private void SkipIfNoAdlxSupport()
    {
        Skip.IfNot(ADLXApiHelper.IsADLXDllAvailable(out var dllError), $"ADLX DLL unavailable: {dllError}");
        Skip.IfNot(ADLXHardwareDetection.HasAMDGPU(out var gpuError), $"AMD GPU not detected: {gpuError}");
    }

    private unsafe ComPtr<IADLXGPUTuningServices> GetTuningServicesComPtrOrSkip(out IADLXGPUTuningServices* services)
    {
        services = null;
        IADLXGPUTuningServices* local = null;
        var result = _session.System->GetGPUTuningServices(&local);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU tuning services not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        services = local;
        return new ComPtr<IADLXGPUTuningServices>(local);
    }

    private unsafe ComPtr<IADLXGPUList> GetGpuListOrSkip(out IADLXGPUList* list)
    {
        IADLXGPUList* local = null;
        var result = _session.System->GetGPUs(&local);
        Skip.If(result == ADLX_RESULT.ADLX_NOT_SUPPORTED, "GPU enumeration not supported on this hardware/driver.");
        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        list = local;
        return new ComPtr<IADLXGPUList>(local);
    }

    private static bool AssertResultOrContinue(ADLX_RESULT result)
    {
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED ||
            result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE ||
            result == ADLX_RESULT.ADLX_INVALID_OBJECT ||
            result == ADLX_RESULT.ADLX_PENDING_OPERATION)
            return false;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        return true;
    }

    private static unsafe ADLX_RESULT QueryInterface(IADLXInterface* iface, string name, void** obj)
    {
        var terminated = name + "\0";
        fixed (char* chars = terminated)
        {
            return iface->QueryInterface((ushort*)chars, obj);
        }
    }

    private static unsafe void QueryInterfaceOrSkip(IADLXInterface* iface, string name)
    {
        void* queried = null;
        var result = QueryInterface(iface, name, &queried);
        if (result == ADLX_RESULT.ADLX_NOT_SUPPORTED ||
            result == ADLX_RESULT.ADLX_UNKNOWN_INTERFACE ||
            result == ADLX_RESULT.ADLX_PENDING_OPERATION)
            return;

        Assert.Equal(ADLX_RESULT.ADLX_OK, result);
        Assert.NotEqual<IntPtr>(IntPtr.Zero, (IntPtr)queried);
        ((IADLXInterface*)queried)->Release();
    }

    private unsafe delegate void GpuVisitor(IADLXGPU* gpu);

    private unsafe void ForEachGpu(IADLXGPUList* list, GpuVisitor visitor)
    {
        var count = list->Size();
        Skip.If(count == 0, "No GPUs returned by ADLX.");

        for (uint i = 0; i < count; i++)
        {
            IADLXGPU* gpu = null;
            var gpuResult = list->At(i, &gpu);
            if (gpuResult == ADLX_RESULT.ADLX_NOT_SUPPORTED)
                continue;

            Assert.Equal(ADLX_RESULT.ADLX_OK, gpuResult);
            using var gpuPtr = new ComPtr<IADLXGPU>(gpu);
            visitor(gpu);
        }
    }

    private sealed unsafe class DummyGPUTuningChangedListener : IDisposable
    {
        private IntPtr _vtable;
        private IntPtr _instance;

        public IADLXGPUTuningChangedListener* Pointer => (IADLXGPUTuningChangedListener*)_instance;

        public DummyGPUTuningChangedListener()
        {
            _vtable = Marshal.AllocHGlobal(IntPtr.Size);
            *((IntPtr*)_vtable) = (IntPtr)(delegate* unmanaged[Stdcall]<IADLXGPUTuningChangedListener*, IADLXGPUTuningChangedEvent*, byte>)&OnChanged;

            _instance = Marshal.AllocHGlobal(sizeof(IADLXGPUTuningChangedListener));
            ((IADLXGPUTuningChangedListener*)_instance)->lpVtbl = (void**)_vtable;
        }

        [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
        private static byte OnChanged(IADLXGPUTuningChangedListener* self, IADLXGPUTuningChangedEvent* evt) => 1;

        public void Dispose()
        {
            if (_instance != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_instance);
                _instance = IntPtr.Zero;
            }

            if (_vtable != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(_vtable);
                _vtable = IntPtr.Zero;
            }
        }
    }
}
