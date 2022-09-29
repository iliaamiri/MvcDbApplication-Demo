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

4. Connect to the database using the connection string above. And run these:

```sql
CREATE
DATABASE Baraga;
GO
USE Baraga;
GO
CREATE TABLE Member
(
    MemberId       INT PRIMARY KEY IDENTITY (1, 1),
    FirstName      VARCHAR(50)  NOT NULL,
    LastName       VARCHAR(50)  NOT NULL,
    Street         VARCHAR(100) NOT NULL,
    City           VARCHAR(50)  NOT NULL,
    Province       VARCHAR(50)  NOT NULL,
    PostalCode     VARCHAR(15)  NOT NULL,
    MobilePhone    VARCHAR(15)  NOT NULL,
    Email          VARCHAR(100) NOT NULL,
    IsReceiveEmail bit          NOT NULL DEFAULT (0)
);
GO 
```