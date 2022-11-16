using System;
using JetBrains.Annotations;
using MongoDB.Bson.Serialization.Attributes;
using static JetBrains.Annotations.ImplicitUseTargetFlags;

namespace _Darkland.Sources.Models.Persistence.Entity {

    [UsedImplicitly(Members)]
    public record DarklandAccountEntity : MongoEntity {
        [BsonRequired]
        public string name;
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createDate;
    }

}