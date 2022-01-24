using AareonTechnicalTest.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AareonTechnicalTest.Repositories
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();
            services.AddScoped<INoteRepository, NoteRepository>();
            return services;
        }
    }
}
