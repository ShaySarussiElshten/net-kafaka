using System;
using System.Configuration;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsoleApp
{
    class Program
    {
        private static readonly KafkaConfig _config = new KafkaConfig();
        private static readonly KafkaService _kafkaService = new KafkaService(_config.BootstrapServers, _config.TopicName);

        static async Task Main(string[] args)
        {
            Console.WriteLine("🚀 Kafka Console App - .NET Framework 4.8");
            Console.WriteLine("==========================================");
            Console.WriteLine($"🔗 Kafka Server: {_config.BootstrapServers}");
            Console.WriteLine($"📋 Topic: {_config.TopicName}");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Choose action:");
                Console.WriteLine("1. Send message (Producer)");
                Console.WriteLine("2. Receive messages (Consumer)");
                Console.WriteLine("3. Exit");
                Console.Write("Your choice: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await SendMessage();
                        break;
                    case "2":
                        ReceiveMessages();
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

        private static async Task SendMessage()
        {
            Console.Write("Enter message to send: ");
            var message = Console.ReadLine();
            await _kafkaService.SendMessageAsync(message);
        }

        private static void ReceiveMessages()
        {
            var cts = new CancellationTokenSource();

            var keyTask = Task.Run(() =>
            {
                Console.ReadLine();
                cts.Cancel();
            });

            try
            {
                _kafkaService.StartConsumer(cts.Token);
            }
            catch (OperationCanceledException)
            {
                Console.WriteLine("🛑 Listening stopped by user");
            }
        }
    }
} 