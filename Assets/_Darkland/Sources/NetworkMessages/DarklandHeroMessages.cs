using System.Collections.Generic;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;

namespace _Darkland.Sources.NetworkMessages {

    public struct WearableDto {

        public string itemName;
        public WearableSlot wearableSlot;

    }
    
    public static class DarklandHeroMessages {

        public struct GetHeroSheetRequestMessage : NetworkMessage {
            public string heroName;
        }

        public struct GetHeroSheetResponseMessage : NetworkMessage {
            public int heroLevel;
            public string heroName;
            public UnitTraits heroTraits;
            public HeroVocationType heroVocationType;
        }

        public struct GetEqRequestMessage : NetworkMessage { }

        public struct GetEqResponseMessage : NetworkMessage {

            public List<string> itemNames;
            public List<WearableDto> equippedWearables;
            public int goldAmount;

        }

    }

}