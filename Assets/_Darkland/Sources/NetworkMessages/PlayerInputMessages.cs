using _Darkland.Sources.Models.Equipment;
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
    
        public struct NpcClearRequestMessage : NetworkMessage {
        }

        public struct GetHealthStatsRequestMessage : NetworkMessage {
            public uint statsHolderNetId;
        }

        public struct GetHealthStatsResponseMessage : NetworkMessage {
            public float health;
            public float maxHealth;
            public float mana;
            public float maxMana;
            public uint statsHolderNetId;
            public string unitName; //todo change message struct name
        }

        public struct CastSpellRequestMessage : NetworkMessage {
            public int spellIdx;
        }
        
        public struct PickupItemRequestMessage : NetworkMessage {
            public Vector3Int eqItemPos;
        }

        public struct DropItemRequestMessage : NetworkMessage {
            public int backpackSlot;
        }

        public struct UseItemRequestMessage : NetworkMessage {
            public int backpackSlot;
        }

        public struct UnequipWearableRequestMessage : NetworkMessage {
            public WearableSlot wearableSlot;
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