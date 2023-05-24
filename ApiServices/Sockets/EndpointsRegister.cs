namespace ApiServices.Sockets
{
    public static class EndpointsRegister
    {
        public static void RegisterSocketEndpoints(this WebApplication app)
        {
            app.UseRouting();
            app.MapHub<RgbService>("/rgbSocket");
        }
    }
}
