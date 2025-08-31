using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyModel;
using PizzApp.Domain.Interfaces;
using PizzApp.Infrastructure.Context;
using PizzApp.Infrastructure.EntitiesConfiguration;
using PizzApp.Infrastructure.Repositories;
using Scrutor;
using System.Reflection;

namespace PizzApp.Infrastructure
{
    public static class Configuration
    {
        public static void ConfigureInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            var connection = configuration.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connection, x => x.MigrationsAssembly("PizzApp"));
            }, ServiceLifetime.Scoped);

            services.Scan(scan => scan
                .FromAssemblyOf<ProductRepository>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Repository")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
