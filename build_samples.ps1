#
# ADLXWrapper Samples Build Script (PowerShell)
# Builds the wrapper first, then builds all sample apps via Samples/ADLXWrapper.Samples.sln
#

# Get the directory where this script is located
$scriptRoot = $PSScriptRoot
if (-not $scriptRoot) {
    $scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
}

Set-Location $scriptRoot
Write-Host "Working directory: $scriptRoot" -ForegroundColor Cyan

# Version info (reuse VERSION file + git commit count)
$versionFile = Join-Path $scriptRoot "VERSION"
if (-not (Test-Path $versionFile)) {
    Write-Host "ERROR: VERSION file not found: $versionFile" -ForegroundColor Red
    exit 1
}

$versionContent = Get-Content $versionFile
$major = "1"; $minor = "0"
foreach ($line in $versionContent) {
    if ($line -match "^MAJOR=(\d+)") { $major = $matches[1] }
    elseif ($line -match "^MINOR=(\d+)") { $minor = $matches[1] }
}
$patch = "0"
try {
    $gitPath = Get-Command git -ErrorAction SilentlyContinue
    if ($gitPath) {
        $commitCount = & git rev-list --count HEAD 2>&1
        if ($LASTEXITCODE -eq 0 -and $commitCount -match "^\d+$") { $patch = $commitCount }
    }
} catch { }
$version = "$major.$minor.$patch"

Write-Host "Building with version $version" -ForegroundColor Green

# Check dotnet
try {
    $dotnetPath = Get-Command dotnet -ErrorAction Stop
    $dotnetVersion = & dotnet --version 2>&1
    Write-Host ".NET CLI found: $dotnetVersion" -ForegroundColor Green
} catch {
    Write-Host "ERROR: dotnet CLI not found in PATH" -ForegroundColor Red
    exit 1
}

# Build wrapper first (ensures references are current)
$wrapperSolution = Join-Path $scriptRoot "ADLXWrapper.sln"
if (-not (Test-Path $wrapperSolution)) {
    Write-Host "ERROR: Wrapper solution not found: $wrapperSolution" -ForegroundColor Red
    exit 1
}

Write-Host "Restoring/building wrapper..." -ForegroundColor Cyan
dotnet restore $wrapperSolution
if ($LASTEXITCODE -ne 0) { Write-Host "Restore failed."; exit 1 }
dotnet build $wrapperSolution --configuration Debug /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version --no-restore
if ($LASTEXITCODE -ne 0) { Write-Host "Wrapper build failed."; exit 1 }

# Build samples solution
$samplesSolution = Join-Path $scriptRoot "Samples\ADLXWrapper.Samples.sln"
if (-not (Test-Path $samplesSolution)) {
    Write-Host "ERROR: Samples solution not found: $samplesSolution" -ForegroundColor Red
    exit 1
}

Write-Host "Restoring/building samples..." -ForegroundColor Cyan
dotnet restore $samplesSolution
if ($LASTEXITCODE -ne 0) { Write-Host "Samples restore failed."; exit 1 }
dotnet build $samplesSolution --configuration Debug /p:Version=$version /p:AssemblyVersion=$version /p:FileVersion=$version --no-restore
if ($LASTEXITCODE -ne 0) { Write-Host "Samples build failed."; exit 1 }

Write-Host "============================================================================" -ForegroundColor Green
Write-Host "*** SAMPLES BUILD SUCCESSFUL ***" -ForegroundColor Green
Write-Host "Wrapper + samples built with version $version" -ForegroundColor Green
Write-Host "============================================================================" -ForegroundColor Green
