using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Equipment {

    public interface IEqHolder {
        
        Dictionary<WearableSlot, WearableItemDef> EquippedWearables { get; }
        List<IEqItemDef> Backpack { get; }
        int BackpackSize { get; }

        void EquipWearable(int backpackSlot, WearableSlot wearableSlot);
        void UnequipWearable(WearableSlot wearableSlot);
        void DropOnGround(int backpackSlot);
        void PickupFromGround(IOnGroundEqItem onGroundItem);
        void UseConsumable(int backpackSlot);
        void AddToBackpack(IEqItemDef item);
        void RemoveFromBackpack(int backpackSlot);


        event Action<List<IEqItemDef>> ServerBackpackChanged;
        event Action<WearableSlot, WearableItemDef> ServerEquippedWearable;
        event Action<WearableSlot, WearableItemDef> ServerUnequippedWearable;

    }

}