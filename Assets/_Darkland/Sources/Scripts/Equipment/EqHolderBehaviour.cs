using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Equipment;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Equipment {

    public class EqHolderBehaviour : NetworkBehaviour, IEqHolder {

        public Dictionary<WearableSlot, WearableItemDef> EquippedWearables { get; } = new();
        public List<IEqItemDef> Backpack { get; } = new();
        public int BackpackSize { get; private set; }

        public event Action<List<IEqItemDef>> ServerBackpackChanged;
        public event Action<WearableSlot, WearableItemDef> ServerEquippedWearable;
        public event Action<WearableSlot, WearableItemDef> ServerUnequippedWearable;

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
        public void EquipWearable(int backpackSlot, WearableSlot wearableSlot) {
            var itemDef = Backpack[backpackSlot];

            Assert.IsTrue(backpackSlot < BackpackSize);
            Assert.IsNotNull(itemDef);
            Assert.IsTrue(itemDef.ItemType == EqItemType.Wearable);

            var wearable = (IWearable)itemDef;
            Assert.AreEqual(wearable.WearableItemSlot, wearableSlot);

            if (EquippedWearables[wearableSlot] != null) {
                ServerInsertToBackpack(backpackSlot, EquippedWearables[wearableSlot].itemDef);
            }
            else {
                RemoveFromBackpack(backpackSlot);
            }
            
            EquippedWearables[wearableSlot] = new WearableItemDef {
                wearable = wearable,
                itemDef = itemDef
            };
        }

        [Server]
        public void UnequipWearable(WearableSlot wearableSlot) {
            if (Backpack.Count >= BackpackSize) return; //todo maybe message to client?

            var wearableItemDef = EquippedWearables[wearableSlot];
            AddToBackpack(wearableItemDef.itemDef);
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
        public void PickupFromGround(IOnGroundEqItem onGroundItem) {
            if (Backpack.Count >= BackpackSize) return; //todo maybe message to client?
            Assert.IsNotNull(onGroundItem);

            var item = EqItemsContainer._.ItemDef2(onGroundItem.ItemName);
            AddToBackpack(item);
            
            OnGroundItemsManager._.ServerDestroyOnGroundItem(onGroundItem);
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
        private void ServerInsertToBackpack(int backpackSlot, IEqItemDef item) {
            Assert.IsNotNull(item);
            Assert.IsTrue(backpackSlot > -1 && backpackSlot < BackpackSize);
            Assert.IsTrue(backpackSlot < Backpack.Count);

            Backpack[backpackSlot] = item;
            ServerBackpackChanged?.Invoke(Backpack);
        }

    }

}