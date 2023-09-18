using ApiServices.Controllers;
using ApiServices.Services;
using Microsoft.AspNetCore.SignalR;

namespace ApiServices.Sockets
{
    public class RgbServiceFilter : IHubFilter
    {
        private readonly HttpService _http;

        public RgbServiceFilter(HttpService http)
        {
            _http = http;
        }

        public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext invocationContext,
        Func<HubInvocationContext, ValueTask<object?>> next)
        {
            var httpContext = invocationContext.Context.GetHttpContext() ??
                throw new HubException("Illegal access to RgbService with null httpContext");
            if (!RgbController.CheckIfIpIsLocalNetwork(httpContext, _http))
            {
                throw new HubException($"Illegal access to RgbService from {HttpService.GetRemoteIp(httpContext)}");
            }
            return await next(invocationContext);
        }

        public async Task OnConnectedAsync(HubLifetimeContext context, Func<HubLifetimeContext, Task> next)
        {
            var httpContext = context.Context.GetHttpContext() ??
                throw new HubException("Illegal access to RgbService with null httpContext");
            if (!RgbController.CheckIfIpIsLocalNetwork(httpContext, _http))
            {
                throw new HubException($"Illegal access to RgbService from {HttpService.GetRemoteIp(httpContext)}");
            }
            await next(context);
        }

    }
}
