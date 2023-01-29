using MongoDB.Bson;

namespace _Darkland.Sources.Models.Persistence {

    public interface IMongoIdHolder {

        void Set(ObjectId id);
        ObjectId mongoId { get; }

    }

}