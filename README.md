# Ticket API

.Net API used in my Ticketing app project.

## Dependencies

- .Net 10
- PostGreSQL

## Starting the project

- Install PostGreSQL : https://www.postgresql.org/download/
- Install .Net 10 : https://dotnet.microsoft.com/en-us/download
- Create the Database using the script in the folder "DB" (NOTE: Use the first line alone, then select the created db as the default one before running the other commands)
- In Visual Studio you can launch the api in dev mode using ctrl+F5 (It hasn't been tested in a build yet)
- You can access swagger using the "http://localhost:5156/swagger/index.html"
- You can if wanted go check out this repository to get my Angular project used for the Front : https://github.com/carotte98/TicketAppFront

IMPORTANT NOTE: If you don't use the default ports for postgres and Angular you might need to change the ports manually in the code first
- In appsettings.json under ConnectionStrings>DefaultConnection
- In Program.cs under the CORS builder


