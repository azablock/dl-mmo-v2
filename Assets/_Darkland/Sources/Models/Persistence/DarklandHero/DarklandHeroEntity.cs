using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace _Darkland.Sources.Models.Persistence.DarklandHero {

    [UsedImplicitly(ImplicitUseTargetFlags.Members)]
    public record DarklandHeroEntity : MongoEntity {

        [BsonRequired]
        public ObjectId darklandAccountId;
        [BsonRequired]
        [BsonDateTimeOptions(Kind = DateTimeKind.Local)]
        public DateTime createDate;
        [BsonRequired]
        public string name;
        [BsonRequired]
        public string vocation;
        [BsonRequired]
        public int posX;
        [BsonRequired]
        public int posY;
        [BsonRequired]
        public int posZ;

        //stats
        [BsonRequired]
        public int health;

        //other
        [BsonRequired]
        public int xp;
        [BsonRequired]
        public int level;

        //traits
        [BsonRequired]
        public int might;
        [BsonRequired]
        public int constitution;
        [BsonRequired]
        public int dexterity;
        [BsonRequired]
        public int intellect;
        [BsonRequired]
        public int soul;
        
        //equipment
        public List<string> itemNames;

    }

}