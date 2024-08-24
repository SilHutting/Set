Note: connection info in appsettings.json. Conform, or change.
"Server=localhost,1433;Initial Catalog=SetDb;User ID=SA;Password=PGZlmOYlFKAKqPTWDPA83763528;TrustServerCertificate=true"
Mssql connection string ^

//Install all packages

// Accept certs
dotnet dev-certs https --trust
// Spin up docker DB
docker run -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=PGZlmOYlFKAKqPTWDPA83763528" -e "MSSQL_PID=Express" -p 1433:1433 -d mcr.microsoft.com/mssql/server:2022-latest

// Set up DB Tables
dotnet ef migrations add InitialMigration --context SetContext
dotnet ef database update --context SetContext

// Want to reset DB?
dotnet ef database update 0
dotnet ef migrations add InitialMigration --context SetContext

Run project

