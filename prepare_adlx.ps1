# Enhanced ADLX Preparation Script
# Downloads the AMD ADLX SDK for the ClangSharp-based wrapper
# Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.

# Get the directory where this script is located (works from any location)
$scriptRoot = $PSScriptRoot
if (-not $scriptRoot) {
    $scriptRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
}

# Change to script directory to ensure relative paths work correctly
Set-Location $scriptRoot
Write-Host "Working directory: $scriptRoot" -ForegroundColor Cyan
Write-Host ""

# Define ADLX-related variables (all relative to script location)
$zipUrl = "https://github.com/GPUOpen-LibrariesAndSDKs/ADLX/archive/refs/heads/main.zip"
$zipFilePath = Join-Path $scriptRoot "main.zip"
$destinationFolder = Join-Path $scriptRoot "ADLX"
$tempExtractFolder = Join-Path $scriptRoot "ADLX-main"

# Function to validate ADLX SDK completeness
function Test-ADLXSDKCompleteness {
    param([string]$adlxPath)
    
    $requiredPaths = @(
        "$adlxPath\SDK\Include\ADLX.h",
        "$adlxPath\SDK\Include\ISystem.h",
        "$adlxPath\SDK\Include\ISystem1.h",
        "$adlxPath\SDK\Include\ISystem2.h",
        "$adlxPath\SDK\ADLXHelper\Windows\Cpp\ADLXHelper.h",
        "$adlxPath\SDK\ADLXHelper\Windows\Cpp\ADLXHelper.cpp",
        "$adlxPath\SDK\Platform\Windows\WinAPIs.cpp"
    )
    
    $missingFiles = @()
    foreach ($path in $requiredPaths) {
        if (-not (Test-Path -Path $path)) {
            $missingFiles += $path
        }
    }
    
    if ($missingFiles.Count -gt 0) {
        Write-Host "ERROR: ADLX SDK is incomplete. Missing files:" -ForegroundColor Red
        foreach ($file in $missingFiles) {
            Write-Host "  - $file" -ForegroundColor Red
        }
        return $false
    }
    
    Write-Host "ADLX SDK validation passed - all required files present." -ForegroundColor Green
    return $true
}

# Function to check internet connectivity
function Test-InternetConnection {
    try {
        $response = Invoke-WebRequest -Uri "https://www.google.com" -Method Head -TimeoutSec 10 -ErrorAction Stop
        return $true
    } catch {
        return $false
    }
}

# ============================================================================
# Main Script Execution
# ============================================================================

Write-Host "=== ADLXWrapper Preparation Script ===" -ForegroundColor Cyan
Write-Host "This script will download the AMD ADLX SDK" -ForegroundColor Cyan
Write-Host ""

# ============================================================================
# Check .NET 10.0 SDK (informational only)
# ============================================================================
Write-Host "Checking for .NET 10.0 SDK..." -ForegroundColor Yellow

$dotnetInstalled = $false
$net10Installed = $false

try {
    $dotnetPath = Get-Command dotnet -ErrorAction SilentlyContinue
    if ($dotnetPath) {
        $dotnetInstalled = $true
        $sdks = & dotnet --list-sdks 2>&1
        $net10Sdk = $sdks | Where-Object { $_ -match "10\.0\." }
        
        if ($net10Sdk) {
            $net10Installed = $true
            Write-Host ".NET 10.0 SDK found:" -ForegroundColor Green
            $net10Sdk | ForEach-Object { Write-Host "  $_" -ForegroundColor Green }
        } else {
            Write-Host ".NET CLI found, but .NET 10.0 SDK not installed" -ForegroundColor Yellow
            Write-Host "Available SDKs:" -ForegroundColor Gray
            $sdks | ForEach-Object { Write-Host "  $_" -ForegroundColor Gray }
        }
    } else {
        Write-Host ".NET CLI not found in PATH" -ForegroundColor Yellow
    }
} catch {
    Write-Host "Could not check .NET installation: $_" -ForegroundColor Yellow
}

