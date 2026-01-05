# Project Brief: Reenbit.BMAD Event Tracking

## Summary
The solution provides an event-ingestion API that publishes events to an Azure Service Bus queue and a Function App that persists those events into SQL Server. The API also supports querying stored events. This keeps ingestion responsive while database writes are handled asynchronously.

## Problem
Client applications need a simple, reliable way to record user events (page views, clicks, purchases). Writes should be resilient to transient database issues and decoupled from the request path.

## Goals
- Accept event submissions via a REST API.
- Publish events to a queue for asynchronous processing.
- Persist events in SQL Server with a stable schema.
- Provide a read endpoint to fetch stored events.
- Support local development with an Aspire-based AppHost.

## Non-Goals
- User authentication/authorization.
- Analytics dashboards or reporting.
- Data export, retention policies, or archival.
- Real-time streaming to other systems.

## Users and Stakeholders
- Client apps posting events.
- Internal developers maintaining the ingestion pipeline.
- Ops/Dev teams running local and hosted environments.

## Scope Evidence
- API endpoints in `Reenbit.BMAD.API`.
- Queue publisher in `Reenbit.BMAD.ServiceBus`.
- Event processing Function App in `Reenbit.BMAD.FunctionApp`.
- SQL schema and stored procedures in `Reenbit.BMAD.Sql/SqlScripts`.
