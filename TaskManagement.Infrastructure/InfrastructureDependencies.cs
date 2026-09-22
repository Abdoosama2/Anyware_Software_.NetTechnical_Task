using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Domain.Interfaces;
using TaskManagement.Infrastructure.Data;
using TaskManagement.Infrastructure.Repositories;

namespace TaskManagement.Infrastructure
{
    public static class InfrastructureDependencies
    {

        public static IServiceCollection InfrastructureLayerServicesCollection(this IServiceCollection service, IConfiguration config)
        {

            service.AddDbContext<AppDbContext>(options =>
            {
                options.UseNpgsql(config.GetConnectionString("PostgresConnection"));
            });

            service.AddScoped<IUserRepository, UserRepository>();

 
            return service;
        }
    }
}
