using MongoDB.Bson;

namespace _Darkland.Sources.Models.Persistence {

    public interface IMongoIdHolder {

        ObjectId mongoId { get; }

    }

}