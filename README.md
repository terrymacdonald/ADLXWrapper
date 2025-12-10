# ADLXWrapper

A modern, high-performance C# wrapper for the AMD Display Library Extensions (ADLX) SDK, generated using ClangSharp.

This wrapper provides a safe, idiomatic, and vtable-based interop layer, allowing .NET applications to interact directly and efficiently with AMD GPU features.

## Features

- **Full API Coverage**: Wraps the core ADLX services for System, GPU, Display, Desktop, 3D Settings, Performance Monitoring, Power Tuning, and Multimedia.
- **Helper-Based Architecture**: Simplifies complex ADLX operations into easy-to-use static helper methods (e.g., `ADLXGpuHelpers.EnumerateAllGpus`).
- **Serializable Data Objects**: All hardware states are read into serializable `Info` structs, perfect for saving configurations to JSON.
- **Configuration Management**: Includes `Apply` methods to restore hardware states from deserialized `Info` objects.
- **Real-time Event Handling**: Provides listeners for display, desktop, and GPU tuning changes.

## Project Structure

```
ADLXWrapper/
├── ADLX/                     # ADLX SDK headers (downloaded by script)
├── ADLXWrapper/              # The main C# wrapper project
│   ├── Helpers/              # Helper classes for each ADLX service
│   ├── ADLXApi.cs            # Core ADLX initialization and lifetime management
│   └── README.md             # Detailed API documentation and examples
├── ADLXWrapper.Tests/        # xUnit test suite
├── Samples/                  # Sample console applications
└── scripts/                  # Build, test, and preparation scripts
```

## Getting Started

### 1. Prepare the Environment

First, run the preparation script. This will download the required ADLX SDK headers into the `ADLX/` directory.

```powershell
.\prepare_adlx.ps1
```

### 2. Build the Solution

Once the SDK is in place, you can build the entire solution, including the wrapper, tests, and samples.

```powershell
.\build_adlx.ps1
```

### 3. Run Tests

To verify the build and check hardware compatibility, run the test script.

```powershell
.\test_adlx.ps1
```

## Detailed Usage and Examples

For detailed API documentation, code examples, and a list of all available helpers, please see the **ADLXWrapper Project README**.