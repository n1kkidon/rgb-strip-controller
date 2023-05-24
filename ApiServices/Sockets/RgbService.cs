using Microsoft.AspNetCore.SignalR;
using System.Collections.Concurrent;

namespace ApiServices.Sockets;

public class RgbService : Hub
{
    private static readonly ConcurrentDictionary<string, string> ConnectedUsers = new();


    public override async Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        return base.OnDisconnectedAsync(exception);
    }

    public async Task ReceiveMessage(string message)
    {
        await Console.Out.WriteLineAsync(message);
    }
}
