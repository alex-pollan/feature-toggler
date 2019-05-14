using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<IFeatureToggleModel> Get()
        {
            return _featureToggles.Find(ft => true).ToList();
        }

        public IFeatureToggleModel GetByName(string name)
        {
            return _featureToggles.Find(ft => ft.Name == name).FirstOrDefault();
        }

        public IFeatureToggleModel Get(string id)
        {
            return _featureToggles.Find(ft => ft.Id == id).FirstOrDefault();
        }

        public IFeatureToggleModel Create(IFeatureToggleModel model)
        {
            var concreteModel = new MongoDbFeatureToggleModel(model);

            if (_featureToggles.Find(ft => ft.Name == model.Name).Any())
            {
                throw new DuplicateFeatureToggleNameException(model.Name);
            }

            _featureToggles.InsertOne(concreteModel);

            return concreteModel;
        }

        public void Update(string id, IFeatureToggleModel model)
        {
            var concreteModel = new MongoDbFeatureToggleModel(model);

            _featureToggles.ReplaceOne(ft => ft.Id == id, concreteModel);
        }

        public void Remove(string id)
        {
            _featureToggles.DeleteOne(ft => ft.Id == id);
        }

        public IFeatureToggleModel CreateModel()
        {
            return new MongoDbFeatureToggleModel();
        }
    }
}
