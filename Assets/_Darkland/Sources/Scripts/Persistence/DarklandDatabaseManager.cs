using _Darkland.Sources.Models.Persistence;
using _Darkland.Sources.Models.Persistence.DarklandAccount;
using _Darkland.Sources.Models.Persistence.DarklandHero;
using _Darkland.Sources.Models.Persistence.GameReport;
using _Darkland.Sources.Models.Persistence.OnGroundEqItem;
using MongoDB.Driver;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Persistence {

    public class DarklandDatabaseManager : MonoBehaviour {

        public static readonly DarklandAccountRepository darklandAccountRepository = new();
        public static readonly DarklandHeroRepository darklandHeroRepository = new();
        public static readonly OnGroundEqItemRepository onGroundEqItemRepository = new();
        public static readonly GameReportRepository gameReportRepository = new();

        private static IMongoDatabase _db;
        private static MongoClient _client;

        private void Awake() {
            const string hostname = "70.34.242.30";
            const string dbName = "darkland";
            var uri = $"mongodb://root:rootpassword@{hostname}:27017";

            _client = new MongoClient(uri);
            _db = _client.GetDatabase(dbName);
        }

        public static IMongoCollection<T> GetCollection<T>(string collectionName) => _db.GetCollection<T>(collectionName);
    }

}