using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _Darkland.Sources.Models.Persistence.Entity {

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public record DarklandHeroEntity : MongoEntity {
        [BsonRequired]
        public ObjectId darklandAccountId;
        [BsonRequired]
        public string name;
        [BsonRequired]
        public int posX;
        [BsonRequired]
        public int posY;
        [BsonRequired]
        public int posZ;
    }

}