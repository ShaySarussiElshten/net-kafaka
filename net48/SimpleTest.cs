using System;
using System.Configuration;

namespace KafkaConsoleApp
{
    class SimpleTest
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🚀 .NET Framework 4.7.2 Test");
            Console.WriteLine("============================");
            
            // בדיקת קריאת הגדרות
            string kafkaServer = ConfigurationManager.AppSettings["KafkaBootstrapServers"] ?? "localhost:9092";
            string topic = ConfigurationManager.AppSettings["KafkaTopic"] ?? "test-topic";
            
            Console.WriteLine($"🔗 Kafka Server: {kafkaServer}");
            Console.WriteLine($"📋 Topic: {topic}");
            Console.WriteLine();
            
            Console.WriteLine("✅ הקוד עובד! .NET Framework 4.7.2 מותקן ופועל כמו שצריך");
            Console.WriteLine("📝 כדי להשתמש ב-Kafka, צריך להתקין את חבילות NuGet");
            Console.WriteLine();
            Console.WriteLine("לחץ Enter כדי לסגור...");
            Console.ReadLine();
        }
    }
}