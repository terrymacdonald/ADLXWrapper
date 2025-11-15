<#
.SYNOPSIS
    Updates version information for ADLXWrapper DLL and C# bindings
.DESCRIPTION
    Reads MAJOR and MINOR from VERSION file, calculates PATCH from git commit count,
    updates ADLXWrapper.rc and generates cs_bindings/ADLXVersion.cs
.NOTES
    This script is automatically called by MSBuild during compilation
#>

param(
    [string]$ProjectDir = $PSScriptRoot,
    [string]$VersionFile = "$PSScriptRoot\..\..\VERSION",
    [string]$OutputRcFile = "$PSScriptRoot\..\ADLXWrapper.rc",
    [string]$OutputCsFile = "$PSScriptRoot\..\cs_bindings\ADLXVersion.cs"
)

# Function to write colored output
function Write-ColorOutput {
    param([string]$Message, [string]$Color = "White")
    Write-Host $Message -ForegroundColor $Color
}

# Function to read VERSION file
function Get-VersionFromFile {
    param([string]$FilePath)
    
    if (-not (Test-Path $FilePath)) {
        Write-ColorOutput "ERROR: VERSION file not found at: $FilePath" "Red"
        return $null
    }
    
    $content = Get-Content $FilePath
    $major = ($content | Select-String "^MAJOR=(\d+)" | ForEach-Object { $_.Matches.Groups[1].Value })
    $minor = ($content | Select-String "^MINOR=(\d+)" | ForEach-Object { $_.Matches.Groups[1].Value })
    
    if (-not $major -or -not $minor) {
        Write-ColorOutput "ERROR: Invalid VERSION file format. Expected MAJOR=n and MINOR=n" "Red"
        return $null
    }
    
    return @{
        Major = [int]$major
        Minor = [int]$minor
    }
}

# Function to get git commit count for patch version
function Get-PatchVersion {
    try {
        # Check if we're in a git repository
        $gitCheck = git rev-parse --git-dir 2>&1
        if ($LASTEXITCODE -eq 0) {
            # Get commit count
            $commitCount = git rev-list --count HEAD 2>&1
            if ($LASTEXITCODE -eq 0) {
                return [int]$commitCount
            }
        }
    }
    catch {
        Write-ColorOutput "Git not available, using fallback build counter" "Yellow"
    }
    
    # Fallback: Use build counter file
    $buildCounterFile = Join-Path $PSScriptRoot "..\..\VERSION.build"
    $buildNumber = 0
    
    if (Test-Path $buildCounterFile) {
        $buildNumber = [int](Get-Content $buildCounterFile)
    }
    
    $buildNumber++
    Set-Content -Path $buildCounterFile -Value $buildNumber
    
    return $buildNumber
}

# Function to get git commit hash
function Get-GitCommitHash {
    try {
        $hash = git rev-parse --short HEAD 2>&1
        if ($LASTEXITCODE -eq 0) {
            return $hash.Trim()
        }
    }
    catch {}
    
    return "unknown"
}

# Function to generate C++ resource file
function New-ResourceFile {
    param(
        [int]$Major,
        [int]$Minor,
        [int]$Patch,
        [string]$OutputPath
    )
    
    $buildDate = Get-Date -Format "yyyy-MM-dd"
    
    $rcContent = @"
//
// Auto-generated version resource file - DO NOT EDIT MANUALLY
// Generated on: $buildDate
// Build system: ADLXWrapper version management
//

#include <windows.h>

#define VER_FILEVERSION             $Major,$Minor,$Patch,0
#define VER_FILEVERSION_STR         "$Major.$Minor.$Patch.0\0"

#define VER_PRODUCTVERSION          $Major,$Minor,$Patch,0
#define VER_PRODUCTVERSION_STR      "$Major.$Minor.$Patch\0"

#define VER_COMPANYNAME_STR         "Terry MacDonald\0"
#define VER_FILEDESCRIPTION_STR     "ADLXWrapper - C# Wrapper for AMD ADLX SDK\0"
#define VER_INTERNALNAME_STR        "ADLXWrapper\0"
#define VER_LEGALCOPYRIGHT_STR      "Copyright (c) 2025 Terry MacDonald\0"
#define VER_ORIGINALFILENAME_STR    "ADLXWrapper.dll\0"
#define VER_PRODUCTNAME_STR         "ADLXWrapper\0"

VS_VERSION_INFO VERSIONINFO
FILEVERSION     VER_FILEVERSION
PRODUCTVERSION  VER_PRODUCTVERSION
FILEFLAGSMASK   VS_FFI_FILEFLAGSMASK
#ifdef _DEBUG
FILEFLAGS       VS_FF_DEBUG
#else
FILEFLAGS       0x0L
#endif
FILEOS          VOS__WINDOWS32
FILETYPE        VFT_DLL
FILESUBTYPE     VFT2_UNKNOWN
BEGIN
    BLOCK "StringFileInfo"
    BEGIN
        BLOCK "040904b0"
        BEGIN
            VALUE "CompanyName",      VER_COMPANYNAME_STR
            VALUE "FileDescription",  VER_FILEDESCRIPTION_STR
            VALUE "FileVersion",      VER_FILEVERSION_STR
            VALUE "InternalName",     VER_INTERNALNAME_STR
            VALUE "LegalCopyright",   VER_LEGALCOPYRIGHT_STR
            VALUE "OriginalFilename", VER_ORIGINALFILENAME_STR
            VALUE "ProductName",      VER_PRODUCTNAME_STR
            VALUE "ProductVersion",   VER_PRODUCTVERSION_STR
        END
    END
    BLOCK "VarFileInfo"
    BEGIN
        VALUE "Translation", 0x409, 1200
    END
END
"@
    
    Set-Content -Path $OutputPath -Value $rcContent -Encoding UTF8
}

