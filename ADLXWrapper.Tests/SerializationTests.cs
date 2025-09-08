using Xunit;
using Newtonsoft.Json;
using ADLXWrapper; // Use the new bindings project
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
            ADLX_RGB originalRgb = new ADLX_RGB { gamutR = 255, gamutG = 128, gamutB = 0 };
            ADLX_RGB_DTO originalDto = new ADLX_RGB_DTO(originalRgb);

            string json = JsonConvert.SerializeObject(originalDto, Formatting.Indented);
            Assert.False(string.IsNullOrEmpty(json));

            ADLX_RGB_DTO deserializedDto = JsonConvert.DeserializeObject<ADLX_RGB_DTO>(json);
            Assert.NotNull(deserializedDto);
            Assert.Equal(originalDto.red, deserializedDto.red);
            Assert.Equal(originalDto.green, deserializedDto.green);
            Assert.Equal(originalDto.blue, deserializedDto.blue);

            ADLX_RGB deserializedRgb = deserializedDto.ToADLX_RGB();
            Assert.Equal(originalRgb.gamutR, deserializedRgb.gamutR);
            Assert.Equal(originalRgb.gamutG, deserializedRgb.gamutG);
            Assert.Equal(originalRgb.gamutB, deserializedRgb.gamutB);
        }

        [Fact]
        public void ADLX_TimingInfo_DTO_Serialization_ShouldWork()
        {
            ADLX_TimingInfo originalTimingInfo = new ADLX_TimingInfo
            {
                timingFlags = 1,
                hTotal = 1920,
                vTotal = 1080,
                hDisplay = 1920,
                vDisplay = 1080,
                hFrontPorch = 100,
                vFrontPorch = 5,
                hSyncWidth = 50,
                vSyncWidth = 2,
                hPolarity = 0,
                vPolarity = 0
            };
            ADLX_TimingInfo_DTO originalDto = new ADLX_TimingInfo_DTO(originalTimingInfo);

            string json = JsonConvert.SerializeObject(originalDto, Formatting.Indented);
            Assert.False(string.IsNullOrEmpty(json));

            ADLX_TimingInfo_DTO deserializedDto = JsonConvert.DeserializeObject<ADLX_TimingInfo_DTO>(json);
            Assert.NotNull(deserializedDto);
            Assert.Equal(originalDto.timingFlags, deserializedDto.timingFlags);
            Assert.Equal(originalDto.hTotal, deserializedDto.hTotal);
            Assert.Equal(originalDto.vTotal, deserializedDto.vTotal);
            Assert.Equal(originalDto.hDisplay, deserializedDto.hDisplay);
            Assert.Equal(originalDto.vDisplay, deserializedDto.vDisplay);
            Assert.Equal(originalDto.hFrontPorch, deserializedDto.hFrontPorch);
            Assert.Equal(originalDto.vFrontPorch, deserializedDto.vFrontPorch);
            Assert.Equal(originalDto.hSyncWidth, deserializedDto.hSyncWidth);
            Assert.Equal(originalDto.vSyncWidth, deserializedDto.vSyncWidth);
            Assert.Equal(originalDto.hPolarity, deserializedDto.hPolarity);
            Assert.Equal(originalDto.vPolarity, deserializedDto.vPolarity);

            ADLX_TimingInfo deserializedTimingInfo = deserializedDto.ToADLX_TimingInfo();
            Assert.Equal(originalTimingInfo.timingFlags, deserializedTimingInfo.timingFlags);
            Assert.Equal(originalTimingInfo.hTotal, deserializedTimingInfo.hTotal);
            Assert.Equal(originalTimingInfo.vTotal, deserializedTimingInfo.vTotal);
            Assert.Equal(originalTimingInfo.hDisplay, deserializedTimingInfo.hDisplay);
            Assert.Equal(originalTimingInfo.vDisplay, deserializedTimingInfo.vDisplay);
            Assert.Equal(originalTimingInfo.hFrontPorch, deserializedTimingInfo.hFrontPorch);
            Assert.Equal(originalTimingInfo.vFrontPorch, deserializedTimingInfo.vFrontPorch);
            Assert.Equal(originalTimingInfo.hSyncWidth, deserializedTimingInfo.hSyncWidth);
            Assert.Equal(originalTimingInfo.vSyncWidth, deserializedTimingInfo.vSyncWidth);
            Assert.Equal(originalTimingInfo.hPolarity, deserializedTimingInfo.hPolarity);
            Assert.Equal(originalTimingInfo.vPolarity, deserializedTimingInfo.vPolarity);
        }
    }
}
