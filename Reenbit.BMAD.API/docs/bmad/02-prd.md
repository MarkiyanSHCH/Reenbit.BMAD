# Product Requirements Document (PRD)

## Objective
Provide a lightweight event tracking service that accepts events from clients, queues them, and persists them reliably in SQL Server while keeping API latency low.

## Personas
- **Client Integrator**: wants a simple HTTP API to post events.
- **Data Consumer**: needs to fetch stored events for downstream use.
- **Developer/Ops**: runs the stack locally and in hosted environments.

## Functional Requirements
- **FR-1**: `POST /api/events` accepts `userId`, `type`, and `description` and responds `200 OK` on success or `422` with problem details on validation or processing errors.
- **FR-2**: Event submissions publish a message to the `events` queue in Azure Service Bus.
- **FR-3**: A Function App consumes the queue messages and inserts events into SQL Server via stored procedures.
- **FR-4**: `GET /api/events` returns a list of stored events.
- **FR-5**: Event types are constrained to `PageView`, `Click`, and `Purchase` (aligned with `EventTypes` table).
- **FR-6**: CORS can be configured via configuration to allow front-end clients.
- **FR-7**: Logging is emitted to console (Serilog) for both API and Function App.

## Non-Functional Requirements
- **Reliability**: Queue-based processing should tolerate transient failures without blocking the API.
- **Performance**: API should return quickly after queuing events.
- **Consistency**: Events become visible to readers once the Function App writes them.
- **Security**: No auth in current scope; transport is expected to be HTTPS.
- **Operability**: Local development via Aspire AppHost with a service bus emulator and SQL container.

## Out of Scope
- Authentication/authorization.
- Filtering, pagination, or analytics on events.
- Multi-tenant isolation or data retention policies.

## Success Metrics
- Events are successfully persisted for all valid submissions.
- No blocking DB calls in the API request path for writes.
- Local environment can be spun up with a single command.
