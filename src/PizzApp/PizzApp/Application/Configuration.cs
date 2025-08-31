using PizzApp.Application.Features.OrderFeatures.Create;

namespace PizzApp.Application
{
    public static class Configuration
    {
        public static void ConfigureApplication(this IServiceCollection services)
        {
            services.Scan(scan => scan
                .FromAssemblyOf<CreateOrderService>()
                .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service")))
                .AsImplementedInterfaces()
                .WithScopedLifetime());
        }
    }
}
