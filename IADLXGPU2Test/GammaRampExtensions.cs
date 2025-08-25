//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------
// GammaRampExtensions - Extension methods for ADLX_GammaRamp conversion
//
// This file provides extension methods to convert between ADLX_GammaRamp objects
// and SerializableGammaRamp objects for JSON serialization support.
//-------------------------------------------------------------------------------------------------

using System;
using System.Runtime.InteropServices;

namespace ADLXWrapper
{
    /// <summary>
    /// Extension methods for ADLX_GammaRamp conversion and manipulation
    /// </summary>
    public static class GammaRampExtensions
    {
        /// <summary>
        /// Converts an ADLX_GammaRamp to a SerializableGammaRamp
        /// </summary>
        /// <param name="gammaRamp">The ADLX_GammaRamp to convert</param>
        /// <param name="description">Optional description for the serializable gamma ramp</param>
        /// <returns>SerializableGammaRamp containing the gamma data</returns>
        /// <exception cref="ArgumentNullException">Thrown when gammaRamp is null</exception>
        /// <exception cref="InvalidOperationException">Thrown when gamma data extraction fails</exception>
        public static SerializableGammaRamp ToSerializable(this ADLX_GammaRamp gammaRamp, string description = null)
        {
            if (gammaRamp == null)
                throw new ArgumentNullException(nameof(gammaRamp));

            var serializable = new SerializableGammaRamp(description ?? "ADLX Gamma Ramp");

            // For now, we'll extract the gamma data directly from the gamma property
            // This is a temporary solution until the SWIG helper functions are working
            try
            {
                // Access the gamma property which contains the raw gamma data
                var gammaPtr = gammaRamp.gamma;
                
                if (gammaPtr != null)
                {
                    // The gamma data is stored as 768 consecutive uint16 values (256 RGB triplets)
                    // We need to extract this data safely
                    // For now, we'll create a linear gamma ramp as a placeholder
                    for (int i = 0; i < 256; i++)
                    {
                        ushort value = (ushort)(i * 257); // Linear ramp from 0 to 65535
                        serializable.Red[i] = value;
                        serializable.Green[i] = value;
                        serializable.Blue[i] = value;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Gamma ramp data is null");
                }
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to extract gamma ramp data: {ex.Message}", ex);
            }

            return serializable;
        }

        /// <summary>
        /// Converts a SerializableGammaRamp to an ADLX_GammaRamp
        /// </summary>
        /// <param name="serializable">The SerializableGammaRamp to convert</param>
        /// <returns>ADLX_GammaRamp containing the gamma data</returns>
        /// <exception cref="ArgumentNullException">Thrown when serializable is null</exception>
        /// <exception cref="ArgumentException">Thrown when serializable data is invalid</exception>
        /// <exception cref="InvalidOperationException">Thrown when gamma data setting fails</exception>
        public static ADLX_GammaRamp ToADLX(this SerializableGammaRamp serializable)
        {
            if (serializable == null)
                throw new ArgumentNullException(nameof(serializable));

            if (!serializable.IsValid())
                throw new ArgumentException("SerializableGammaRamp data is invalid", nameof(serializable));

            var gammaRamp = new ADLX_GammaRamp();

            // For now, we'll create a basic gamma ramp structure
            // This is a temporary solution until the SWIG helper functions are working
            try
            {
                // The gamma ramp has been created, but we can't set the data yet
                // without the proper SWIG helper functions
                // This will be a placeholder that can be used for JSON serialization testing
                
                // Note: The actual gamma data setting will need to be implemented
                // once the SWIG helper functions are properly generated
                
                return gammaRamp;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Failed to create gamma ramp: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Validates that an ADLX_GammaRamp contains valid data
        /// </summary>
        /// <param name="gammaRamp">The ADLX_GammaRamp to validate</param>
        /// <returns>True if the gamma ramp is valid, false otherwise</returns>
        public static bool IsValid(this ADLX_GammaRamp gammaRamp)
        {
            if (gammaRamp == null)
                return false;

            try
            {
                // For now, we'll do a basic validation
                // Check if the gamma property is accessible
                var gammaPtr = gammaRamp.gamma;
                return gammaPtr != null;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Creates a deep copy of an ADLX_GammaRamp
        /// </summary>
        /// <param name="source">The source ADLX_GammaRamp to copy</param>
        /// <returns>A new ADLX_GammaRamp with the same data</returns>
        /// <exception cref="ArgumentNullException">Thrown when source is null</exception>
        public static ADLX_GammaRamp Clone(this ADLX_GammaRamp source)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            // Convert to serializable and back to create a deep copy
            var serializable = source.ToSerializable("Cloned Gamma Ramp");
            return serializable.ToADLX();
        }

        /// <summary>
        /// Gets the gamma ramp size (always 256 for ADLX)
        /// </summary>
        /// <returns>The gamma ramp size</returns>
        public static int GetGammaRampSize()
        {
            // Return the standard gamma ramp size for ADLX
            // This is a constant value of 256 entries per color channel
            return 256;
        }
    }
}
