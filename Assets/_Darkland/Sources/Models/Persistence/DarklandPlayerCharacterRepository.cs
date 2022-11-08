using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _Darkland.Sources.Models.Persistence {

    public sealed class DarklandPlayerCharacterRepository : Repository<DarklandPlayerCharacterEntity> {

        public IEnumerable<DarklandPlayerCharacterEntity> FindAllByDarklandAccountId(ObjectId id) =>
            GetCollection()
                .Find(entity => entity.darklandAccountId.Equals(id))
                .ToList();

        public DarklandPlayerCharacterEntity FindByName(string name) => GetCollection()
            .Find(entity => entity.name.Equals(name))
            .FirstOrDefault();

        public bool ExistsByName(string name) => FindAll().Any(entity => entity.name.Equals(name));

        public override string collectionName => "darklandPlayerCharacter";
    }

}