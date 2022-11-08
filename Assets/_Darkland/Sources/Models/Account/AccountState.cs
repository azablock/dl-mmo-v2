using System.Collections.Generic;
using MongoDB.Bson;

namespace _Darkland.Sources.Models.Account {

    public record AccountState {
        public ObjectId accountId;
        public ObjectId selectedPlayerCharacterId;
        public IEnumerable<PlayerCharacterState> playerCharacters;
    }

    public record PlayerCharacterState {
        public ObjectId playerCharacterId;
        public string name;
    }

}