using _Darkland.Sources.Models.Persistence;
using MongoDB.Driver;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Persistence {

    public class DarklandDatabaseManager : MonoBehaviour {

        private IMongoDatabase _db;
        private MongoClient _client;

        private void Awake() {
            const string hostname = "70.34.242.30";
            const string dbName = "darkland";
            var uri = $"mongodb://root:rootpassword@{hostname}:27017";

            _client = new MongoClient(uri);
            _db = _client.GetDatabase(dbName);

            Debug.LogWarning("================ DB connected ================");

            _db.ListCollectionNames()
               .ForEachAsync((s, i) => { Debug.LogWarning($"collection[{i}] name: {s}\n"); });

            var mongoCollection = _db.GetCollection<DarklandAccountEntity>("darkland-account");
            var findAsync = mongoCollection.FindAsync(entity => true);

            findAsync.Result.ForEachAsync((entity, i) => {
                Debug.LogWarning($"entity[{i}] {entity}");
            });
        }
    }

}