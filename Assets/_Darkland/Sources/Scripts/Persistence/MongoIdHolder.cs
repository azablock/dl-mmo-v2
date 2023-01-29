using _Darkland.Sources.Models.Persistence;
using Mirror;
using MongoDB.Bson;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Persistence {

    public class MongoIdHolder : MonoBehaviour, IMongoIdHolder {
        
        public ObjectId mongoId { get; private set; }

        [Server]
        public void Set(ObjectId id) {
            Assert.IsTrue(mongoId.Equals(ObjectId.Empty));
            mongoId = id;
        }

    }

}