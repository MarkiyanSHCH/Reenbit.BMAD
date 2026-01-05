using Aspire.Hosting.Azure;

var builder = DistributedApplication.CreateBuilder(args);

IResourceBuilder<AzureServiceBusResource> serviceBus = builder.AddAzureServiceBus("azure-service-bus")
    .RunAsEmulator();

serviceBus.AddServiceBusQueue("events");

IResourceBuilder<SqlServerServerResource> sql = builder.AddSqlServer("sql")
    .WithLifetime(ContainerLifetime.Persistent);

IResourceBuilder<SqlServerDatabaseResource> db = sql.AddDatabase("TestDb");

IResourceBuilder<ProjectResource> eventApi = builder.AddProject<Projects.Reenbit_BMAD_API>("event-api")
    .WithReference(serviceBus)
    .WithReference(db)
    .WaitFor(db)
    .WaitFor(serviceBus);

builder.AddAzureFunctionsProject<Projects.Reenbit_BMAD_FunctionApp>("event-app")
    .WithReference(serviceBus)
    .WithReference(db)
    .WaitFor(db)
    .WaitFor(serviceBus);

builder.AddNpmApp("event-ui", "../Reenbit.BMAD.UI")
    .WaitFor(eventApi);

builder.Build().Run();
