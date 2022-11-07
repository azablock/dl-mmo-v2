using System.Linq;
using _Darkland.Sources.Models.Persistence;
using MongoDB.Driver;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Persistence {

    public class DarklandDatabaseManager : MonoBehaviour {

        public static readonly DarklandAccountRepository darklandAccountRepository = new();
        public static readonly DarklandPlayerCharacterRepository darklandPlayerCharacterRepository = new();

        private static IMongoDatabase _db;
        private static MongoClient _client;

        private void Awake() {
            const string hostname = "70.34.242.30";
            const string dbName = "darkland";
            var uri = $"mongodb://root:rootpassword@{hostname}:27017";

            _client = new MongoClient(uri);
            _db = _client.GetDatabase(dbName);

            Debug.LogWarning("================ DB connected ================");

            var darklandAccountEntities = darklandAccountRepository
                                          .FindAll()
                                          .ToList();

            for (var i = 0; i < darklandAccountEntities.Count; i++) Debug.LogWarning($"[{i}] {darklandAccountEntities[i]}");


            const string accountName = "azab";
            var darklandAccount = darklandAccountRepository.FindByName(accountName);
            Debug.LogWarning($"[{accountName}] account characters:");

            var playerCharacterEntities = darklandPlayerCharacterRepository
                .FindAllByDarklandAccountId(darklandAccount.id)
                .ToList();

            for (var i = 0; i < playerCharacterEntities.Count; i++) Debug.LogWarning($"[{i}] {playerCharacterEntities[i]}");

            // var e = new DarklandPlayerCharacterEntity { name = "asd", darklandAccountId = ObjectId.Empty };
        }

        public static IMongoCollection<T> GetCollection<T>(string collectionName) => _db.GetCollection<T>(collectionName);
    }

}