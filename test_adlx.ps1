#
# ADLXWrapper Test Script (PowerShell)
# Runs unit tests for the ADLXWrapper C# bindings
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
Write-Host "Building and Running ADLX Wrapper Unit Tests (.NET 9.0)" -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

# ============================================================================
# Verify dotnet CLI is available
# ============================================================================
Write-Host "Checking for .NET CLI..." -ForegroundColor Yellow

try {
    $dotnetPath = Get-Command dotnet -ErrorAction Stop
    Write-Host ".NET CLI found at: $($dotnetPath.Source)" -ForegroundColor Green
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
# Verify test project exists
# ============================================================================
$testProjectPath = Join-Path $scriptRoot "ADLXWrapper.Tests\ADLXWrapper.Tests.csproj"

if (-not (Test-Path $testProjectPath)) {
    Write-Host "ERROR: Test project not found at: $testProjectPath" -ForegroundColor Red
    Write-Host ""
    Read-Host "Press Enter to exit"
    exit 1
}

Write-Host "Test project found: $testProjectPath" -ForegroundColor Green
Write-Host ""

# ============================================================================
# Build and run unit tests
# ============================================================================
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host "Building and running unit tests..." -ForegroundColor Cyan
Write-Host "============================================================================" -ForegroundColor Cyan
Write-Host ""

try {
    # Run tests with detailed console output
    & dotnet test $testProjectPath -c Debug -f net9.0 --logger "console;verbosity=detailed"
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host ""
        Write-Host "============================================================================" -ForegroundColor Green
        Write-Host "*** TESTS PASSED! ***" -ForegroundColor Green
        Write-Host "============================================================================" -ForegroundColor Green
        Write-Host "All unit tests completed successfully using .NET 9.0" -ForegroundColor Green
        Write-Host ""
    } else {
        Write-Host ""
        Write-Host "============================================================================" -ForegroundColor Yellow
        Write-Host "*** TESTS FAILED! ***" -ForegroundColor Yellow
        Write-Host "============================================================================" -ForegroundColor Yellow
        Write-Host "Exit code: $LASTEXITCODE" -ForegroundColor Yellow
        Write-Host ""
        Write-Host "Troubleshooting tips:" -ForegroundColor Yellow
        Write-Host "  - Ensure AMD GPU with ADLX support is present" -ForegroundColor Gray
        Write-Host "  - Verify AMD Adrenalin drivers are installed (21.10.1 or newer)" -ForegroundColor Gray
        Write-Host "  - Check that ADLXWrapper.dll is in the test output directory" -ForegroundColor Gray
        Write-Host "  - Ensure all required NuGet packages are restored" -ForegroundColor Gray
        Write-Host "  - Some tests may be skipped on non-AMD hardware" -ForegroundColor Gray
        Write-Host "  - Review test output above for specific failure details" -ForegroundColor Gray
        Write-Host ""
        
        # Show summary of test categories
        Write-Host "Note: This test suite includes several categories:" -ForegroundColor Cyan
        Write-Host "  - Initialization Tests" -ForegroundColor Gray
        Write-Host "  - GPU Tests" -ForegroundColor Gray
        Write-Host "  - Display Tests" -ForegroundColor Gray
        Write-Host "  - Desktop Tests" -ForegroundColor Gray
        Write-Host "  - Performance Tests" -ForegroundColor Gray
        Write-Host "  - System2 Tests (requires newer GPUs)" -ForegroundColor Gray
        Write-Host ""
        Write-Host "Some tests may be skipped if hardware/drivers don't support them." -ForegroundColor Cyan
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

Write-Host "Press Enter to exit..."
Read-Host
