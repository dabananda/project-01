using Handler.Collections;
using Handler.Handlers;
using Microsoft.Extensions.DependencyInjection;

namespace Handler
{
    public static class HandlerServiceExtensions
    {
        public static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddScoped<IDataHandler, DataHandler>();
            services.AddScoped<IHandlerCollection, HandlerCollection>();

            return services;
        }
    }
}
