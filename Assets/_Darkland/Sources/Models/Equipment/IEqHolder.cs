using System;
using System.Collections.Generic;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Models.Equipment {

    public interface IEqHolder {
        
        Dictionary<WearableSlot, WearableItemDef> EquippedWearables { get; }
        List<IEqItemDef> Backpack { get; }
        int BackpackSize { get; }

        void EquipWearable(int backpackSlot);
        void UnequipWearable(WearableSlot wearableSlot);
        void DropOnGround(int backpackSlot);
        void PickupFromGround(IOnGroundEqItem onGroundItem);
        void UseConsumable(int backpackSlot);
        void AddToBackpack(IEqItemDef item);
        void RemoveFromBackpack(int backpackSlot);

        event Action<List<IEqItemDef>> ServerBackpackChanged;
        event Action<WearableSlot, WearableItemDef> ServerEquippedWearable;
        event Action<WearableSlot> ServerUnequippedWearable;

    }

    public static class EqHolderExtensions {

        [Server]
        public static bool ServerBackpackFull(this IEqHolder eqHolder) {
            return eqHolder.BackpackSize == eqHolder.Backpack.Count;
        }

        [Server]
        public static IEqItemDef ServerBackpackItem(this IEqHolder eqHolder, int backpackSlot) {
            Assert.IsTrue(backpackSlot > -1 && backpackSlot < eqHolder.BackpackSize);
            return eqHolder.Backpack[backpackSlot];
        }
        
    } 

}