# mediatorExample

### Summary
Sample Web Application using mediator pattern and CQRS concepts. Developed using .Net Core, Autofac, SqlServer with Entity Framework,  MadiatR, and Docker (containerized SQLServer).

### Commands
To restore nuget packages:
- nuget restore MediatorExample.sln

To generate migration's file:
- dotnet ef migrations add <AnyMigrationName> --project Infrastructure --context SqlServerContext --output-dir "Migrations/" --verbose

To generate the containerized Database, update it with migrations:
- docker-compose up

To run application:
- dotnet run Application/Application.csproj
