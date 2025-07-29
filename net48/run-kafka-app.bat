@echo off
echo 🚀 Kafka Console App - .NET Framework 4.8
echo ==========================================

echo 📦 Starting Kafka with Docker...
docker-compose -f kafka-docker-compose.yml up -d

echo ⏳ Waiting for Kafka to start...
timeout /t 10 /nobreak > nul

echo 🔧 Building the project...
call "C:\Program Files\Microsoft Visual Studio\2022\Community\Common7\Tools\VsDevCmd.bat" && msbuild KafkaConsoleApp.csproj /p:Configuration=Debug

if %ERRORLEVEL% NEQ 0 (
    echo ❌ Error building project
    pause
    exit /b 1
)

echo 🎯 Running the application...
bin\Debug\KafkaConsoleApp.exe

echo 🛑 Stopping Kafka...
docker-compose -f kafka-docker-compose.yml down

pause