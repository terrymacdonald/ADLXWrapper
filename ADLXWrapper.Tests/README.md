# ADLXWrapper.Tests

xUnit test suite for the ADLXWrapper library (targets `net10.0`).

## Contents
- Hand-written coverage for initialization, GPU enumeration, display services, performance monitoring, and tuning support: `BasicApiTests.cs`, `CoreApiTests.cs`, `DisplayServicesTests.cs`, `PerformanceMonitoringServicesTests.cs`, `GpuTuningServicesTests.cs`.
- Auto-generated struct/layout validation tests in `generated_tests/` to ensure interop types remain blittable and correctly sized.

## Running tests
```powershell
cd ..\ADLXWrapper.Tests
dotnet test --verbosity normal
# or from repo root
dotnet test ADLXWrapper.Tests/ADLXWrapper.Tests.csproj --verbosity normal
# script
..\test_adlx.ps1
```

Filter examples:
```powershell
dotnet test --filter "FullyQualifiedName~BasicApiTests"
dotnet test --filter "FullyQualifiedName~DisplayServicesTests"
```

## Hardware behavior
- Tests that require ADLX-backed hardware gracefully skip when an AMD GPU/driver is not present.
- No tests modify user settings; interactions are read-only capability/metrics queries.

## Versions
- Target framework: net10.0
- Test SDK: Microsoft.NET.Test.Sdk 18.0.1
- xUnit: 2.9.3 (+ xunit.runner.visualstudio 3.1.5)
- Skippable facts: Xunit.SkippableFact 1.5.23
