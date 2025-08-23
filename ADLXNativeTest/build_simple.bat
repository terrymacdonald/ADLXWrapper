@echo off
echo Building Simplified ADLX Test (AMD Official Method)...
echo Using vcvars64.bat for proper Visual Studio environment setup

REM Set up Visual Studio environment using vcvars64.bat
echo Setting up Visual Studio 2022 environment...
call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

if %ERRORLEVEL% NEQ 0 (
    echo ERROR: Failed to set up Visual Studio environment
    pause
    exit /b 1
)

echo Visual Studio environment configured successfully
echo.

REM Build the simplified test
echo Compiling simplified ADLX test...
cl /nologo /W3 /TC main_amd_simple.c ^
   "..\ADLX\SDK\ADLXHelper\Windows\C\ADLXHelper.c" ^
   "..\ADLX\SDK\platform\Windows\WinAPIs.c" ^
   /Fe:ADLXSimpleTest.exe ^
   /I"..\ADLX\SDK\Include" ^
   /I"..\ADLX\SDK\ADLXHelper\Windows\C" ^
   /link kernel32.lib user32.lib

if %ERRORLEVEL% EQU 0 (
    echo.
    echo *** BUILD SUCCESSFUL! ***
    echo ADLXSimpleTest.exe created using AMD official method
    echo.
    echo Running the simplified test to diagnose interface issues...
    echo.
    ADLXSimpleTest.exe
) else (
    echo.
    echo *** BUILD FAILED! ***
    echo Error level: %ERRORLEVEL%
    echo Check the error messages above for details
    echo.
    pause
)
