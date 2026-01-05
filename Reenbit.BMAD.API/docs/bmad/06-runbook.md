# Runbook

## Local Run (Recommended)
Use Aspire AppHost to start the API, Function App, Service Bus emulator, and SQL Server:

```bash
 dotnet run --project Reenbit.BMAD.AppHost
```

Prerequisites:
- .NET SDK (compatible with the solution)
- Docker (for SQL Server and Service Bus emulator)

## Manual Run (If not using AppHost)
1. Start SQL Server and apply scripts from `Reenbit.BMAD.Sql/SqlScripts/Bootstrap`.
2. Create a Service Bus namespace/queue named `events` (or use emulator).
3. Set required environment variables (below).
4. Run API:

```bash
 dotnet run --project Reenbit.BMAD.API
```

5. Run Function App:

```bash
 dotnet run --project Reenbit.BMAD.FunctionApp
```

## Environment Variables
API (`Reenbit.BMAD.API`):
- `ConnectionStrings__TestDb` = SQL connection string
- `ConnectionStrings__azure-service-bus` = Service Bus connection string
- `Cors__Url__0` = Allowed origin (optional)
- `ASPNETCORE_ENVIRONMENT` = `Development` (optional)

Function App (`Reenbit.BMAD.FunctionApp`):
- `ConnectionStrings__TestDb` = SQL connection string
- `ConnectionStrings__azure-service-bus` = Service Bus connection string
- `AzureWebJobsStorage` = Storage connection string (required by Functions runtime)
- `FUNCTIONS_WORKER_RUNTIME` = `dotnet-isolated`

## Smoke Test
1. POST an event:

```bash
curl -X POST http://localhost:5000/api/events \
  -H "Content-Type: application/json" \
  -d '{"userId":"u1","type":1,"description":"page view"}'
```

2. GET events:

```bash
curl http://localhost:5000/api/events
```
