# mediatorExample

### Summary
Sample Web Application using mediator pattern and CQRS concepts. Developed using .Net Core, Autofac, SqlServer with Entity Framework,  MadiatR, and Docker (containerized SQLServer).

### Commands
To restore nuget packages:
- nuget restore MediatorExample.sln

To generate migration's file:
- dotnet ef migrations add "<AnyMigrationName>" --project Infrastructure --context SqlServerContext --output-dir "Migrations/" --verbose

To generate the containerized Database, update it with migrations:
- docker-compose up
- docker-compose up -d (if you want to run in background mode)

To run application:
- dotnet run Application/Application.csproj

To enter in mongo container:
- docker exec -it mongodb bash

To run mongo shell inside it's container:
- mongo -u root -p 1Secure*Password1 (if you chage the user or pwd in your "init-mongo.js" file, you will need to change here to)
