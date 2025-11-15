using Xunit;
using Xunit.Abstractions;
using ADLXWrapper;

namespace ADLXWrapper.Tests;

/// <summary>
/// Tests for display enumeration, properties, and configuration
/// </summary>
[Collection("ADLX Tests")]
public class DisplayTests
{
    private readonly ADLXTestFixture _fixture;
    private readonly ITestOutputHelper _output;
    
    public DisplayTests(ADLXTestFixture fixture, ITestOutputHelper output)
    {
        _fixture = fixture;
        _output = output;
    }
    
    [SkippableFact]
    public void Test_01_Enumerate_Displays()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        
        var displayServicesPtr = ADLX.new_displaySerP_Ptr();
        try
        {
            var result = _fixture.System!.GetDisplaysServices(displayServicesPtr);
            Assert.Equal(ADLX_RESULT.ADLX_OK, result);
            
            var displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);
            Assert.NotNull(displayServices);
            
            var displayListPtr = ADLX.new_displayListP_Ptr();
            try
            {
                result = displayServices!.GetDisplays(displayListPtr);
                Assert.Equal(ADLX_RESULT.ADLX_OK, result);
                
                var displayList = ADLX.displayListP_Ptr_value(displayListPtr);
                Assert.NotNull(displayList);
                
                uint displayCount = displayList!.Size();
                
                _output.WriteLine($"Found {displayCount} display(s)");
                Assert.True(displayCount >= 0, "Display count should be valid");
                Assert.Equal(_fixture.Capabilities.DisplayCount, displayCount);
            }
            finally
            {
                ADLX.delete_displayListP_Ptr(displayListPtr);
            }
        }
        finally
        {
            ADLX.delete_displaySerP_Ptr(displayServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_02_Display_Properties()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.DisplayCount > 0, "No displays available");
        
        var displayServicesPtr = ADLX.new_displaySerP_Ptr();
        try
        {
            _fixture.System!.GetDisplaysServices(displayServicesPtr);
            var displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);
            
            var displayListPtr = ADLX.new_displayListP_Ptr();
            try
            {
                displayServices!.GetDisplays(displayListPtr);
                var displayList = ADLX.displayListP_Ptr_value(displayListPtr);
                
                uint displayCount = displayList!.Size();
                
                for (uint i = 0; i < displayCount; i++)
                {
                    var displayPtr = ADLX.new_displayP_Ptr();
                    displayList.At(i, displayPtr);
                    var display = ADLX.displayP_Ptr_value(displayPtr);
                    
                    if (display != null)
                    {
                        _output.WriteLine($"\n=== Display {i} Properties ===");
                        
                        // Display name
                        var namePtr = ADLX.new_charP_Ptr();
                        display.Name(namePtr);
                        string? displayName = ADLX.charP_Ptr_value(namePtr);
                        ADLX.delete_charP_Ptr(namePtr);
                        
                        _output.WriteLine($"Name: {displayName}");
                        Assert.NotNull(displayName);
                        
                        // Display type
                        var typePtr = ADLX.new_adlx_displayTypeP();
                        display.DisplayType(typePtr);
                        var displayType = ADLX.adlx_displayTypeP_value(typePtr);
                        ADLX.delete_adlx_displayTypeP(typePtr);
                        
                        _output.WriteLine($"Type: {displayType}");
                        
                        // Connector type
                        var connectorPtr = ADLX.new_adlx_displayConnectTypeP();
                        display.ConnectorType(connectorPtr);
                        var connectorType = ADLX.adlx_displayConnectTypeP_value(connectorPtr);
                        ADLX.delete_adlx_displayConnectTypeP(connectorPtr);
                        
                        _output.WriteLine($"Connector: {connectorType}");
                        
                        // Manufacturer ID
                        var mfgIdPtr = ADLX.new_adlx_intP();
                        display.ManufacturerID(mfgIdPtr);
                        int mfgId = ADLX.adlx_intP_value(mfgIdPtr);
                        ADLX.delete_adlx_intP(mfgIdPtr);
                        
                        _output.WriteLine($"Manufacturer ID: 0x{mfgId:X4}");
                    }
                    
                    ADLX.delete_displayP_Ptr(displayPtr);
                }
            }
            finally
            {
                ADLX.delete_displayListP_Ptr(displayListPtr);
            }
        }
        finally
        {
            ADLX.delete_displaySerP_Ptr(displayServicesPtr);
        }
    }
    
    [SkippableFact]
    public void Test_03_Display_Resolution()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.DisplayCount > 0, "No displays available");
        
        var display = GetFirstDisplay();
        if (display == null)
        {
            Skip.Always("Could not get first display");
        }
        
        _output.WriteLine("=== Display Resolution ===");
        
        // Native resolution
        var widthPtr = ADLX.new_adlx_intP();
        var heightPtr = ADLX.new_adlx_intP();
        
        try
        {
            var result = display!.NativeResolution(widthPtr, heightPtr);
            if (result == ADLX_RESULT.ADLX_OK)
            {
                int width = ADLX.adlx_intP_value(widthPtr);
                int height = ADLX.adlx_intP_value(heightPtr);
                
                _output.WriteLine($"Native Resolution: {width}x{height}");
                Assert.True(width > 0 && height > 0, "Resolution should be positive");
            }
        }
        finally
        {
            ADLX.delete_adlx_intP(widthPtr);
            ADLX.delete_adlx_intP(heightPtr);
        }
        
        // Refresh rate
        var refreshRatePtr = ADLX.new_doubleP();
        try
        {
            var result = display!.RefreshRate(refreshRatePtr);
            if (result == ADLX_RESULT.ADLX_OK)
            {
                double refreshRate = ADLX.doubleP_value(refreshRatePtr);
                _output.WriteLine($"Refresh Rate: {refreshRate:F2} Hz");
                Assert.True(refreshRate > 0, "Refresh rate should be positive");
            }
        }
        finally
        {
            ADLX.delete_doubleP(refreshRatePtr);
        }
    }
    
    [SkippableFact]
    public void Test_04_Display_Pixel_Clock()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.DisplayCount > 0, "No displays available");
        
        var display = GetFirstDisplay();
        if (display == null)
        {
            Skip.Always("Could not get first display");
        }
        
        _output.WriteLine("=== Display Pixel Clock ===");
        
        var pixelClockPtr = ADLX.new_adlx_intP();
        try
        {
            var result = display!.PixelClock(pixelClockPtr);
            if (result == ADLX_RESULT.ADLX_OK)
            {
                int pixelClock = ADLX.adlx_intP_value(pixelClockPtr);
                _output.WriteLine($"Pixel Clock: {pixelClock} KHz");
                Assert.True(pixelClock > 0, "Pixel clock should be positive");
            }
            else
            {
                _output.WriteLine($"Pixel clock not available: {result}");
            }
        }
        finally
        {
            ADLX.delete_adlx_intP(pixelClockPtr);
        }
    }
    
    [SkippableFact]
    public void Test_05_Display_Scan_Type()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.DisplayCount > 0, "No displays available");
        
        var display = GetFirstDisplay();
        if (display == null)
        {
            Skip.Always("Could not get first display");
        }
        
        _output.WriteLine("=== Display Scan Type ===");
        
        var scanTypePtr = ADLX.new_adlx_displayScanTypeP();
        try
        {
            var result = display!.ScanType(scanTypePtr);
            if (result == ADLX_RESULT.ADLX_OK)
            {
                var scanType = ADLX.adlx_displayScanTypeP_value(scanTypePtr);
                _output.WriteLine($"Scan Type: {scanType}");
            }
            else
            {
                _output.WriteLine($"Scan type not available: {result}");
            }
        }
        finally
        {
            ADLX.delete_adlx_displayScanTypeP(scanTypePtr);
        }
    }
    
    [SkippableFact]
    public void Test_06_Display_EDID()
    {
        Skip.IfNot(_fixture.IsADLXSupported, _fixture.SkipReason ?? "ADLX not supported");
        Skip.IfNot(_fixture.Capabilities.DisplayCount > 0, "No displays available");
        
        var display = GetFirstDisplay();
        if (display == null)
        {
            Skip.Always("Could not get first display");
        }
        
        _output.WriteLine("=== Display EDID Information ===");
        
        // Manufacturer ID
        var mfgIdPtr = ADLX.new_adlx_intP();
        try
        {
            display!.ManufacturerID(mfgIdPtr);
            int mfgId = ADLX.adlx_intP_value(mfgIdPtr);
            _output.WriteLine($"Manufacturer ID: 0x{mfgId:X4}");
        }
        finally
        {
            ADLX.delete_adlx_intP(mfgIdPtr);
        }
        
        // Product ID
        var productIdPtr = ADLX.new_adlx_intP();
        try
        {
            display!.ProductID(productIdPtr);
            int productId = ADLX.adlx_intP_value(productIdPtr);
            _output.WriteLine($"Product ID: 0x{productId:X4}");
        }
        finally
        {
            ADLX.delete_adlx_intP(productIdPtr);
        }
    }
    
    // Helper method to get first display
    private IADLXDisplay? GetFirstDisplay()
    {
        var displayServicesPtr = ADLX.new_displaySerP_Ptr();
        try
        {
            var result = _fixture.System!.GetDisplaysServices(displayServicesPtr);
            if (result != ADLX_RESULT.ADLX_OK)
                return null;
            
            var displayServices = ADLX.displaySerP_Ptr_value(displayServicesPtr);
            if (displayServices == null)
                return null;
            
            var displayListPtr = ADLX.new_displayListP_Ptr();
            try
            {
                result = displayServices.GetDisplays(displayListPtr);
                if (result != ADLX_RESULT.ADLX_OK)
                    return null;
                
                var displayList = ADLX.displayListP_Ptr_value(displayListPtr);
                if (displayList == null)
                    return null;
                
                uint displayCount = displayList.Size();
                if (displayCount == 0)
                    return null;
                
                var displayPtr = ADLX.new_displayP_Ptr();
                displayList.At(0, displayPtr);
                var display = ADLX.displayP_Ptr_value(displayPtr);
                ADLX.delete_displayP_Ptr(displayPtr);
                
                return display;
            }
            finally
            {
                ADLX.delete_displayListP_Ptr(displayListPtr);
            }
        }
        finally
        {
            ADLX.delete_displaySerP_Ptr(displayServicesPtr);
        }
    }
}
