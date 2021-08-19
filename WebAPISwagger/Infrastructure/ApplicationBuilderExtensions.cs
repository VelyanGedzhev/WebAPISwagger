using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using WebAPISwagger.Data;

namespace WebAPISwagger.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
         public static IApplicationBuilder PrepareDatabase(this IApplicationBuilder app)
         {
             var serviceScope = app.ApplicationServices.CreateScope();
             var services = serviceScope.ServiceProvider;

             MigrateDatabase(services);

             return app;
         }

        private static void MigrateDatabase(IServiceProvider services)
        {
            var data = services.GetRequiredService<ApplicationDbContext>();

            data.Database.Migrate();
        }

    }
}
