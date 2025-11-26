#
# ADLXWrapper Build Script (PowerShell)
# Builds the ClangSharp-based C# wrapper for ADLX
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
Write-Host "ADLXWrapper Build Script (ClangSharp Implementation)" -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

# ============================================================================
# Read version from VERSION file and calculate build number from git commits
# ============================================================================
Write-Host "Determining version number..." -ForegroundColor Yellow

$versionFile = Join-Path $scriptRoot "VERSION"
if (-not (Test-Path $versionFile)) {
    Write-Host "ERROR: VERSION file not found: $versionFile" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

# Read MAJOR and MINOR from VERSION file
$versionContent = Get-Content $versionFile
$major = "1"
$minor = "0"

foreach ($line in $versionContent) {
    if ($line -match "^MAJOR=(\d+)") {
        $major = $matches[1]
    }
    elseif ($line -match "^MINOR=(\d+)") {
        $minor = $matches[1]
    }
}

# Get git commit count for PATCH/build number
$patch = "0"
try {
    # Check if git is available
    $gitPath = Get-Command git -ErrorAction SilentlyContinue
    if ($gitPath) {
        # Get the commit count
        $commitCount = & git rev-list --count HEAD 2>&1
        if ($LASTEXITCODE -eq 0 -and $commitCount -match "^\d+$") {
            $patch = $commitCount
        }
        else {
            Write-Host "Warning: Could not get git commit count, using 0" -ForegroundColor Yellow
        }
    }
    else {
        Write-Host "Warning: git not found in PATH, using PATCH=0" -ForegroundColor Yellow
    }
}
catch {
    Write-Host "Warning: Error reading git commit count: $_" -ForegroundColor Yellow
    Write-Host "Using PATCH=0" -ForegroundColor Yellow
}

$version = "$major.$minor.$patch"
Write-Host "Version: $version" -ForegroundColor Green
Write-Host "  MAJOR: $major (from VERSION file)" -ForegroundColor Gray
Write-Host "  MINOR: $minor (from VERSION file)" -ForegroundColor Gray
Write-Host "  PATCH: $patch (git commit count)" -ForegroundColor Gray
Write-Host ""

# ============================================================================
# Verify dotnet CLI is available
# ============================================================================
Write-Host "Checking for .NET CLI..." -ForegroundColor Yellow

try {
    $dotnetPath = Get-Command dotnet -ErrorAction Stop
    $dotnetVersion = & dotnet --version 2>&1
    Write-Host ".NET CLI found: $($dotnetPath.Source)" -ForegroundColor Green
    Write-Host ".NET version: $dotnetVersion" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: dotnet CLI not found in PATH" -ForegroundColor Red
    Write-Host ""
    Write-Host "Please ensure .NET 9.0 SDK is installed" -ForegroundColor Yellow
    Write-Host "Download from: https://dotnet.microsoft.com/download/dotnet/9.0" -ForegroundColor Cyan
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Check .NET 9.0 SDK availability
# ============================================================================
Write-Host "Checking for .NET 9.0 SDK..." -ForegroundColor Yellow

try {
    $sdks = & dotnet --list-sdks 2>&1
    $net9Sdk = $sdks | Where-Object { $_ -match "9\.0\." }
    
    if ($net9Sdk) {
        Write-Host ".NET 9.0 SDK found:" -ForegroundColor Green
        $net9Sdk | ForEach-Object { Write-Host "  $_" -ForegroundColor Green }
        Write-Host ""
    } else {
        Write-Host ""
        Write-Host "ERROR: .NET 9.0 SDK not found" -ForegroundColor Red
        Write-Host ""
        Write-Host "Available SDKs:" -ForegroundColor Yellow
        $sdks | ForEach-Object { Write-Host "  $_" -ForegroundColor Gray }
        Write-Host ""
        Write-Host "Please install .NET 9.0 SDK from: https://dotnet.microsoft.com/download/dotnet/9.0" -ForegroundColor Cyan
        Write-Host ""
        Read-Host "Press Enter to exit"
        exit 1
    }
} catch {
    Write-Host ""
    Write-Host "ERROR: Failed to check .NET SDK version: $_" -ForegroundColor Red
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Restore NuGet packages
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Restoring NuGet packages..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

$solutionPath = Join-Path $scriptRoot "ADLXWrapper.sln"

if (-not (Test-Path $solutionPath)) {
    Write-Host "ERROR: Solution file not found: $solutionPath" -ForegroundColor Red
    Read-Host "Press Enter to exit"
    exit 1
}

try {
    & dotnet restore $solutionPath
    
    if ($LASTEXITCODE -ne 0) {
        throw "Restore failed with exit code $LASTEXITCODE"
    }
    
    Write-Host ""
    Write-Host "NuGet packages restored successfully!" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: Failed to restore NuGet packages!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Build the solution
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Building ADLXWrapper solution..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

try {
    & dotnet build $solutionPath --configuration Debug --no-restore /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version
    
    if ($LASTEXITCODE -ne 0) {
        throw "Build failed with exit code $LASTEXITCODE"
    }
    
    Write-Host ""
    Write-Host "Build completed successfully!" -ForegroundColor Green
    Write-Host ""
} catch {
    Write-Host ""
    Write-Host "ERROR: Build failed!" -ForegroundColor Red
    Write-Host "Error: $_" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "Troubleshooting:" -ForegroundColor Yellow
    Write-Host "  - Ensure all NuGet packages are restored" -ForegroundColor Gray
    Write-Host "  - Verify .NET 9.0 SDK is installed" -ForegroundColor Gray
    Write-Host "  - Check project files for errors" -ForegroundColor Gray
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

# ============================================================================
# Success summary
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Green
Write-Host "*** BUILD SUCCESSFUL! ***" -ForegroundColor Green
Write-Host "============================================================================" -ForegroundColor Green
Write-Host ""
Write-Host "Projects built:" -ForegroundColor Cyan
Write-Host "  - ADLXWrapper (ClangSharp-based wrapper)" -ForegroundColor Green
Write-Host "  - ADLXWrapper.Tests (Test suite)" -ForegroundColor Green
Write-Host ""
Write-Host "Next steps:" -ForegroundColor Cyan
Write-Host "  - Run tests: .\test_adlx.ps1" -ForegroundColor Gray
Write-Host "  - Use in your project: Add reference to ADLXWrapper\ADLXWrapper.csproj" -ForegroundColor Gray
Write-Host ""
Write-Host "Press Enter to exit..."
Read-Host
