using System.Collections.Generic;
using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class DarklandAuthMessages {

        public struct DarklandAuthRequestMessage : NetworkMessage {
            public bool asBot;
        }

        public struct DarklandAuthResponseMessage : NetworkMessage {
            public NetworkIdentity spawnedPlayerNetworkIdentity;
        }

        public struct GetPlayerCharactersRequestMessage : NetworkMessage { }

        public struct GetPlayerCharactersResponseMessage : NetworkMessage {
            public List<string> playerCharacterNames;
        }

        public struct DarklandPlayerDisconnectRequestMessage : NetworkMessage {
        }

        public struct DarklandPlayerDisconnectResponseMessage : NetworkMessage {
            public NetworkIdentity disconnectedPlayerNetworkIdentity;
        }

    }

}