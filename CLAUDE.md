# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

ASP.NET Core Razor Pages app (net10.0) doing CRUD on students against SQL Server. Single project `StudentsApp/` inside `StudentsRazorDBApp.slnx`. There is no test project.

## Commands

Run from the repo root:

- Start SQL Server: `docker compose up -d` (needs `SQL_IMAGE_TAG`, `MSSQL_SA_PASSWORD`, `MSSQL_PORT` in `.env`; `.env` is committed to git)
- Build: `dotnet build StudentsApp`
- Run: `dotnet run --project StudentsApp` (profiles in `StudentsApp/Properties/launchSettings.json`)

## Architecture

Layered, with plain ADO.NET (no EF):

`Pages/Students/*.cshtml(.cs)` → `Services/IStudentService` (`StudentServiceImpl`) → `DAO/IStudentDAO` (`StudentDAOImpl`) → `Core/DBHelper` (creates `SqlConnection`s from `ConnectionStrings:DefaultConnection`).

- DI is wired in `Program.cs`: `DBHelper` singleton; DAO and service scoped; AutoMapper profile in `Configuration/MapperConfig.cs`.
- Pages bind and validate DTOs (`StudentInsertDTO`, `StudentUpdateDTO`); the service maps DTO ↔ `Models/Student` and returns `StudentReadOnlyDTO`. Pages never touch the DAO or the model.
- Services wrap writes in `TransactionScope`. They check existence via `GetById` first and throw `StudentNotFoundException` (logged as a warning, then rethrown). Page models catch exceptions and surface them through `ErrorArray` (`Models/Error`).
- Successful POSTs use Post-Redirect-Get to `/Students/Success`, passing the message through `TempData`.
- DAO code is synchronous, uses raw parameterized SQL, and maps columns by hand from `SqlDataReader`.
- Logging is Serilog, configured in `appsettings*.json` (console plus rolling file at `Logs/logs.txt`).

## Configuration gotchas

- The connection string lives only in `appsettings.Development.json` (hardcoded to `localhost,1438`, so it must match `MSSQL_PORT`). `appsettings.json` has none, so a non-Development environment will fail in `DBHelper` with a null connection string.
- The `Students` table schema is not defined anywhere in the repo (no migrations or SQL scripts). The DAO expects columns `Id`, `Firstname`, `Lastname` in database `StudentsRazorApp10DB`.
