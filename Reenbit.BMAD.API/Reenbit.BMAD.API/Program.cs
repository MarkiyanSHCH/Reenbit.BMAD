using Reenbit.BMAD.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var app = builder
    .Configure()
    .ConfigureCORS()
    .ConfigureSettings()
    .ConfigureServices()
    .ConfigureRepositories()
    .Build()
    .ConfigurePipeline();

app.Run();
