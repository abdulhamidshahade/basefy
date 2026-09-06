using Basefy.Domain.Interfaces;
using Besefy.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace Besefy.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
        {
            services.AddSingleton(NpgsqlDataSource.Create(connectionString));

            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IPromptRepository, PromptRepository>();
            services.AddScoped<IPromptVersionRepository, PromptVersionRepository>();
            services.AddScoped<IToolRepository, ToolRepository>();
            services.AddScoped<IToolVersionRepository, ToolVersionRepository>();

            return services;
        }
    }
}
