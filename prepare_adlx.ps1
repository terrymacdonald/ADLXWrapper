# Enhanced ADLX Preparation Script with Improved Error Handling and SWIG Integration
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

# Define SWIG-related variables (all relative to script location)
$swigZipUrl = "https://cytranet-dal.dl.sourceforge.net/project/swig/swigwin/swigwin-4.3.1/swigwin-4.3.1.zip?viasf=1"
$swigZipFilePath = Join-Path $scriptRoot "swigwin.zip"
$swigDestinationFolder = Join-Path $scriptRoot "swigwin"
$swigTempExtractFolder = Join-Path $scriptRoot "swigwin-4.3.1"
$swigExecutablePath = Join-Path $swigDestinationFolder "swig.exe"

# Define ADLX-related variables (all relative to script location)
$zipUrl = "https://github.com/GPUOpen-LibrariesAndSDKs/ADLX/archive/refs/heads/main.zip"
$zipFilePath = Join-Path $scriptRoot "main.zip"
$destinationFolder = Join-Path $scriptRoot "ADLX"
$tempExtractFolder = Join-Path $scriptRoot "ADLX-main"
$outFolder = Join-Path $scriptRoot "ADLXWrapper\cs_bindings"

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

# Function to validate SWIG installation
function Test-SwigInstallation {
    param([string]$swigPath)
    
    # Check if SWIG executable exists
    if (-not (Test-Path -Path $swigPath)) {
        Write-Host "SWIG executable not found at: $swigPath" -ForegroundColor Yellow
        return $false
    }
    
    # Test SWIG functionality by checking version
    try {
        $swigVersion = & $swigPath -version 2>&1
        if ($LASTEXITCODE -eq 0 -and $swigVersion -match "SWIG Version") {
            Write-Host "SWIG validation passed - executable is functional." -ForegroundColor Green
            return $true
        } else {
            Write-Host "SWIG executable exists but is not functional." -ForegroundColor Yellow
            return $false
        }
    } catch {
        Write-Host "Failed to execute SWIG: $_" -ForegroundColor Yellow
        return $false
    }
}

# Function to install SWIG for Windows
function Install-SwigWindows {
    Write-Host "Downloading SWIG v4.3.1 for Windows... (may take a while)"
    
    try {
        # Add progress tracking for downloads and handle redirects
        $ProgressPreference = 'Continue'
        Invoke-WebRequest -Uri $swigZipUrl -OutFile $swigZipFilePath -MaximumRedirection 10 -ErrorAction Stop
        Write-Host "SWIG download succeeded." -ForegroundColor Green
        
        # Validate downloaded file
        if (-not (Test-Path -Path $swigZipFilePath)) {
            throw "Downloaded SWIG file not found"
        }
        
        $fileSize = (Get-Item $swigZipFilePath).Length
        # SWIG 4.3.1 zip file should be around 10-15MB, so anything less than 5MB is likely an error page
        if ($fileSize -lt 5MB) {
            throw "Downloaded SWIG file appears to be too small ($fileSize bytes) - likely a redirect page or error"
        }
        
        Write-Host "Downloaded SWIG file validated ($([math]::Round($fileSize/1MB, 2)) MB)." -ForegroundColor Green
        
    } catch {
        Write-Host "ERROR: Failed to download SWIG: $_" -ForegroundColor Red
        # Clean up partial download
        if (Test-Path -Path $swigZipFilePath) {
            Remove-Item -Path $swigZipFilePath -Force
        }
        return $false
    }
    
    # Extract SWIG
    Write-Host "Extracting SWIG... (may take a while)"
    try {
        Expand-Archive -Path $swigZipFilePath -DestinationPath $scriptRoot -Force -ErrorAction Stop
        Write-Host "SWIG extraction completed successfully." -ForegroundColor Green
    } catch {
        Write-Host "ERROR: Failed to extract SWIG: $_" -ForegroundColor Red
        # Clean up
        if (Test-Path -Path $swigZipFilePath) {
            Remove-Item -Path $swigZipFilePath -Force
        }
        return $false
    }
    
    # Validate extracted folder
    if (-not (Test-Path -Path $swigTempExtractFolder)) {
        Write-Host "ERROR: Extracted SWIG folder not found." -ForegroundColor Red
        # Clean up
        if (Test-Path -Path $swigZipFilePath) {
            Remove-Item -Path $swigZipFilePath -Force
        }
        return $false
    }
    
    # Rename folder
    Write-Host "Renaming SWIG folder..."
    try {
        # Remove old destination if exists
        if (Test-Path -Path $swigDestinationFolder) {
            Remove-Item -Path $swigDestinationFolder -Recurse -Force
        }
        
        Rename-Item -Path $swigTempExtractFolder -NewName "swigwin" -ErrorAction Stop
        Write-Host "SWIG folder renamed successfully." -ForegroundColor Green
    } catch {
        Write-Host "ERROR: Failed to rename SWIG folder: $_" -ForegroundColor Red
        # Clean up
        if (Test-Path -Path $swigZipFilePath) {
            Remove-Item -Path $swigZipFilePath -Force
        }
        if (Test-Path -Path $swigTempExtractFolder) {
            Remove-Item -Path $swigTempExtractFolder -Recurse -Force
        }
        return $false
    }
    
    # Validate SWIG installation
    if (-not (Test-SwigInstallation -swigPath $swigExecutablePath)) {
        Write-Host "ERROR: SWIG installation validation failed." -ForegroundColor Red
        return $false
    }
    
    # Clean up zip file
    Write-Host "Cleaning up SWIG download files..."
    try {
        Remove-Item -Path $swigZipFilePath -Force -ErrorAction Stop
        Write-Host "SWIG cleanup completed." -ForegroundColor Green
    } catch {
        Write-Host "WARNING: Failed to remove SWIG zip file: $_" -ForegroundColor Yellow
    }
    
    return $true
}

