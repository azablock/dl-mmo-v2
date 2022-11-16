using System.Collections.Generic;

namespace _Darkland.Sources.Models.Account {

    public record DarklandAuthState {
        public string accountName;
        public List<string> heroNames;
        public string selectedHeroName;
        public bool isRegister;
        public bool isBot;
    }

}