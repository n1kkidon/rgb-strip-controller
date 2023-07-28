using MQTTnet.Client;
using MQTTnet;

namespace ApiServices.Services;

public class MQTTPublisherService
{
    public IMqttClient Client { get; private set; }

    public MQTTPublisherService()
    {
        var factory = new MqttFactory();
        Client = factory.CreateMqttClient();
        var options = new MqttClientOptionsBuilder()
            .WithClientId(Guid.NewGuid().ToString())
            .WithTcpServer("cloud.qh-tek.com", 1883)
            .WithCleanSession()
            .Build();
        Client.ConnectedAsync += Client_ConnectedAsync;
        Client.DisconnectedAsync += Client_DisconnectedAsync;

        Client.ConnectAsync(options).Wait();
    }


    private Task Client_DisconnectedAsync(MqttClientDisconnectedEventArgs arg)
    {
        Console.WriteLine("Disconnected");
        return Task.CompletedTask;
    }

    private Task Client_ConnectedAsync(MqttClientConnectedEventArgs arg)
    {
        Console.WriteLine("Connected. You can now send messages.");
        return Task.CompletedTask;
    }

    public async Task DisconnectAsync() => await Client.DisconnectAsync();

    public async Task PublishMessageAsync(string message, string topic)
    {
        var msg = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(message)
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
            .Build();
        if (Client.IsConnected)
        {
            await Client.PublishAsync(msg);
            Console.WriteLine($"Message sent to topic \"{topic}\". {message}");
        }
    }
    public async Task PublishMessageAsync(byte[] message, string topic)
    {
        var msg = new MqttApplicationMessageBuilder()
            .WithTopic(topic)
            .WithPayload(message)
            .WithQualityOfServiceLevel(MQTTnet.Protocol.MqttQualityOfServiceLevel.AtLeastOnce)
            .Build();
        if (Client.IsConnected)
        {
            await Client.PublishAsync(msg);
            Console.WriteLine($"Message sent to topic \"{topic}\". {message.Length}");
        }
    }
}

