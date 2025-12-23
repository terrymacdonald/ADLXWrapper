# Create a release zip for ADLXWrapper
param(
    [string]$Configuration = "Release",
    [switch]$IncludeSources
)

$scriptRoot = $PSScriptRoot
if (-not $scriptRoot) {
    $scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
}

Set-Location $scriptRoot

$adlxHeader = Join-Path $scriptRoot "ADLX/SDK/Include/ADLX.h"
if (-not (Test-Path $adlxHeader)) {
    Write-Host "ADLX SDK headers not found. Run ./prepare_adlx.ps1 first." -ForegroundColor Red
    exit 1
}

$projectPath = Join-Path $scriptRoot "ADLXWrapper/ADLXWrapper.csproj"
Write-Host "Building ADLXWrapper ($Configuration)..." -ForegroundColor Cyan
dotnet build $projectPath -c $Configuration
if ($LASTEXITCODE -ne 0) {
    Write-Host "Build failed; aborting packaging." -ForegroundColor Red
    exit $LASTEXITCODE
}

$buildOutput = Join-Path $scriptRoot "ADLXWrapper/bin/$Configuration/net10.0"
$assemblyPath = Join-Path $buildOutput "ADLXWrapper.dll"
if (-not (Test-Path $assemblyPath)) {
    Write-Host "Build output not found at $assemblyPath" -ForegroundColor Red
    exit 1
}

$versionInfo = [System.Diagnostics.FileVersionInfo]::GetVersionInfo($assemblyPath)
$version = if ($versionInfo.FileVersion) { $versionInfo.FileVersion } else { "0.0.0" }

$artifactsDir = Join-Path $scriptRoot "release-zip"
New-Item -ItemType Directory -Force -Path $artifactsDir | Out-Null
$zipPath = Join-Path $artifactsDir "adlxwrapper-$version-$Configuration.zip"
if (Test-Path $zipPath) { Remove-Item $zipPath }

$pathsToPack = @()
$pathsToPack += $assemblyPath
$pathsToPack += Join-Path $buildOutput "ADLXWrapper.pdb"
$pathsToPack += Join-Path $buildOutput "ADLXWrapper.deps.json"
$pathsToPack += Join-Path $buildOutput "ADLXWrapper.xml"
$pathsToPack += Join-Path $scriptRoot "LICENSE"
$pathsToPack += Join-Path $scriptRoot "README.md"
$pathsToPack += Join-Path $scriptRoot "ADLXWrapper/README.md"
$pathsToPack = $pathsToPack | Where-Object { Test-Path $_ }

if ($IncludeSources) {
    $pathsToPack += (Join-Path $scriptRoot "ADLXWrapper/cs_generated")
    $sourceFiles = Get-ChildItem -Path (Join-Path $scriptRoot "ADLXWrapper") -Filter *.cs -File
    $pathsToPack += $sourceFiles.FullName
    $pathsToPack += (Join-Path $scriptRoot "ADLXWrapper/ADLXWrapper.csproj")
}

$pathsToPack = $pathsToPack | Select-Object -Unique

Write-Host "Creating $zipPath..." -ForegroundColor Cyan
Compress-Archive -Path $pathsToPack -DestinationPath $zipPath -Force
Write-Host "Release zip created at $zipPath" -ForegroundColor Green
