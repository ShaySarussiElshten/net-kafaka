using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;

namespace KafkaConsoleApp
{
    class Program
    {
        private static IConfiguration _configuration = null!;
        private static string BootstrapServers => _configuration["KafkaBootstrapServers"] ?? "localhost:9092";
        private static string TopicName => _configuration["KafkaTopic"] ?? "test-topic";

        static async Task Main(string[] args)
        {
            // הגדרת Configuration עבור .NET 7
            _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            Console.WriteLine("🚀 Kafka Console App - .NET 7");
            Console.WriteLine("==============================");
            Console.WriteLine($"🔗 Kafka Server: {BootstrapServers}");
            Console.WriteLine($"📋 Topic: {TopicName}");
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("בחר פעולה:");
                Console.WriteLine("1. שלח הודעה (Producer)");
                Console.WriteLine("2. קבל הודעות (Consumer)");
                Console.WriteLine("3. יציאה");
                Console.Write("הבחירה שלך: ");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        await ProduceMessage();
                        break;
                    case "2":
                        ConsumeMessages();
                        break;
                    case "3":
                        Console.WriteLine("להתראות! 👋");
                        return;
                    default:
                        Console.WriteLine("בחירה לא תקינה, נסה שוב.");
                        break;
                }

                Console.WriteLine();
            }
        }

        /// <summary>
        /// שליחת הודעה לקפקא (Producer)
        /// </summary>
        private static async Task ProduceMessage()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = BootstrapServers,
                Acks = Acks.All, // ודא שהודעה נשמרה בכל הברוקרים
                MessageSendMaxRetries = 3,
                MessageTimeoutMs = 5000
            };

            try
            {
                using var producer = new ProducerBuilder<string, string>(config).Build();
                
                Console.Write("הכנס הודעה לשליחה: ");
                var message = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(message))
                {
                    Console.WriteLine("הודעה ריקה, מבטל...");
                    return;
                }

                // יצירת מפתח ייחודי להודעה
                var key = $"key-{DateTime.Now:yyyy-MM-dd-HH-mm-ss-fff}";

                // שליחת ההודעה
                var result = await producer.ProduceAsync(TopicName, new Message<string, string>
                {
                    Key = key,
                    Value = message,
                    Timestamp = new Timestamp(DateTime.UtcNow)
                });

                Console.WriteLine($"✅ הודעה נשלחה בהצלחה!");
                Console.WriteLine($"   📍 Partition: {result.Partition}");
                Console.WriteLine($"   📊 Offset: {result.Offset}");
                Console.WriteLine($"   🔑 Key: {key}");
                Console.WriteLine($"   💬 Message: {message}");
            }
            catch (ProduceException<string, string> ex)
            {
                Console.WriteLine($"❌ שגיאה בשליחת הודעה: {ex.Error.Reason}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ שגיאה כללית: {ex.Message}");
            }
        }

        /// <summary>
        /// קבלת הודעות מקפקא (Consumer)
        /// </summary>
        private static void ConsumeMessages()
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = BootstrapServers,
                GroupId = "console-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false // נאשר ידנית שקיבלנו את ההודעה
            };

            try
            {
                using var consumer = new ConsumerBuilder<string, string>(config).Build();
                
                consumer.Subscribe(TopicName);

                Console.WriteLine($"🎧 מתחיל להאזין להודעות בנושא: {TopicName}");
                Console.WriteLine("לחץ על Enter כדי להפסיק...");
                Console.WriteLine(new string('=', 50));

                var cts = new CancellationTokenSource();

                // Task שמאזין לקלט מהמשתמש כדי להפסיק
                var keyTask = Task.Run(() =>
                {
                    Console.ReadLine();
                    cts.Cancel();
                });

                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(TimeSpan.FromSeconds(1));

                            if (consumeResult != null)
                            {
                                var message = consumeResult.Message;
                                var timestamp = consumeResult.Message.Timestamp.UtcDateTime;

                                Console.WriteLine($"📨 הודעה חדשה התקבלה:");
                                Console.WriteLine($"   🕐 זמן: {timestamp:yyyy-MM-dd HH:mm:ss} UTC");
                                Console.WriteLine($"   📍 Partition: {consumeResult.Partition}");
                                Console.WriteLine($"   📊 Offset: {consumeResult.Offset}");
                                Console.WriteLine($"   🔑 Key: {message.Key ?? "ללא מפתח"}");
                                Console.WriteLine($"   💬 Message: {message.Value}");
                                Console.WriteLine("   " + new string('-', 40));

                                // אישור שקיבלנו את ההודעה
                                consumer.Commit(consumeResult);
                            }
                        }
                        catch (ConsumeException ex)
                        {
                            Console.WriteLine($"⚠️ שגיאה בקבלת הודעה: {ex.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("🛑 הפסקת האזנה על ידי המשתמש");
                }
                finally
                {
                    consumer.Close();
                    Console.WriteLine("🔌 Consumer נסגר");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ שגיאה כללית ב-Consumer: {ex.Message}");
            }
        }
    }
} 