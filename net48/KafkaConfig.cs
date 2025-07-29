using System.Configuration;

namespace KafkaConsoleApp
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; }
        public string TopicName { get; }
        public string ConsumerGroup { get; }

        public KafkaConfig()
        {
            BootstrapServers = ConfigurationManager.AppSettings["KafkaBootstrapServers"] ?? "localhost:9092";
            TopicName = ConfigurationManager.AppSettings["KafkaTopic"] ?? "test-topic";
            ConsumerGroup = ConfigurationManager.AppSettings["KafkaConsumerGroup"] ?? "console-consumer-group";
        }
    }
}