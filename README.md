## MVC + EntityFramework Web Application - Demo

## Pre-requisities
* Have Docker installed
* .NET v6.0.9

## Setup
1. Start the Sql Express Docker Container
```shell
docker run --cap-add SYS_PTRACE -e ACCEPT_EULA=1 -e MSSQL_SA_PASSWORD=SqlExpress! -p 1444:1433 --name azsql-0923f -d mcr.microsoft.com/azure-sql-edge
```
2. Rename the `example.appsettings.json` to `appsettings.json`
3. Paste these in your `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "DataSource=app.db;Cache=Shared",
    "LocalhostSqlConnection": "Data Source=localhost,1444;Initial Catalog=Baraga;Persist Security Info=True;User ID=sa;Password=SqlExpress!"
  },
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*"
}
```