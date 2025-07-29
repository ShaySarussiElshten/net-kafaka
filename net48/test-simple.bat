@echo off
echo 🧪 Simple Test - .NET Framework 4.8
echo ===================================

echo 🔧 Building simple test code...
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat" && csc /target:exe /out:TestProgram.exe /reference:System.Configuration.dll TestProgram.cs

if %ERRORLEVEL% NEQ 0 (
    echo ❌ Compilation error
    pause
    exit /b 1
)

echo 🎯 Running the test...
TestProgram.exe

pause