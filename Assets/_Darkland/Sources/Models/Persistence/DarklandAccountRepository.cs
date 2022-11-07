using MongoDB.Driver;

namespace _Darkland.Sources.Models.Persistence {

    public sealed class DarklandAccountRepository : Repository<DarklandAccountEntity> {

        public DarklandAccountEntity FindByName(string name) => GetCollection()
                                                                .Find(entity => entity.name.Equals(name))
                                                                .FirstOrDefault();

        public override string collectionName => "darklandAccount";
    }

}