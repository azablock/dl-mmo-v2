using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Equipment {

    public interface IEqChangeServerListener {

        event Action<List<string>> ClientBackpackChanged;
        event Action<WearableSlot, string> ClientWearableEquipped;
        event Action<WearableSlot, string> LocalPlayerWearableEquipped;
        event Action<WearableSlot> ClientWearableCleared;
        event Action<WearableSlot> LocalPlayerWearableCleared;

    }

}