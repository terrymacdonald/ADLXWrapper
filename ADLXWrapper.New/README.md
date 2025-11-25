# ADLXWrapper - ClangSharp-based C# Wrapper

This is the new ClangSharp-based C# wrapper for the AMD ADLX SDK, replacing the previous SWIG-based implementation.

## Project Structure

```
ADLXWrapper.New/
??? ADLXWrapper.csproj          # .NET 9 project file
??? ClangSharpConfig.rsp        # ClangSharp generator configuration
??? ADLXNative.cs               # Manual P/Invoke declarations for DLL entry points
??? Generated/                  # ClangSharp auto-generated bindings (DO NOT EDIT)
?   ??? README.cs              # Placeholder
??? (Coming in Stage 2)
    ??? ADLXApi.cs             # Main wrapper API (IDisposable)
    ??? ADLXExtensions.cs      # Helper methods and VTable accessors
```

## Build Status

? **Stage 1 Complete:** Project Setup and ClangSharp Configuration
- Created .NET 9 C# project
- Added ClangSharp NuGet packages (v18.1.0 / v20.1.2)
- Created ClangSharpConfig.rsp for ADLX header processing
- Added manual P/Invoke declarations for dynamic DLL loading
- Project builds successfully

## How to Build

```powershell
cd ADLXWrapper.New
dotnet restore
dotnet build
```

## ClangSharp Code Generation

To generate P/Invoke bindings from ADLX headers:

```powershell
cd ADLXWrapper.New
ClangSharpPInvokeGenerator @ClangSharpConfig.rsp
```

Note: ClangSharp generation will be set up in later stages once the configuration is finalized.

## Architecture

This wrapper uses a layered approach:

1. **Native Layer (ADLXNative.cs):** Manual P/Invoke for DLL loading
2. **Generated Layer (Generated/):** ClangSharp auto-generated types and structures
3. **Wrapper Layer (Coming in Stage 2):** Managed API with IntPtr handles
4. **Helper Layer (Coming in Stage 3):** Convenience methods for common operations

## Dependencies

- .NET 9.0
- ClangSharp 18.1.0
- ClangSharp.Interop 20.1.2
- AMD ADLX SDK (in ../ADLX/SDK/)

## Next Steps

- **Stage 2:** Create core wrapper layer (ADLXApi.cs)
- **Stage 3:** Create helper extensions (ADLXExtensions.cs)
- **Stage 4:** Basic tests and validation
- And more...

## References

- Planning docs: `../.cline/`
- ADLX SDK: `../ADLX/SDK/`
- C samples: `../ADLX/Samples/C/`
- IGCLWrapper reference: `C:\vs-code\IGCLWrapper\`
