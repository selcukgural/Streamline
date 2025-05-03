# Streamline.Domain

`Streamline.Domain` defines the core domain model and business logic for the Streamline workflow engine. It contains all BPMN process entities, value objects, and domain services, ensuring a clean separation of concerns and a robust foundation for workflow orchestration.

## Purpose

This project encapsulates the essential business rules, process definitions, and domain-driven design (DDD) patterns for the Streamline engine. It is independent of infrastructure and application layers, making it reusable and testable.

## Features

- **BPMN 2.0 Entities**: Classes for processes, tasks, events, gateways, and flows.
- **Domain Events**: Encapsulates business events for process execution and state changes.
- **Value Objects**: Strongly-typed representations for process IDs, states, and other invariants.
- **Aggregates & Repositories**: Aggregate roots for process instances and interfaces for persistence.
- **Validation**: Business rule enforcement and process validation logic.
- **Extensibility**: Designed for easy extension and customization of workflow elements.

## Project Structure

- `Entities/` — BPMN process elements (Process, Task, Event, Gateway, etc.)
- `ValueObjects/` — Strongly-typed value objects (IDs, States, etc.)
- `Events/` — Domain events for process lifecycle
- `Repositories/` — Interfaces for persistence
- `Services/` — Domain services for business logic
- `Enums/` — Enumerations for process types, states, etc.
