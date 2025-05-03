# Streamline.Api

Streamline.Api is the HTTP API layer for the Streamline BPMN Workflow Engine. It exposes RESTful endpoints for process instance management, BPMN XML import/export, and user task operations. The API is built with ASP.NET Core and integrates with Hangfire for background job scheduling.

## Features

- **Process Instance Management:** Start, query, and manage BPMN process instances.
- **BPMN XML Import/Export:** Import BPMN 2.0.2 XML definitions and export definitions as XML.
- **User Task Management:** Claim, unclaim, assign, delegate, complete, and update user tasks.
- **Background Job Scheduling:** Uses Hangfire for timer events and background processing.
- **OpenAPI/Swagger:** Interactive API documentation and testing.

## Technologies

- **.NET 9.0**
- **ASP.NET Core Web API**
- **Entity Framework Core (SQLite)**
- **Hangfire (with SQLite storage)**
- **MediatR (CQRS and notifications)**
- **Swashbuckle (Swagger/OpenAPI)**
- **Scrutor (Assembly scanning/DI)**

## Getting Started

### Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQLite](https://www.sqlite.org/) (or use the included file-based DB)
- [Node.js](https://nodejs.org/) (optional, for some script tasks)

### Setup

1. **Clone the repository:**
   ```sh
   git clone https://github.com/your-org/streamline.git
   cd streamline/src/Streamline.Api