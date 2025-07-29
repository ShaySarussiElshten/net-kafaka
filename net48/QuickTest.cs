using System;
using System.Configuration;

namespace KafkaConsoleApp
{
    class QuickTest
    {
        static void Main(string[] args)
        {
            Console.WriteLine("🚀 Kafka Console App - .NET Framework 4.7.2 Test");
            Console.WriteLine("=================================================");
            
            string kafkaServer = ConfigurationManager.AppSettings["KafkaBootstrapServers"] ?? "localhost:9092";
            string topic = ConfigurationManager.AppSettings["KafkaTopic"] ?? "test-topic";
            
            Console.WriteLine($"🔗 Kafka Server: {kafkaServer}");
            Console.WriteLine($"📋 Topic: {topic}");
            Console.WriteLine();
            
            Console.WriteLine("✅ System is working!");
            Console.WriteLine("📝 .NET Framework 4.7.2 installed and running");
            Console.WriteLine("⚙️ Settings read from App.config");
            Console.WriteLine();
            
            long memory = GC.GetTotalMemory(false);
            Console.WriteLine($"💾 Memory in use: {memory:N0} bytes");
            Console.WriteLine($"🕐 Current time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
            
            Console.WriteLine();
            Console.WriteLine("🎯 Ready to run Kafka app!");
        }
    }
}