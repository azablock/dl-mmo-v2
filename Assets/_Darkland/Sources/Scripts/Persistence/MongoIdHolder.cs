using Mirror;
using MongoDB.Bson;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Persistence {

    public class MongoIdHolder : MonoBehaviour {
        
        public ObjectId mongoId { get; private set; }

        [Server]
        public void ServerSetMongoId(ObjectId id) {
            if (!mongoId.Equals(ObjectId.Empty)) {
                //throw exception?
            }
            
            mongoId = id;
        }

    }

}