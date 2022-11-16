using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _Darkland.Sources.Models.Persistence.Entity {

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public record MongoEntity {
        [BsonId]
        public ObjectId id { get; private set; }
    }

}