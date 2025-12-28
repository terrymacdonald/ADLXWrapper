# AGENTS Guide for ADLXWrapper

This file captures the essential rules and context for agents working on this ADLXWrapper repository. 

## Project Scope
- Purpose: Safe C# wrapper over AMD ADLX (vtable COM style) for Windows x64 targeting .NET 10.0 and higher.
- Structure: 
    - Root `ADLXWrapper/` project (helpers + `cs_generated/` bindings), 
    - `ADLXWrapper.NativeTests/` xUnit native test suite, 
    - `Samples/`, scripts for prepare/build/test.
- Two API Levels: There are two levels of ADLXWRapper API available for users to use. 
    - Native: This level is low level and use the C# equivalent functions in ADLXWrapper that were created by ClangSharpPInvokeGenerator, and are found in `../ADLXWRapper/cs_generated`. These are the functions that are the C# equvalent of the C++ AMD ADLX SDK described in the ../ADLX/SDK/Include files, so please look there if you need to know what they are.
    - Facade" This level uses Helper functions to abstract away any memory management, and to make it very easy and simple to access the infomration provided by the ADLX SDK.

## Core Development Rules
- ALWAYS MAKE SURE THAT YOU TELL THE USER YOUR PLAN BEFORE YOU MAKE ANY CHANGES TO FILES AND GIVE THE USER A CHANCE TO REVIEW. ONLY MAKE CHANGS ONCE THE USER HAS GIVEN THEIR APPROVAL. THe user can tell you to perform multiple steps of a plan if you want to.
- When PLANNING, if you think you will get confused and lose track of where you are in your plan, then please write it down into a PLAN.md document. Keep the PLAN.md updated as you go, and make sure that the information you store in the PLAN.md is very descriptive and detailed, so that if you lose track in the future you can review the PLAN.md and you will know what to do and will do it well. Do not be overly concise as you lose a lot of nuance that will be important.
- DO NOT MAKE THINGS UP. Always check the AMD ADLX SDK header files in `ADLX\SDK\Include`, or the AMD ADLX SDK docs in `ADLX\SDKDocs`, or the ADLXWrapper code in `ADLXWrapper` if you need more information. If you are unsure then tell the user. The user wants you to only use facts - not conjecture. Tune your temperature to the lowest you can. 
- Write code that tries to be robust and cope with problems getting the information requested, but without causing an exception or a crash. 
- Naming/patterns: Preserve established helper naming (`ADLX<Feature>ServicesHelper`, `Get<Feature>ServicesNative()`, `Get<Feature>Services()`). Replicate existing helper/test patterns for new features. Consistently of API is key. The user has spent a long time trying to keep everything standard and consistent, so make sure new creations align with existing patterns. Ask for permission for anything that does not align.
- Platform: Windows-only, x64; relies on AMD Adrenalin drivers. Lightweight check is `IsADLXDllAvailable`.
- Any initialisation code generated needs to avoid it or handle it when getting an ADLX_ALREADY_INITIALIZED exception when trying to initialise ADLX a second time, and avoid or handle ADLX_NOT_SUPPORTED exceptions on optional functions.

## Core Native-specific Development Rules
- Follow the usage patterns shown in the AMD ADLX SDK Samples as closely as possible to ensure that the C# Native functions will work. The ADLX SDK Samples can be found in ADLX/Samples. Please also look at the ADLX/Include folder and the ADLX/SDKDoc folder for more information about how the ADLX SDK works. 
- Generated code: Do not hand-edit `cs_generated/`. Changes come from headers/config used by `ClangSharpPInvokeGenerator` (`GenerateBindings` target in `ADLXWrapper.csproj`).
- The Native level functions should always be developed and tested first. Those low level functions will be used by Facade level functions, so its important that we make sure that they Native functinos work before moving up to the higher-level Facade functions.
- Initialization (Native): Use `using var adlx = ADLXApi.Initialize();` by default, or `ADLXApi.InitializeWithCallerAdl()` when integrating with an existing ADL session. Retrieve native system services via `adlx.GetSystemServices()` and wrap returned pointers in `ComPtr<T>` for lifetime safety.
- Disposal (Native): Dispose any system services and child COM pointers before disposing `adlx`. `ADLXApi.Dispose` calls `ADLXTerminate` and unloads the DLL; any call after disposal should throw `ObjectDisposedException`.


