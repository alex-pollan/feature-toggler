using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NUnit.Framework;
using TogglerAdmin.Domain.ViewModels;

namespace TogglerAdmin.Integration.Tests
{
    [TestFixture]
    public class FeatureToggleControllerTests : BaseTest
    {
        [Test]
        public async Task Get()
        {
            // Arrange
            //Seed.

            // Act
            var response = await Client.GetAsync("/api/featuretoggle");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());

            var stringResponse = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<FeatureToggleViewModel>>(stringResponse);

        }
    }
}
