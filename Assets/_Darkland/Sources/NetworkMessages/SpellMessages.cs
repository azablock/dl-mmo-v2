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

    }

}