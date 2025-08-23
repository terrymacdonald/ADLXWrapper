@echo off
echo Building C# ADLX Test Application (VSCode Optimized)...
echo Using vcvars64.bat for proper Visual Studio environment setup

cd IADLXGPU2Test

REM Set up Visual Studio environment using vcvars64.bat (VSCode compatible approach)
echo Setting up Visual Studio 2022 environment...
call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to set up Visual Studio environment
    echo Please ensure Visual Studio 2022 Community is installed
    pause
    exit /b 1
)

echo Visual Studio environment configured successfully
echo.

REM Verify msbuild.exe is available
where msbuild >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: msbuild.exe not found in PATH after vcvars64.bat
    echo Visual Studio environment setup may have failed
    pause
    exit /b 1
)

echo MSBuild found in PATH: 
where msbuild
echo.

REM Build the C# project
echo Building C# project...
msbuild IADLXGPU2Test.csproj /p:Configuration=Debug /p:Platform=x64 /nologo

if %ERRORLEVEL% EQU 0 (
    echo.
    echo *** BUILD SUCCESSFUL! ***
    echo IADLXGPU2Test.exe created using VSCode-compatible approach
    echo.
    echo Running the C# ADLX test...
    echo.
    bin\Debug\IADLXGPU2Test.exe
) else (
    echo.
    echo *** BUILD FAILED! ***
    echo Error level: %ERRORLEVEL%
    echo Check the error messages above for details
    echo.
    pause
)
