using System.Collections.Generic;

namespace _Darkland.Sources.Models.Account {

    public record DarklandAuthState {
        public string accountName;
        public List<string> playerCharacterNames;
        public string selectedPlayerCharacterName;
        public bool isRegister;
        public bool isBot;
    }

}