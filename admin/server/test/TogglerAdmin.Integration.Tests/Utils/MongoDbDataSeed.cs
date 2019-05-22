using System;
using MongoDB.Driver;
using TogglerAdmin.Data.MongoDb;

namespace TogglerAdmin.Integration.Tests.Utils
{
    public class MongoDbDataSeed
    {
        private readonly IMongoCollection<MongoDbFeatureToggleModel> _featureToggles;

        public MongoDbDataSeed(string connectionString, string databaseName)
        {
            var client = new MongoClient(connectionString);
            var database = client.GetDatabase(databaseName);
            _featureToggles = database.GetCollection<MongoDbFeatureToggleModel>("FeatureToggles");
        }

        public void Seed()
        {            
            _featureToggles.InsertOne(CreateModel("ft1", true, "ft1"));
            _featureToggles.InsertOne(CreateModel("ft2", false, "ft2"));
            _featureToggles.InsertOne(CreateModel("ft3", true, "ft3"));
        }

        private MongoDbFeatureToggleModel CreateModel(string name, bool enabled, string description)
        {
            return new MongoDbFeatureToggleModel
            {
                Name = name,
                Enabled = enabled,
                Description = description,
                Creator = "seed",
                CreatedAt = DateTime.UtcNow
            };
        }

        internal void Cleanup()
        {
            _featureToggles.DeleteMany<MongoDbFeatureToggleModel>(ftm => true);
        }
    }
}
