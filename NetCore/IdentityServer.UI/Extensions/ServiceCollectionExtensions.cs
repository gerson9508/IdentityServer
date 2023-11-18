using IdentityServer.Infraestructure.Repositories.Base;
using IdentityServer4.Services;
using IdentityServerUI.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace IdentityServerUI.Extensions
{
   public static class ServiceCollectionExtensions
   {
      public static IServiceCollection Identity(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
      {
         var assembly = typeof(Program).Assembly.GetName().Name;
         var sqlConnString = configuration.GetConnectionString("SQLServerConnection");

         services.AddDbContext<AspNetIdentityDbContext>(options => options.UseSqlServer(sqlConnString, b => b.MigrationsAssembly(assembly)));

         //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AspNetIdentityDbContext>();
         services.AddIdentity<IdentityUser, IdentityRole>()
           .AddEntityFrameworkStores<AspNetIdentityDbContext>()
           .AddDefaultTokenProviders();

         var identity = services.AddIdentityServer().AddAspNetIdentity<IdentityUser>()
            .AddConfigurationStore(options =>
            {
               options.ConfigureDbContext = b => b.UseSqlServer(sqlConnString, opt => opt.MigrationsAssembly(assembly));
            })
            .AddOperationalStore(options =>
            {
               options.ConfigureDbContext = b => b.UseSqlServer(sqlConnString, opt => opt.MigrationsAssembly(assembly));
            });

         services.AddScoped<IProfileService, ProfileService>();

         if (environment.IsDevelopment())
            identity.AddDeveloperSigningCredential();
         else
         {
            string path = Path.Combine(environment.ContentRootPath, configuration.GetConnectionString("CertName")),
               pwd = configuration.GetConnectionString("CertPwd");
            //Build the file path.                        
            identity.AddSigningCredential(Config.GetCert(path, pwd));
         }

         return services;
      }
   }
}
