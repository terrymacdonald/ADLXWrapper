@echo off
echo Building Native C ADLX Test Application (AMD vcvars64.bat method)...

call "C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Auxiliary\Build\vcvars64.bat" >nul 2>&1

REM Fixed to use main.c instead of mainGPUs_AMD.c and correct WinAPIs.c path
cl /I"../ADLX/SDK/Include" /I"../ADLX/SDK/ADLXHelper/Windows/C" main.c ../ADLX/SDK/ADLXHelper/Windows/C/ADLXHelper.c ../ADLX/SDK/platform/Windows/WinAPIs.c /Fe:ADLXNativeTest.exe

if %errorlevel% equ 0 (
    echo Build successful! ADLXNativeTest.exe created.
    echo.
    echo Running the test...
    ADLXNativeTest.exe
) else (
    echo Build failed!
)

pause
