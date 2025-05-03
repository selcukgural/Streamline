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

# Streamline.Engine Project Roadmap

## Phase 1: Core Functionality
- **BPMN 2.0 Compliance**: Ensure full support for BPMN 2.0 process definitions.
- **Script Task Enhancements**:
  - Finalize C# and JavaScript script task execution.
  - Add support for additional scripting languages if needed.
- **Error Handling**:
  - Implement advanced error, escalation, and compensation handling.
- **Event Handling**:
  - Complete support for message, signal, and timer events.
- **Persistence Layer**:
  - Finalize integration with Entity Framework Core.
  - Add support for other ORMs or databases if required.

## Phase 2: Extensibility and DSL
- **Custom DSL**:
  - Design and implement a domain-specific language (DSL) for defining workflows.
  - Provide tooling for DSL validation and conversion to BPMN.
- **Pluggable Architecture**:
  - Enhance support for custom node handlers and external integrations.
- **Engine Improvements**:
  - Optimize execution engine for scalability and performance.

## Phase 3: Developer Experience
- **Documentation**:
  - Expand README with detailed usage examples and API references.
  - Add tutorials for common use cases.
- **Tooling**:
  - Develop a visual workflow editor for designing BPMN processes.
  - Provide CLI tools for managing workflows and deployments.
- **Testing Framework**:
  - Add utilities for testing workflows and custom handlers.

## Phase 4: Community and Ecosystem
- **Open Source Contributions**:
  - Create contribution guidelines and issue templates.
  - Actively engage with the community for feedback and contributions.
- **Integrations**:
  - Add support for popular frameworks like ASP\.NET Core, Kubernetes, etc.
- **Plugins**:
  - Develop plugins for common use cases (e.g., email notifications, REST API calls).

## Phase 5: Advanced Features
- **Monitoring and Analytics**:
  - Add real-time monitoring and logging for workflow execution.
  - Provide analytics dashboards for process insights.
- **Cloud Support**:
  - Enable deployment to cloud platforms (e.g., Azure, AWS).
- **Multi-Tenancy**:
  - Add support for multi-tenant workflow execution.
