using Azure.Messaging.ServiceBus;
using Reenbit.BMAD.Core.Entities.Events;
using Reenbit.BMAD.Core.Internal.ServiceBus;
using Reenbit.BMAD.ServiceBus;
using Reenbit.BMAD.Sql.Common;
using Reenbit.BMAD.Sql.DataAccess.Events;
using Serilog;

namespace Reenbit.BMAD.API.Extensions
{
    internal static class HostingExtensions
    {
        public static WebApplicationBuilder Configure(this WebApplicationBuilder builder)
        {
            builder.Services.AddControllers()
                .AddJsonOptions(opts =>
                {
                    opts.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                });

            builder.Services.AddControllers();

            builder.Configuration.AddEnvironmentVariables();

            // Configure Serilog
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Configuration)
                .Enrich.FromLogContext()
                .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();

            // Configure Service Bus
            builder.Services.AddSingleton<ServiceBusClient>(_ =>
            {
                string connectionString = builder.Configuration.GetConnectionString("azure-service-bus")!;

                return new ServiceBusClient(connectionString);
            });

            return builder;
        }

        public static WebApplicationBuilder ConfigureCORS(this WebApplicationBuilder builder)
        {
            builder.Services.AddCors(options =>
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(builder.Configuration.GetSection("Cors:Url").Get<string[]>()!);
                    policy.AllowAnyMethod();
                    policy.AllowAnyHeader();
                }));

            return builder;
        }

        public static WebApplicationBuilder ConfigureSettings(this WebApplicationBuilder builder)
        {
            builder.Services.Configure<RouteOptions>(options => { options.LowercaseUrls = true; });

            return builder;
        }


        public static WebApplicationBuilder ConfigureServices(this WebApplicationBuilder builder)
        {
            // Database Contexts
            builder.Services.AddSingleton<IDbConnectionHandler, DbConnectionHandler>();

            // Services
            builder.Services.AddScoped<IEventsBoundary, EventsInteractor>();
            builder.Services.AddScoped<IServiceBusPublisher, ServiceBusPublisher>();


            return builder;
        }

        public static WebApplicationBuilder ConfigureRepositories(this WebApplicationBuilder builder)
        {
            // Repositories
            builder.Services.AddScoped<IEventsGateway, EventsRepository>();

            return builder;
        }

        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            app.UseHttpsRedirection();

            app.UseCors();

            app.UseAuthorization();

            app.MapControllers();


            app.Run();

            return app;
        }
    }
}
