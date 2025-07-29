using System;
using System.Configuration;
using System.Threading;

namespace KafkaConsoleApp
{
    class KafkaDemo
    {
        private static readonly string BootstrapServers = ConfigurationManager.AppSettings["KafkaBootstrapServers"] ?? "localhost:9092";
        private static readonly string TopicName = ConfigurationManager.AppSettings["KafkaTopic"] ?? "test-topic";

        static void Main(string[] args)
        {
            Console.WriteLine("🚀 Kafka Console App - .NET Framework 4.7.2 Demo");
            Console.WriteLine("================================================");
            Console.WriteLine($"🔗 Kafka Server: {BootstrapServers}");
            Console.WriteLine($"📋 Topic: {TopicName}");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Send message (Producer Demo)");
                Console.WriteLine("2. Receive messages (Consumer Demo)");
                Console.WriteLine("3. Exit");
                Console.Write("Your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        ProduceMessageDemo();
                        break;
                    case "2":
                        ConsumeMessagesDemo();
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

        private static void ProduceMessageDemo()
        {
            Console.Write("Enter message to send: ");
            var message = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("Empty message, canceling...");
                return;
            }

            var key = $"key-{DateTime.Now:yyyy-MM-dd-HH-mm-ss-fff}";

            Console.WriteLine("📤 Simulating Kafka Producer...");
            Thread.Sleep(500); // Simulate network delay

            Console.WriteLine($"✅ Message sent successfully!");
            Console.WriteLine($"   📍 Partition: 0");
            Console.WriteLine($"   📊 Offset: {new Random().Next(1000, 9999)}");
            Console.WriteLine($"   🔑 Key: {key}");
            Console.WriteLine($"   💬 Message: {message}");
            Console.WriteLine($"   🕐 Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss}");
        }

        private static void ConsumeMessagesDemo()
        {
            Console.WriteLine($"🎧 Simulating Kafka Consumer for topic: {TopicName}");
            Console.WriteLine("Press Enter to stop...");
            Console.WriteLine(new string('=', 50));

            var random = new Random();
            var messageCount = 0;

            while (!Console.KeyAvailable)
            {
                Thread.Sleep(2000); // Simulate message interval

                messageCount++;
                var key = $"demo-key-{messageCount}";
                var message = $"Demo message #{messageCount} from Kafka";
                var partition = random.Next(0, 3);
                var offset = random.Next(1000, 9999);

                Console.WriteLine($"📨 New message received:");
                Console.WriteLine($"   🕐 Time: {DateTime.Now:yyyy-MM-dd HH:mm:ss} UTC");
                Console.WriteLine($"   📍 Partition: {partition}");
                Console.WriteLine($"   📊 Offset: {offset}");
                Console.WriteLine($"   🔑 Key: {key}");
                Console.WriteLine($"   💬 Message: {message}");
                Console.WriteLine("   " + new string('-', 40));

                if (messageCount >= 5) break; // Stop after 5 demo messages
            }

            Console.ReadLine(); // Clear any pending input
            Console.WriteLine("🛑 Consumer stopped");
        }
    }
}