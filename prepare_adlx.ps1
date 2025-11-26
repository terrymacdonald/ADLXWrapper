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
        Write-Host "You can now build the wrapper using:" -ForegroundColor Green
        Write-Host "  - Visual Studio: Open ADLXWrapper.sln and build" -ForegroundColor Gray
        Write-Host "  - Command line: .\build.ps1" -ForegroundColor Gray
        Write-Host "  - VS Code: dotnet build" -ForegroundColor Gray
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
Write-Host "Next steps:" -ForegroundColor Green
Write-Host "  - Build the wrapper: .\build.ps1" -ForegroundColor Gray
Write-Host "  - Or open ADLXWrapper.sln in Visual Studio" -ForegroundColor Gray
Write-Host "  - Or use: dotnet build" -ForegroundColor Gray
Write-Host ""
