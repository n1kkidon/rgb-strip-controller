using ApiServices.Sockets;
using Microsoft.AspNetCore.HttpOverrides;

namespace ApiServices;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        //var policyName = "defaultCorsPolicy";
        //builder.Services.AddCors(options =>
        //{
        //    options.AddPolicy(policyName, builder =>
        //    {
        //        builder.WithOrigins("http://localhost:4200") // the Angular app url
        //            .AllowAnyMethod()
        //            .AllowAnyHeader()
        //            .AllowCredentials();
        //    });
        //});


        builder.Services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = null;
                //options.JsonSerializerOptions.IgnoreNullValues = true;
                
            });
        builder.Services.Configure<ForwardedHeadersOptions>(options =>
        {
            options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
        });

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        builder.Services.AddSignalR();


        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        //app.UseCors(policyName);
        //app.UseHttpsRedirection();
        app.UseAuthorization();
        app.UseForwardedHeaders();
        app.MapControllers();
        app.RegisterSocketEndpoints();
        app.Run();
    }
}
