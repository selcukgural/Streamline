# Streamline.Application

Streamline.Application is the application layer of the Streamline BPM engine, responsible for orchestrating process execution, handling commands, queries, and domain events, and integrating with background job schedulers (e.g., Hangfire). It leverages MediatR for CQRS and event-driven patterns, and is designed to be modular, testable, and extensible.

## Features

- **CQRS with MediatR:** Clean separation of commands, queries, and notifications.
- **Process Instance Management:** Start, continue, and query process instances.
- **Execution Flow Management:** Delegate execution logic to domain services.
- **Event Handling:** React to domain events (activity failed, message/signal/timer events, etc.).
- **Background Job Integration:** Trigger timer events via Hangfire jobs.
- **Extensible Repository Pattern:** Abstracts data access for entities.
- **Logging:** Rich logging for observability and troubleshooting.

## Project Structure

- `Features/Executions/Commands`: Commands and handlers for execution control.
- `Features/Incidents/EventHandlers`: Event handlers for incident management.
- `Features/ProcessInstances/Commands`: Commands and handlers for process instance lifecycle.
- `Features/ProcessInstances/Queries`: Queries and handlers for process instance retrieval.
- `Notifications/Handlers`: Handlers for domain notifications (messages, signals, timers).
- `Services`: Application services (e.g., timer job trigger for Hangfire).

## Key Dependencies

- [.NET 9.0](https://dotnet.microsoft.com/)
- [MediatR](https://github.com/jbogard/MediatR)
- [Hangfire.Core](https://www.hangfire.io/)
- [NodaTime](https://nodatime.org/)

## Getting Started

### Prerequisites

- .NET 9.0 SDK
- Hangfire-compatible storage (e.g., SQL Server, Redis) if using timers
- Access to process definition XML files

### Build

```sh
dotnet build src/Streamline.Application/Streamline.Application.csproj