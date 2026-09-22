
using Microsoft.Extensions.DependencyInjection;
using TaskManagement.Application.Common;
using TaskManagement.Application.Interfaces;
using TaskManagement.Application.Services;

namespace TaskManagement.Application
{
    public static class ApplicationDependencies
    {

        public static IServiceCollection AddApplicationServices(this IServiceCollection service )
        {
           

            service.AddScoped<IAuthService, AuthService>();
            service.AddScoped<IJwtService, JwtService>();
            return service;
        }
    }
}
