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

        //stats
        [BsonRequired]
        public int health;
        [BsonRequired]
        public int maxHealth; //todo to bedzie wyliczane, wiec bez zapisu do bazy w przyszlosci?

        //other
        [BsonRequired]
        public int xp;

    }

}