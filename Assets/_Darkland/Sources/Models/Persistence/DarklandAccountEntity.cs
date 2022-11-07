using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static JetBrains.Annotations.ImplicitUseTargetFlags;

namespace _Darkland.Sources.Models.Persistence {

    [UsedImplicitly(Members)]
    public record MongoEntity {
        [BsonId]
        public ObjectId id { get; private set; }
    }
    
    [UsedImplicitly(Members)]
    public record DarklandAccountEntity : MongoEntity {
        [BsonRequired]
        public string name;
    }

    [UsedImplicitly(Members)]
    public record DarklandPlayerCharacterEntity : MongoEntity {
        [BsonRequired]
        public ObjectId darklandAccountId;
        [BsonRequired]
        public string name;
    }

}