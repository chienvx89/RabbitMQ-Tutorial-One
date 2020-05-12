using RabbitMQ.Client;
using System;
using System.Text;

namespace TestConnectRabbitMQ.Sent
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            {
                using var channel = connection.CreateModel();

                channel.QueueDeclare(queue: "hello",
                                        durable: false, //True: Queue vẫn tồn tại nếu nhưng RabitMQ khởi động lại
                                        exclusive: false, //Được sử dụng bởi chỉ một connection và queue sẽ bị xóa khi connection đó kết thúc
                                        autoDelete: false, //Queue sẽ bị xóa nếu như consumer cuối cùng hủy subscribe
                                        arguments: null);
                Console.WriteLine("Input string to Queue: ");
                string message = Console.ReadLine();
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(exchange: "",
                                     routingKey: "hello",
                                     basicProperties: null,
                                     body: body);

                Console.WriteLine(" [x] Sent {0}", message);
                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
            }
        }
    }
}