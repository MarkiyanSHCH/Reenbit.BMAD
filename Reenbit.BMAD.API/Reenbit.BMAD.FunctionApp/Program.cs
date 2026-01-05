using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Reenbit.BMAD.Core.Entities.Events;
using Reenbit.BMAD.Core.Internal.ServiceBus;
using Reenbit.BMAD.ServiceBus;
using Reenbit.BMAD.Sql.Common;
using Reenbit.BMAD.Sql.DataAccess.Events;
using Serilog;

var builder = FunctionsApplication.CreateBuilder(args);

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .Enrich.FromLogContext()
    .WriteTo.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
    .CreateLogger();

builder.Services.AddSingleton<IDbConnectionHandler, DbConnectionHandler>();
builder.Services.AddScoped<IEventsGateway, EventsRepository>();

builder.ConfigureFunctionsWebApplication();

builder.Build().Run();
