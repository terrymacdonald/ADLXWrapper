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

            try
            {
                // Get the gamma ramp size from SWIG helper function
                uint gammaSize = ADLX.GetGammaRampSize();
                
                // Create arrays to hold the extracted gamma data
                var redArray = ADLX.new_uint16Array(gammaSize);
                var greenArray = ADLX.new_uint16Array(gammaSize);
                var blueArray = ADLX.new_uint16Array(gammaSize);

                try
                {
                    // Extract gamma data using SWIG helper function
                    ADLX_RESULT result = ADLX.GetGammaRampData(gammaRamp, redArray, greenArray, blueArray, gammaSize);
                    
                    if (result != ADLX_RESULT.ADLX_OK)
                    {
                        throw new InvalidOperationException($"Failed to extract gamma ramp data. ADLX result: {result}");
                    }

                    // Copy the data from SWIG arrays to our serializable arrays
                    for (uint i = 0; i < gammaSize; i++)
                    {
                        serializable.Red[i] = ADLX.uint16Array_getitem(redArray, i);
                        serializable.Green[i] = ADLX.uint16Array_getitem(greenArray, i);
                        serializable.Blue[i] = ADLX.uint16Array_getitem(blueArray, i);
                    }
                }
                finally
                {
                    // Clean up SWIG arrays
                    ADLX.delete_uint16Array(redArray);
                    ADLX.delete_uint16Array(greenArray);
                    ADLX.delete_uint16Array(blueArray);
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

            try
            {
                // Get the gamma ramp size from SWIG helper function
                uint gammaSize = ADLX.GetGammaRampSize();
                
                // Create arrays to hold the gamma data for setting
                var redArray = ADLX.new_uint16Array(gammaSize);
                var greenArray = ADLX.new_uint16Array(gammaSize);
                var blueArray = ADLX.new_uint16Array(gammaSize);

                try
                {
                    // Copy data from serializable arrays to SWIG arrays
                    for (uint i = 0; i < gammaSize; i++)
                    {
                        ADLX.uint16Array_setitem(redArray, i, serializable.Red[i]);
                        ADLX.uint16Array_setitem(greenArray, i, serializable.Green[i]);
                        ADLX.uint16Array_setitem(blueArray, i, serializable.Blue[i]);
                    }

                    // Set gamma data using SWIG helper function
                    ADLX_RESULT result = ADLX.SetGammaRampData(gammaRamp, redArray, greenArray, blueArray, gammaSize);
                    
                    if (result != ADLX_RESULT.ADLX_OK)
                    {
                        throw new InvalidOperationException($"Failed to set gamma ramp data. ADLX result: {result}");
                    }
                }
                finally
                {
                    // Clean up SWIG arrays
                    ADLX.delete_uint16Array(redArray);
                    ADLX.delete_uint16Array(greenArray);
                    ADLX.delete_uint16Array(blueArray);
                }
                
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
