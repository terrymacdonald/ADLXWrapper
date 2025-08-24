//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------
// SerializableGammaRamp - JSON-serializable gamma ramp data structure
//
// This class provides a JSON-friendly representation of ADLX_GammaRamp data
// that can be easily serialized and deserialized for storage and transmission.
//-------------------------------------------------------------------------------------------------

using System;

namespace ADLXCSharpBind
{
    /// <summary>
    /// JSON-serializable representation of gamma ramp data
    /// </summary>
    public class SerializableGammaRamp
    {
        /// <summary>
        /// Red channel gamma values (256 entries, 0-65535 range)
        /// </summary>
        public ushort[] Red { get; set; }

        /// <summary>
        /// Green channel gamma values (256 entries, 0-65535 range)
        /// </summary>
        public ushort[] Green { get; set; }

        /// <summary>
        /// Blue channel gamma values (256 entries, 0-65535 range)
        /// </summary>
        public ushort[] Blue { get; set; }

        /// <summary>
        /// Timestamp when the gamma ramp was captured
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Creation timestamp (alias for Timestamp for compatibility)
        /// </summary>
        public DateTime CreatedAt 
        { 
            get => Timestamp; 
            set => Timestamp = value; 
        }

        /// <summary>
        /// Optional description or identifier for this gamma ramp
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Version identifier for compatibility tracking
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Default constructor
        /// </summary>
        public SerializableGammaRamp()
        {
            Red = new ushort[256];
            Green = new ushort[256];
            Blue = new ushort[256];
            Timestamp = DateTime.UtcNow;
            Description = string.Empty;
            Version = "1.0";
        }

        /// <summary>
        /// Constructor with description
        /// </summary>
        /// <param name="description">Description for this gamma ramp</param>
        public SerializableGammaRamp(string description) : this()
        {
            Description = description ?? string.Empty;
        }

        /// <summary>
        /// Validates that the gamma ramp data is properly formatted
        /// </summary>
        /// <returns>True if valid, false otherwise</returns>
        public bool IsValid()
        {
            return Red != null && Red.Length == 256 &&
                   Green != null && Green.Length == 256 &&
                   Blue != null && Blue.Length == 256;
        }

        /// <summary>
        /// Creates a linear gamma ramp (identity transformation)
        /// </summary>
        /// <returns>SerializableGammaRamp with linear values</returns>
        public static SerializableGammaRamp CreateLinear()
        {
            var gammaRamp = new SerializableGammaRamp("Linear Gamma Ramp");
            
            for (int i = 0; i < 256; i++)
            {
                // Linear mapping: 0-255 input maps to 0-65535 output
                ushort value = (ushort)(i * 257); // 257 = 65535 / 255
                gammaRamp.Red[i] = value;
                gammaRamp.Green[i] = value;
                gammaRamp.Blue[i] = value;
            }
            
            return gammaRamp;
        }

        /// <summary>
        /// Creates a gamma ramp with specified gamma correction
        /// </summary>
        /// <param name="gamma">Gamma correction value (typically 1.0-3.0)</param>
        /// <returns>SerializableGammaRamp with gamma correction applied</returns>
        public static SerializableGammaRamp CreateWithGamma(double gamma)
        {
            var gammaRamp = new SerializableGammaRamp($"Gamma {gamma:F2}");
            
            for (int i = 0; i < 256; i++)
            {
                // Apply gamma correction: output = (input/255)^(1/gamma) * 65535
                double normalized = i / 255.0;
                double corrected = Math.Pow(normalized, 1.0 / gamma);
                ushort value = (ushort)Math.Min(65535, Math.Max(0, corrected * 65535));
                
                gammaRamp.Red[i] = value;
                gammaRamp.Green[i] = value;
                gammaRamp.Blue[i] = value;
            }
            
            return gammaRamp;
        }

        /// <summary>
        /// Creates a copy of this gamma ramp
        /// </summary>
        /// <returns>Deep copy of the gamma ramp</returns>
        public SerializableGammaRamp Clone()
        {
            var clone = new SerializableGammaRamp(Description)
            {
                Timestamp = Timestamp,
                Version = Version
            };
            
            Array.Copy(Red, clone.Red, 256);
            Array.Copy(Green, clone.Green, 256);
            Array.Copy(Blue, clone.Blue, 256);
            
            return clone;
        }

        /// <summary>
        /// Gets a summary of the gamma ramp for debugging
        /// </summary>
        /// <returns>String representation of the gamma ramp</returns>
        public override string ToString()
        {
            return $"SerializableGammaRamp: {Description} (Version: {Version}, Timestamp: {Timestamp:yyyy-MM-dd HH:mm:ss} UTC)";
        }
    }
}
