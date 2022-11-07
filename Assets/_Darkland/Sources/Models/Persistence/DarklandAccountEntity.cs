using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static JetBrains.Annotations.ImplicitUseTargetFlags;

namespace _Darkland.Sources.Models.Persistence {

    [UsedImplicitly(Members)]
    public record DarklandAccountEntity {
        [BsonId]
        public ObjectId id { get; private set; }
        [BsonRequired]
        public string name;
    }

    [UsedImplicitly(Members)]
    public record DarklandPlayerCharacterEntity {
        [BsonId]
        public ObjectId id { get; private set; }
        [BsonRequired]
        public ObjectId darklandAccountId;
        [BsonRequired]
        public string name;
    }

}