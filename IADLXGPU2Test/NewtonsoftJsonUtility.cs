//
// Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.
//
//-------------------------------------------------------------------------------------------------
// NewtonsoftJsonUtility - JSON serialization utility using Newtonsoft.Json for .NET 8.0
//
// This utility provides JSON serialization and deserialization for SerializableGammaRamp
// objects using the popular Newtonsoft.Json library in .NET 8.0.
//-------------------------------------------------------------------------------------------------

using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ADLXCSharpBind;

/// <summary>
/// Modern JSON utility for SerializableGammaRamp using Newtonsoft.Json
/// </summary>
public static class NewtonsoftJsonUtility
{
    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        Formatting = Formatting.Indented,
        NullValueHandling = NullValueHandling.Ignore,
        DateFormatHandling = DateFormatHandling.IsoDateFormat,
        Converters = { new StringEnumConverter() }
    };

    /// <summary>
    /// Serializes a SerializableGammaRamp to JSON string
    /// </summary>
    /// <param name="gammaRamp">The gamma ramp to serialize</param>
    /// <returns>JSON string representation</returns>
    /// <exception cref="ArgumentNullException">Thrown when gammaRamp is null</exception>
    /// <exception cref="JsonException">Thrown when serialization fails</exception>
    public static string ToJson(SerializableGammaRamp gammaRamp)
    {
        if (gammaRamp == null)
            throw new ArgumentNullException(nameof(gammaRamp));

        try
        {
            return JsonConvert.SerializeObject(gammaRamp, _settings);
        }
        catch (Exception ex)
        {
            throw new JsonException($"Failed to serialize SerializableGammaRamp to JSON: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deserializes a JSON string to SerializableGammaRamp
    /// </summary>
    /// <param name="json">The JSON string to deserialize</param>
    /// <returns>Deserialized SerializableGammaRamp object</returns>
    /// <exception cref="ArgumentException">Thrown when json is null or empty</exception>
    /// <exception cref="JsonException">Thrown when deserialization fails</exception>
    public static SerializableGammaRamp FromJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new ArgumentException("JSON string cannot be null or empty", nameof(json));

        try
        {
            var result = JsonConvert.DeserializeObject<SerializableGammaRamp>(json, _settings);
            if (result == null)
                throw new JsonException("Deserialization resulted in null object");

            return result;
        }
        catch (JsonException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new JsonException($"Failed to deserialize JSON to SerializableGammaRamp: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Saves a SerializableGammaRamp to a JSON file
    /// </summary>
    /// <param name="gammaRamp">The gamma ramp to save</param>
    /// <param name="filePath">The file path to save to</param>
    /// <exception cref="ArgumentNullException">Thrown when gammaRamp is null</exception>
    /// <exception cref="ArgumentException">Thrown when filePath is null or empty</exception>
    /// <exception cref="IOException">Thrown when file operations fail</exception>
    /// <exception cref="JsonException">Thrown when serialization fails</exception>
    public static void SaveToFile(SerializableGammaRamp gammaRamp, string filePath)
    {
        if (gammaRamp == null)
            throw new ArgumentNullException(nameof(gammaRamp));

        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

        try
        {
            string json = ToJson(gammaRamp);
            File.WriteAllText(filePath, json);
        }
        catch (JsonException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to save SerializableGammaRamp to file '{filePath}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Loads a SerializableGammaRamp from a JSON file
    /// </summary>
    /// <param name="filePath">The file path to load from</param>
    /// <returns>Loaded SerializableGammaRamp object</returns>
    /// <exception cref="ArgumentException">Thrown when filePath is null or empty</exception>
    /// <exception cref="FileNotFoundException">Thrown when file doesn't exist</exception>
    /// <exception cref="IOException">Thrown when file operations fail</exception>
    /// <exception cref="JsonException">Thrown when deserialization fails</exception>
    public static SerializableGammaRamp LoadFromFile(string filePath)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        try
        {
            string json = File.ReadAllText(filePath);
            return FromJson(json);
        }
        catch (JsonException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to load SerializableGammaRamp from file '{filePath}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Saves a SerializableGammaRamp to a JSON file asynchronously
    /// </summary>
    /// <param name="gammaRamp">The gamma ramp to save</param>
    /// <param name="filePath">The file path to save to</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <exception cref="ArgumentNullException">Thrown when gammaRamp is null</exception>
    /// <exception cref="ArgumentException">Thrown when filePath is null or empty</exception>
    /// <exception cref="IOException">Thrown when file operations fail</exception>
    /// <exception cref="JsonException">Thrown when serialization fails</exception>
    public static async Task SaveToFileAsync(SerializableGammaRamp gammaRamp, string filePath, CancellationToken cancellationToken = default)
    {
        if (gammaRamp == null)
            throw new ArgumentNullException(nameof(gammaRamp));

        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

        try
        {
            string json = ToJson(gammaRamp);
            await File.WriteAllTextAsync(filePath, json, cancellationToken);
        }
        catch (JsonException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to save SerializableGammaRamp to file '{filePath}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Loads a SerializableGammaRamp from a JSON file asynchronously
    /// </summary>
    /// <param name="filePath">The file path to load from</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>Loaded SerializableGammaRamp object</returns>
    /// <exception cref="ArgumentException">Thrown when filePath is null or empty</exception>
    /// <exception cref="FileNotFoundException">Thrown when file doesn't exist</exception>
    /// <exception cref="IOException">Thrown when file operations fail</exception>
    /// <exception cref="JsonException">Thrown when deserialization fails</exception>
    public static async Task<SerializableGammaRamp> LoadFromFileAsync(string filePath, CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(filePath))
            throw new ArgumentException("File path cannot be null or empty", nameof(filePath));

        if (!File.Exists(filePath))
            throw new FileNotFoundException($"File not found: {filePath}");

        try
        {
            string json = await File.ReadAllTextAsync(filePath, cancellationToken);
            return FromJson(json);
        }
        catch (JsonException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new IOException($"Failed to load SerializableGammaRamp from file '{filePath}': {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Validates that a JSON string can be deserialized to SerializableGammaRamp
    /// </summary>
    /// <param name="json">The JSON string to validate</param>
    /// <returns>True if valid, false otherwise</returns>
    public static bool IsValidJson(string json)
    {
        if (string.IsNullOrWhiteSpace(json))
            return false;

        try
        {
            var result = FromJson(json);
            return result.IsValid();
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets the JSON serializer settings used by this utility
    /// </summary>
    /// <returns>JsonSerializerSettings instance</returns>
    public static JsonSerializerSettings GetSerializerSettings()
    {
        return new JsonSerializerSettings
        {
            Formatting = _settings.Formatting,
            NullValueHandling = _settings.NullValueHandling,
            DateFormatHandling = _settings.DateFormatHandling,
            Converters = new List<JsonConverter>(_settings.Converters)
        };
    }

    /// <summary>
    /// Serializes a SerializableGammaRamp to JSON with custom settings
    /// </summary>
    /// <param name="gammaRamp">The gamma ramp to serialize</param>
    /// <param name="settings">Custom JSON serializer settings</param>
    /// <returns>JSON string representation</returns>
    /// <exception cref="ArgumentNullException">Thrown when gammaRamp is null</exception>
    /// <exception cref="JsonException">Thrown when serialization fails</exception>
    public static string ToJson(SerializableGammaRamp gammaRamp, JsonSerializerSettings settings)
    {
        if (gammaRamp == null)
            throw new ArgumentNullException(nameof(gammaRamp));

        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        try
        {
            return JsonConvert.SerializeObject(gammaRamp, settings);
        }
        catch (Exception ex)
        {
            throw new JsonException($"Failed to serialize SerializableGammaRamp to JSON: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Deserializes a JSON string to SerializableGammaRamp with custom settings
    /// </summary>
    /// <param name="json">The JSON string to deserialize</param>
    /// <param name="settings">Custom JSON serializer settings</param>
    /// <returns>Deserialized SerializableGammaRamp object</returns>
    /// <exception cref="ArgumentException">Thrown when json is null or empty</exception>
    /// <exception cref="ArgumentNullException">Thrown when settings is null</exception>
    /// <exception cref="JsonException">Thrown when deserialization fails</exception>
    public static SerializableGammaRamp FromJson(string json, JsonSerializerSettings settings)
    {
        if (string.IsNullOrWhiteSpace(json))
            throw new ArgumentException("JSON string cannot be null or empty", nameof(json));

        if (settings == null)
            throw new ArgumentNullException(nameof(settings));

        try
        {
            var result = JsonConvert.DeserializeObject<SerializableGammaRamp>(json, settings);
            if (result == null)
                throw new JsonException("Deserialization resulted in null object");

            return result;
        }
        catch (JsonException)
        {
            throw;
        }
        catch (Exception ex)
        {
            throw new JsonException($"Failed to deserialize JSON to SerializableGammaRamp: {ex.Message}", ex);
        }
    }

    /// <summary>
    /// Creates a compact JSON representation (no indentation)
    /// </summary>
    /// <param name="gammaRamp">The gamma ramp to serialize</param>
    /// <returns>Compact JSON string representation</returns>
    /// <exception cref="ArgumentNullException">Thrown when gammaRamp is null</exception>
    /// <exception cref="JsonException">Thrown when serialization fails</exception>
    public static string ToCompactJson(SerializableGammaRamp gammaRamp)
    {
        var compactSettings = GetSerializerSettings();
        compactSettings.Formatting = Formatting.None;
        return ToJson(gammaRamp, compactSettings);
    }
}