if (-not $net10Installed) {
    Write-Host ""
    Write-Host "??  WARNING: .NET 10.0 SDK not detected" -ForegroundColor Yellow
    Write-Host ""
    Write-Host "To build this project, you need:" -ForegroundColor Yellow
    Write-Host "  - .NET 10.0 SDK" -ForegroundColor Cyan
    Write-Host "  - Download from: https://dotnet.microsoft.com/download/dotnet/10.0" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Or install via Visual Studio:" -ForegroundColor Yellow
    Write-Host "  - Open Visual Studio Installer" -ForegroundColor Cyan
    Write-Host "  - Modify your installation" -ForegroundColor Cyan
    Write-Host "  - Under 'Individual components', select '.NET 10.0 Runtime'" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Continuing with ADLX SDK download..." -ForegroundColor Gray
    Write-Host ""
}

Write-Host ""

# Check internet connection
Write-Host "Checking internet connectivity..."
if (-not (Test-InternetConnection)) {
    Write-Host "ERROR: No internet connection detected." -ForegroundColor Red
    Write-Host "This script requires internet access to download the ADLX SDK." -ForegroundColor Red
    exit 1
}
Write-Host "Internet connection verified." -ForegroundColor Green
Write-Host ""

# ============================================================================
# ADLX SDK Download
# ============================================================================
Write-Host "=== Checking ADLX SDK ===" -ForegroundColor Cyan

# Check if the ADLX folder already exists
if (Test-Path -Path $destinationFolder) {
    Write-Host "ADLX SDK folder already exists at: $destinationFolder"
    
    # Validate existing ADLX installation
    if (Test-ADLXSDKCompleteness -adlxPath $destinationFolder) {
        Write-Host "Existing ADLX SDK is complete and functional." -ForegroundColor Green
        Write-Host ""
        Write-Host "=== ADLX SDK is ready ===" -ForegroundColor Green
        Write-Host "SDK location: $destinationFolder" -ForegroundColor Cyan
        Write-Host ""
        
        if ($net10Installed) {
            Write-Host "Next steps:" -ForegroundColor Green
            Write-Host "  - Build the wrapper: .\build_adlx.ps1" -ForegroundColor Gray
            Write-Host "  - Or open ADLXWrapper.sln in Visual Studio" -ForegroundColor Gray
            Write-Host "  - Or use: dotnet build" -ForegroundColor Gray
        } else {
            Write-Host "Before building:" -ForegroundColor Yellow
            Write-Host "  - Install .NET 10.0 SDK (see warning above)" -ForegroundColor Cyan
            Write-Host ""
            Write-Host "Then build:" -ForegroundColor Green
            Write-Host "  - .\build_adlx.ps1" -ForegroundColor Gray
            Write-Host "  - Or open ADLXWrapper.sln in Visual Studio" -ForegroundColor Gray
        }
        Write-Host ""
        exit 0
    } else {
        Write-Host "Existing ADLX SDK is incomplete. Re-downloading..." -ForegroundColor Yellow
        
        # Remove incomplete installation
        try {
            Remove-Item -Path $destinationFolder -Recurse -Force -ErrorAction Stop
        } catch {
            Write-Host "ERROR: Failed to remove incomplete ADLX SDK: $_" -ForegroundColor Red
            exit 1
        }
    }
}

# Download the zip file
Write-Host "Downloading the latest version of ADLX SDK from GitHub..." -ForegroundColor Yellow
Write-Host "(This may take a while depending on your connection)" -ForegroundColor Gray
Write-Host ""

try {
    # Add progress tracking for large downloads
    $ProgressPreference = 'Continue'
    Invoke-WebRequest -Uri $zipUrl -OutFile $zipFilePath -ErrorAction Stop
    Write-Host "Download succeeded." -ForegroundColor Green
    
    # Validate downloaded file
    if (-not (Test-Path -Path $zipFilePath)) {
        throw "Downloaded file not found"
    }
    
    $fileSize = (Get-Item $zipFilePath).Length
    if ($fileSize -lt 1MB) {
        throw "Downloaded file appears to be too small ($fileSize bytes)"
    }
    
    Write-Host "Downloaded file validated ($([math]::Round($fileSize/1MB, 2)) MB)." -ForegroundColor Green
    Write-Host ""
    
} catch {
    Write-Host "ERROR: Failed to download ADLX SDK: $_" -ForegroundColor Red
    # Clean up partial download
    if (Test-Path -Path $zipFilePath) {
        Remove-Item -Path $zipFilePath -Force
    }
    exit 1
}

