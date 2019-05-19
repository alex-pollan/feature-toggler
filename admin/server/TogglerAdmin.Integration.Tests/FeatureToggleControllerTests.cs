using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using TogglerAdmin.Domain.ViewModels;
using Xunit;

namespace TogglerAdmin.Integration.Tests
{
    public class FeatureToggleControllerTests : IClassFixture<CustomWebApplicationFactory<TogglerAdmin.Api.Startup>>
    {
        private readonly CustomWebApplicationFactory<TogglerAdmin.Api.Startup> _factory;

        public FeatureToggleControllerTests(CustomWebApplicationFactory<TogglerAdmin.Api.Startup> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async void Get()
        {
            // Arrange
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/featuretoggle");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.Equal("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            //var stringResponse = await response.Content.ReadAsStringAsync();
            //var models = JsonConvert.DeserializeObject<IEnumerable<FeatureToggleViewModel>>(stringResponse);

        }
    }
}
