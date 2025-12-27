# ADLXWrapper.Tests

xUnit test suite for the ADLXWrapper library (targets `net10.0`).

## Contents
- Tests are divided into 'Feature' areas that specialise in testing the groups of functions. The feature areas are are aligned with the files in ADLXWrapper, e.g:
    - Api Tests which test the initialisation and teardown of the ADLX SDK access objects, and any features within the ADLXApiHelper
    - System Tests which test the ADLXSystemServicesHelper functions and teh ADLXGPU functions
    - Desktop Tests which test the ADLXDesktopServicesHelper and the ADLXDesktop functions 
    - Display Tests which test the ADLXDisplayServicesHelper and the ADLXDisplay functions
    - 3DSettings Tests which test the ADLXDesktopServicesHelper functions
    - GPU Tuning Tests which test the ADLXDesktopServicesHelper functions
    - Multimedia Tests which test the ADLXDesktopServicesHelper functions
    - Performance Monitoring Tests which test the ADLXDesktopServicesHelper functions
    - Power Tuning Tests which test the ADLXDesktopServicesHelper functions
    - Other Tests wjhich test any other tests that are needed but not included above
- There are auto-generated struct/layout validation tests created automatically by ClangSharpPInvokeGenerator in `generated_tests/` to ensure interop types remain blittable and correctly sized. 
- There are two types of tests that we have:
    - 'Native Tests' use the C# equivalent functions in ADLXWrapper that were created by ClangSharpPInvokeGenerator, and are found in `../ADLXWRapper/cs_generated`. These are the functions that are the C# equvalent of the C++ AMD ADLX SDK described in the ../ADLX/SDK/Include files, so please look there if you need to know what they are.
    - 'Facade Tests' use the Helper functions that we have created in ADLXWrapper to make it easier to use the ADLX SDK.
- The Native Tests should never ever use Facade functions in order to make the tests. The idea is that the Native Tests should run successfully even if we deleted the Facade objects within ADLXWrapper.  Native Tests should be stored within `*NativeTests.cs` files. 
- The Facade Tests should use the Native functions to build an SDK that is nicer for the users to use. The users should not need to worry about maintaining pointers to objects when using the Facade functions (that should be handled within the Facade objects). The Facade objects should take care of memory management including disposal, and should be designed to be robust.  Facade Tests should be stored within `*FacadeTests.cs` files.
- Some file naming examples to help:
    -  ADLXApiHelper.cs in ADLXWrapper is tested by ADLXApiHelperNativeTests.cs (for Native Tests) and ADLXApiHelperFacadeTests.cs (for the Facade Tests) in ADLXWrapper.Tests.
    -  ADLDesktopServicesHelper.cs in ADLXWrapper is tested by ADLXDesktopServicesHelperNativeTests.cs and ADLXDesktopServicesHelperFacadeTests.cs in ADLXWrapper.Tests.

## Creating Tests
- The point of this tests suite is to find errors in the ADLXWrapper codebase. Do not be surprised if there is an error found. It may not be a hardware problem, but may be a problem with the ADLXWrapper. Do not skip fixing the underlying ADLXWrapper errors if you find them. It is important to find and fix ADLXWrapper bugs and errors.
- Native Tests should be created first. We want to test at the lowest level first to make sure that the Native functions work, and then tests the higher level Facade functions once we know the underlying Native functions are working correctly. The aim is to build a foundation of working tests that build on the success of the lower level tests, so that we know that all parts of the ADLXWrapper codebase are working as intended.
- Please note that some features are not available on older hardware and driver versions, meaning that some tests will need to be skipped if the hardware does not support them. Make sure that tests are only skipped if the AMD ADLX SDK says that they are optional or if they provide an 'IsSupported' function. THe IsSupported function is indicative that the function is optional and must be tested before being used. Please add a gated check to functions that are optional so that they are only testsed if supported.

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