# Unzip the downloaded file into a temporary folder
Write-Host "Extracting ADLX SDK..." -ForegroundColor Yellow
Write-Host "(This may take a while)" -ForegroundColor Gray
Write-Host ""

try {
    Expand-Archive -Path $zipFilePath -DestinationPath $scriptRoot -Force -ErrorAction Stop
    Write-Host "Extraction completed successfully." -ForegroundColor Green
} catch {
    Write-Host "ERROR: Failed to extract ADLX SDK: $_" -ForegroundColor Red
    # Clean up
    if (Test-Path -Path $zipFilePath) {
        Remove-Item -Path $zipFilePath -Force
    }
    exit 1
}

# Validate extracted folder exists
if (-not (Test-Path -Path $tempExtractFolder)) {
    Write-Host "ERROR: Extracted folder '$tempExtractFolder' not found." -ForegroundColor Red
    # Clean up
    if (Test-Path -Path $zipFilePath) {
        Remove-Item -Path $zipFilePath -Force
    }
    exit 1
}

# Rename the ADLX-main folder to ADLX
Write-Host "Renaming ADLX-main to ADLX..."
try {
    Rename-Item -Path $tempExtractFolder -NewName "ADLX" -ErrorAction Stop
    Write-Host "Folder renamed successfully." -ForegroundColor Green
} catch {
    Write-Host "ERROR: Failed to rename ADLX folder: $_" -ForegroundColor Red
    # Clean up
    if (Test-Path -Path $zipFilePath) {
        Remove-Item -Path $zipFilePath -Force
    }
    if (Test-Path -Path $tempExtractFolder) {
        Remove-Item -Path $tempExtractFolder -Recurse -Force
    }
    exit 1
}

# Validate ADLX SDK completeness
Write-Host "Validating ADLX SDK completeness..."
if (-not (Test-ADLXSDKCompleteness -adlxPath $destinationFolder)) {
    Write-Host "ERROR: Downloaded ADLX SDK is incomplete." -ForegroundColor Red
    # Clean up
    if (Test-Path -Path $zipFilePath) {
        Remove-Item -Path $zipFilePath -Force
    }
    if (Test-Path -Path $destinationFolder) {
        Remove-Item -Path $destinationFolder -Recurse -Force
    }
    exit 1
}

# Remove the zip file after successful extraction and validation
Write-Host "Cleaning up download files..."
try {
    Remove-Item -Path $zipFilePath -Force -ErrorAction Stop
    Write-Host "Cleanup completed." -ForegroundColor Green
} catch {
    Write-Host "WARNING: Failed to remove zip file: $_" -ForegroundColor Yellow
    # This is not critical, continue
}

Write-Host ""
Write-Host "=== ADLX SDK Setup Complete ===" -ForegroundColor Green
Write-Host ""
Write-Host "Summary:" -ForegroundColor Cyan
Write-Host "  - ADLX SDK location: $destinationFolder" -ForegroundColor Cyan
Write-Host ""

if ($net10Installed) {
    Write-Host "Next steps:" -ForegroundColor Green
    Write-Host "  - Build the wrapper: .\build_adlx.ps1" -ForegroundColor Gray
    Write-Host "  - Or open ADLXWrapper.sln in Visual Studio" -ForegroundColor Gray
    Write-Host "  - Or use: dotnet build" -ForegroundColor Gray
} else {
    Write-Host "Before building:" -ForegroundColor Yellow
    Write-Host "  1. Install .NET 10.0 SDK from: https://dotnet.microsoft.com/download/dotnet/10.0" -ForegroundColor Cyan
    Write-Host "     Or via Visual Studio Installer (see warning above)" -ForegroundColor Cyan
    Write-Host ""
    Write-Host "Then build:" -ForegroundColor Green
    Write-Host "  - .\build_adlx.ps1" -ForegroundColor Gray
    Write-Host "  - Or open ADLXWrapper.sln in Visual Studio" -ForegroundColor Gray
}
Write-Host ""
