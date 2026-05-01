# Facade DTO Refactoring Plan

## Goal
Remove all native/unsafe types from the public API of every Facade Helper and Facade Object.
After this change:
- Every public method on every facade helper returns only managed DTOs, primitives, or enums
- Every public method on every facade helper takes only managed DTOs, primitives, or enums
- All existing DTO structs that contained native structs (ADLX_IntRange, ADLX_LUID, ADLX_GammaRamp,
  ADLX_RegammaCoeff, ADLX_Point, ADLX_GamutColorSpace) now contain managed DTO equivalents
- JSON round-trip works for all DTOs

## New Managed DTO Replacements (in ADLXDTOTypes.cs)
| New DTO            | Replaces             | Notes                                    |
|--------------------|----------------------|------------------------------------------|
| IntRangeInfo       | ADLX_IntRange        | MinValue/MaxValue/Step (PascalCase)      |
| LuidInfo           | ADLX_LUID            | LowPart/HighPart                         |
| RegammaCoeffInfo   | ADLX_RegammaCoeff    | CoefficientA0-A3, Gamma                  |
| GammaRampInfo      | ADLX_GammaRamp       | IReadOnlyList<ushort> Values (768 items) |
| PointInfo          | ADLX_Point           | X/Y                                      |
| GamutColorSpaceInfo| ADLX_GamutColorSpace | Contains PointInfo Red/Green/Blue        |

## DTO Field Changes
- GpuInfo.Luid: ADLX_LUID -> LuidInfo
- GammaInfo.RegammaCoefficient: ADLX_RegammaCoeff -> RegammaCoeffInfo
- GammaInfo.ReGammaRamp/DeGammaRamp: ADLX_GammaRamp -> GammaRampInfo
- GamutInfo.CurrentGamutSpace: ADLX_GamutColorSpace -> GamutColorSpaceInfo
- GamutInfo.WhitePoint: ADLX_Point -> PointInfo
- ThreeDLUTInfo.DynamicContrastRange: ADLX_IntRange -> IntRangeInfo
- BoostInfo.ResolutionRange: ADLX_IntRange -> IntRangeInfo
- RadeonImageSharpeningInfo.SharpnessRange: ADLX_IntRange -> IntRangeInfo
- FrameRateTargetControlInfo.FpsRange: ADLX_IntRange -> IntRangeInfo
- RadeonSuperResolutionInfo.SharpnessRange: ADLX_IntRange -> IntRangeInfo
- VideoUpscaleInfo.SharpnessRange: ADLX_IntRange -> IntRangeInfo
- SmartShiftMaxInfo.BiasRange: ADLX_IntRange -> IntRangeInfo
- ManualPowerTuningInfo.PowerLimitRange/TdcLimitRange: ADLX_IntRange -> IntRangeInfo
- ADLXPerformanceMonitoringServicesHelper.GetSamplingIntervalRange() return type: ADLX_IntRange -> IntRangeInfo

## GammaInfo and GamutInfo - JsonConstructor Update
These currently have incomplete [JsonConstructor] params. Update them to include ALL fields
so JSON round-trip is complete.

## Making Native Methods Internal
Pattern: public -> internal for any method that returns or takes native pointers
These remain accessible within the same assembly (used by Facade Objects: ADLXGPU, ADLXDisplay, ADLXDesktop)

### ADLXApiHelper
- GetSystemServicesNative() -> internal
- GetSystemServicesHandle() -> internal

### ADLXSystemServicesHelper
All Get*Native(), Get*Handle(), TryGet*Native() -> internal
Also: EnumerateGPUsHandle(), GetGpuInfo(IADLXGPU*), EnumerateGPUsNative() -> internal
Factory methods updated to pass _system to each helper constructor.

### ADLX3DSettingsServicesHelper
- Get3DSettingsServicesNative/Handle -> internal
- Get3DSettingsChangedHandlingNative/TryGet.../GetHandle -> internal
- GetAll3DSettings(IADLXGPU*), TryGetAll3DSettings(IADLXGPU*,...) -> internal
- ApplyAll3DSettings(IADLXGPU*,...), TryApplyAll3DSettings -> internal
- GetFluidMotionFrames(IADLXGPU*), TryGetFluidMotionFrames -> internal
- ADD constructor param: IADLXSystem* system = null
- ADD public gpuUniqueId overloads for all GPU methods
- ADD private WithGpuByUniqueId<T> helper

