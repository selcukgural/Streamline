# Streamline.Engine

Streamline.Engine is a modular, extensible workflow engine for executing BPMN-based process definitions in .NET. It provides a flexible runtime for orchestrating business processes, supporting custom node handlers, event subscriptions, and integration with external systems.

## Features

- **BPMN 2.0 Process Execution**: Parses and executes BPMN process definitions.
- **Pluggable Node Handlers**: Easily extend or override behavior for BPMN elements (tasks, events, gateways, etc.).
- **Event Subscription & Timer Support**: Handles message, signal, and timer events with persistence and scheduling.
- **Script Task Execution**: Supports C# (via Roslyn) and JavaScript (via Jint) script tasks.
- **Error, Escalation, and Compensation Handling**: Implements BPMN error propagation and advanced event types.
- **Dependency Injection**: All handlers and services are resolved via DI for testability and flexibility.
- **Persistence Layer**: Uses repositories and Unit of Work for database operations (Entity Framework Core recommended).
- **Logging**: Integrated with Microsoft.Extensions.Logging.

## Getting Started

### Prerequisites

- .NET 9.0 or later
- A database supported by Entity Framework Core (for persistence)
- [NodaTime](https://nodatime.org/) for robust date/time handling
- [Jint](https://github.com/sebastienros/jint) for JavaScript execution
- [MediatR](https://github.com/jbogard/MediatR) for event publishing

### Installation

Add the project reference to your solution:

```shell
dotnet add reference src/Streamline.Engine/Streamline.Engine/Streamline.Engine.csproj