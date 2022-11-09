using System;
using MongoDB.Driver;

namespace _Darkland.Sources.Models.Persistence {

    public sealed class DarklandAccountRepository : Repository<DarklandAccountEntity> {

        public void Create(string name) {
            /*
             https://stackoverflow.com/questions/37782251/auto-populate-date-in-mongodb-on-insert

             The problem with most driver will be then to make sure the new object is created on mondodb server- not on the machine where the code is running. 
             You driver hopefully allows to run raw insert command.
             Both will serve the purpose of avoiding time differences/ time sync issue between application server machines.
             */
            
            GetCollection().InsertOne(new DarklandAccountEntity{name = name, createDate = DateTime.Now});
        }
        
        public DarklandAccountEntity FindByName(string name) => GetCollection()
                                                                .Find(entity => entity.name.Equals(name))
                                                                .FirstOrDefault();

        public bool ExistsByName(string name) => FindByName(name) != null;
        
        public override string collectionName => "darklandAccount";
    }

}