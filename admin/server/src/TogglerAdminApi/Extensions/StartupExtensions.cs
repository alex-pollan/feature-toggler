using Microsoft.Extensions.DependencyInjection;
using TogglerAdmin.Domain;
using TogglerAdmin.Abstractions.Data;
using TogglerAdmin.Abstractions.Domain;
using TogglerAdmin.Data.MongoDb;
using TogglerAdmin.Abstractions;

namespace TogglerAdmin.Api.Extensions
{
    public static class StartupExtensions
    {
        public static IServiceCollection AddFeatureTogglesAdminSupport(this IServiceCollection services)
        {
            //TODO: configuration
            var mongoDbConfiguration = new MongoDbConfiguration(
                "mongodb://localhost:27017",
                "featuretoggles");
            services.AddSingleton(mongoDbConfiguration);
            services.AddScoped<IFeatureToggleRepository, MongoDbFeatureToggleRepository>();
            services.AddScoped<IFeatureToggleService, FeatureToggleService>();
            services.AddScoped<ITimeProvider, TimeProvider>();

            return services;
        }
    }
}
