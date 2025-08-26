# Enhanced ADLX Preparation Script with Improved Error Handling
# Copyright (c) 2021 - 2025 Advanced Micro Devices, Inc. All rights reserved.

# Define the URL for the latest zip file and the destination folder
$zipUrl = "https://github.com/GPUOpen-LibrariesAndSDKs/ADLX/archive/refs/heads/main.zip"
$zipFilePath = ".\main.zip"
$destinationFolder = ".\ADLX"
$tempExtractFolder = ".\ADLX-main"
$outFolder = ".\out"

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

# Change to the parent Solution Folder (as that is where we want the folders)
Set-Location ..

Write-Host "=== Enhanced ADLX Preparation Script ===" -ForegroundColor Cyan
Write-Host "Preparing ADLX SDK for IADLXGPU2 support..." -ForegroundColor Cyan

# Check internet connectivity
Write-Host "Checking internet connectivity..."
if (-not (Test-InternetConnection)) {
    Write-Host "ERROR: No internet connection available. Cannot download ADLX SDK." -ForegroundColor Red
    exit 1
}
Write-Host "Internet connectivity confirmed." -ForegroundColor Green

# Check if ADLX folder already exists and is complete
if (Test-Path -Path $destinationFolder) {
    Write-Host "Existing ADLX folder found. Validating completeness..."
    if (Test-ADLXSDKCompleteness -adlxPath $destinationFolder) {
        Write-Host "Existing ADLX SDK is complete. Skipping download." -ForegroundColor Green
        
        # Still create the out folder if it doesn't exist
        if (-not (Test-Path -Path $outFolder)) {
            Write-Host "Creating the out folder..."
            New-Item -ItemType Directory -Path $outFolder | Out-Null
        }
        
        Write-Host "Project pre-build tasks completed successfully." -ForegroundColor Green
        exit 0
    } else {
        Write-Host "Existing ADLX SDK is incomplete. Re-downloading..." -ForegroundColor Yellow
        try {
            Remove-Item -Path $destinationFolder -Recurse -Force -ErrorAction Stop
            Write-Host "Removed incomplete ADLX folder." -ForegroundColor Green
        } catch {
            Write-Host "ERROR: Failed to remove existing ADLX folder: $_" -ForegroundColor Red
            exit 1
        }
    }
}

# Download the zip file
Write-Host "Downloading the latest version of ADLX... (may take a while)"
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
    
} catch {
    Write-Host "ERROR: Failed to download ADLX SDK: $_" -ForegroundColor Red
    # Clean up partial download
    if (Test-Path -Path $zipFilePath) {
        Remove-Item -Path $zipFilePath -Force
    }
    exit 1
}

# Unzip the downloaded file into a temporary folder
Write-Host "Extracting the contents of the zip file... (may take a while)"
try {
    Expand-Archive -Path $zipFilePath -DestinationPath . -Force -ErrorAction Stop
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
    Rename-Item -Path $tempExtractFolder -NewName $destinationFolder -ErrorAction Stop
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

# Create the out folder if it doesn't exist
if (-not (Test-Path -Path $outFolder)) {
    Write-Host "Creating the out folder..."
    try {
        New-Item -ItemType Directory -Path $outFolder -ErrorAction Stop | Out-Null
        Write-Host "Out folder created successfully." -ForegroundColor Green
    } catch {
        Write-Host "ERROR: Failed to create out folder: $_" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "Out folder already exists." -ForegroundColor Green
}

Write-Host "=== Project pre-build tasks completed successfully ===" -ForegroundColor Green
Write-Host "ADLX SDK is ready for IADLXGPU2 support compilation." -ForegroundColor Green
