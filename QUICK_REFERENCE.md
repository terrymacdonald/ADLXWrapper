# Quick Reference: Path Changes Made

## Summary
All hardcoded paths have been replaced with relative, portable paths that work from any location.

---

## Before ? After

### rebuild_adlx.bat
```batch
# BEFORE (Hardcoded)
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat"
msbuild ADLXWrapper\ADLXWrapper.vcxproj ...

# AFTER (Auto-detected)
# Uses vswhere.exe to find any VS installation
# Falls back to "dotnet msbuild" if VS not found
# Uses %~dp0 for script-relative paths
```

### test_adlx.bat
```batch
# BEFORE
cd IADLXGPU2Test
dotnet test ...

# AFTER
set "SCRIPT_DIR=%~dp0"
cd /d "%SCRIPT_DIR%"
dotnet test ADLXWrapper.Tests\ADLXWrapper.Tests.csproj ...
```

### prepare_adlx.ps1
```powershell
# BEFORE (Hardcoded)
$swigZipFilePath = ".\swigwin.zip"
$destinationFolder = ".\ADLX"

# AFTER (Portable)
$scriptRoot = $PSScriptRoot
$swigZipFilePath = Join-Path $scriptRoot "swigwin.zip"
$destinationFolder = Join-Path $scriptRoot "ADLX"
```

### ADLXWrapper.vcxproj (Pending)
```xml
<!-- BEFORE (Hardcoded) -->
<Command>..\swigwin\swig.exe -c++ -csharp ... .\ADLXWrapper.i</Command>
<ClInclude Include="..\ADLX\SDK\ADLXHelper\Windows\Cpp\ADLXHelper.h" />

<!-- AFTER (MSBuild Variables) -->
<PropertyGroup Label="UserMacros">
  <SwigExe>$(SolutionDir)swigwin\swig.exe</SwigExe>
  <SwigOutputDir>$(ProjectDir)cs_bindings</SwigOutputDir>
</PropertyGroup>

<Command>"$(SwigExe)" -c++ -csharp ... "$(ProjectDir)ADLXWrapper.i"</Command>
<ClInclude Include="$(SolutionDir)ADLX\SDK\ADLXHelper\Windows\Cpp\ADLXHelper.h" />
```

---

## Key Patterns Used

### Batch Scripts
- **`%~dp0`** - Gets directory where .bat file is located
- **`cd /d "%SCRIPT_DIR%"`** - Changes to script directory

### PowerShell Scripts
- **`$PSScriptRoot`** - Gets directory where .ps1 file is located
- **`Join-Path $scriptRoot "subfolder"`** - Creates portable paths
- **`Set-Location $scriptRoot`** - Changes to script directory

### MSBuild/Visual Studio
- **`$(SolutionDir)`** - Solution root directory
- **`$(ProjectDir)`** - Project directory
- **`$(MSBuildThisFileDirectory)`** - Directory of current .targets file

---

## Files Status

| File | Status | Action Required |
|------|--------|----------------|
| `rebuild_adlx.bat` | ? Complete | None - ready to use |
| `test_adlx.bat` | ? Complete | None - ready to use |
| `prepare_adlx.ps1` | ? Complete | None - ready to use |
| `ADLXWrapper.vcxproj` | ?? Pending | Close VS, apply changes from VCXPROJ_UPDATES.md |
| `UpdateVersion.ps1` | ? Already Good | None - no changes needed |
| `ApplyVersion.targets` | ? Already Good | None - no changes needed |

---

## To Complete Setup

1. Close Visual Studio
2. Follow instructions in `VCXPROJ_UPDATES.md`
3. Reopen Visual Studio
4. Build and test

---

## Testing Commands

```cmd
# Test from any location
cd C:\Temp
C:\vs-code\ADLXWrapper\rebuild_adlx.bat

# Test with fresh clone
git clone https://github.com/terrymacdonald/ADLXWrapper C:\test
cd C:\test
powershell -ExecutionPolicy Bypass -File prepare_adlx.ps1
rebuild_adlx.bat
test_adlx.bat
```

---

## Benefits Achieved

? **Portable** - Works anywhere
? **Flexible** - Works with any VS edition
? **Automatic** - No manual configuration
? **CI/CD Ready** - Works in automation
? **Developer Friendly** - Clone and build

---

**Ready to use after vcxproj update!** ??
