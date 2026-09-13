param(
    [Parameter(Mandatory = $true)]
    [string]$SourcePath,
    [Parameter(Mandatory = $true)]
    [string]$OutputPath,
    [Parameter(Mandatory = $true)]
    [string]$OverlayPath
)

$sourcePath = [System.IO.Path]::GetFullPath($SourcePath)
$outputPath = [System.IO.Path]::GetFullPath($OutputPath)
$overlayPath = [System.IO.Path]::GetFullPath($OverlayPath)

if (-not (Test-Path -LiteralPath $sourcePath)) {
    throw "ADLX structures header was not found: $sourcePath"
}

$content = [System.IO.File]::ReadAllText($sourcePath)
$requiredReplacements = @{
    'using namespace adlx;' = '// Omitted for ClangSharpPInvokeGenerator.'
    'ADLX_DISPLAY_SCAN_TYPE presentation;' = 'adlx::ADLX_DISPLAY_SCAN_TYPE presentation;'
    'ADLX_TIMING_STANDARD timingStandard;' = 'adlx::ADLX_TIMING_STANDARD timingStandard;'
}

foreach ($replacement in $requiredReplacements.GetEnumerator()) {
    if (-not $content.Contains($replacement.Key)) {
        throw "Expected ADLX v1.5 declaration was not found: $($replacement.Key)"
    }
    $content = $content.Replace($replacement.Key, $replacement.Value)
}

[System.IO.Directory]::CreateDirectory([System.IO.Path]::GetDirectoryName($outputPath)) | Out-Null
[System.IO.File]::WriteAllText($outputPath, $content, [System.Text.UTF8Encoding]::new($false))

$overlay = @{
    version = 0
    roots = @(
        @{
            type = 'file'
            name = $sourcePath.Replace('\', '/')
            'external-contents' = $outputPath.Replace('\', '/')
        }
    )
}

[System.IO.File]::WriteAllText(
    $overlayPath,
    ($overlay | ConvertTo-Json -Depth 5),
    [System.Text.UTF8Encoding]::new($false))