using System.Collections.Generic;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.NetworkMessages {

    public static class SpellMessages {

        public struct GetAvailableSpellsRequestMessage : NetworkMessage {
        }

        public struct GetAvailableSpellsResponseMessage : NetworkMessage {
            public List<string> spellNames;
        }
        
        public struct FireballSpellVfxResponseMessage : NetworkMessage {

            public Vector3Int castPos;
            public Vector3Int targetPos;
            public float fireballFlyDuration;

        }

        public struct TransferManaSpellVfxResponseMessage : NetworkMessage {

            public Vector3Int castPos;
            public Vector3Int targetPos;

        }

        public struct HealSpellVfxResponseMessage : NetworkMessage {
            public Vector3Int targetPos;
        }

        public struct DarkNovaSpellVfxResponseMessage : NetworkMessage {
            public Vector3Int castPos;
            public float radius;

        }

        public struct CircleOfLightSpellVfxResponseMessage : NetworkMessage {
            public Vector3Int castPos;
            public float radius;
            public float duration;

        }

    }

}