using Mirror;
using MongoDB.Bson;

namespace _Darkland.Sources.Models.Equipment {

    public interface IOnGroundEqItem {

        void Init(string itemName, ObjectId itemMongoId);
        string ItemName { get; }
        ObjectId ItemMongoId { get; }
        NetworkIdentity NetIdentity { get; }

    }

}