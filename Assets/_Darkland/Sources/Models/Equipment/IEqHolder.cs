using System;
using System.Collections.Generic;
using _Darkland.Sources.Scripts.Equipment;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Models.Equipment {

    public interface IEqHolder {
        
        SyncDictionary<WearableSlot, string> EquippedWearables { get; }
        List<IEqItemDef> Backpack { get; }
        int BackpackSize { get; }

        void SetWearable(WearableSlot wearableSlot, string wearableName);
        void EquipWearableFromBackpack(int backpackSlot);
        void UnequipWearableToBackpack(WearableSlot wearableSlot);
        void DropOnGround(int backpackSlot);
        void PickupFromGround(IOnGroundEqItem onGroundItem);
        void UseConsumable(int backpackSlot);
        void AddToBackpack(IEqItemDef item);
        void RemoveFromBackpack(int backpackSlot);

        event Action<List<IEqItemDef>> ServerBackpackChanged;
        event Action<WearableSlot, string> ServerEquippedWearable;
        event Action<WearableSlot, string> ServerUnequippedWearable;

    }

    public static class EqHolderExtensions {

        [Server]
        public static bool ServerBackpackFull(this IEqHolder eqHolder) {
            return eqHolder.BackpackSize == eqHolder.Backpack.Count;
        }

        [Server]
        public static IEqItemDef ServerBackpackItem(this IEqHolder eqHolder, int backpackSlot) {
            Assert.IsTrue(backpackSlot > -1 && backpackSlot < eqHolder.Backpack.Count);
            return eqHolder.Backpack[backpackSlot];
        }

        public static IWeaponDef ServerEquippedWeapon(this IEqHolder eqHolder) =>
            eqHolder.EquippedWearables.ContainsKey(WearableSlot.RightHand)
                ? (IWeaponDef) EqItemsContainer.ItemDef2(eqHolder.EquippedWearables[WearableSlot.RightHand])
                : null;

    } 

}