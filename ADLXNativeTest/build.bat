@echo off
echo Building Native C ADLX Test Application...

REM Compile the C application with ADLXHelper and WinAPIs
"C:\Program Files\Microsoft Visual Studio\2022\Community\VC\Tools\MSVC\14.43.34808\bin\Hostx64\x64\cl.exe" /nologo /W3 /TC main.c "..\ADLX\SDK\ADLXHelper\Windows\C\ADLXHelper.c" "..\ADLX\SDK\platform\Windows\WinAPIs.c" /Fe:ADLXNativeTest.exe /I"..\ADLX\SDK\Include" /I"..\ADLX\SDK\ADLXHelper\Windows\C" /link kernel32.lib user32.lib

if %ERRORLEVEL% EQU 0 (
    echo.
    echo Build successful! ADLXNativeTest.exe created.
    echo.
    echo Running the test...
    echo.
    ADLXNativeTest.exe
) else (
    echo.
    echo Build failed!
    pause
)