# Function to generate C# version file
function New-CSharpVersionFile {
    param(
        [int]$Major,
        [int]$Minor,
        [int]$Patch,
        [string]$GitCommit,
        [string]$OutputPath
    )
    
    $buildDate = Get-Date -Format "yyyy-MM-ddTHH:mm:ssZ"
    $year = Get-Date -Format "yyyy"
    
    # Ensure the cs_bindings directory exists
    $dir = Split-Path -Parent $OutputPath
    if (-not (Test-Path $dir)) {
        New-Item -ItemType Directory -Path $dir -Force | Out-Null
    }
    
    $csContent = @"
//
// Auto-generated version file - DO NOT EDIT MANUALLY
// Generated on: $buildDate
// Build system: ADLXWrapper version management
//
// Copyright (c) $year Terry MacDonald. All rights reserved.
//

namespace ADLXWrapper
{
    /// <summary>
    /// ADLXWrapper version information
    /// </summary>
    public static class ADLXVersion
    {
        /// <summary>
        /// Major version number (breaking changes)
        /// </summary>
        public const int Major = $Major;
        
        /// <summary>
        /// Minor version number (new features, backwards compatible)
        /// </summary>
        public const int Minor = $Minor;
        
        /// <summary>
        /// Patch version number (bug fixes, auto-incremented)
        /// </summary>
        public const int Patch = $Patch;
        
        /// <summary>
        /// Full version string in SemVer format (Major.Minor.Patch)
        /// </summary>
        public const string Version = "$Major.$Minor.$Patch";
        
        /// <summary>
        /// Build date in ISO 8601 format
        /// </summary>
        public const string BuildDate = "$buildDate";
        
        /// <summary>
        /// Git commit hash (short form)
        /// </summary>
        public const string GitCommit = "$GitCommit";
        
        /// <summary>
        /// Full version information string
        /// </summary>
        public const string FullVersionInfo = "ADLXWrapper v$Major.$Minor.$Patch (build $buildDate, commit $GitCommit)";
    }
}
"@
    
    Set-Content -Path $OutputPath -Value $csContent -Encoding UTF8
}

# Main execution
Write-ColorOutput "`n=== ADLXWrapper Version Update ===" "Cyan"
Write-ColorOutput "Project Directory: $ProjectDir" "Gray"

# Read version from VERSION file
$versionInfo = Get-VersionFromFile -FilePath $VersionFile
if (-not $versionInfo) {
    exit 1
}

$major = $versionInfo.Major
$minor = $versionInfo.Minor

# Get patch version
$patch = Get-PatchVersion

# Get git commit hash
$gitCommit = Get-GitCommitHash

# Display version information
Write-ColorOutput "`nVersion Information:" "Cyan"
Write-ColorOutput "  Major:      $major" "White"
Write-ColorOutput "  Minor:      $minor" "White"
Write-ColorOutput "  Patch:      $patch" "White"
Write-ColorOutput "  Git Commit: $gitCommit" "Gray"
Write-ColorOutput "  Full:       $major.$minor.$patch" "Green"

# Generate files
Write-ColorOutput "`nGenerating version files..." "Cyan"

try {
    # Generate C++ resource file
    Write-ColorOutput "  Creating: $OutputRcFile" "Gray"
    New-ResourceFile -Major $major -Minor $minor -Patch $patch -OutputPath $OutputRcFile
    Write-ColorOutput "  ? Resource file created" "Green"
    
    # Generate C# version file
    Write-ColorOutput "  Creating: $OutputCsFile" "Gray"
    New-CSharpVersionFile -Major $major -Minor $minor -Patch $patch -GitCommit $gitCommit -OutputPath $OutputCsFile
    Write-ColorOutput "  ? C# version file created" "Green"
    
    Write-ColorOutput "`n=== Version Update Complete ===" "Green"
    exit 0
}
catch {
    Write-ColorOutput "`nERROR: Failed to generate version files" "Red"
    Write-ColorOutput $_.Exception.Message "Red"
    exit 1
}
