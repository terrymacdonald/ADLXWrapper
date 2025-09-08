@echo off
echo Rebuilding ADLX C# Wrapper...

REM Set up Visual Studio environment
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat" >nul 2>&1
if errorlevel 1 (
    call "C:\Program Files (x86)\Microsoft Visual Studio\2019\Community\Common7\Tools\VsDevCmd.bat" >nul 2>&1
)

REM Rebuild the native C++ project
echo Building native DLL...
msbuild ADLXWrapper\ADLXWrapper.vcxproj /p:Configuration=Debug /p:Platform=x64 /t:Rebuild /v:minimal
if errorlevel 1 (
    echo Native C++ build failed!
    pause
    exit /b 1
)
echo Native C++ build completed successfully!

REM Ensure the target directories exist for copying
if not exist "ADLXWrapper.Bindings\bin\Debug\net8.0\" mkdir "ADLXWrapper.Bindings\bin\Debug\net8.0\"
if not exist "ADLXWrapper.Tests\bin\Debug\net8.0\" mkdir "ADLXWrapper.Tests\bin\Debug\net8.0\"

REM Copy generated C# binding files to ADLXWrapper.Bindings and ADLXWrapper.Tests projects
echo Copying C# binding files...
xcopy /Y ADLXWrapper\cs_bindings\*.cs ADLXWrapper.Bindings\cs_bindings\ >nul
if errorlevel 1 (
    echo Failed to copy C# binding files!
    pause
    exit /b 1
)


echo Copying C# binding files...
xcopy /Y ADLXWrapper\cs_bindings\*.cs ADLXWrapper.Tests\cs_bindings\ >nul
if errorlevel 1 (
    echo Failed to copy C# binding files!
    pause
    exit /b 1
)



REM Copy compiled ADLXWrapper.dll to ADLXWrapper.Bindings and ADLXWrapper.Tests projects
echo Copying ADLXWrapper.dll...
copy ADLXWrapper\x64\Debug\ADLXWrapper.dll ADLXWrapper.Bindings\bin\Debug\net8.0\ >nul
if errorlevel 1 (
    echo Failed to copy ADLXWrapper.dll to Bindings project!
    pause
    exit /b 1
)

copy ADLXWrapper\x64\Debug\ADLXWrapper.dll ADLXWrapper.Tests\bin\Debug\net8.0\ >nul
if errorlevel 1 (
    echo Failed to copy ADLXWrapper.dll to Tests project!
    pause
    exit /b 1
)

REM Build the ADLXWrapper.Bindings project
echo Building ADLXWrapper.Bindings project...
dotnet build ADLXWrapper.Bindings\ADLXWrapper.Bindings.csproj -c Debug -f net8.0
if errorlevel 1 (
    echo ADLXWrapper.Bindings build failed!
    pause
    exit /b 1
)
echo ADLXWrapper.Bindings build completed successfully!

REM Build and run unit tests
echo Building and running unit tests...
dotnet test ADLXWrapper.Tests\ADLXWrapper.Tests.csproj -c Debug -f net8.0
if errorlevel 1 (
    echo Unit tests failed!
    pause
    exit /b 1
)

echo All builds and tests completed successfully!
pause
