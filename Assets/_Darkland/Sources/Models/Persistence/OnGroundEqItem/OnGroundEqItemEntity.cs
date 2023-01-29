using System;
using JetBrains.Annotations;
using MongoDB.Bson.Serialization.Attributes;

namespace _Darkland.Sources.Models.Persistence.OnGroundEqItem {

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public record OnGroundEqItemEntity : MongoEntity {

        [BsonRequired]
        public string itemName;
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createDate;
        [BsonRequired]
        public int posX;
        [BsonRequired]
        public int posY;
        [BsonRequired]
        public int posZ;

    }

}