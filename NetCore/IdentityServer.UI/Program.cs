using Serilog;
using IdentityServerUI.Extensions;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using IdentityServer.Infraestructure.Repositories.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(cors => cors
   .AddPolicy("MyPolicy", builder =>
   {
      builder
      .AllowAnyOrigin()
      .AllowAnyMethod()
      .AllowAnyHeader();
   }));

builder.Services.Identity(builder.Configuration, builder.Environment);
builder.Services.AddControllersWithViews();

var app = builder.Build();

#region Initialized Database
using (var serviceScope = app.Services.GetService<IServiceScopeFactory>().CreateScope())
{
   serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();

   var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
   context.Database.Migrate();

   if (!context.Clients.Any())
   {
      foreach (var client in Config.GetClients())
      {
         context.Clients.Add(client.ToEntity());
      }
      context.SaveChanges();
   }

   if (!context.IdentityResources.Any())
   {
      foreach (var resource in Config.GetIdentityResources())
      {
         context.IdentityResources.Add(resource.ToEntity());
      }
      context.SaveChanges();
   }

   if (!context.ApiScopes.Any())
   {
      foreach (var resource in Config.GetApiScopes())
      {
         context.ApiScopes.Add(resource.ToEntity());
      }

      context.SaveChanges();
   }

   if (!context.ApiResources.Any())
   {
      foreach (var resource in Config.GetApis())
      {
         context.ApiResources.Add(resource.ToEntity());
      }
      context.SaveChanges();
   }
}
#endregion

app.UseCors("MyPolicy");
app.UseStaticFiles();
app.UseRouting();
app.UseIdentityServer();
app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
   endpoints.MapDefaultControllerRoute();
});

#region Logger
Log.Logger = new LoggerConfiguration().MinimumLevel
   .Debug().MinimumLevel
   .Override("Microsoft", Serilog.Events.LogEventLevel.Warning).MinimumLevel
   .Override("Microsoft.Hosting.Lifetime", Serilog.Events.LogEventLevel.Information).MinimumLevel
   .Override("System", Serilog.Events.LogEventLevel.Warning).MinimumLevel
   .Override("Microsoft.AspNetCore.Authentication", Serilog.Events.LogEventLevel.Information).Enrich
   .FromLogContext().WriteTo
   .File($@"{builder.Environment.ContentRootPath}\identityserver.txt",
   fileSizeLimitBytes: 1_000_000,
   rollOnFileSizeLimit: true,
   shared: true,
   flushToDiskInterval: TimeSpan
   .FromSeconds(1)).WriteTo
   .Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
   theme: Serilog.Sinks.SystemConsole.Themes.AnsiConsoleTheme.Code)
   .CreateLogger();

try
{
   Log.Information("Starting host...");
   app.Run();
   return 0;
}
catch (System.Exception ex)
{
   Log.Fatal(ex, "Host terminated unexpectedly.");
   return 1;
}
finally
{
   Log.CloseAndFlush();
}
#endregion
