using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public static class DarklandHeroMessages {

        public struct GetHeroSheetRequestMessage : NetworkMessage {
            public string heroName;
        }

        public struct GetHeroSheetResponseMessage : NetworkMessage {
            public int heroLevel;
            public string heroName;
            public UnitTraits heroTraits;
            public HeroVocation heroVocation;
        }

    }

}