using FirstProject.Repositories;
using FirstProject.Repositories.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Repository
{
    public static class RepositoryExtenstions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IDataRepo, DataRepo>();
            return services;
        }
    }
}
