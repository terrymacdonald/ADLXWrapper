using Xunit;
using Newtonsoft.Json;
using ADLXWrapper.Bindings; // Use the new bindings project
using System.Collections.Generic;

namespace ADLXWrapper.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void ADLX_Point_DTO_Serialization_ShouldWork()
        {
            ADLX_Point originalPoint = new ADLX_Point { x = 10, y = 20 };
            ADLX_Point_DTO originalDto = new ADLX_Point_DTO(originalPoint);

            string json = JsonConvert.SerializeObject(originalDto, Formatting.Indented);
            Assert.False(string.IsNullOrEmpty(json));

            ADLX_Point_DTO deserializedDto = JsonConvert.DeserializeObject<ADLX_Point_DTO>(json);
            Assert.NotNull(deserializedDto);
            Assert.Equal(originalDto.x, deserializedDto.x);
            Assert.Equal(originalDto.y, deserializedDto.y);

            ADLX_Point deserializedPoint = deserializedDto.ToADLX_Point();
            Assert.Equal(originalPoint.x, deserializedPoint.x);
            Assert.Equal(originalPoint.y, deserializedPoint.y);
        }

        [Fact]
        public void ADLX_RGB_DTO_Serialization_ShouldWork()
        {
            ADLX_RGB originalRgb = new ADLX_RGB { red = 255, green = 128, blue = 0 };
            ADLX_RGB_DTO originalDto = new ADLX_RGB_DTO(originalRgb);

            string json = JsonConvert.SerializeObject(originalDto, Formatting.Indented);
            Assert.False(string.IsNullOrEmpty(json));

            ADLX_RGB_DTO deserializedDto = JsonConvert.DeserializeObject<ADLX_RGB_DTO>(json);
            Assert.NotNull(deserializedDto);
            Assert.Equal(originalDto.red, deserializedDto.red);
            Assert.Equal(originalDto.green, deserializedDto.green);
            Assert.Equal(originalDto.blue, deserializedDto.blue);

            ADLX_RGB deserializedRgb = deserializedDto.ToADLX_RGB();
            Assert.Equal(originalRgb.red, deserializedRgb.red);
            Assert.Equal(originalRgb.green, deserializedRgb.green);
            Assert.Equal(originalRgb.blue, deserializedRgb.blue);
        }

        [Fact]
        public void ADLX_TimingInfo_DTO_Serialization_ShouldWork()
        {
            ADLX_TimingInfo originalTimingInfo = new ADLX_TimingInfo
            {
                mode = 1,
                refreshRate = 60,
                horizontalTotal = 1920,
                horizontalAddressable = 1920,
                horizontalSyncWidth = 100,
                horizontalSyncStart = 50,
                verticalTotal = 1080,
                verticalAddressable = 1080,
                verticalSyncWidth = 5,
                verticalSyncStart = 2,
                scanType = 0, // Progressive
                pixelClock = 148500,
                colorDepth = 8,
                stereoMode = 0
            };
            ADLX_TimingInfo_DTO originalDto = new ADLX_TimingInfo_DTO(originalTimingInfo);

            string json = JsonConvert.SerializeObject(originalDto, Formatting.Indented);
            Assert.False(string.IsNullOrEmpty(json));

            ADLX_TimingInfo_DTO deserializedDto = JsonConvert.DeserializeObject<ADLX_TimingInfo_DTO>(json);
            Assert.NotNull(deserializedDto);
            Assert.Equal(originalDto.mode, deserializedDto.mode);
            Assert.Equal(originalDto.refreshRate, deserializedDto.refreshRate);
            Assert.Equal(originalDto.horizontalTotal, deserializedDto.horizontalTotal);
            Assert.Equal(originalDto.horizontalAddressable, deserializedDto.horizontalAddressable);
            Assert.Equal(originalDto.horizontalSyncWidth, deserializedDto.horizontalSyncWidth);
            Assert.Equal(originalDto.horizontalSyncStart, deserializedDto.horizontalSyncStart);
            Assert.Equal(originalDto.verticalTotal, deserializedDto.verticalTotal);
            Assert.Equal(originalDto.verticalAddressable, deserializedDto.verticalAddressable);
            Assert.Equal(originalDto.verticalSyncWidth, deserializedDto.verticalSyncWidth);
            Assert.Equal(originalDto.verticalSyncStart, deserializedDto.verticalSyncStart);
            Assert.Equal(originalDto.scanType, deserializedDto.scanType);
            Assert.Equal(originalDto.pixelClock, deserializedDto.pixelClock);
            Assert.Equal(originalDto.colorDepth, deserializedDto.colorDepth);
            Assert.Equal(originalDto.stereoMode, deserializedDto.stereoMode);

            ADLX_TimingInfo deserializedTimingInfo = deserializedDto.ToADLX_TimingInfo();
            Assert.Equal(originalTimingInfo.mode, deserializedTimingInfo.mode);
            Assert.Equal(originalTimingInfo.refreshRate, deserializedTimingInfo.refreshRate);
            Assert.Equal(originalTimingInfo.horizontalTotal, deserializedTimingInfo.horizontalTotal);
            Assert.Equal(originalTimingInfo.horizontalAddressable, deserializedTimingInfo.horizontalAddressable);
            Assert.Equal(originalTimingInfo.horizontalSyncWidth, deserializedTimingInfo.horizontalSyncWidth);
            Assert.Equal(originalTimingInfo.horizontalSyncStart, deserializedTimingInfo.horizontalSyncStart);
            Assert.Equal(originalTimingInfo.verticalTotal, deserializedTimingInfo.verticalTotal);
            Assert.Equal(originalTimingInfo.verticalAddressable, deserializedTimingInfo.verticalAddressable);
            Assert.Equal(originalTimingInfo.verticalSyncWidth, deserializedTimingInfo.verticalSyncWidth);
            Assert.Equal(originalTimingInfo.verticalSyncStart, deserializedTimingInfo.verticalSyncStart);
            Assert.Equal(originalTimingInfo.scanType, deserializedTimingInfo.scanType);
            Assert.Equal(originalTimingInfo.pixelClock, deserializedTimingInfo.pixelClock);
            Assert.Equal(originalTimingInfo.colorDepth, deserializedTimingInfo.colorDepth);
            Assert.Equal(originalTimingInfo.stereoMode, deserializedTimingInfo.stereoMode);
        }
    }
}
