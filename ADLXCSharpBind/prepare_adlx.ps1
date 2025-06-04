# Define the URL for the latest zip file and the destination folder
$zipUrl = "https://github.com/GPUOpen-LibrariesAndSDKs/ADLX/archive/refs/heads/main.zip"
$zipFilePath = ".\main.zip"
$destinationFolder = ".\ADLX"
$tempExtractFolder = ".\ADLX-main"
$outFolder = ".\out"

# Change to the parent Solution Folder (as that is where we want the folders)
Set-Location ..

# Download the zip file
Write-Host "Downloading the latest version of ADLX... (may take a while)"
try {
    Invoke-WebRequest -Uri $zipUrl -OutFile $zipFilePath -ErrorAction Stop
    Write-Host "Download succeeded."
} catch {
    Write-Host "Error downloading the zip file: $_"
    exit 1
}

# Check if the ADLX folder exists, and if it does, remove it
if (Test-Path -Path $destinationFolder) {
    Write-Host "Removing existing ADLX folder..."
    Remove-Item -Path $destinationFolder -Recurse -Force
}

# Unzip the downloaded file into a temporary folder
Write-Host "Extracting the contents of the zip file... (may take a while)"
Expand-Archive -Path $zipFilePath -DestinationPath . -Force

# Rename the ADLX-main folder to ADLX
Write-Host "Renaming ADLX-main to ADLX..."
Rename-Item -Path $tempExtractFolder -NewName $destinationFolder

# Remove the zip file after extraction
Write-Host "Cleaning up..."
Remove-Item -Path $zipFilePath -Force

# Create the out folder if it doesn't exist
if (-not (Test-Path -Path $outFolder)) {
    Write-Host "Creating the out folder..."
    New-Item -ItemType Directory -Path $outFolder
}

Write-Host "Project pre-build tasks completed."
