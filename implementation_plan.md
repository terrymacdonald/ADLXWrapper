# Implementation Plan

## [Overview]
Integrate SWIG installation functionality from install_swig.ps1 into prepare_adlx.ps1 to create a unified pre-build script that handles both ADLX SDK and SWIG dependencies automatically.

The current build process requires users to manually run install_swig.ps1 before building the project, which can lead to build failures if SWIG is not installed. By integrating SWIG installation into the existing prepare_adlx.ps1 script that is already called during the pre-build process, we can ensure all dependencies are automatically available. This integration will maintain the existing robust error handling and validation patterns already established in prepare_adlx.ps1 while adding SWIG detection and installation capabilities.

## [Types]
No new type definitions required for this PowerShell script integration.

All functionality will be implemented using existing PowerShell built-in types (strings, booleans, arrays) and standard cmdlets. The integration will follow the same patterns as the existing ADLX validation functions.

## [Files]
Modify existing PowerShell script to include SWIG installation functionality.

**Files to be modified:**
- `ADLXWrapper/prepare_adlx.ps1` - Add SWIG detection, validation, and installation functions integrated with existing ADLX preparation workflow
- `README.md` - Update installation instructions to reflect that SWIG is now automatically handled
- `ADLX_CSHARP_WRAPPER_GUIDE.md` - Update build process documentation to remove manual SWIG installation step

**Files that can be removed after integration:**
- `install_swig.ps1` - Functionality will be integrated into prepare_adlx.ps1

**Files referenced but not modified:**
- `ADLXWrapper/ADLXWrapper.vcxproj` - Already calls prepare_adlx.ps1 in pre-build event
- All batch files - Will continue to work without changes

## [Functions]
Add SWIG-related functions to the existing prepare_adlx.ps1 script structure.

**New functions to be added:**
- `Test-SwigInstallation` - Check if SWIG is installed and validate version (similar to Test-ADLXSDKCompleteness pattern)
- `Install-SwigWindows` - Download and install SWIG for Windows (adapted from install_swig.ps1 logic)
- `Test-SwigExecutable` - Validate that swig.exe is functional and accessible

**Modified functions:**
- Main script flow - Add SWIG validation and installation before ADLX processing
- Error handling sections - Include SWIG-related error scenarios

**Function integration pattern:**
- Follow existing validation-first approach used for ADLX SDK
- Maintain consistent error handling and cleanup patterns
- Use same progress reporting and colored output style

## [Classes]
No class modifications required for this PowerShell script integration.

All functionality will be implemented using PowerShell functions and existing cmdlet patterns consistent with the current prepare_adlx.ps1 structure.

## [Dependencies]
No new external dependencies required for this integration.

The integration will use existing PowerShell capabilities already utilized in prepare_adlx.ps1: Invoke-WebRequest, Expand-Archive, Test-Path, and standard file system operations. SWIG download URL and version information will be embedded in the script following the same pattern as the ADLX SDK URL.

## [Testing]
Validate integration through build process testing and manual verification scenarios.

**Test scenarios:**
- Clean environment (no SWIG, no ADLX) - should download and install both
- SWIG present, ADLX missing - should skip SWIG, install ADLX
- Both present and valid - should skip both installations
- Corrupted SWIG installation - should detect and reinstall
- Network connectivity issues - should provide clear error messages
- Build process integration - verify pre-build event continues to work correctly

**Validation approach:**
- Test prepare_adlx.ps1 execution in isolation
- Test full build process via rebuild_adlx.bat
- Verify SWIG functionality by checking generated C# bindings

## [Implementation Order]
Sequential integration maintaining backward compatibility and minimizing disruption.

**Step 1:** Create SWIG validation and installation functions in prepare_adlx.ps1
- Add Test-SwigInstallation function with version checking
- Add Install-SwigWindows function adapted from install_swig.ps1
- Add Test-SwigExecutable function for post-installation validation

**Step 2:** Integrate SWIG processing into main script flow
- Add SWIG check and installation before ADLX processing
- Maintain existing error handling patterns and exit codes
- Ensure consistent progress reporting and user feedback

**Step 3:** Update documentation to reflect integrated workflow
- Update README.md installation instructions
- Update ADLX_CSHARP_WRAPPER_GUIDE.md build process documentation
- Remove references to manual install_swig.ps1 execution

**Step 4:** Test and validate integration
- Test all scenarios (clean install, partial install, full install)
- Verify build process continues to work correctly
- Validate error handling and recovery scenarios

**Step 5:** Clean up obsolete files
- Remove install_swig.ps1 after confirming integration works correctly
- Update any remaining documentation references
