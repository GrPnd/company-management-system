# icd0024-24-25-s


Gregor Pendis
grpend
222366IADB

~~~sh
docker compose up --build
docker run --name webapp_docker --rm -it -p 8888:8080 webapp
~~~


~~~sh
dotnet ef migrations add InitialCreate --project App.DAL.EF --startup-project WebApp --context AppDbContext

dotnet ef database --project App.DAL.EF --startup-project WebApp update

dotnet ef migrations --project App.DAL.EF --startup-project WebApp remove

dotnet ef database --project App.DAL.EF --startup-project WebApp drop
~~~

~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name AbsencesController -actions -m App.Domain.Absence -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name DepartmentsController -actions -m App.Domain.Department -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MeetingsController -actions -m App.Domain.Meeting -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name MessagesController -actions -m App.Domain.Message -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TeamRolesController -actions -m App.Domain.TeamRole -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name StatusesController -actions -m App.Domain.Status -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TasksController -actions -m App.Domain.Task -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TeamsController -actions -m App.Domain.Team -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name TicketsController -actions -m App.Domain.Ticket -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name PersonsController -actions -m App.Domain.Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UsersInRolesController -actions -m App.Domain.UserInRole -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UsersInTeamsController -actions -m App.Domain.UserInTeam -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -name UsersInTeamsInTasksController -actions -m App.Domain.UserInTeamInTask -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f

dotnet aspnet-codegenerator identity -dc App.DAL.EF.AppDbContext -f
~~~

~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name AbsencesController -actions -m App.Domain.Absence -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name DepartmentsController -actions -m App.Domain.Department -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MeetingsController -actions -m App.Domain.Meeting -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name MessagesController -actions -m App.Domain.Message -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TeamRolesController -actions -m App.Domain.TeamRole -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name StatusesController -actions -m App.Domain.Status -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TasksController -actions -m App.Domain.Task -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TeamsController -actions -m App.Domain.Team -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name TicketsController -actions -m App.Domain.Ticket -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name PersonsController -actions -m App.Domain.Person -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name UsersInRolesController -actions -m App.Domain.UserInRole -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name UsersInTeamsController -actions -m App.Domain.UserInTeam -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
dotnet aspnet-codegenerator controller -name UsersInTeamsInTasksController -actions -m App.Domain.UserInTeamInTask -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~