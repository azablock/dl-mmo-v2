using System.Collections.Generic;
using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class SpellMessages {

        public struct GetAvailableSpellsRequestMessage : NetworkMessage {
        }

        public struct GetAvailableSpellsResponseMessage : NetworkMessage {

            public List<string> spellNames;

        }

    }

}