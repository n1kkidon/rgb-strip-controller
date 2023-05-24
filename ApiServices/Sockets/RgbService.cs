using ApiServices.Services;
using Microsoft.AspNetCore.SignalR;

namespace ApiServices.Sockets;

public class RgbService : Hub
{
    private readonly MQTTPublisherService _publisher;
    private readonly RGB _rgb;
    public RgbService(MQTTPublisherService publisher, RGB rgb)
    {
        _publisher = publisher;
        _rgb = rgb;
        _publisher.PublishMessageAsync(RGB.firstLoginMsg, RGB.Topic).Wait();
        _publisher.PublishMessageAsync(RGB.secondLoginMsg, RGB.Topic).Wait();
        _publisher.PublishMessageAsync(RGB.LoginMsg3, RGB.Topic).Wait();
    }
    public override async Task OnConnectedAsync()
    {
        try
        {
            Console.WriteLine($"Client from {HttpService.GetRemoteIp(Context.GetHttpContext())} connected to Rgb service.");
            Clients.Caller.SendAsync("onLogin", _rgb.GetHex()).Wait();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error on connecting client: {ex.Message}");
        }


        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        Console.WriteLine($"Client from {HttpService.GetRemoteIp(Context.GetHttpContext())} connected to Rgb service.");
        return base.OnDisconnectedAsync(exception);
    }

    public async Task ReceiveMessage(string message)
    {
        await Clients.Others.SendAsync("SendMessage", message);
        await Console.Out.WriteLineAsync(message);
        _rgb.SetRGB(message);
        await _publisher.PublishMessageAsync(_rgb.WithColors, RGB.Topic);
        await Console.Out.WriteLineAsync($"RGB {_rgb.Red}, {_rgb.Green}, {_rgb.Blue}");
    }

    public async Task TurnOn(string message)
    {
        await _publisher.PublishMessageAsync(RGB.on, RGB.Topic);
        RGB.LED_IS_ON = true;
    }
    public async Task TurnOff(string message)
    {
        await _publisher.PublishMessageAsync(RGB.off, RGB.Topic);
        RGB.LED_IS_ON = false;
    }
}
