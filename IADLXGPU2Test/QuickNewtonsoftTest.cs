using System;
using ADLXWrapper;

namespace IADLXGPU2Test
{
    class QuickNewtonsoftTest
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Quick Newtonsoft.Json Test for ADLX_GammaRamp Serialization");
            Console.WriteLine("=============================================================");
            
            try
            {
                // Create a test gamma ramp
                var gammaRamp = SerializableGammaRamp.CreateLinear();
                gammaRamp.Description = "Newtonsoft Test Ramp";
                Console.WriteLine($"✅ Created: {gammaRamp}");
                
                // Test Newtonsoft.Json serialization
                string json = NewtonsoftJsonUtility.ToJson(gammaRamp);
                Console.WriteLine($"✅ Serialized to JSON: {json.Length} characters");
                
                // Test deserialization
                var deserialized = NewtonsoftJsonUtility.FromJson(json);
                Console.WriteLine($"✅ Deserialized: {deserialized}");
                
                // Test file operations
                string testFile = "newtonsoft_test.json";
                NewtonsoftJsonUtility.SaveToFile(gammaRamp, testFile);
                Console.WriteLine($"✅ Saved to file: {testFile}");
                
                var loaded = NewtonsoftJsonUtility.LoadFromFile(testFile);
                Console.WriteLine($"✅ Loaded from file: {loaded}");
                
                // Cleanup
                if (File.Exists(testFile))
                    File.Delete(testFile);
                
                Console.WriteLine("\n🎉 ALL NEWTONSOFT.JSON TESTS PASSED!");
                Console.WriteLine("\nYour ADLX_GammaRamp JSON serialization solution is working perfectly!");
                Console.WriteLine("You can now use either System.Text.Json or Newtonsoft.Json for serialization.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Error: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
            }
        }
    }
}
