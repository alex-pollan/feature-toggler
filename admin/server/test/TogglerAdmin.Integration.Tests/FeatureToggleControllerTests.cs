using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Newtonsoft.Json;
using NUnit.Framework;
using TogglerAdmin.Api.Controllers.Models;
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
            var response = await Client.PostAsync(ApiUrl, CreateHttpContent(postRequest));

            //Assert
            AssertOkResponse(response);

            var models = await GetToggles();

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
            var response = await Client.PostAsync(ApiUrl, CreateHttpContent(postRequest));

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
            var response = await Client.PostAsync(ApiUrl, CreateHttpContent(postRequest));

            //Assert
            AssertConflictResponse(response);
        }

        [Test]
        [TestCase(false, PatchFeatureToggleRequest.OperationEnable, "true", HttpStatusCode.NoContent, "Enable value must change")]
        [TestCase(false, "unknown", "true", HttpStatusCode.BadRequest, "Enable value must not change if invalid patch property name")]
        [TestCase(false, PatchFeatureToggleRequest.OperationEnable, "wrong_bool_value", HttpStatusCode.BadRequest, "Enable value must not change if invalid patch property value")]
        public async Task Patch_Enable(bool enabled, string patchPropertyName, string patchPropertyValue, HttpStatusCode expectedStatusCode, string assertMessage)
        {
            //Arrange
            var toggle = new MongoDbFeatureToggleModel { Name = "ft1", Enabled = enabled };
            DataSeed.SeedFeatureToggles(new[] { toggle });
            var patchRequest = new PatchFeatureToggleRequest
            {
                Operation = patchPropertyName,
                Value = patchPropertyValue
            };

            //Act
            var response = await Client.PatchAsync($"{ApiUrl}/{toggle.Id}", CreateHttpContent(new[] { patchRequest }));

            //Assert
            AssertResponse(response, expectedStatusCode);

            var expectedStatusCodeAsInt = (int)expectedStatusCode;
            var isSuccessStatusCode = expectedStatusCodeAsInt >= 200 && expectedStatusCodeAsInt <= 299;
            var expectedEnabledValue = isSuccessStatusCode ? !enabled : enabled;

            var models = await GetToggles();
            Assert.AreEqual(1, models.Count);
            var model = models[0];
            Assert.AreEqual(expectedEnabledValue, model.Enabled, assertMessage);
        }

        [Test]
        public async Task Delete()
        {
            //Arrange
            var toggleToDelete = new MongoDbFeatureToggleModel { Name = "ft1", Enabled = true };
            var toggleThatRemains = new MongoDbFeatureToggleModel { Name = "ft2", Enabled = true };
            DataSeed.SeedFeatureToggles(new[] { toggleToDelete, toggleThatRemains });

            //Act
            var response = await Client.DeleteAsync($"{ApiUrl}/{toggleToDelete.Id}");

            //Assert
            AssertNoContentResponse(response);

            var models = await GetToggles();
            Assert.AreEqual(1, models.Count);
            var model = models[0];
            Assert.AreEqual(toggleThatRemains.Id, model.Id);
        }

        [Test]
        [TestCase("ft1", "ft1", true, "exists must return true")]
        [TestCase("ft1", "ft2", false, "exists must return false")]
        public async Task Exists(string existingToggleName, string testToggleName, bool expectedResult, string assertMessage)
        {
            //Arrange
            var toggle = new MongoDbFeatureToggleModel { Name = existingToggleName, Enabled = true };
            DataSeed.SeedFeatureToggles(new[] { toggle });

            //Act
            var response = await Client.GetAsync($"{ApiUrl}/exists/{testToggleName}");

            //Assert
            AssertOkResponse(response);

            var stringResponse = await response.Content.ReadAsStringAsync();
            var existsResponse = JsonConvert.DeserializeObject<ExistsResponse>(stringResponse);

            Assert.AreEqual(expectedResult, existsResponse.Exists, assertMessage);
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

        private async Task<List<FeatureToggleViewModel>> GetToggles()
        {
            var getResponse = await Client.GetAsync(ApiUrl);
            var stringResponse = await getResponse.Content.ReadAsStringAsync();
            var models = JsonConvert.DeserializeObject<IEnumerable<FeatureToggleViewModel>>(stringResponse).ToList();
            return models;
        }

        private static StringContent CreateHttpContent<T>(T postRequest) where T : class
        {
            return new StringContent(JsonConvert.SerializeObject(postRequest), Encoding.UTF8, "application/json");
        }

        public class ExistsResponse
        {
            public bool Exists { get; set; }
        }
    }
}
