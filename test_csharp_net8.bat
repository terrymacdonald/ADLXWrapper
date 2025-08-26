@echo off
echo Building C# ADLX Test Application (.NET 8.0)...
echo Using dotnet CLI for .NET 8.0 build and execution

cd IADLXGPU2Test

REM Verify dotnet CLI is available
where dotnet >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: dotnet CLI not found in PATH
    echo Please ensure .NET 8.0 SDK is installed
    echo Download from: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

echo .NET CLI found in PATH:
where dotnet
echo.

REM Check .NET 8.0 SDK availability
echo Checking for .NET 8.0 SDK...
dotnet --list-sdks | findstr "8.0" >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: .NET 8.0 SDK not found
    echo Available SDKs:
    dotnet --list-sdks
    echo.
    echo Please install .NET 8.0 SDK from: https://dotnet.microsoft.com/download/dotnet/8.0
    pause
    exit /b 1
)

echo .NET 8.0 SDK found successfully
echo.

REM Build the .NET 8.0 C# project
echo Building .NET 8.0 C# project...
dotnet build IADLXGPU2Test_Net8.csproj -c Debug -p:Platform=x64 --nologo

if %ERRORLEVEL% EQU 0 (
    echo.
    echo *** BUILD SUCCESSFUL! ***
    echo IADLXGPU2Test_Net8.dll/.exe created using .NET 8.0 SDK
    echo.
    echo Running the .NET 8.0 C# ADLX test...
    echo.
    dotnet run --project IADLXGPU2Test_Net8.csproj -c Debug --no-build
) else (
    echo.
    echo *** BUILD FAILED! ***
    echo Error level: %ERRORLEVEL%
    echo Check the error messages above for details
    echo.
    echo Troubleshooting tips:
    echo - Ensure .NET 8.0 SDK is installed
    echo - Verify ADLXWrapper.dll is present in the project directory
    echo - Check that all required NuGet packages are restored
    echo.
    pause
)
