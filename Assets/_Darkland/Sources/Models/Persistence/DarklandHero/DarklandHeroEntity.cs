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
        public string race;
        [BsonRequired]
        public int posX;
        [BsonRequired]
        public int posY;
        [BsonRequired]
        public int posZ;

        //stats
        [BsonRequired]
        public int health;
        [BsonRequired]
        public int mana;
        [BsonRequired]
        public Dictionary<string, int> learnedSpells; //spell name - spell level

        //other
        [BsonRequired]
        public int xp;
        [BsonRequired]
        public int level;
        [BsonRequired]
        public int hairId;
        [BsonRequired]
        public int hairColorId;

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
        [BsonRequired]
        public List<string> itemNames;
        [BsonRequired]
        public Dictionary<string, string> equippedWearables;
        [BsonRequired]
        public int goldAmount;
    }

}