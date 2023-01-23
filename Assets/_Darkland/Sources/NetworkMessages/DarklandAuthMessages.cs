using System.Collections.Generic;
using _Darkland.Sources.Models.Hero;
using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class DarklandAuthMessages {

        public struct GetHeroesRequestMessage : NetworkMessage { }

        public struct GetDarklandHeroesResponseMessage : NetworkMessage {
            public List<DarklandHeroDto> heroes;
        }

        public struct NewHeroRequestMessage : NetworkMessage {
            public string heroName;
            public HeroVocation heroVocation;
        }

        public struct NewDarklandHeroResponseMessage : NetworkMessage {
            public bool success;
            public string message;
        }
        
        public struct HeroEnterGameRequestMessage : NetworkMessage {
            public string selectedHeroName;
        }
        
        public struct DarklandHeroEnterGameResponseMessage : NetworkMessage {
            
        }

        public struct DarklandPlayerDisconnectRequestMessage : NetworkMessage {
        }

        public struct DarklandPlayerDisconnectResponseMessage : NetworkMessage {
            public NetworkIdentity disconnectedPlayerNetworkIdentity;
        }

    }

}