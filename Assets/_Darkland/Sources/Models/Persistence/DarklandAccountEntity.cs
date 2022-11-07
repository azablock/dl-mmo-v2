using JetBrains.Annotations;
using MongoDB.Bson;
using static JetBrains.Annotations.ImplicitUseTargetFlags;

namespace _Darkland.Sources.Models.Persistence {

    [UsedImplicitly(Members)]
    public record DarklandAccountEntity {
        public ObjectId id;
        public string accountName;
    }

}