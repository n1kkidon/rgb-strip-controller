using Microsoft.AspNetCore.SignalR;

namespace ApiServices.Sockets
{
    public static class EndpointsRegister
    {
        public static void RegisterSocketEndpoints(this WebApplication app)
        {
            app.UseRouting();
            app.MapHub<RgbService>("/rgbSocket");
        }

        public static void RegisterEndpointOptions(this ISignalRServerBuilder builder, IServiceCollection services)
        {
            builder.AddHubOptions<RgbService>(options =>
            {
                options.AddFilter<RgbServiceFilter>();
            });
            services.AddSingleton<RgbServiceFilter>();
        }
    }
}
