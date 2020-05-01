# mediatorExample

### Summary
Sample Web Application using mediator pattern and CQRS concepts. Developed using .Net Core, Autofac, SqlServer with Entity Framework,  MadiatR, and Docker (containerized SQLServer and MongoDB).

### Commands
To restore nuget packages (inside api folder):
- nuget restore MediatorExample.sln

To generate migration's file (inside api folder):
- dotnet ef migrations add "AnyMigrationName" --project Infrastructure --context SqlServerContext --output-dir "Migrations/" --verbose

This command will generate the containerized Databases (mongo and SQLServer) and will up SqlServer Seed Container wich will update SqlSever Database with migrations. MongoDb will initialize with "init-mongo.js" file configuration:
- docker-compose up
- docker-compose up -d (if you want to run in background mode)

To run application:
- dotnet run Application/Application.csproj

To enter in mongo container:
- docker exec -it mongodb bash

To run mongo shell inside it's container:
- mongo -u root -p 1Secure*Password1 (if you chage the user or pwd in your "init-mongo.js" file, you will need to change here to)