### ADLXDesktopServicesHelper
All *Native() and *Handle() methods -> internal
Also: CreateADLXDesktop(IADLXDesktop*,...), CreateEyefinityDesktop(IADLXSimpleEyefinity*),
DestroyEyefinityDesktop(...), DestroyAllEyefinityDesktops(IADLXSimpleEyefinity*),
EnumerateDesktopHandles(), GetDesktopDisplayListNative, EnumerateDesktopDisplays(IADLXDesktop*),
GetEyefinityGridSize(IADLXEyefinityDesktop*), EnumerateEyefinityDisplays(IADLXEyefinityDesktop*),
GetSimpleEyefinityHandle() -> internal

### ADLXDisplayServicesHelper
All *Native() and *Handle() methods -> internal
Also: EnumerateDisplayHandles(), TryEnumerateDisplayHandles(), CreateADLXDisplay(IADLXDisplay*,...),
GetDisplayInfo(IADLXDisplay*) -> internal
ALL (IADLXDisplay*) parameter methods: GetFreeSyncState, SetFreeSyncEnabled, GetGPUScalingState,
SetGPUScalingEnabled, GetScalingMode, SetScalingMode, etc. -> internal (called from ADLXDisplay)

### ADLXGPUTuningServicesHelper
All *Native() and *Handle() methods -> internal
All (IADLXGPU*) methods -> internal
ADD constructor param: IADLXSystem* system = null
ADD public gpuUniqueId overloads: GetCapabilities, IsAutoTuningSupported, TryIs...,
IsPresetTuningSupported, IsManualGfxTuningSupported, IsManualVramTuningSupported,
IsManualFanTuningSupported, IsManualPowerTuningSupported (and all Try... variants),
GetManualFanTuning, GetManualVramTuning, GetManualGfxTuning, GetPresetTuning (and Try... variants)
ADD private WithGpuByUniqueId<T> helper

### ADLXMultimediaServicesHelper
All *Native() and *Handle() methods -> internal
All (IADLXGPU*) and (IADLXVideoUpscale*) and (IADLXVideoSuperResolution*) methods -> internal
ADD constructor param: IADLXSystem* system = null
ADD public gpuUniqueId overloads: GetVideoUpscale, TryGetVideoUpscale,
SetVideoUpscaleEnabled, TrySetVideoUpscaleEnabled, SetVideoUpscaleSharpness, TrySetVideoUpscaleSharpness,
GetVideoSuperResolution, TryGetVideoSuperResolution, SetVideoSuperResolutionEnabled, TrySetVideoSuperResolutionEnabled
ADD private WithGpuByUniqueId<T> helper

### ADLXPerformanceMonitoringServicesHelper
All *Native() and *Handle() methods -> internal
All (IADLXGPU*) methods -> internal
ADD constructor param: IADLXSystem* system = null
ADD public gpuUniqueId overloads: GetGpuMetricsSupport, TryGetGpuMetricsSupport,
GetCurrentGpuMetrics, TryGetCurrentGpuMetrics, EnumerateGpuMetricsHistory, TryEnumerateGpuMetricsHistory
ADD private WithGpuByUniqueId<T> helper
CHANGE GetSamplingIntervalRange() return type: ADLX_IntRange -> IntRangeInfo

### ADLXPowerTuningServicesHelper
All *Native() and *Handle() methods -> internal
EnumerateGPUConnectGpuHandles, TryEnumerateGPUConnectGpuHandles -> internal
GetSmartShiftMaxNative() -> internal
GetManualPowerTuning(IADLXGPUTuningServices*, IADLXGPU*) -> internal
CHANGE GetManualPowerTuning(ADLXGPUTuningServicesHelper, ADLXInterfaceHandle) signature
  to GetManualPowerTuning(int gpuUniqueId, ADLXGPUTuningServicesHelper)
  (removing the ADLXInterfaceHandle-based overload)
ApplyManualPowerTuning(IADLXManualPowerTuning*,...) -> internal
TryApplyManualPowerTuning(IADLXManualPowerTuning*,...) -> internal
ApplyManualPowerTuning(IADLXGPUTuningServices*, IADLXGPU*,...) -> internal
TryApplyManualPowerTuning(IADLXGPUTuningServices*, IADLXGPU*,...) -> internal
ADD public ApplyManualPowerTuning(int gpuUniqueId, ADLXGPUTuningServicesHelper, ManualPowerTuningInfo)
ADD public TryApplyManualPowerTuning(int gpuUniqueId, ADLXGPUTuningServicesHelper, ManualPowerTuningInfo)

