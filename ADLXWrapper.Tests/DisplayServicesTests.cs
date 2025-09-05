using Xunit;
using System;
using ADLXWrapper.Bindings; // Use the new bindings project

namespace ADLXWrapper.Tests
{
    public class DisplayServicesTests
    {
        [Fact]
        public void QueryDisplayServices_ShouldReturnValidInterface()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXDisplayServices displayServices = null;
            res = sys.GetDisplayServices(ref displayServices);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displayServices);

            displayServices.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }

        [Fact]
        public void EnumerateDisplays_ShouldReturnAtLeastOneDisplay()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXDisplayServices displayServices = null;
            res = sys.GetDisplayServices(ref displayServices);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displayServices);

            IADLXDisplayList displays = null;
            res = displayServices.GetDisplays(ref displays);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displays);
            Assert.True(displays.Size() > 0);

            displays.Release();
            displayServices.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }

        [Fact]
        public void GetDisplayResolution_ShouldReturnValidResolution()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXDisplayServices displayServices = null;
            res = sys.GetDisplayServices(ref displayServices);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displayServices);

            IADLXDisplayList displays = null;
            res = displayServices.GetDisplays(ref displays);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displays);
            Assert.True(displays.Size() > 0);

            IADLXDisplay display = null;
            res = displays.At(0, ref display);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(display);

            IADLXDisplayResolution displayResolution = null;
            res = display.GetResolution(ref displayResolution);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displayResolution);

            uint width = 0, height = 0;
            res = displayResolution.GetDesktopResolution(ref width, ref height);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.True(width > 0);
            Assert.True(height > 0);

            displayResolution.Release();
            display.Release();
            displays.Release();
            displayServices.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }

        [Fact]
        public void GetDisplayRefreshRate_ShouldReturnValidRefreshRate()
        {
            ADLX_RESULT res = ADLXHelper.Initialize();
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXSystem sys = null;
            res = ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            IADLXDisplayServices displayServices = null;
            res = sys.GetDisplayServices(ref displayServices);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displayServices);

            IADLXDisplayList displays = null;
            res = displayServices.GetDisplays(ref displays);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displays);
            Assert.True(displays.Size() > 0);

            IADLXDisplay display = null;
            res = displays.At(0, ref display);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(display);

            uint refreshRate = 0;
            res = display.RefreshRate(ref refreshRate);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.True(refreshRate > 0);

            display.Release();
            displays.Release();
            displayServices.Release();
            sys.Release();
            ADLXHelper.Terminate();
        }
    }
}
