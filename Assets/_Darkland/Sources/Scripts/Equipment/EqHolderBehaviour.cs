using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Equipment;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Equipment {

    public class EqHolderBehaviour : NetworkBehaviour, IEqHolder {

        public SyncDictionary<WearableSlot, string> EquippedWearables { get; } = new();

        public List<IEqItemDef> Backpack { get; } = new();
        public int BackpackSize { get; private set; }

        public event Action<List<IEqItemDef>> ServerBackpackChanged;
        public event Action<WearableSlot, string> ServerEquippedWearable;
        public event Action<WearableSlot, string> ServerUnequippedWearable;

        private IDiscretePosition _discretePosition;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartServer() {
            BackpackSize = 8;
        }

        [Server]
        public void UseConsumable(int backpackSlot) {
            Assert.IsTrue(backpackSlot < BackpackSize);
            Assert.IsTrue(Backpack[backpackSlot]?.ItemType == EqItemType.Consumable);

            var consumable = (IConsumable)Backpack[backpackSlot];
            consumable.Consume(gameObject);

            RemoveFromBackpack(backpackSlot);
        }

        [Server]
        public void SetWearable(WearableSlot wearableSlot, string wearableName) {
            var itemDef = EqItemsContainer.ItemDef2(wearableName);
            Assert.IsNotNull(itemDef);
            Assert.IsTrue(itemDef.ItemType == EqItemType.Wearable);
            
            var wearable = (IWearable)itemDef;
            Assert.AreEqual(wearable.WearableItemSlot, wearableSlot);
            
            EquippedWearables[wearableSlot] = itemDef.ItemName;
            ServerEquippedWearable?.Invoke(wearableSlot, EquippedWearables[wearableSlot]);
        }

        [Server]
        public void EquipWearableFromBackpack(int backpackSlot) {
            var itemDef = Backpack[backpackSlot];

            Assert.IsTrue(backpackSlot < BackpackSize);
            Assert.IsNotNull(itemDef);
            Assert.IsTrue(itemDef.ItemType == EqItemType.Wearable);

            var wearable = (IWearable)itemDef;
            var wearableSlot = wearable.WearableItemSlot;
            Assert.AreEqual(wearable.WearableItemSlot, wearableSlot);

            //todo EquippedWearables.ContainsKey(wearableSlot) <---- tu jest bug bo zostalo w backpack
            if (EquippedWearables.ContainsKey(wearableSlot) && EquippedWearables[wearableSlot] != null) {
                var currentlyEquippedWearable = EqItemsContainer.ItemDef2(EquippedWearables[wearableSlot]);
                ServerReturnWearableToBackpack(backpackSlot, currentlyEquippedWearable);
            }
            else {
                RemoveFromBackpack(backpackSlot);
            }

            EquippedWearables[wearableSlot] = itemDef.ItemName;
            ServerEquippedWearable?.Invoke(wearableSlot, EquippedWearables[wearableSlot]);
        }

        [Server]
        public void UnequipWearableToBackpack(WearableSlot wearableSlot) {
            if (Backpack.Count >= BackpackSize) return; //todo maybe message to client?

            var wearableItemName = EquippedWearables[wearableSlot];
            var eqItemDef = EqItemsContainer.ItemDef2(wearableItemName);
            AddToBackpack(eqItemDef);

            EquippedWearables.Remove(wearableSlot);
            ServerUnequippedWearable?.Invoke(wearableSlot, wearableItemName);
        }

        [Server]
        public void PickupFromGround(IOnGroundEqItem onGroundItem) {
            if (Backpack.Count >= BackpackSize) return; //todo maybe message to client?
            Assert.IsNotNull(onGroundItem);

            var item = EqItemsContainer.ItemDef2(onGroundItem.ItemName);
            AddToBackpack(item);

            OnGroundItemsManager._.ServerDestroyOnGroundItem(onGroundItem);
        }

        [Server]
        public void DropOnGround(int backpackSlot) {
            Assert.IsTrue(backpackSlot > -1 && backpackSlot < BackpackSize);
            Assert.IsNotNull(Backpack[backpackSlot]);

            var pos = _discretePosition.Pos;
            var item = Backpack[backpackSlot];
            OnGroundItemsManager._.ServerCreateOnGroundItem(pos, item.ItemName);

            RemoveFromBackpack(backpackSlot);
        }

        [Server]
        public void AddToBackpack(IEqItemDef item) {
            Assert.IsNotNull(item);

            Backpack.Add(item);
            ServerBackpackChanged?.Invoke(Backpack);
        }

        public void RemoveFromBackpack(int backpackSlot) {
            Assert.IsTrue(backpackSlot > -1 && backpackSlot < BackpackSize);
            Assert.IsNotNull(Backpack[backpackSlot]);

            Backpack.RemoveAt(backpackSlot);
            ServerBackpackChanged?.Invoke(Backpack);
        }

        [Server]
        private void ServerReturnWearableToBackpack(int backpackSlot, IEqItemDef item) {
            Assert.IsNotNull(item);
            Assert.IsTrue(backpackSlot > -1 && backpackSlot < BackpackSize);
            Assert.IsTrue(backpackSlot < Backpack.Count);

            Backpack[backpackSlot] = item;
            ServerBackpackChanged?.Invoke(Backpack);

            var wearable = (IWearable) EqItemsContainer.ItemDef2(item.ItemName);
            ServerUnequippedWearable?.Invoke(wearable.WearableItemSlot, item.ItemName);
        }

    }

}