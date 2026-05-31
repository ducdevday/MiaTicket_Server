# Source Base Architecture

Kiến trúc 4 layer cho ASP.NET Core backend.

## Sơ đồ phụ thuộc

```
WebAPI  →  BusinessLogic  →  DataAccess  →  Data
```

---

## 1. WebAPI (Presentation)

Bootstrap app, expose HTTP endpoints, auth, error handling.

```
App.WebAPI/
├── Controllers/        ← REST endpoints, inject I{X}Business
├── Middleware/         ← ExceptionMiddleware (map exception → HTTP code)
├── Policy/             ← UserAuthorizeAttribute + UserAuthorizeHandler (role-based auth)
├── Properties/         ← launchSettings.json
├── appsettings.json
├── Program.cs          ← DI bootstrap (JWT, Swagger, CORS, EF, Redis, RabbitMQ, Hangfire)
└── Dockerfile
```

---

## 2. BusinessLogic (Application)

Logic nghiệp vụ, DTO, mapping, rule động.

```
App.BusinessLogic/
├── Business/           ← I{X}Business + {X}Business (logic chính)
├── Request/            ← Request DTO từ client
├── Response/           ← Response DTO trả về client
├── Model/              ← Model nội bộ (không thuộc Request/Response)
├── Mapper/             ← AutoMapperProfile (Entity ↔ DTO)
├── Strategy/           ← I{X}Strategy + các implementation (rule động)
├── Factory/            ← {X}Factory tạo Strategy theo enum type
├── Validation/         ← FluentValidation hoặc validator thủ công
└── Util/               ← Helper (hash, token, format…)
```

---

## 3. DataAccess (Repository + Unit-of-Work)

Repository pattern, gom thành **một facade** dùng chung `DbContext`.

```
App.DataAccess/
├── Data/               ← I{X}Data + {X}Data (repository từng entity)
└── IDataAccessFacade.cs ← Unit-of-Work: lazy property cho từng repo + Commit()
```

Business chỉ inject `IDataAccessFacade` → gọi `facade.UserData.GetById(...)` → `facade.Commit()`.

---

## 4. Data (Persistence)

EF Core, entity, migration.

```
App.Data/
├── Entity/             ← POCO class (User, Order, …) + BaseEntity (CreatedAt/UpdatedAt)
├── Configuration/      ← {X}Cfg : IEntityTypeConfiguration<X> (fluent mapping)
├── Enum/               ← Enum dùng trong entity (Role, Status, …)
├── Extensions/         ← ModelBuilder.Seed(), extension dùng nội bộ Data
├── Migrations/         ← EF Core migration files
└── AppDbContext.cs     ← DbContext, DbSet, OnModelCreating, override SaveChanges
```

---

## Pattern chính

- **Unit-of-Work qua Facade**: business inject 1 `IDataAccessFacade`, mọi repo share một `DbContext`, `Commit()` = `SaveChanges()`.
- **Strategy + Factory**: tách rule động (pricing, permission…) ra khỏi business class.
- **Custom Authorization**: `[UserAuthorize(RequireRoles = …)]` + `UserAuthorizeHandler` đọc claim từ JWT.
- **Exception tập trung**: `ExceptionMiddleware` map exception type → HTTP status + JSON chuẩn.

## Quy tắc thêm feature mới

1. **Data**: Entity + `{X}Cfg` + `DbSet` + migration.
2. **DataAccess**: `I{X}Data` + `{X}Data` → expose ở `IDataAccessFacade`.
3. **BusinessLogic**: Request/Response DTO + Mapper + `I{X}Business` + `{X}Business` → `AddTransient` trong `Program.cs`.
4. **WebAPI**: Controller inject `I{X}Business`, gắn `[UserAuthorize]` nếu cần.
