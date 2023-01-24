using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Persistence.Entity;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _Darkland.Sources.Models.Persistence {

    public sealed class DarklandHeroRepository : Repository<DarklandHeroEntity> {

        public IEnumerable<DarklandHeroEntity> FindAllByDarklandAccountId(ObjectId id) =>
            GetCollection()
                .Find(entity => entity.darklandAccountId.Equals(id))
                .SortByDescending(it => it.createDate)
                .ToList();

        public DarklandHeroEntity FindByName(string name) => GetCollection()
            .Find(entity => entity.name.Equals(name))
            .FirstOrDefault();

        public bool ExistsByName(string name) => FindAll().Any(entity => entity.name.Equals(name));

        public override string collectionName => "darklandHero";
    }

}