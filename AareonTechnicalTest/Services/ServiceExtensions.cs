using AareonTechnicalTest.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AareonTechnicalTest.Services
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<ITicketService, TicketService>();
            return services;
        }
    }
}
