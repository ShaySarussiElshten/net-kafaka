using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsoleApp
{
    class TestProgram
    {
        private static readonly string BootstrapServers = ConfigurationManager.AppSettings["KafkaBootstrapServers"] ?? "localhost:9092";
        private static readonly string TopicName = ConfigurationManager.AppSettings["KafkaTopic"] ?? "test-topic";

        static void Main(string[] args)
        {
            Console.WriteLine("🚀 Kafka Console App - .NET Framework 4.7.2 Test");
            Console.WriteLine("=================================================");
            Console.WriteLine($"🔗 Kafka Server: {BootstrapServers}");
            Console.WriteLine($"📋 Topic: {TopicName}");
            Console.WriteLine();

            Console.WriteLine("✅ System is working!");
            Console.WriteLine("📝 .NET Framework 4.7.2 installed and running");
            Console.WriteLine("⚙️  Settings read from App.config");
            Console.WriteLine();
            
            Console.WriteLine("🔧 To use Kafka, you need:");
            Console.WriteLine("   1. Install NuGet CLI");
            Console.WriteLine("   2. Run: nuget restore");
            Console.WriteLine("   3. Build with MSBuild");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Show settings");
                Console.WriteLine("2. Memory check");
                Console.WriteLine("3. Exit");
                Console.Write("Your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ShowSettings();
                        break;
                    case "2":
                        CheckMemory();
                        break;
                    case "3":
                        Console.WriteLine("Goodbye! 👋");
                        return;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }

                Console.WriteLine();
            }
        }

        private static void ShowSettings()
        {
            Console.WriteLine("📋 Settings from App.config:");
            Console.WriteLine($"   🔗 Kafka Server: {BootstrapServers}");
            Console.WriteLine($"   📋 Topic: {TopicName}");
            Console.WriteLine($"   🕐 Current time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        private static void CheckMemory()
        {
            Console.WriteLine("💾 Memory check:");
            long memory = GC.GetTotalMemory(false);
            Console.WriteLine($"   📊 Memory in use: {memory:N0} bytes");
            Console.WriteLine($"   🔄 Generation 0: {GC.CollectionCount(0)}");
            Console.WriteLine($"   🔄 Generation 1: {GC.CollectionCount(1)}");
            Console.WriteLine($"   🔄 Generation 2: {GC.CollectionCount(2)}");
        }
    }
}