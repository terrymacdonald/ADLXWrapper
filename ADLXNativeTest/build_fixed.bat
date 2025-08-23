@echo off
echo Building Native C ADLX Test Application (Fixed Version)...
echo Using AMD-aligned build approach with dynamic compiler detection

REM Find the latest MSVC compiler version dynamically
set "VS_PATH=C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Tools\MSVC"
for /f "delims=" %%i in ('dir "%VS_PATH%" /b /ad /o-n 2^>nul ^| findstr /r "^[0-9]"') do (
    set "COMPILER_VERSION=%%i"
    goto :found_compiler
)

:found_compiler
if not defined COMPILER_VERSION (
    echo ERROR: Could not find MSVC compiler in %VS_PATH%
    echo Please ensure Visual Studio 2022 Community is installed
    pause
    exit /b 1
)

echo Found MSVC compiler version: %COMPILER_VERSION%
set "CL_PATH=%VS_PATH%\%COMPILER_VERSION%\bin\Hostx64\x64\cl.exe"

REM Verify compiler exists
if not exist "%CL_PATH%" (
    echo ERROR: Compiler not found at %CL_PATH%
    pause
    exit /b 1
)

echo Compiler path: %CL_PATH%
echo.

REM Build using AMD's standard pattern: main.c + ADLXHelper.c + WinAPIs.c
echo Compiling with AMD-standard file pattern...
"%CL_PATH%" /nologo /W3 /TC main.c ^
    "..\ADLX\SDK\ADLXHelper\Windows\C\ADLXHelper.c" ^
    "..\ADLX\SDK\platform\Windows\WinAPIs.c" ^
    /Fe:ADLXNativeTest.exe ^
    /I"..\ADLX\SDK\Include" ^
    /I"..\ADLX\SDK\ADLXHelper\Windows\C" ^
    /link kernel32.lib user32.lib

if %ERRORLEVEL% EQU 0 (
    echo.
    echo *** BUILD SUCCESSFUL! ***
    echo ADLXNativeTest.exe created using AMD-aligned build approach
    echo.
    echo Running the test on your newer hardware...
    echo.
    ADLXNativeTest.exe
) else (
    echo.
    echo *** BUILD FAILED! ***
    echo Check the error messages above for details
    echo.
    pause
)
