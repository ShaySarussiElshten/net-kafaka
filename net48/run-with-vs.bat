@echo off
echo 🚀 Opening Kafka Console App in Visual Studio 2022
echo ================================================

echo 📦 Starting Kafka with Docker...
docker-compose -f kafka-docker-compose.yml up -d

echo ⏳ Waiting for Kafka to start...
timeout /t 5 /nobreak > nul

echo 🎯 Opening Visual Studio 2022...
start KafkaConsoleApp.sln

echo ✅ Ready! You can now:
echo    1. Build and run from Visual Studio
echo    2. Set breakpoints and debug
echo    3. Use IntelliSense and all VS features
echo.
echo 💡 To stop Kafka later, run:
echo    docker-compose -f kafka-docker-compose.yml down
echo.
pause