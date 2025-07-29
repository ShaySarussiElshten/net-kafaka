@echo off
echo Building KafkaConsoleApp...

REM Try to find MSBuild
set MSBUILD_PATH=""

REM Check for Visual Studio 2022
if exist "C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Professional\MSBuild\Current\Bin\MSBuild.exe"
) else if exist "C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Community\MSBuild\Current\Bin\MSBuild.exe"
) else if exist "C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe" (
    set MSBUILD_PATH="C:\Program Files\Microsoft Visual Studio\2022\Enterprise\MSBuild\Current\Bin\MSBuild.exe"
)

REM Check for Build Tools
if %MSBUILD_PATH%=="" (
    if exist "C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe" (
        set MSBUILD_PATH="C:\Program Files (x86)\Microsoft Visual Studio\2022\BuildTools\MSBuild\Current\Bin\MSBuild.exe"
    )
)

REM Use dotnet if available
if %MSBUILD_PATH%=="" (
    echo Trying with dotnet build...
    dotnet build KafkaConsoleApp.csproj -c Debug
    if %ERRORLEVEL% EQU 0 (
        echo Build successful with dotnet!
        goto :end
    )
)

REM Use MSBuild if found
if not %MSBUILD_PATH%=="" (
    echo Using MSBuild: %MSBUILD_PATH%
    %MSBUILD_PATH% KafkaConsoleApp.csproj /p:Configuration=Debug /p:Platform="Any CPU"
    if %ERRORLEVEL% EQU 0 (
        echo Build successful with MSBuild!
    ) else (
        echo Build failed!
    )
) else (
    echo MSBuild not found. Please install Visual Studio 2022 or Build Tools.
)

:end
pause 