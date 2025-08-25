@echo off
echo Rebuilding ADLX C# Wrapper...

REM Set up Visual Studio environment
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat" >nul 2>&1
if errorlevel 1 (
    call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat" >nul 2>&1
)

REM Rebuild the project
echo Building native DLL...
msbuild ADLXWrapper\ADLXWrapper.vcxproj /p:Configuration=Debug /p:Platform=x64 /t:Rebuild /v:minimal

if errorlevel 1 (
    echo Build failed!
    pause
    exit /b 1
)

echo Build completed successfully!
echo Copying DLL to test directory...
powershell -Command "Copy-Item ADLXWrapper\x64\Debug\ADLXWrapper.dll IADLXGPU2Test\"

echo Done!
