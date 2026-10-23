using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using Xunit;

namespace ADLXWrapper.FacadeTests;

public unsafe class SettingsPersistenceTests
{
    [Fact]
    public void Fluid_motion_frames_round_trips_all_fields()
    {
        var expected = new FluidMotionFramesDto(true, true, true,
            (ADLX_AFMF_ALGORITHM)1, (ADLX_AFMF_SEARCH_MODE_TYPE)1,
            (ADLX_AFMF_PERFORMANCE_MODE_TYPE)1, (ADLX_AFMF_FAST_MOTION_RESP)1);
        var settings = new All3DSettingsDto { FluidMotionFrames = expected };

        string json = JsonConvert.SerializeObject(settings);
        var restored = JsonConvert.DeserializeObject<All3DSettingsDto>(json);

        Assert.Contains("\"FluidMotionFrames\":", json);
        Assert.Equal(expected, restored.FluidMotionFrames);
    }

    [Fact]
    public void Fluid_motion_frames_reads_legacy_two_field_json()
    {
        var restored = JsonConvert.DeserializeObject<FluidMotionFramesDto>(
            "{\"IsSupported\":true,\"IsEnabled\":true}");

        Assert.True(restored.IsSupported);
        Assert.True(restored.IsEnabled);
        Assert.False(restored.IsAlgorithmSupported);
        Assert.Equal(default, restored.Algorithm);
    }

    [Theory]
    [InlineData(true, false, 1)]
    [InlineData(false, true, 2)]
    [InlineData(false, false, 3)]
    public void Three_d_lut_applies_saved_preset_not_contrast_readability(bool disabled, bool vivid, int expectedPreset)
    {
        void** vtable = stackalloc void*[14];
        vtable[7] = (delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_RESULT>)&SetDisabled;
        vtable[8] = (delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_RESULT>)&SetVivid;
        vtable[13] = (delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, int, ADLX_RESULT>)&SetContrast;
        FakeLut lut = new() { Native = new IADLXDisplay3DLUT { lpVtbl = vtable } };
        var saved = new ThreeDLUTDto(true, true, true, false, disabled, vivid, true,
            7, new IntRangeDto(0, 10, 1));

        Apply(&lut.Native, saved);

        Assert.Equal(expectedPreset, lut.Preset);
        Assert.Equal(expectedPreset == 3 ? 7 : 0, lut.Contrast);
        Assert.Equal(1, lut.Calls);
    }

    [Theory]
    [InlineData(ADLX_RESULT.ADLX_NOT_SUPPORTED)]
    [InlineData(ADLX_RESULT.ADLX_FAIL)]
    public void Three_d_lut_does_not_hide_driver_write_failures(ADLX_RESULT result)
    {
        void** vtable = stackalloc void*[14];
        vtable[7] = (delegate* unmanaged[Stdcall]<IADLXDisplay3DLUT*, ADLX_RESULT>)&SetDisabled;
        FakeLut lut = new() { Native = new IADLXDisplay3DLUT { lpVtbl = vtable }, Result = result };
        var saved = new ThreeDLUTDto(true, false, false, false, true, false, false, 0, default);
        ADLX_RESULT? actual = null;
        try
        {
            Apply(&lut.Native, saved);
        }
        catch (TargetInvocationException ex) when (ex.InnerException is ADLXException adlx)
        {
            actual = adlx.Result;
        }
        Assert.Equal(result, actual);
    }

    private static void Apply(IADLXDisplay3DLUT* lut, ThreeDLUTDto settings)
    {
        Type ops = typeof(ADLXDisplayServicesHelper).GetNestedType("DisplaySettingsOps", BindingFlags.NonPublic)!;
        MethodInfo apply = ops.GetMethod("Apply3DLUT", BindingFlags.Public | BindingFlags.Static)!;
        apply.Invoke(null, new object[] { Pointer.Box(lut, typeof(IADLXDisplay3DLUT*)), settings });
    }

    [StructLayout(LayoutKind.Sequential)]
    private struct FakeLut
    {
        public IADLXDisplay3DLUT Native;
        public int Preset;
        public int Contrast;
        public int Calls;
        public ADLX_RESULT Result;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static ADLX_RESULT SetDisabled(IADLXDisplay3DLUT* native)
    {
        FakeLut* lut = (FakeLut*)native;
        lut->Preset = 1;
        lut->Calls++;
        return lut->Result;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static ADLX_RESULT SetVivid(IADLXDisplay3DLUT* native)
    {
        FakeLut* lut = (FakeLut*)native;
        lut->Preset = 2;
        lut->Calls++;
        return lut->Result;
    }

    [UnmanagedCallersOnly(CallConvs = new[] { typeof(CallConvStdcall) })]
    private static ADLX_RESULT SetContrast(IADLXDisplay3DLUT* native, int contrast)
    {
        FakeLut* lut = (FakeLut*)native;
        lut->Preset = 3;
        lut->Contrast = contrast;
        lut->Calls++;
        return lut->Result;
    }
}
