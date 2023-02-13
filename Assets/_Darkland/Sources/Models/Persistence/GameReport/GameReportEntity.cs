using System;
using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using static JetBrains.Annotations.ImplicitUseTargetFlags;

namespace _Darkland.Sources.Models.Persistence.GameReport {

    [UsedImplicitly(Members)]
    public record GameReportEntity : MongoEntity {
        [BsonRequired]
        public ObjectId darklandHeroId;
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createDate;
        [BsonRequired]
        public string title;
        [BsonRequired]
        public string content;
        [BsonRequired]
        public string gameReportType;
    }

}