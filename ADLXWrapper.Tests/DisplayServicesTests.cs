using Xunit;
using System;
using ADLXWrapper;

namespace ADLXWrapper.Tests
{
    public class DisplayServicesTests
    {
        [Fact]
        public void ADLX_GetDisplays_ShouldReturnAtLeastOneDisplay()
        {
            global::ADLX.ADLX_RESULT res = global::ADLX.ADLXInitialize();
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);

            global::ADLXWrapper.IADLXSystem sys = null;
            res = global::ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            global::ADLXWrapper.IADLXDisplayServices displayServices = null;
            res = sys.GetDisplayServices(ref displayServices);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displayServices);

            global::ADLXWrapper.IADLXDisplayList displays = null;
            res = displayServices.GetDisplays(ref displays);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displays);
            Assert.True(displays.Size() > 0);

            global::ADLXWrapper.IADLXDisplay display = null;
            res = displays.At(0, ref display);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(display);

            string displayId = "";
            res = display.GetUniqueId(ref displayId);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.False(string.IsNullOrEmpty(displayId));

            display.Release();
            displays.Release();
            displayServices.Release();
            sys.Release();
            global::ADLX.ADLXTerminate();
        }

        [Fact]
        public void ADLX_GetDisplayDetails_ShouldReturnValidDetails()
        {
            global::ADLX.ADLX_RESULT res = global::ADLX.ADLXInitialize();
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);

            global::ADLXWrapper.IADLXSystem sys = null;
            res = global::ADLX.ADLXGetSystemServices(ref sys);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(sys);

            global::ADLXWrapper.IADLXDisplayServices displayServices = null;
            res = sys.GetDisplayServices(ref displayServices);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displayServices);

            global::ADLXWrapper.IADLXDisplayList displays = null;
            res = displayServices.GetDisplays(ref displays);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(displays);
            Assert.True(displays.Size() > 0);

            global::ADLXWrapper.IADLXDisplay display = null;
            res = displays.At(0, ref display);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.NotNull(display);

            string name = "";
            res = display.Name(ref name);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.False(string.IsNullOrEmpty(name));

            string manufacturer = "";
            res = display.Manufacturer(ref manufacturer);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.False(string.IsNullOrEmpty(manufacturer));

            uint refreshRate = 0;
            res = display.RefreshRate(ref refreshRate);
            Assert.Equal(global::ADLX.ADLX_RESULT.ADLX_OK, res);
            Assert.True(refreshRate > 0);

            display.Release();
            displays.Release();
            displayServices.Release();
            sys.Release();
            global::ADLX.ADLXTerminate();
        }
    }
}
