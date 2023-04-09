using Mirror;
using MongoDB.Bson;
using UnityEngine;

namespace _Darkland.Sources.Models.Equipment {

    public interface IOnGroundEqItem {

        void Init(string itemName, ObjectId itemMongoId, Vector3Int discretePos);
        string ItemName { get; }
        ObjectId ItemMongoId { get; }
        NetworkIdentity NetIdentity { get; }
        Vector3Int Pos { get; }

    }

}