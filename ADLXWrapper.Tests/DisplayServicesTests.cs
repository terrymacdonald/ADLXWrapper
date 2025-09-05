using Xunit;
using System;
using ADLXWrapper.Bindings; // Use the new bindings project

namespace ADLXWrapper.Tests
{
    public class DisplayServicesTests : IDisposable
    {
        private readonly ADLXHelper _adlxHelper;
        private readonly IADLXSystem _sys;
        private readonly IADLXDisplayServices _displayServices;

        public DisplayServicesTests()
        {
            _adlxHelper = new ADLXHelper();
            _adlxHelper.Initialize();
            _sys = _adlxHelper.GetSystemServices();
            _displayServices = _sys.GetDisplayServices();
        }

        public void Dispose()
        {
            _displayServices.Dispose();
            _sys.Dispose();
            _adlxHelper.Terminate();
            _adlxHelper.Dispose();
        }

        [Fact]
        public void QueryDisplayServices_ShouldReturnValidInterface()
        {
            Assert.NotNull(_displayServices);
        }

        [Fact]
        public void EnumerateDisplays_ShouldReturnAtLeastOneDisplay()
        {
            SWIGTYPE_p_p_adlx__IADLXDisplayList displays_ptr = ADLX.new_display_list_ptr();
            ADLX_RESULT res = _displayServices.GetDisplays(displays_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXDisplayList displays = new IADLXDisplayList(ADLX.display_list_ptr_value(displays_ptr), true);
            Assert.NotNull(displays);
            Assert.True(displays.Size() > 0);
            displays.Dispose();
            ADLX.delete_display_list_ptr(displays_ptr);
        }

        [Fact]
        public void GetDisplayResolution_ShouldReturnValidResolution()
        {
            SWIGTYPE_p_p_adlx__IADLXDisplayList displays_ptr = ADLX.new_display_list_ptr();
            ADLX_RESULT res = _displayServices.GetDisplays(displays_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXDisplayList displays = new IADLXDisplayList(ADLX.display_list_ptr_value(displays_ptr), true);
            Assert.NotNull(displays);
            Assert.True(displays.Size() > 0);

            SWIGTYPE_p_p_adlx__IADLXDisplay display_ptr = ADLX.new_display_ptr();
            res = displays.At(0, display_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXDisplay display = new IADLXDisplay(ADLX.display_ptr_value(display_ptr), true);
            Assert.NotNull(display);

            SWIGTYPE_p_p_adlx__IADLXDisplayResolution displayResolution_ptr = ADLX.new_display_resolution_ptr();
            res = display.GetResolution(displayResolution_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXDisplayResolution displayResolution = new IADLXDisplayResolution(ADLX.display_resolution_ptr_value(displayResolution_ptr), true);
            Assert.NotNull(displayResolution);

            uint width = 0, height = 0;
            res = displayResolution.GetDesktopResolution(ref width, ref height);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.True(width > 0);
            Assert.True(height > 0);

            displayResolution.Dispose();
            display.Dispose();
            displays.Dispose();
            ADLX.delete_display_list_ptr(displays_ptr);
            ADLX.delete_display_ptr(display_ptr);
            ADLX.delete_display_resolution_ptr(displayResolution_ptr);
        }

        [Fact]
        public void GetDisplayRefreshRate_ShouldReturnValidRefreshRate()
        {
            SWIGTYPE_p_p_adlx__IADLXDisplayList displays_ptr = ADLX.new_display_list_ptr();
            ADLX_RESULT res = _displayServices.GetDisplays(displays_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXDisplayList displays = new IADLXDisplayList(ADLX.display_list_ptr_value(displays_ptr), true);
            Assert.NotNull(displays);
            Assert.True(displays.Size() > 0);

            SWIGTYPE_p_p_adlx__IADLXDisplay display_ptr = ADLX.new_display_ptr();
            res = displays.At(0, display_ptr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);

            IADLXDisplay display = new IADLXDisplay(ADLX.display_ptr_value(display_ptr), true);
            Assert.NotNull(display);

            uint refreshRate = 0;
            res = display.RefreshRate(ref refreshRate);
            Assert.Equal(ADLX_RESULT.ADLX_OK, res);
            Assert.True(refreshRate > 0);

            display.Dispose();
            displays.Dispose();
            ADLX.delete_display_list_ptr(displays_ptr);
            ADLX.delete_display_ptr(display_ptr);
        }
    }
}
