using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Newtonsoft.Json;
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
            AssertOkResponse(response);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<FeatureToggleViewModel>>(stringResponse)
                .ToList();

            Assert.AreEqual(toggles.Count, models.Count);
        }

        [Test]
        [TestCase("ft1", "description ft1", false)]
        [TestCase("ft2", "description ft2", true)]
        [TestCase("ft1", null, false)]
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
            AssertOkResponse(response);

            var getResponse = await Client.GetAsync(ApiUrl);
            var stringResponse = await getResponse.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<FeatureToggleViewModel>>(stringResponse).ToList();

            Assert.AreEqual(1, models.Count);
            var model = models[0];
            Assert.AreEqual(name, model.Name);
            Assert.AreEqual(description, model.Description);
            Assert.AreEqual(enabled, model.Enabled);
        }

        [Test]
        public async Task Create_When_Name_Is_Null()
        {
            //Arrange
            var postRequest = new FeatureToggleViewModel
            {
                Name = null,
                Description = "description",
                Enabled = true
            };

            //Act
            var response = await Client.PostAsync(ApiUrl,
                new StringContent(JsonConvert.SerializeObject(postRequest), Encoding.UTF8, "application/json"));

            //Assert
            AssertBadRequestResponse(response);
        }

        [Test]
        public async Task Create_When_Name_Is_Duplicate()
        {
            //Arrange
            DataSeed.SeedFeatureToggles(new[] { new MongoDbFeatureToggleModel { Name = "ft1" } });

            var postRequest = new FeatureToggleViewModel
            {
                Name = "ft1",
                Description = "description",
                Enabled = true
            };

            //Act
            var response = await Client.PostAsync(ApiUrl,
                new StringContent(JsonConvert.SerializeObject(postRequest), Encoding.UTF8, "application/json"));

            //Assert
            AssertDuplicateResponse(response);
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
