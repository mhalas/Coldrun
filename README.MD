# Coldrun Recruitment Task

The project is built using modern **.NET 10** best practices, showcasing strict separation of concerns, enterprise-grade validation, and unit tests.

---

## Tech Stack & Libraries

* **Backend:** .NET 10 (C#)
* **Database:** Microsoft SQL Server
* **ORM:** Entity Framework Core
* **Mediator Pattern:** MediatR
* **Validation:** FluentValidation
* **Testing:** NUnit

---

## Getting Started

### Prerequisites
* .NET 10 SDK
* MS SQL Server (running locally or via Docker)

### Database Setup
Before running the application, initialize your SQL Server database schema. The SQL script required to create and seed the database is located at:
`./sql/initialize-db.sql`

*(You can execute this script using SQL Server Management Studio, Azure Data Studio, or any preferred database tool connected to your MS SQL instance).*

### Configuration
Update the connection string in `src/Api/appsettings.json` to point to your MS SQL instance:

```
"ConnectionStrings": {
  "ColdrunConnection": "Server=YOUR_SERVER;Database=YOUR_DATABASE;Trusted_Connection=True;TrustServerCertificate=True;User Id=YOUR_USER;Password=YOUR_PASSWORD;"
}
```

### Running the Application
Navigate to the API project folder and launch the server:

```
cd src/Api
dotnet run
```

The API will be available at the ports specified in `launchSettings.json`. You can explore and test endpoints via the interactive Swagger UI.

---

## Testing

The solution emphasizes high code quality and confidence through comprehensive unit testing using **NUnit**. Tests cover request validation, state transition logic, and handler behavior.

To execute the test suite, run the following command from the `src` directory:

```
dotnet test
```

---

## API Endpoints Summary

| Method | Endpoint | Description |
|---|---|---|
| **GET** | `/api/heartbeat` | Health check endpoint |
| **GET** | `/api/trucks` | Get filtered, sorted, and paginated list of trucks |
| **GET** | `/api/trucks/{id}` | Get details of a single truck by ID |
| **POST** | `/api/trucks` | Create a new truck |
| **PUT** | `/api/trucks/{id}` | Update truck details & trigger status transition validation |
| **DELETE** | `/api/trucks/{id}` | Remove a truck from the fleet |