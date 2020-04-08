# mediatorExample

### Summary
Sample Web Application using mediator pattern and CQRS concepts. Developed using .Net Core, Autofac, SqlServer with Entity Framework,  MadiatR, and Docker (one container for webApp and one for SQLServer).

### Binaries

### Commands
To restore nuget packages:
- nuget restore MediatorExample.sln

To generate migrations file:
- dotnet ef migrations add <AnyMigrationName> --project Infrastructure --context SqlServerContext --output-dir "Migrations/" --verbose

To run apllication:
- docker-compose up
