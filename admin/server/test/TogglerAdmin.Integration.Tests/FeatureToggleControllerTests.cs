using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;
using TogglerAdmin.Data.MongoDb;
using TogglerAdmin.Domain.ViewModels;

namespace TogglerAdmin.Integration.Tests
{
    [TestFixture]
    public class FeatureToggleControllerTests : BaseTest
    {
        private const string ApiUrl = "/api/featuretoggle";

        [Test]
        public async Task Get()
        {
            // Arrange
            var toggles = GenerateFeatureToggles();
            DataSeed.SeedFeatureToggles(toggles);

            // Act
            var response = await Client.GetAsync(ApiUrl);

            // Assert
            AssertResponse(response);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<FeatureToggleViewModel>>(stringResponse)
                .ToList();

            Assert.AreEqual(toggles.Count, models.Count);
        }

        [Test]
        [TestCase("ft1", "description ft1", false)]
        public async Task Create(string name, string description, bool enabled)
        {
            //Arrange
            var postRequest = new FeatureToggleViewModel
            {
                Name = name,
                Description = description,
                Enabled = enabled
            };

            //Act
            var response = await Client.PostAsync(ApiUrl,
                new StringContent(JsonConvert.SerializeObject(postRequest), Encoding.UTF8, "application/json"));

            //Assert
            AssertResponse(response);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<dynamic>(stringResponse);
            var jt = model as JToken;
            Assert.AreEqual("id", jt.First.Path);
            Assert.IsNotNull(jt.First.First.Value<string>());
        }

        private void AssertResponse(HttpResponseMessage response)
        {
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Assert.AreEqual("application/json; charset=utf-8",
                response.Content.Headers.ContentType.ToString());
        }

        public static IList<MongoDbFeatureToggleModel> GenerateFeatureToggles()
        {
            var testFts = new Faker<MongoDbFeatureToggleModel>()
                .RuleFor(f => f.Id, _ => null)
                .RuleFor(f => f.Name, (f, u) => f.Lorem.Word())
                .RuleFor(f => f.Description, (f, u) => f.Lorem.Sentence())
                .RuleFor(f => f.Creator, (f, u) => f.Internet.Email())
                .RuleFor(f => f.CreatedAt, (f, u) => f.Date.Recent());

            return testFts.Generate(10);
        }
    }
}
