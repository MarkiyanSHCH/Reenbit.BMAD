# Tradeoffs and Decisions

## Async ingestion via Service Bus
- **Decision**: API writes are queued to Service Bus and persisted by a Function App.
- **Alternatives**: Direct DB writes in the API.
- **Tradeoff**: Adds infrastructure and eventual consistency but reduces API latency and isolates failures.

## Stored procedures with Dapper
- **Decision**: Use Dapper and stored procedures for reads/writes.
- **Alternatives**: Entity Framework Core.
- **Tradeoff**: More manual SQL management but precise control and lightweight runtime.

## Enum-based event types
- **Decision**: Maintain `EventType` enum synchronized with `EventTypes` table.
- **Alternatives**: Fully dynamic event types.
- **Tradeoff**: Strong typing but requires coordination when adding new types.

## Aspire AppHost for local dev
- **Decision**: Use AppHost to run Service Bus emulator and SQL locally.
- **Alternatives**: Manual Docker or direct cloud resources.
- **Tradeoff**: Faster local setup with Aspire, but requires Docker and Aspire tooling.
