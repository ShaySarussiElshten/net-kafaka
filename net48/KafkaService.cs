using System;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;

namespace KafkaConsoleApp
{
    public class KafkaService : IDisposable
    {
        private readonly string _bootstrapServers;
        private readonly string _topicName;
        private bool _disposed = false;

        public KafkaService(string bootstrapServers, string topicName)
        {
            _bootstrapServers = bootstrapServers;
            _topicName = topicName;
        }

        public async Task<bool> SendMessageAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                Console.WriteLine("❌ Message cannot be empty");
                return false;
            }

            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                Acks = Acks.All,
                MessageSendMaxRetries = 3,
                MessageTimeoutMs = 30000,
                RequestTimeoutMs = 30000,
                SocketTimeoutMs = 30000
            };

            try
            {
                using (var producer = new ProducerBuilder<string, string>(config).Build())
                {
                    var key = $"key-{DateTime.Now:yyyy-MM-dd-HH-mm-ss-fff}";
                    
                    var result = await producer.ProduceAsync(_topicName, new Message<string, string>
                    {
                        Key = key,
                        Value = message,
                        Timestamp = new Timestamp(DateTime.UtcNow)
                    });

                    Console.WriteLine($"✅ Message sent successfully!");
                    Console.WriteLine($"   📍 Partition: {result.Partition}");
                    Console.WriteLine($"   📊 Offset: {result.Offset}");
                    Console.WriteLine($"   🔑 Key: {key}");
                    Console.WriteLine($"   💬 Message: {message}");
                    
                    return true;
                }
            }
            catch (ProduceException<string, string> ex)
            {
                Console.WriteLine($"❌ Kafka produce error: {ex.Error.Reason}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Unexpected error: {ex.Message}");
                return false;
            }
        }

        public void StartConsumer(CancellationToken cancellationToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "console-consumer-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            try
            {
                using (var consumer = new ConsumerBuilder<string, string>(config).Build())
                {
                    consumer.Subscribe(_topicName);
                    Console.WriteLine($"🎧 Starting to listen for messages on topic: {_topicName}");
                    Console.WriteLine("Press Enter to stop...");
                    Console.WriteLine(new string('=', 50));

                    while (!cancellationToken.IsCancellationRequested)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume(TimeSpan.FromSeconds(1));

                            if (consumeResult != null)
                            {
                                var message = consumeResult.Message;
                                var timestamp = consumeResult.Message.Timestamp.UtcDateTime;

                                Console.WriteLine($"📨 New message received:");
                                Console.WriteLine($"   🕐 Time: {timestamp:yyyy-MM-dd HH:mm:ss} UTC");
                                Console.WriteLine($"   📍 Partition: {consumeResult.Partition}");
                                Console.WriteLine($"   📊 Offset: {consumeResult.Offset}");
                                Console.WriteLine($"   🔑 Key: {message.Key ?? "No key"}");
                                Console.WriteLine($"   💬 Message: {message.Value}");
                                Console.WriteLine("   " + new string('-', 40));

                                consumer.Commit(consumeResult);
                            }
                        }
                        catch (ConsumeException ex)
                        {
                            Console.WriteLine($"⚠️ Error receiving message: {ex.Error.Reason}");
                        }
                    }

                    consumer.Close();
                    Console.WriteLine("🔌 Consumer closed");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ General consumer error: {ex.Message}");
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed && disposing)
            {
                // Cleanup resources if needed
                _disposed = true;
            }
        }
    }
}