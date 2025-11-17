#
# ADLXWrapper Build Script (PowerShell)
# Automatically detects Visual Studio or uses dotnet CLI to build the native C++ wrapper
# and then runs unit tests
#

# Get the directory where this script is located
$scriptRoot = $PSScriptRoot
if (-not $scriptRoot) {
    $scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
}

# Change to script directory
Set-Location $scriptRoot
Write-Host "Working directory: $scriptRoot" -ForegroundColor Cyan
Write-Host ""

Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Rebuilding ADLX C# Wrapper..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

# ============================================================================
# Find MSBuild using vswhere.exe (works with any VS edition/version)
# ============================================================================
$msbuildPath = $null
$vswherePath = "${env:ProgramFiles(x86)}\Microsoft Visual Studio\Installer\vswhere.exe"

if (-not (Test-Path $vswherePath)) {
    $vswherePath = "${env:ProgramFiles}\Microsoft Visual Studio\Installer\vswhere.exe"
}

if (Test-Path $vswherePath) {
    Write-Host "Detecting Visual Studio installation using vswhere.exe..." -ForegroundColor Yellow
    
    try {
        # Find latest Visual Studio installation with MSBuild
        $msbuildPath = & $vswherePath -latest -requires Microsoft.Component.MSBuild -find MSBuild\**\Bin\MSBuild.exe | Select-Object -First 1
        
        if ($msbuildPath -and (Test-Path $msbuildPath)) {
            Write-Host "Found MSBuild: $msbuildPath" -ForegroundColor Green
            Write-Host ""
        } else {
            Write-Host "vswhere.exe found but could not locate MSBuild." -ForegroundColor Yellow
            $msbuildPath = $null
        }
    } catch {
        Write-Host "Error running vswhere.exe: $_" -ForegroundColor Yellow
        $msbuildPath = $null
    }
} else {
    Write-Host "vswhere.exe not found - skipping Visual Studio detection." -ForegroundColor Yellow
}

# ============================================================================
# Fallback: Try dotnet msbuild if Visual Studio MSBuild not found
# ============================================================================
$useDotnetMsbuild = $false

if (-not $msbuildPath) {
    Write-Host ""
    Write-Host "Visual Studio MSBuild not found, trying dotnet msbuild..." -ForegroundColor Yellow
    
    try {
        $dotnetVersion = & dotnet --version 2>&1
        if ($LASTEXITCODE -eq 0) {
            Write-Host "Found dotnet CLI (version $dotnetVersion), will use: dotnet msbuild" -ForegroundColor Green
            $useDotnetMsbuild = $true
            Write-Host ""
        } else {
            throw "dotnet command failed"
        }
    } catch {
        Write-Host ""
        Write-Host "ERROR: Neither Visual Studio MSBuild nor dotnet CLI found!" -ForegroundColor Red
        Write-Host ""
        Write-Host "Please install one of the following:" -ForegroundColor Yellow
        Write-Host "  - Visual Studio 2019/2022 (any edition) with C++ workload" -ForegroundColor Yellow
        Write-Host "  - .NET SDK 6.0 or later" -ForegroundColor Yellow
        Write-Host ""
        Write-Host "Download Visual Studio: https://visualstudio.microsoft.com/downloads/" -ForegroundColor Cyan
        Write-Host "Download .NET SDK: https://dotnet.microsoft.com/download" -ForegroundColor Cyan
        Write-Host ""
        Read-Host "Press Enter to exit"
        exit 1
    }
}

# ============================================================================
# Build the native C++ project
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Building native DLL..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

# Build the solution file instead of the project file to ensure $(SolutionDir) resolves correctly
$solutionPath = Join-Path $scriptRoot "ADLXWrapper.sln"
$projectPath = Join-Path $scriptRoot "ADLXWrapper\ADLXWrapper.vcxproj"

# Check if solution file exists, otherwise fall back to project file
if (Test-Path $solutionPath) {
    $buildTarget = $solutionPath
    Write-Host "Building solution: $solutionPath" -ForegroundColor Cyan
} else {
    $buildTarget = $projectPath
    Write-Host "Solution file not found, building project directly: $projectPath" -ForegroundColor Yellow
    Write-Host "Note: $(SolutionDir) may not resolve correctly when building project directly." -ForegroundColor Yellow
}

