using System.Globalization;
using App.DAL.Contracts;
using App.DAL.EF;
using App.DAL.EF.Repositories;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                       throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");


if (builder.Environment.IsProduction())
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));
}
else
{
    builder.Services.AddDbContext<AppDbContext>(options =>
        options.UseSqlite(connectionString, o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery))
            .ConfigureWarnings(w => w.Throw(RelationalEventId.MultipleCollectionIncludeWarning))
            .EnableDetailedErrors()
            .EnableSensitiveDataLogging());
}



builder.Services.AddDatabaseDeveloperPageExceptionFilter();



builder.Services.AddScoped<IAppUOW, AppUow>();



builder.Services.AddIdentity<AppUser, AppRole>(options =>
            options.SignIn.RequireConfirmedAccount = false)
    .AddDefaultUI()
    .AddEntityFrameworkStores<AppDbContext>()
    .AddDefaultTokenProviders();

/*
builder.Services.AddDefaultIdentity<AppUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<AppDbContext>();
*/

builder.Services.AddControllersWithViews();


// add culture switching support
var supportedCultures = builder.Configuration
    .GetSection("SupportedCultures")
    .GetChildren()
    .Select(x => new CultureInfo(x.Value!))
    .ToArray();

builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    // datetime and currency support
    options.SupportedCultures = supportedCultures;
    // UI translated strings
    options.SupportedUICultures = supportedCultures;
    // if nothing is found, use this
    options.DefaultRequestCulture =
        new RequestCulture(builder.Configuration["DefaultCulture"]!, 
            builder.Configuration["DefaultCulture"]!);
    options.SetDefaultCulture(builder.Configuration["DefaultCulture"]!);

    options.RequestCultureProviders = new List<IRequestCultureProvider>
    {
        // Order is important, it's in which order they will be evaluated
        // add support for ?culture=ru-RU
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRequestLocalization(options: app.Services.GetService<IOptions<RequestLocalizationOptions>>()!.Value!);

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
    .WithStaticAssets();

app.Run();