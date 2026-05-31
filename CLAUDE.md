# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

MiaTicket is an ASP.NET (.NET) backend for a ticketing/event platform. It is a multi-project solution (`MiaTicket.sln`) orchestrated through `MiaTicket.WebAPI/Program.cs`, with SQL Server (EF Core), Redis, RabbitMQ, and Hangfire as core infrastructure dependencies, and Cloudinary / VNPay / ZaloPay / SMTP as external integrations.

## Running & Building

Local development is Docker-first (see `README.md` for the full env-var list):

```powershell
docker compose up -d              # spins up webapi + redis + rabbitmq
# Swagger: https://localhost:8081/swagger/index.html
# Hangfire dashboard: /hangfire
```

SQL Server and the Hangfire DB are expected to be provided externally (two separate SQL Server instances/databases — see `MiaTickConnectionString` and `MiaTickHangFireConnectionString`). `init-db.sql` exists at the repo root for seeding.

Direct dotnet workflows:

```powershell
dotnet build MiaTicket.sln
dotnet run --project MiaTicket.WebAPI

# EF Core migrations (DbContext lives in MiaTicket.Data, but startup is WebAPI)
dotnet ef migrations add <Name> --project MiaTicket.Data --startup-project MiaTicket.WebAPI
dotnet ef database update     --project MiaTicket.Data --startup-project MiaTicket.WebAPI
```

There is no test project in the solution.

## Configuration / Secrets

All secrets are read from **environment variables** via `MiaTicket.Setting/EnviromentSetting.cs` (singleton), not from `appsettings.json`. The full list (DB, JWT issuer/audience/secret, Cloudinary, SMTP, Redis, VNPay, ZaloPay, RabbitMQ, Hangfire DB) is in `README.md`. VNPay/ZaloPay configs that are NOT secrets are bound from `appsettings.json` via `IOptions<VNPayConfig>` / `IOptions<ZaloPayConfig>`.

`MiaTicketDBContext` in `MiaTicket.Data` builds its own connection string from `EnviromentSetting` in `OnConfiguring`, so it works without DI — `DataAccessFacade` simply `new`s it up.

## Architecture

Layered architecture; each layer is a separate csproj. Dependency direction is WebAPI → BussinessLogic → DataAccess → Data, plus cross-cutting infrastructure projects.

- **MiaTicket.WebAPI** — Controllers, JWT auth setup, Swagger, Hangfire server, RabbitMQ connection registration, CORS ("Angular UI" policy allows any origin with credentials), `ExceptionMiddleware`, and the custom `UserAuthorize` authorization policy. All DI wiring lives in `Program.cs`.
- **MiaTicket.BussinessLogic** — One `I{Domain}Business` per feature (Account, Admin, Event, Order, Payment, Organizer, Voucher, Summary, Banner, Category, VnAddress, VerificationCode, Token). All registered `Transient`. Also holds `Mapper/AutoMapperProfile.cs`, request/response DTOs, and the `Stragegy/` + `Factory/` pattern pair (note the misspelling — keep it consistent when adding files):
  - `OrderPricingFactory` + `IOrderPriceStragegy` — pluggable order pricing rules.
  - `OrganizerPermissionFactory` + `IOrganizerPermissionStragegy` — per-role permission checks for event organizers.
- **MiaTicket.DataAccess** — `IDataAccessFacade` is the unit-of-work surface. Every business takes a single `IDataAccessFacade` and accesses repositories via lazy properties (`facade.OrderData`, `facade.EventData`, …). All repositories share one `MiaTicketDBContext` instance per request, and `Commit()` calls `SaveChanges`. When adding a new entity, you must (a) add an `I{X}Data` + `{X}Data` pair under `MiaTicket.DataAccess/Data/`, and (b) expose it on `IDataAccessFacade` + `DataAccessFacade`.
- **MiaTicket.Data** — EF Core entities (`Entity/`), fluent configurations (`Configuration/*Cfg.cs`), enums (`Enum/`), and the `MiaTicketDBContext`. Migrations live in `Migrations/`.
- **MiaTicket.DataCache** — `IRedisCacheService` over StackExchange.Redis (singleton).
- **MiaTicket.Email** — RabbitMQ-backed email pipeline: `IEmailProducer` enqueues, `IEmailConsumer` consumes, `EmailBackgroundHandler` is an `IHostedService` that runs the consumer loop. Templates live in `Template/`.
- **MiaTicket.Schedular** — Hangfire-driven jobs (e.g., `OrderCancellationService` for auto-cancelling unpaid orders).
- **MiaTicket.CloudinaryStorage** — image uploads via `ICloudinaryService`.
- **MiaTicket.VNPay / MiaTicket.ZaloPay** — payment gateway integrations; configs bound from `appsettings.json`.
- **MiaTicket.Setting** — `EnviromentSetting` singleton; the only place that reads `Environment.GetEnvironmentVariable`.

## Auth

JWT bearer authentication is configured in `Program.cs` (Issuer/Audience/Secret from env). Authorization uses a custom policy: `UserAuthorizeAttribute` (in `MiaTicket.WebAPI/Policy/`) combined with `UserAuthorizeHandler` (singleton `IAuthorizationHandler`). When adding new endpoints, prefer `[UserAuthorize(...)]` over raw `[Authorize]` so role/permission rules stay centralized. `opt.MapInboundClaims = false` is set — claims are read by their original JWT names, not the .NET-remapped ones.

## Conventions

- Naming: the codebase uses the misspelling **`Stragegy`** (not `Strategy`) and **`Bussiness`** (not `Business`) in folder/namespace names. Follow existing spellings rather than "correcting" them — they're load-bearing across namespaces.
- Business → DataAccessFacade → Data. Don't inject `MiaTicketDBContext` directly into a Business class; go through `IDataAccessFacade`.
- New entities require: entity class in `Data/Entity/`, fluent config in `Data/Configuration/{X}Cfg.cs` (applied via `OnModelCreating`), `DbSet` on `MiaTicketDBContext`, repository pair under `DataAccess/Data/`, registration on `IDataAccessFacade`/`DataAccessFacade`, and a migration.
- Every new `IXxxBusiness` must be registered as `Transient` in `Program.cs` — there's no auto-discovery.
