using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TogglerAdmin.Data.MongoDb;
using TogglerAdmin.Integration.Tests.Utils;

namespace TogglerAdmin.Integration.Tests
{
    public class CustomWebApplicationFactory<TStartup>
    : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

            if (string.IsNullOrEmpty(environment))
            {
                environment = "IntegrationTests";
            }

            builder.UseEnvironment(environment);

            builder.ConfigureServices(services =>
            {
                var sp = services.BuildServiceProvider();

                //TODO: seed at test level for better control
                var mongoDbConfiguration = new MongoDbConfiguration();
                sp.GetRequiredService<IConfiguration>().GetSection("MongoDb").Bind(mongoDbConfiguration);

                var seed = new MongoDbDataSeed(mongoDbConfiguration.ConnectionString, "integration_tests");

                seed.Cleanup();

                seed.Seed();
            });
        }
    }
}
