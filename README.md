# ADLXCSharpBind

ADLX C# wrapper DLL and bindings for [ADLX](https://github.com/GPUOpen-LibrariesAndSDKs/ADLX).

🔗 **Official ADLX Documentation:** https://gpuopen.com/adlx/

## 📚 Documentation

This repository contains comprehensive documentation to help you get started:

- **[ADLX_CSHARP_WRAPPER_GUIDE.md](ADLX_CSHARP_WRAPPER_GUIDE.md)** - Complete C# wrapper guide with build instructions, API usage, and troubleshooting
- **[ADLX_GAMMA_RAMP_JSON_GUIDE.md](ADLX_GAMMA_RAMP_JSON_GUIDE.md)** - Gamma ramp JSON serialization guide for .NET 8.0
- **[implementation_plan.md](implementation_plan.md)** - Development implementation plan and project structure

## 🚀 Quick Start

### Prerequisites
- Windows 10/11
- Visual Studio 2022 Community (or higher)
- AMD graphics drivers with ADLX support
- SWIG 4.3.1 (auto-installed via `install_swig.ps1`)

### Installation
1. **Install SWIG:** Run `install_swig.ps1` PowerShell script, or manually download [SWIG](https://www.swig.org/download.html) and extract to `swigwin/` folder
2. **Build the wrapper:** Run `rebuild_adlx.bat` to build the C++ wrapper DLL
3. **Test the wrapper:** Run `test_csharp.bat` to build and test the C# bindings

### Build Process
The project automatically handles the following when you build:
- Downloads the latest ADLX SDK from AMD
- Generates C# bindings using SWIG
- Builds `ADLXWrapper.dll` and associated `.cs` files
- Creates test applications for both .NET Framework 4.8 and .NET 8.0

## 🛠️ Build Scripts Reference

### Main Scripts
| Script | Purpose | Output |
|--------|---------|--------|
| `rebuild_adlx.bat` | Build C++ wrapper DLL | `ADLXWrapper.dll` |
| `test_csharp.bat` | Build & run C# test (.NET Framework 4.8) | `IADLXGPU2Test.exe` |

### Native C Test Scripts (`ADLXNativeTest/`)
| Script | Method | Best For |
|--------|--------|----------|
| `build_fixed.bat` | Dynamic compiler detection | **Most systems (recommended)** |
| `build_vscode.bat` | VSCode-optimized | VSCode development |
| `build_amd.bat` | AMD vcvars64.bat method | Standard AMD workflow |
| `build.bat` | Direct compiler path | Known VS2022 installations |
| `build_simple.bat` | Simplified test | Troubleshooting |

## 🎯 .NET Version Support

### .NET Framework 4.8
```batch
.\rebuild_adlx.bat
.\test_csharp.bat
```

### .NET 8.0
```batch
.\rebuild_adlx.bat
cd IADLXGPU2Test
dotnet build IADLXGPU2Test_Net8.csproj -c Debug -p:Platform=x64
dotnet run --project IADLXGPU2Test_Net8.csproj -c Debug
```

## 📁 Project Structure

```
ADLXWrapper/
├── 📄 README.md                           # This file
├── 📄 ADLX_CSHARP_WRAPPER_GUIDE.md       # Complete wrapper guide
├── 📄 ADLX_GAMMA_RAMP_JSON_GUIDE.md      # Gamma ramp JSON guide
├── 📄 implementation_plan.md              # Development plan
├── 🔧 rebuild_adlx.bat                    # Main build script
├── 🔧 test_csharp.bat                     # C# test script
├── 📁 ADLXWrapper/                        # C++ wrapper project
├── 📁 IADLXGPU2Test/                      # C# test projects
├── 📁 ADLXNativeTest/                     # Native C tests
├── 📁 ADLX/                               # AMD ADLX SDK
└── 📁 swigwin/                            # SWIG installation
```

## 🔧 Usage in Your Application

1. **Copy files** to your project:
   - `ADLXWrapper.dll` (or `ADLXCSharpBind.dll` for .NET Framework 4.8)
   - All generated `.cs` files from the build process

2. **Load the DLL** during application startup

3. **Import the bindings** and follow the [wrapper guide](ADLX_CSHARP_WRAPPER_GUIDE.md)

4. **Reference documentation:**
   - [AMD ADLX Documentation](https://gpuopen.com/manuals/adlx/adlx-page_guide_init_help/#to-initialize-adlx-in-a-c-application)
   - [AMD C# Samples](https://gpuopen.com/manuals/adlx/adlx-page_sample_cs/)

## ✨ Key Features

- **Full ADLX API Access** - Complete C# wrapper for all ADLX functionality
- **Dual .NET Support** - Works with both .NET Framework 4.8 and .NET 8.0
- **GPU Management** - Enumeration, monitoring, and tuning
- **Display Control** - Resolution, color management, FreeSync settings
- **Performance Monitoring** - Real-time GPU metrics and system monitoring
- **Advanced Features** - Gamma ramp JSON serialization (.NET 8.0)
- **Multiple Build Methods** - Various build scripts for different environments

## 🔍 Advanced Features

### Gamma Ramp JSON Serialization (.NET 8.0)
```csharp
// Serialize gamma ramp to JSON
var serializable = gammaRamp.ToSerializable("My Profile");
string json = NewtonsoftJsonUtility.ToJson(serializable);

// Load from JSON
var loaded = NewtonsoftJsonUtility.FromJson(jsonString);
var restored = loaded.ToADLX();
```

See [ADLX_GAMMA_RAMP_JSON_GUIDE.md](ADLX_GAMMA_RAMP_JSON_GUIDE.md) for complete details.

## 🐛 Troubleshooting

### Common Issues
- **Build Errors:** Try different build scripts in `ADLXNativeTest/` - use `build_fixed.bat` for most robust compilation
- **ADLX Runtime Not Available:** Ensure AMD graphics drivers are installed
- **Wrong DLL Referenced:** .NET Framework 4.8 uses `ADLXCSharpBind.dll`, .NET 8.0 uses `ADLXWrapper.dll`

### Getting Help
1. Check the [comprehensive wrapper guide](ADLX_CSHARP_WRAPPER_GUIDE.md)
2. Try different build methods from the scripts reference above
3. Review the [gamma ramp guide](ADLX_GAMMA_RAMP_JSON_GUIDE.md) for .NET 8.0 specific features

## 📋 Version Information

- **SWIG Version:** 4.3.1
- **ADLX SDK Version:** 1.4.0.110
- **Target Frameworks:** .NET Framework 4.8, .NET 8.0
- **Platform:** x64
- **Visual Studio:** 2022 Community (recommended)

## 📄 License

This wrapper follows the same licensing terms as the ADLX SDK. See the original ADLX documentation for details.

---

**🎯 Ready to get started?** Check out the [ADLX_CSHARP_WRAPPER_GUIDE.md](ADLX_CSHARP_WRAPPER_GUIDE.md) for detailed instructions and examples!
