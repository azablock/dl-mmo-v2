using _Darkland.Sources.Models.Persistence;
using MongoDB.Bson;
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

            var darklandAccountsCollection = _db.GetCollection<DarklandAccountEntity>("darklandAccount");
            var darklandAccounts = darklandAccountsCollection.FindAsync(entity => true);
            darklandAccounts.Result.ForEachAsync((entity, i) => {
                Debug.LogWarning($"[{i}] {entity}");
            });

            var darklandPlayerCharactersCollection =
                _db.GetCollection<DarklandPlayerCharacterEntity>("darklandPlayerCharacter");

            const string accountName = "azab";
            var darklandAccount = darklandAccountsCollection
                .FindAsync(entity => entity.name == accountName)
                .Result
                .FirstAsync()
                .Result;

            Debug.LogWarning($"[{accountName}] account characters:");
            
            darklandPlayerCharactersCollection
                .FindAsync(entity => entity.darklandAccountId.Equals(darklandAccount.id))
                .Result
                .ForEachAsync((entity, i) => {
                    Debug.LogWarning($"[{i}] {entity}");
                });

            var e = new DarklandPlayerCharacterEntity{name = "asd", darklandAccountId = ObjectId.Empty};
        }
    }

}