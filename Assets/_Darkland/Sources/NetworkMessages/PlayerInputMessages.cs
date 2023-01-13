using Mirror;
using UnityEngine;

namespace _Darkland.Sources.NetworkMessages {

    public static class PlayerInputMessages {

        public struct MoveRequestMessage : NetworkMessage {
            public Vector3Int movementVector;
        }

        public struct ChangeFloorRequestMessage : NetworkMessage {
            public Vector3Int movementVector;
        }
        
        public struct NpcClickRequestMessage : NetworkMessage {
            public uint npcNetId;
        }
    }

}