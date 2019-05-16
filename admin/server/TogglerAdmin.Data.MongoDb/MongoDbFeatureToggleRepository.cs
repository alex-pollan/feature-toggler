using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TogglerAdmin.Abstractions.Data;
using TogglerAdmin.Abstractions.Data.Models;
using TogglerAdmin.Abstractions.Exceptions;

namespace TogglerAdmin.Data.MongoDb
{
    public class MongoDbFeatureToggleRepository : IFeatureToggleRepository
    {
        private readonly IMongoCollection<MongoDbFeatureToggleModel> _featureToggles;

        public MongoDbFeatureToggleRepository(MongoDbConfiguration configuration)
        {
            var client = new MongoClient(configuration.ConnectionString);
            var database = client.GetDatabase(configuration.DatabaseName);
            _featureToggles = database.GetCollection<MongoDbFeatureToggleModel>("FeatureToggles");
        }

        public async Task<IEnumerable<IFeatureToggleModel>> Get()
        {
            return (await _featureToggles.FindAsync(ft => true)).ToList();
        }

        public async Task<IFeatureToggleModel> GetByName(string name)
        {
            return (await _featureToggles.FindAsync(ft => ft.Name == name)).FirstOrDefault();
        }

        public async Task<IFeatureToggleModel> Get(string id)
        {
            return (await _featureToggles.FindAsync(ft => ft.Id == id)).FirstOrDefault();
        }

        public async Task<IFeatureToggleModel> Create(IFeatureToggleModel model)
        {
            var concreteModel = new MongoDbFeatureToggleModel(model);

            if ((await _featureToggles.FindAsync(ft => ft.Name == model.Name)).Any())
            {
                throw new DuplicateFeatureToggleNameException(model.Name);
            }

            await _featureToggles.InsertOneAsync(concreteModel);

            return concreteModel;
        }

        public async Task Update(string id, IFeatureToggleModel model)
        {
            var concreteModel = new MongoDbFeatureToggleModel(model);

            await _featureToggles.ReplaceOneAsync(ft => ft.Id == id, concreteModel);
        }

        public async Task Remove(string id)
        {
            await _featureToggles.DeleteOneAsync(ft => ft.Id == id);
        }
        
        public async Task Enable(string id, bool enable)
        {
            await _featureToggles.UpdateOneAsync(ft => ft.Id == id,
                Builders<MongoDbFeatureToggleModel>.Update.Set(s => s.Enabled, enable));
        }

        public IFeatureToggleModel CreateModel()
        {
            return new MongoDbFeatureToggleModel();
        }
    }
}