## Core Facade-Specific Development Rules
- Facade level objects should handle all underlying Native level memory management themselves. The user should not need to worry about it. This includes memory creation, disposal when objects are deleted, and handling functions being called multiple times in threads. Our aim is to never have memory leaks when using Facades.
- The Facade functions should (in general) return Helper objects that represent the relevant objects within the underlying ADLX SDK, for example ADLXDesktop. Each returned Facade object should have properties that store the information contained within the underlying Native objects e.g. NativeResolutionWidth, and Access to any underlying functions that are offered by the Native objects e.g. ADLXDisplayServicesHelper providing an EnumerateDisplays() function that returns a list of Displays currently known to Windows.
- The Facade functions can also offer some additional functions that provide an advanced user with access to the underlying Native objects, or Native pointers to the underlying objects if it makes sense to do so. 
- Initialization (Facade): Use `using var adlx = ADLXApiHelper.Initialize();` as the standard entry point. Retrieve system services via `using var system = adlx.GetSystemServices();` and work with facade helpers (pointer-free for consumers).
- Disposal (Facade): Dispose facade system services and returned facade objects before disposing `adlx`. Underlying `ComPtr` ownership is handled internally; do not manually release native pointers owned by helpers. `ADLXApiHelper` disposal should result in `ObjectDisposedException` on use-after-dispose.
- Vtable interop: Generated interfaces/enums in `ADLXWrapper/cs_generated/`. When calling vtable functions directly, use `Marshal.GetDelegateForFunctionPointer` with `StdCall`. Make sure that the vtable is up to date and aligns with the generated code created by ClangSharpPInvokeGenerator.

## Build and Scripts
- Prepare: `./prepare_adlx.ps1` (downloads/validates ADLX SDK headers into `ADLX/`).
- Build: `./build_adlx.ps1` (restores, cleans, builds solution; version from `VERSION` + git commit count). Direct build: `dotnet build ADLXWrapper/ADLXWrapper.csproj`.
- Release ZIP: `./create_adlx_release_zip.ps1` (produces artifacts/adlxwrapper-<version>-Release.zip).

## Testing Expectations
- Suite: xUnit in `ADLXWrapper.NativeTests` targeting `net10.0`; hardware-aware and read-only (no tuning changes).
- Run: `dotnet test ADLXWrapper.NativeTests/ADLXWrapper.NativeTests.csproj --verbosity normal` (or from tests folder), or `./test_adlx.ps1`. Filter with `--filter "FullyQualifiedName~..."`
- Native vs Facade tests:
  - Native (`*NativeTests.cs`): Use only ClangSharp-generated APIs in `ADLXWrapper/cs_generated`; never call facades as they will be tested in the Facade tests. THe Native tests should be able to run and pass successfully even if all the ADLXWrapper Facade functions were removed.
  - Facade (`*FacadeTests.cs`): Exercise helper/facade ergonomics built on native APIs.
- Test creation: Write Native tests first to validate low-level APIs, then Facade tests. If ADLX marks features optional or provides `IsSupported`, gate tests accordingly; skip only when unsupported. Fix underlying wrapper bugs rather than skipping failing coverage.
- Hardware skip: Tests that need AMD GPU/driver or ADLX DLL gracefully skip when missing.

## Usage Notes
- DLL loading: `ADLXApi` dynamically loads `amdadlx64.dll` via `LoadLibraryEx`; keep `ADLXNative.GetDllName()` and `ADLXApi.LoadADLXDll()` in sync if names/paths change; surface errors via `ADLXException`.
- Data shapes: Helpers expose serializable `Info` structs (e.g., `GpuInfo`, `DisplayInfo`) and support apply/restore flows.
- Samples: See `ADLXWrapper/README.md` and `Samples/` for usage patterns (enumeration, capability checks, event listeners).

## Versioning
- Version scheme: `VERSION` provides MAJOR.MINOR; PATCH computed from git commit count via `SetVersionFromGit` and `build_adlx.ps1`. Update `VERSION` when bumping MAJOR/MINOR.

## Expectations for Agents
- Keep APIs and helpers consistent with existing conventions; avoid breaking established patterns. Consistentcy is key across the whole codebase. Do not deviate from this consistentcy without first requesting permission from the user. 
- Respect disposal and pointer ownership rules; ensure safe lifetime handling.
- Prefer generated enums (e.g., `ADLX_VRAM_TYPE`) over custom ones to align with ADLX updates.
- Maintain optional-feature gating and hardware skip behavior in tests and helpers.
