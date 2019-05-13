using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using TogglerAdmin.Abstractions.Data.Models;

namespace TogglerAdmin.Data.MongoDb
{
    public class MongoDbFeatureToggleModel : IFeatureToggleModel
    {
        public MongoDbFeatureToggleModel() { }

        public MongoDbFeatureToggleModel(IFeatureToggleModel model)
        {
            Name = model.Name;
            Description = model.Description;
            Enabled = model.Enabled;
            Creator = model.Creator;
            CreatedAt = model.CreatedAt;
            Modifier = model.Modifier;
            ModifiedAt = model.ModifiedAt;
        }

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }

        [BsonElement("Name")]
        public string Name { get; set; }

        [BsonElement("Description")]
        public string Description { get; set; }

        [BsonElement("IsEnabled")]
        public bool Enabled { get; set; }

        [BsonElement("Creator")]
        public string Creator { get; set; }

        [BsonElement("CreatedAt")]
        [BsonDateTimeOptions]
        public DateTime CreatedAt { get; set; }

        [BsonElement("Modifier")]
        public string Modifier { get; set; }

        [BsonElement("ModifiedAt")]
        [BsonDateTimeOptions]
        public DateTime? ModifiedAt { get; set; }
    }
}
