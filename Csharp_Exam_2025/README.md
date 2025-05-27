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

dotnet aspnet-codegenerator controller -name PersonsController -actions -m App.Domain.Person -dc AppDbContext -outDir Controllers --useDefaultLayout --useAsyncActions --referenceScriptLibraries -f


dotnet aspnet-codegenerator identity -dc App.DAL.EF.AppDbContext -f
~~~


~~~sh
cd WebApp

dotnet aspnet-codegenerator controller -name PersonsController -actions -m App.Domain.Person -dc AppDbContext -outDir ApiControllers -api --useAsyncActions -f
~~~