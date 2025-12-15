# ADLXWrapper

High-performance C# bindings for AMD ADLX using facade-first, vtable-based interop (generated with ClangSharp).

## Highlights

- Facade-first API surface (`ADLXSystemServices`, `AdlxDisplay`, `AdlxDesktop`, `AdlxPerformanceMonitor`, `AdlxGpuTuning`, `AdlxMultimedia`, `AdlxPowerTuning`, `Adlx3DSettings`).
- Profile capture/apply patterns with JSON-friendly DTOs; skip-safe applies when features are unsupported.
- Logging wrapper (`ADLXApi.EnableLog/DisableLog`) with file/debug-view/application sinks.
- Samples and tests auto-skip cleanly on non-AMD systems or when `amdadlx64.dll` is absent.

## Quick start

**Visual Studio 2026**
- Open `ADLXWrapper.sln`.
- Restore/build the solution; run tests from Test Explorer (tests skip on non-AMD or missing `amdadlx64.dll`).

**VS Code / dotnet CLI**
```powershell
# From repo root
./prepare_adlx.ps1                  # downloads ADLX SDK into ADLX/
./build_adlx.ps1                    # builds wrapper + tests + samples
# Optional: run samples only
dotnet build Samples/ADLXWrapper.Samples.sln
# Tests (skip on non-AMD or missing DLL)
dotnet test ADLXWrapper.Tests/ADLXWrapper.Tests.csproj
```

## Minimal usage (facade)

```csharp
using ADLXWrapper;

using var adlx = ADLXApi.Initialize();
var system = adlx.GetSystemServicesProfile();

var displays = system.EnumerateAllDisplays();
foreach (var display in displays)
{
	using (display)
	{
		var profile = display.GetProfile();
		display.ApplyProfile(profile, skip => System.Console.WriteLine($"skip: {skip}"));
	}
}
```

## Logging

```csharp
using var adlx = ADLXApi.Initialize();
adlx.EnableLog(new LogProfile
{
	Destination = ADLX_LOG_DESTINATION.DBGVIEW,
	Severity = ADLX_LOG_SEVERITY.LINFO,
});
```

## More

- See [ADLXWrapper/README.md](ADLXWrapper/README.md) for detailed facade docs, capability matrix, and event handling.
- When running on non-AMD hardware or without `amdadlx64.dll`, samples/tests will skip runtime actions gracefully.
- API reference: DocFX config lives in [APIDocs/docfx.json](APIDocs/docfx.json); generate with `docfx docfx.json --serve` from `APIDocs` (output in `APIDocs/_site`).