if (-not (Test-Path $buildTarget)) {
    Write-Host "ERROR: Build target not found: $buildTarget" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

try {
    if ($useDotnetMsbuild) {
        & dotnet msbuild $buildTarget /p:Configuration=Debug /p:Platform=x64 /t:Rebuild /v:minimal
    } else {
        & $msbuildPath $buildTarget /p:Configuration=Debug /p:Platform=x64 /t:Rebuild /v:minimal
    }
    
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed with exit code $LASTEXITCODE"
    }
} catch {
    Write-Host ""
    Write-Host "ERROR: Native C++ build failed!" -ForegroundColor Red
    Write-Host ""
    Write-Host "Troubleshooting:" -ForegroundColor Yellow
    Write-Host "  - Ensure C++ build tools are installed" -ForegroundColor Yellow
    Write-Host "  - Check that ADLX SDK is present in .\ADLX\" -ForegroundColor Yellow
    Write-Host "  - Verify SWIG is present in .\swigwin\" -ForegroundColor Yellow
    Write-Host "  - Run prepare_adlx.ps1 first if you haven't already" -ForegroundColor Yellow
    Write-Host "  - Error: $_" -ForegroundColor Yellow
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "Native C++ build completed successfully!" -ForegroundColor Green
Write-Host ""

# ============================================================================
# Ensure target directories exist
# ============================================================================
$testBindingsDir = Join-Path $scriptRoot "ADLXWrapper.Tests\cs_bindings"

if (-not (Test-Path $testBindingsDir)) {
    New-Item -ItemType Directory -Path $testBindingsDir -Force | Out-Null
}

# ============================================================================
# Copy generated C# binding files
# ============================================================================
Write-Host "Copying C# binding files..." -ForegroundColor Cyan

$sourceBindings = Join-Path $scriptRoot "ADLXWrapper\cs_bindings\*.cs"
$targetBindings = Join-Path $scriptRoot "ADLXWrapper.Tests\cs_bindings\"

try {
    Copy-Item -Path $sourceBindings -Destination $targetBindings -Force -ErrorAction Stop
    Write-Host "C# binding files copied successfully." -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host "ERROR: Failed to copy C# binding files!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Copy compiled ADLXWrapper.dll
# ============================================================================
Write-Host "Copying ADLXWrapper.dll..." -ForegroundColor Cyan

# When building via solution, output goes to solution-level x64 folder
$sourceDll = Join-Path $scriptRoot "x64\Debug\ADLXWrapper.dll"
$targetDll = Join-Path $scriptRoot "ADLXWrapper.Tests\ADLXWrapper.dll"

# Fallback: Check project-level output folder if solution-level doesn't exist
if (-not (Test-Path $sourceDll)) {
    $sourceDll = Join-Path $scriptRoot "ADLXWrapper\ADLXWrapper.dll"
}

if (-not (Test-Path $sourceDll)) {
    Write-Host "ERROR: ADLXWrapper.dll not found!" -ForegroundColor Red
    Write-Host "Searched locations:" -ForegroundColor Yellow
    Write-Host "  - $(Join-Path $scriptRoot 'x64\Debug\ADLXWrapper.dll')" -ForegroundColor Yellow
    Write-Host "  - $(Join-Path $scriptRoot 'ADLXWrapper\x64\Debug\ADLXWrapper.dll')" -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "Found DLL at: $sourceDll" -ForegroundColor Green

try {
    Copy-Item -Path $sourceDll -Destination $targetDll -Force -ErrorAction Stop
    Write-Host "ADLXWrapper.dll copied successfully." -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host "ERROR: Failed to copy ADLXWrapper.dll to Tests project!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Build and run unit tests
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Building and running unit tests..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

$testProjectPath = Join-Path $scriptRoot "ADLXWrapper.Tests\ADLXWrapper.Tests.csproj"

if (-not (Test-Path $testProjectPath)) {
    Write-Host "ERROR: Test project not found: $testProjectPath" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

try {
    & dotnet test $testProjectPath -c Debug -f net9.0
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host ""
        Write-Host "WARNING: Unit tests failed!" -ForegroundColor Yellow
        Write-Host "This may be expected if you don't have compatible AMD hardware." -ForegroundColor Yellow
        Write-Host ""
        Read-Host "Press Enter to exit"
        exit 1
    }
} catch {
    Write-Host ""
    Write-Host "ERROR: Failed to run unit tests!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host ""
Write-Host "============================================================================" -ForegroundColor Green
Write-Host "All builds and tests completed successfully!" -ForegroundColor Green
Write-Host "============================================================================" -ForegroundColor Green
Write-Host ""
Write-Host "Press Enter to exit..."
Read-Host