# ============================================================================
# Main Script Execution
# ============================================================================

Write-Host "=== ADLXWrapper Preparation Script ===" -ForegroundColor Cyan
Write-Host "This script will download and prepare:" -ForegroundColor Cyan
Write-Host "  1. SWIG v4.3.1 (for C# binding generation)" -ForegroundColor Cyan
Write-Host "  2. AMD ADLX SDK (latest from GitHub)" -ForegroundColor Cyan
Write-Host "" -ForegroundColor Cyan

# Check internet connection
Write-Host "Checking internet connectivity..."
if (-not (Test-InternetConnection)) {
    Write-Host "ERROR: No internet connection detected." -ForegroundColor Red
    Write-Host "This script requires internet access to download SWIG and ADLX SDK." -ForegroundColor Red
    exit 1
}
Write-Host "Internet connection verified." -ForegroundColor Green
Write-Host ""

# ============================================================================
# SWIG Installation
# ============================================================================
Write-Host "=== Checking SWIG Installation ===" -ForegroundColor Cyan

if (Test-Path -Path $swigDestinationFolder) {
    Write-Host "SWIG folder already exists at: $swigDestinationFolder"
    
    # Validate existing SWIG installation
    if (Test-SwigInstallation -swigPath $swigExecutablePath) {
        Write-Host "Existing SWIG installation is functional." -ForegroundColor Green
    } else {
        Write-Host "Existing SWIG installation is not functional. Re-downloading..." -ForegroundColor Yellow
        
        # Remove broken installation
        try {
            Remove-Item -Path $swigDestinationFolder -Recurse -Force -ErrorAction Stop
        } catch {
            Write-Host "ERROR: Failed to remove broken SWIG installation: $_" -ForegroundColor Red
            exit 1
        }
        
        # Install SWIG
        if (-not (Install-SwigWindows)) {
            Write-Host "ERROR: Failed to install SWIG." -ForegroundColor Red
            exit 1
        }
    }
} else {
    Write-Host "SWIG not found. Installing..." -ForegroundColor Yellow
    
    # Install SWIG
    if (-not (Install-SwigWindows)) {
        Write-Host "ERROR: Failed to install SWIG." -ForegroundColor Red
        exit 1
    }
}

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
        
        # Skip download
        $skipDownload = $true
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

if (-not $skipDownload) {
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
}

Write-Host ""

# ============================================================================
# Create output folder for C# bindings
# ============================================================================
if (-not (Test-Path -Path $outFolder)) {
    Write-Host "Creating the output folder (cs_bindings)..."
    try {
        New-Item -ItemType Directory -Path $outFolder -ErrorAction Stop | Out-Null
        Write-Host "Output folder cs_bindings created successfully." -ForegroundColor Green
    } catch {
        Write-Host "ERROR: Failed to create output folder: $_" -ForegroundColor Red
        exit 1
    }
} else {
    Write-Host "Output folder already exists." -ForegroundColor Green
}

Write-Host ""
Write-Host "=== Project pre-build tasks completed successfully ===" -ForegroundColor Green
Write-Host "SWIG and ADLX SDK are ready for compilation." -ForegroundColor Green
Write-Host ""
Write-Host "Summary:" -ForegroundColor Cyan
Write-Host "  - SWIG location: $swigDestinationFolder" -ForegroundColor Cyan
Write-Host "  - ADLX SDK location: $destinationFolder" -ForegroundColor Cyan
Write-Host "  - C# bindings output: $outFolder" -ForegroundColor Cyan
Write-Host ""
Write-Host "You can now run rebuild_adlx.bat to build the wrapper." -ForegroundColor Green
