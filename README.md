# StudentsRazorDBApp

An ASP.NET Core Razor Pages app (.NET 10) for managing students (create, list, update, delete), backed by SQL Server.

## Tech stack

- ASP.NET Core Razor Pages, Bootstrap, jQuery validation
- ADO.NET with `Microsoft.Data.SqlClient` (no ORM)
- AutoMapper for DTO/model mapping
- Serilog for logging (console and rolling file at `Logs/logs.txt`)
- SQL Server in Docker via `docker-compose.yml`

## Prerequisites

- .NET 10 SDK
- Docker

## Getting started

1. Set `SQL_IMAGE_TAG`, `MSSQL_SA_PASSWORD` and `MSSQL_PORT` in `.env`.
2. Start the database:
   ```
   docker compose up -d
   ```
3. Create the database `StudentsRazorApp10DB` and a `Students` table with columns `Id` (int, identity), `Firstname` and `Lastname`. The repo has no schema script.
4. Check that the connection string in `StudentsApp/appsettings.Development.json` matches your port and credentials.
5. Run the app:
   ```
   dotnet run --project StudentsApp
   ```

## Project structure

```
StudentsApp/
  Pages/Students/   Razor pages (Create, ViewStudents, Update, Delete, Success)
  Services/         Business logic and transactions
  DAO/              SQL data access
  Core/DBHelper.cs  SqlConnection factory
  DTO/              Input and output data transfer objects
  Models/           Student entity
  Exceptions/       Custom exceptions
  Configuration/    AutoMapper profile
```

Requests flow from page → service → DAO → database. See `CLAUDE.md` for more detail.
