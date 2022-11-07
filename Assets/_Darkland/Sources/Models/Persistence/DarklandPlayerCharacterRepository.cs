using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _Darkland.Sources.Models.Persistence {

    public sealed class DarklandPlayerCharacterRepository : Repository<DarklandPlayerCharacterEntity> {

        public IEnumerable<DarklandPlayerCharacterEntity> FindAllByDarklandAccountId(ObjectId id) =>
            GetCollection()
                .Find(entity => entity.darklandAccountId.Equals(id))
                .ToList();

        public override string collectionName => "darklandPlayerCharacter";
    }

}