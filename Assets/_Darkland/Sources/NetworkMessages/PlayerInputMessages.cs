using Mirror;
using UnityEngine;

namespace _Darkland.Sources.NetworkMessages {

    public static class PlayerInputMessages {

        public struct MoveRequestMessage : NetworkMessage {
            public Vector3Int movementVector;
        }

        public struct ChangeFloorRequestMessage : NetworkMessage {
        }
        
        public struct NpcClickRequestMessage : NetworkMessage {
            public uint npcNetId;
        }

        public struct GetHealthStatsRequestMessage : NetworkMessage {
            public uint statsHolderNetId;
        }

        public struct GetHealthStatsResponseMessage : NetworkMessage {
            public float health;
            public float maxHealth;
            public uint statsHolderNetId;
            public string unitName; //todo change message struct name
        }

        public struct CastSpellRequestMessage : NetworkMessage {

            public int spellIdx;

        }

        // public struct CastSpellResponseMessage : NetworkMessage {
        //
        //     public int spellIdx;
        //     public bool success;
        //     public string status;
        //     public float cooldown;
        //
        // }

    }

}