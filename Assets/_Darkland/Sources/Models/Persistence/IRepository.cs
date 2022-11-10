using System.Collections.Generic;
using _Darkland.Sources.Scripts.Persistence;
using MongoDB.Bson;
using MongoDB.Driver;

namespace _Darkland.Sources.Models.Persistence {

    public interface IRepository<T> where T : MongoEntity {
        IMongoCollection<T> GetCollection();
        IEnumerable<T> FindAll();
        T FindById(ObjectId id);
        void Create(T entity);
    }

    public abstract class Repository<T> : IRepository<T> where T : MongoEntity {

        public IMongoCollection<T> GetCollection() => DarklandDatabaseManager.GetCollection<T>(collectionName);

        public IEnumerable<T> FindAll() => GetCollection()
                                           .Find(entity => true)
                                           .ToList();

        public T FindById(ObjectId id) => GetCollection()
                                          .Find(entity => entity.id.Equals(id))
                                          .FirstOrDefault();

        public void Create(T entity) => GetCollection().InsertOne(entity);

        public abstract string collectionName { get; }
    }

}