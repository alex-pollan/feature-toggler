using Microsoft.Extensions.DependencyInjection;
using TogglerAdmin.Domain;
using TogglerAdmin.Abstractions.Data;
using TogglerAdmin.Abstractions.Domain;
using TogglerAdmin.Data.MongoDb;
using TogglerAdmin.Abstractions;
using Microsoft.Extensions.Configuration;

namespace TogglerAdmin.Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddFeatureTogglesAdminSupport(this IServiceCollection services,
            IConfiguration configuration)
        {
            //TODO: configuration from env variables?
            //  to control it from integration tests
            var mongoDbConfiguration = new MongoDbConfiguration();
            configuration.GetSection("MongoDb").Bind(mongoDbConfiguration);            
            services.AddSingleton(mongoDbConfiguration);
            services.AddScoped<IFeatureToggleRepository, MongoDbFeatureToggleRepository>();
            services.AddScoped<IFeatureToggleService, FeatureToggleService>();
            services.AddScoped<ITimeProvider, TimeProvider>();

            return services;
        }
    }
}
