@echo off
echo Building AMD Sample Copy...

call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

cl /I"../ADLX/SDK/Include" /I"../ADLX/SDK/ADLXHelper/Windows/C" mainGPUs_AMD.c ../ADLX/SDK/ADLXHelper/Windows/C/ADLXHelper.c ../ADLX/SDK/ADLXHelper/Windows/C/WinAPIs.c /Fe:mainGPUs_AMD.exe

if %errorlevel% equ 0 (
    echo Build successful! mainGPUs_AMD.exe created.
    echo.
    echo Running AMD sample copy...
    mainGPUs_AMD.exe
) else (
    echo Build failed!
)

pause