## Facade Object Changes

### AdlxDesktop.cs
- Remove public ComPtr<IADLXDisplayList> GetDisplayListNative()

### AdlxDisplay.cs
- Remove public ComPtr<IADLXDisplayResolutionList> GetCustomResolutionListNative()
- CHANGE void ApplyCustomResolution(IADLXDisplayCustomResolution*, DisplayResolutionInfo)
        to void ApplyCustomResolution(DisplayResolutionInfo info)
- CHANGE bool TryApplyCustomResolution(IADLXDisplayCustomResolution*, DisplayResolutionInfo)
        to bool TryApplyCustomResolution(DisplayResolutionInfo info)

## Facade Test Changes (4 files)
These tests currently use EnumerateGPUsHandle() and gpuHandle.As<IADLXGPU>() to call
native-pointer-taking methods. After changes, they should:
1. Use EnumerateGPUs() -> first GpuInfo -> .UniqueId (int)
2. Call new int gpuUniqueId overloads

### ADLX3DSettingsServicesFacadeTests.cs
- Replace GetFirstGpuHandleOrSkip helper with GetFirstGpuUniqueIdOrSkip
- Update TryGetAll3DSettings(gpuHandle.As<IADLXGPU>(),...) -> TryGetAll3DSettings(gpuUniqueId,...)
- Update TryGetFluidMotionFrames(gpuHandle.As<IADLXGPU>(),...) -> TryGetFluidMotionFrames(gpuUniqueId,...)
- Update range property accesses: .minValue -> .MinValue, .maxValue -> .MaxValue

### ADLXGPUTuningServicesFacadeTests.cs
- Replace EnumerateGPUsHandle/gpuHandle.As<IADLXGPU>() with gpuUniqueId pattern
- Update all GPU-parameterized calls to use int gpuUniqueId

### ADLXMultimediaServicesFacadeTests.cs
- Replace GetFirstGpuHandleOrSkip with GetFirstGpuUniqueIdOrSkip
- Update TryGetVideoUpscale, TryGetVideoSuperResolution calls
- Update SharpnessRange.minValue/.maxValue -> .MinValue/.MaxValue

### ADLXPerformanceMonitoringServicesFacadeTests.cs
- Replace GetFirstGpuHandleOrSkip with GetFirstGpuUniqueIdOrSkip
- Update TryGetGpuMetricsSupport, TryGetCurrentGpuMetrics calls

## Execution Order (track with checkmarks)
- [x] Create PLAN.md
- [ ] Create ADLXDTOTypes.cs (IntRangeInfo, LuidInfo, RegammaCoeffInfo, GammaRampInfo, PointInfo, GamutColorSpaceInfo)
- [ ] Update ADLX3DSettingsServicesHelper.cs: DTO field types + constructor system param + native->internal + gpuUniqueId overloads
- [ ] Update ADLXDisplayServicesHelper.cs: DTO field types + native->internal + GammaInfo/GamutInfo JsonConstructor update
- [ ] Update ADLXMultimediaServicesHelper.cs: DTO field types + constructor system param + native->internal + gpuUniqueId overloads
- [ ] Update ADLXPowerTuningServicesHelper.cs: DTO field types + native->internal + new public overloads
- [ ] Update ADLXSystemServicesHelper.cs: GpuInfo.Luid type + native->internal + factory methods pass _system
- [ ] Update ADLXApiHelper.cs: GetSystemServicesNative/Handle -> internal
- [ ] Update ADLXGPUTuningServicesHelper.cs: native->internal + constructor system param + gpuUniqueId overloads
- [ ] Update ADLXDesktopServicesHelper.cs: native->internal
- [ ] Update ADLXPerformanceMonitoringServicesHelper.cs: native->internal + constructor system param + gpuUniqueId overloads + GetSamplingIntervalRange return type
- [ ] Update AdlxDesktop.cs: remove GetDisplayListNative
- [ ] Update AdlxDisplay.cs: remove native custom resolution methods
- [ ] Update ADLX3DSettingsServicesFacadeTests.cs
- [ ] Update ADLXGPUTuningServicesFacadeTests.cs
- [ ] Update ADLXMultimediaServicesFacadeTests.cs
- [ ] Update ADLXPerformanceMonitoringServicesFacadeTests.cs
- [ ] Build and fix any remaining errors
