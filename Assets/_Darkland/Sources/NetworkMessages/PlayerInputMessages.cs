using Mirror;
using UnityEngine;

namespace _Darkland.Sources.NetworkMessages {

    public static class PlayerInputMessages {

        public struct MoveRequestMessage : NetworkMessage {
            public Vector3Int movementVector;
        }

    }

}