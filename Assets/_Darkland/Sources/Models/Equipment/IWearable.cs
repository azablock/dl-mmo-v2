using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.Models.Equipment {

    [Serializable]
    public struct WearableStatBonus {

        public StatId statId;
        public float buffValue;

    }

    public interface IWearable {

        WearableSlot WearableItemSlot { get; }
        WearableType WearableItemType { get; }
        List<WearableStatBonus> StatBonuses { get; }

    }

}