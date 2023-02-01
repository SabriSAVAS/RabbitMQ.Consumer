using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

//Bağlantı Oluşturma
ConnectionFactory factory = new();
factory.Uri = new("amqps://smlmmjnn:TlCwjz1TsWxldEMzXgkm_wNOnS5-0g6g@hawk.rmq.cloudamqp.com/smlmmjnn");

//Bağlantı Aktifleştirme

using IConnection connection = factory.CreateConnection();
using IModel channel = connection.CreateModel();


//Queue Oluşturma

channel.QueueDeclare(queue: "example-queue", exclusive: false);// Consumer' da kuyruk publisher daki ile birebir aynı yapıladırmada tanımlanmalıdır.

//Queue Mesaj Okuma


EventingBasicConsumer consumer = new(channel);
channel.BasicConsume(queue: "example-queue", autoAck: false, consumer: consumer);
consumer.Received += (sender, e) =>
{
    //Kuyruğa gelen mesajın işlendiği yerdir.
    //e.Body : Kuyruktaki mesajın verisini bütünsel olarak getirecektir.
    //e.Body.Span veya e.Body.ToArray() : Kuyruktaki mesajın byte verisini getirecektir.
    Console.WriteLine(Encoding.UTF8.GetString(e.Body.Span));
};
Console.Read();