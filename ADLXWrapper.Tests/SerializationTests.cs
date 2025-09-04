using Xunit;
using Newtonsoft.Json;
using ADLXWrapper;
using System.Collections.Generic;

namespace ADLXWrapper.Tests
{
    public class SerializationTests
    {
        [Fact]
        public void ADLX_GPU_Serialization_ShouldWork()
        {
            // This test assumes a mock or actual ADLX GPU object can be created.
            // For now, we'll create a dummy object that mimics the structure for serialization.
            // In a real scenario, you would initialize ADLX and retrieve an actual IADLXGPU.

            // Dummy data for serialization
            var dummyGpu = new
            {
                Name = "Dummy AMD Radeon RX 6900 XT",
                VendorId = 1002,
                DeviceId = 0x73AF,
                // Add other relevant properties that would be exposed by IADLXGPU
            };

            string json = JsonConvert.SerializeObject(dummyGpu, Formatting.Indented);
            Assert.False(string.IsNullOrEmpty(json));

            // Deserialize back to a dynamic object or a custom class if available
            dynamic deserializedGpu = JsonConvert.DeserializeObject(json);
            Assert.NotNull(deserializedGpu);
            Assert.Equal("Dummy AMD Radeon RX 6900 XT", deserializedGpu.Name.ToString());
            Assert.Equal(1002, (int)deserializedGpu.VendorId);
        }

        [Fact]
        public void ADLX_Display_Serialization_ShouldWork()
        {
            // Dummy data for serialization
            var dummyDisplay = new
            {
                Name = "Dummy Monitor",
                Manufacturer = "DummyCorp",
                RefreshRate = 144,
                UniqueId = "12345-ABCDE-67890",
                // Add other relevant properties that would be exposed by IADLXDisplay
            };

            string json = JsonConvert.SerializeObject(dummyDisplay, Formatting.Indented);
            Assert.False(string.IsNullOrEmpty(json));

            // Deserialize back
            dynamic deserializedDisplay = JsonConvert.DeserializeObject(json);
            Assert.NotNull(deserializedDisplay);
            Assert.Equal("Dummy Monitor", deserializedDisplay.Name.ToString());
            Assert.Equal("DummyCorp", deserializedDisplay.Manufacturer.ToString());
            Assert.Equal(144, (int)deserializedDisplay.RefreshRate);
        }

        // Note: For actual ADLX objects, you would need to implement custom JsonConverters
        // or ensure the ADLXWrapper objects have public properties/fields that Newtonsoft.Json can access.
        // The current ADLXWrapper interfaces are COM-based, so direct serialization might require
        // a wrapper class or DTOs for proper JSON serialization.
    }
}
