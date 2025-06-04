# Define the URL for the latest zip file and the destination folder
$zipUrl = "https://cfhcable.dl.sourceforge.net/project/swig/swigwin/swigwin-4.3.1/swigwin-4.3.1.zip?viasf=1"
$zipFilePath = ".\swigwin.zip"
$destinationFolder = ".\swigwin"
$tempExtractFolder = ".\swigwin-4.3.1"

# Download the zip file
Write-Host "Downloading Swig v4.3.1 for windows... (may take a while)"
try {
    Invoke-WebRequest -Uri $zipUrl -OutFile $zipFilePath -ErrorAction Stop
    Write-Host "Download succeeded."
} catch {
    Write-Host "Error downloading the zip file: $_"
    exit 1
}

# Check if the swigwin folder exists, and if it does, remove it
if (Test-Path -Path $destinationFolder) {
    Write-Host "Removing existing swigwin folder..."
    Remove-Item -Path $destinationFolder -Recurse -Force
}

# Unzip the downloaded file into a temporary folder
Write-Host "Extracting the contents of the zip file... (may take a while)"
Expand-Archive -Path $zipFilePath -DestinationPath . -Force

# Rename the swigwin-4.3.1 folder to swigwin
Write-Host "Renaming swigwin-4.3.1 folder to swigwin..."
Rename-Item -Path $tempExtractFolder -NewName $destinationFolder

# Remove the zip file after extraction
Write-Host "Cleaning up..."
Remove-Item -Path $zipFilePath -Force

Write-Host "Swig ready to be used to build this project."
