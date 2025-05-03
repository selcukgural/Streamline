# Streamline

Streamline is a modular and extensible workflow engine designed for executing BPMN-based process definitions in .NET environments. It enables orchestration of complex business processes, supports custom node handlers, and integrates easily with external systems.

## Features

- **BPMN 2.0 Process Execution**: Parses and runs BPMN process definitions.
- **Pluggable Node Handlers**: Extend or override behavior for BPMN elements (tasks, events, gateways, etc.).
- **Script Task Execution**: Supports C# (via Roslyn) and JavaScript (via Jint) script tasks.
- **Event Subscription & Timer Support**: Handles message, signal, and timer events with persistence and scheduling.
- **Error, Escalation, and Compensation Handling**: Implements BPMN error propagation and advanced event types.
- **Dependency Injection**: All handlers and services are resolved via DI for testability and flexibility.
- **Persistence Layer**: Uses repositories and Unit of Work for database operations (Entity Framework Core recommended).
- **Logging**: Integrated with Microsoft.Extensions.Logging.

## Getting Started

### Prerequisites

- .NET 9.0 or later
- Entity Framework Core (for persistence)
- [NodaTime](https://nodatime.org/) for robust date/time handling
- [Jint](https://github.com/sebastienros/jint) for JavaScript execution
- [MediatR](https://github.com/jbogard/MediatR) for event publishing
