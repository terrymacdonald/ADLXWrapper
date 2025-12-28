# ADLXWrapper.FacadeTests

xUnit facade test suite for the ADLXWrapper helper layer (targets `net10.0`).

## Goals and scope
- Validate the ergonomic helper/facade API surface (e.g., `ADLXApiHelper`, `ADLXSystemServicesHelper`, display/GPU helper types).
- Ensure optional ADLX capabilities are gated with support checks; unsupported features should skip with a clear reason (`ADLX_NOT_SUPPORTED`), not fail.
- Keep tests read-only: enumerate/query and validate shapes, do not change system settings.
- Run Native tests first, then Facade tests. Facade tests assume the underlying native bindings are already validated.

## Patterns
- Initialization: use `ADLXApiHelper.Initialize()` and dispose helpers in the reverse order created. Handle `ADLX_ALREADY_INITIALIZED` gracefully if encountered when running suites back-to-back.
- Services: acquire system services via `GetSystemServices()` and helper entry points (e.g., `EnumerateDisplays`, `EnumerateADLXGPUs`).
- Assertions: verify helper DTOs (names, IDs, dimensions, VRAM, etc.) are populated; skip when hardware/driver does not support a capability.
- Parallelization is disabled at the assembly level to avoid clashing ADLX sessions.

## Running tests
```powershell
cd ..\ADLXWrapper.FacadeTests
dotnet test --verbosity normal
# or from repo root (after running native tests)
dotnet test ADLXWrapper.FacadeTests/ADLXWrapper.FacadeTests.csproj --verbosity normal
```

## Hardware behavior
- Tests skip when ADLX DLLs are missing or an AMD GPU is not detected.
- Optional features should be gated via the helper surface; catches for `ADLX_NOT_SUPPORTED` should convert to skips where appropriate.

## Versions
- Target framework: net10.0
- Test SDK: Microsoft.NET.Test.Sdk 18.0.1
- xUnit: 2.9.3 (+ xunit.runner.visualstudio 3.1.5)
- Skippable facts: Xunit.SkippableFact 1.5.23
