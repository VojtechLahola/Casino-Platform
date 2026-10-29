# CasinoPlatform
## MySQL

Provider: `Pomelo.EntityFrameworkCore.MySql`. Connection string a verze serveru jsou v `src/CasinoPlatform.API/appsettings.json`
(`ConnectionStrings:DefaultConnection`, `MySql:ServerVersion`).

Rychlý MySQL v Dockeru:

```bash
docker run -d --name casino-mysql -p 3306:3306 -e MYSQL_ROOT_PASSWORD=VaseHeslo123! mysql:8.0
```

## Migrace

```bash
dotnet tool install --global dotnet-ef
dotnet ef migrations add InitialCreate --project src/CasinoPlatform.Infrastructure --startup-project src/CasinoPlatform.API --output-dir Data/Migrations
dotnet ef database update --project src/CasinoPlatform.Infrastructure --startup-project src/CasinoPlatform.API
```

Package Manager Console:

```powershell
Add-Migration InitialCreate -Project CasinoPlatform.Infrastructure -StartupProject CasinoPlatform.API -OutputDir Data/Migrations
Update-Database -Project CasinoPlatform.Infrastructure -StartupProject CasinoPlatform.API
```
