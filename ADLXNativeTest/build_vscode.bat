@echo off
echo Building Native C ADLX Test Application (VSCode Optimized)...
echo Using vcvars64.bat for proper Visual Studio environment setup

REM Set up Visual Studio environment using vcvars64.bat (AMD's recommended approach)
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

REM Verify cl.exe is available
where cl >nul 2>&1
if %ERRORLEVEL% NEQ 0 (
    echo ERROR: cl.exe not found in PATH after vcvars64.bat
    echo Visual Studio environment setup may have failed
    pause
    exit /b 1
)

echo Compiler found in PATH: 
where cl
echo.

REM Build using AMD's standard pattern with proper environment
echo Compiling with AMD-standard approach (vcvars64.bat method)...
cl /nologo /W3 /TC main.c ^
   "..\ADLX\SDK\ADLXHelper\Windows\C\ADLXHelper.c" ^
   "..\ADLX\SDK\platform\Windows\WinAPIs.c" ^
   /Fe:ADLXNativeTest.exe ^
   /I"..\ADLX\SDK\Include" ^
   /I"..\ADLX\SDK\ADLXHelper\Windows\C" ^
   /link kernel32.lib user32.lib

if %ERRORLEVEL% EQU 0 (
    echo.
    echo *** BUILD SUCCESSFUL! ***
    echo ADLXNativeTest.exe created using AMD vcvars64.bat approach
    echo This method is most compatible with VSCode terminal
    echo.
    echo Running the test on your newer hardware...
    echo.
    ADLXNativeTest.exe
) else (
    echo.
    echo *** BUILD FAILED! ***
    echo Error level: %ERRORLEVEL%
    echo Check the error messages above for details
    echo.
    pause
)
