using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Equipment;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Equipment {

    public class EqChangeServerListenerBehaviour : NetworkBehaviour, IEqChangeServerListener {

        private IEqHolder _eqHolder;
        
        public event Action<List<string>> ClientBackpackChanged;
        public event Action<WearableSlot, string> ClientWearableEquipped;
        public event Action<WearableSlot> ClientWearableCleared;

        private void Awake() {
            _eqHolder = GetComponent<IEqHolder>();
        }

        public override void OnStartServer() {
            _eqHolder.ServerBackpackChanged += ServerOnBackpackChanged;
            _eqHolder.ServerEquippedWearable += ServerOnEquippedWearable;
            _eqHolder.ServerUnequippedWearable += TargetRpcUnequippedWearable;
        }

        public override void OnStopServer() {
            _eqHolder.ServerBackpackChanged -= ServerOnBackpackChanged;
            _eqHolder.ServerEquippedWearable -= ServerOnEquippedWearable;
            _eqHolder.ServerUnequippedWearable -= TargetRpcUnequippedWearable;
        }

        [Server]
        private void ServerOnBackpackChanged(List<IEqItemDef> backpack) {
            Assert.IsTrue(backpack?.Count <= _eqHolder.BackpackSize);
            TargetRpcBackpackChanged(backpack.Select(it => it.ItemName).ToList());
        }

        [Server]
        private void ServerOnEquippedWearable(WearableSlot wearableSlot, WearableItemDef item) {
            TargetRpcEquippedWearable(wearableSlot, item.itemDef.ItemName);
        }

        [TargetRpc]
        private void TargetRpcBackpackChanged(List<string> itemNames) => ClientBackpackChanged?.Invoke(itemNames);

        [TargetRpc]
        private void TargetRpcEquippedWearable(WearableSlot wearableSlot, string itemName) =>
            ClientWearableEquipped?.Invoke(wearableSlot, itemName);

        [TargetRpc]
        private void TargetRpcUnequippedWearable(WearableSlot wearableSlot) =>
            ClientWearableCleared?.Invoke(wearableSlot);

    }

}