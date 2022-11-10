using System.Collections.Generic;
using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class DarklandAuthMessages {

        public struct GetPlayerCharactersRequestMessage : NetworkMessage { }

        public struct GetPlayerCharactersResponseMessage : NetworkMessage {
            public List<string> playerCharacterNames;
        }

        public struct NewPlayerCharacterRequestMessage : NetworkMessage {
            public string playerCharacterName;
        }

        public struct NewPlayerCharacterResponseMessage : NetworkMessage {
            public bool success;
            public string message;
        }
        
        public struct PlayerEnterGameRequestMessage : NetworkMessage {
            public string selectedPlayerCharacterName;
        }
        
        public struct PlayerEnterGameResponseMessage : NetworkMessage {
            
        }

        public struct DarklandPlayerDisconnectRequestMessage : NetworkMessage {
        }

        public struct DarklandPlayerDisconnectResponseMessage : NetworkMessage {
            public NetworkIdentity disconnectedPlayerNetworkIdentity;
        }

    }

}