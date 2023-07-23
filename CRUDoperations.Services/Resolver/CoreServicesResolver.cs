using CRUDoperations.IServices.IJob;
using CRUDoperations.IServices.IService;
using CRUDoperations.Services.Job;
using CRUDoperations.Services.Localization;
using CRUDoperations.Services.Mapper;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace CRUDoperations.Services.Resolver
{
    public static class CoreServicesResolver
    {
        public static void ResolveCoreServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IUserService, UserService.UserService>();
            services.AddScoped<ILocalizationService, LocalizationService>();
            services.AddScoped<IJobService, JobService>();
        }
        public static void ResolveMapper(IServiceCollection services)
        {
            TypeAdapterConfig config = TypeAdapterConfig.GlobalSettings;
            config.Scan(Assembly.GetExecutingAssembly());
            services.AddSingleton(config);
            services.AddScoped<IMapper, ServiceMapper>();
        }
    }
}
