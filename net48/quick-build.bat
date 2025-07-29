@echo off
echo Quick Build Script for KafkaConsoleApp
echo =====================================

REM Clean previous build
echo Cleaning previous build...
if exist "bin\Debug\KafkaConsoleApp.exe" del "bin\Debug\KafkaConsoleApp.exe"
if exist "bin\Debug\KafkaConsoleApp.pdb" del "bin\Debug\KafkaConsoleApp.pdb"

REM Try Developer Command Prompt approach
echo Attempting to build with Developer Command Prompt...
call "C:\Program Files\Microsoft Visual Studio\2022\Professional\Common7\Tools\VsDevCmd.bat" 2>nul
if %ERRORLEVEL% EQU 0 goto build_with_msbuild

call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat" 2>nul
if %ERRORLEVEL% EQU 0 goto build_with_msbuild

call "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\Common7\Tools\VsDevCmd.bat" 2>nul
if %ERRORLEVEL% EQU 0 goto build_with_msbuild

echo Visual Studio 2022 not found in standard locations.
echo Please run this from a Developer Command Prompt.
goto end

:build_with_msbuild
echo Building project...
msbuild KafkaConsoleApp.csproj /p:Configuration=Debug /p:Platform="Any CPU" /verbosity:minimal

if exist "bin\Debug\KafkaConsoleApp.exe" (
    echo.
    echo ✅ Build successful! 
    echo Executable created at: bin\Debug\KafkaConsoleApp.exe
    echo.
    echo You can now run the application from Visual Studio 2022.
) else (
    echo.
    echo ❌ Build failed! Check the error messages above.
)

:end
echo.
pause 