using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Equipment {

    public class EqChangeServerListenerBehaviour : NetworkBehaviour, IEqChangeServerListener {

        private IEqHolder _eqHolder;
        //todo to pewnie nowy skrypt...
        private IStatsHolder _statsHolder;
        
        public event Action<List<string>> ClientBackpackChanged;
        public event Action<WearableSlot, string> ClientWearableEquipped;
        public event Action<WearableSlot, string> LocalPlayerWearableEquipped;
        public event Action<WearableSlot> ClientWearableCleared;
        public event Action<WearableSlot> LocalPlayerWearableCleared;

        private void Awake() {
            _eqHolder = GetComponent<IEqHolder>();
            _statsHolder = GetComponent<IStatsHolder>();
        }

        public override void OnStartServer() {
            _eqHolder.ServerBackpackChanged += ServerOnBackpackChanged;
            _eqHolder.ServerEquippedWearable += ServerOnEquippedWearable;
            _eqHolder.ServerUnequippedWearable += ServerOnUnequippedWearable;
            
            foreach (var (wearableSlot, itemName) in _eqHolder.EquippedWearables) {
                ServerOnEquippedWearable(wearableSlot, itemName);
            }
        }

        public override void OnStopServer() {
            _eqHolder.ServerBackpackChanged -= ServerOnBackpackChanged;
            _eqHolder.ServerEquippedWearable -= ServerOnEquippedWearable;
            _eqHolder.ServerUnequippedWearable -= ServerOnUnequippedWearable;
        }

        [Server]
        private void ServerOnBackpackChanged(List<IEqItemDef> backpack) {
            Assert.IsTrue(backpack?.Count <= _eqHolder.BackpackSize);
            TargetRpcBackpackChanged(backpack.Select(it => it.ItemName).ToList());
        }
        
        [Server]
        private void ServerOnEquippedWearable(WearableSlot _, string itemName) {
            var wearable = (IWearable) EqItemsContainer.ItemDef2(itemName);
            wearable.StatBonuses.ForEach(it => _statsHolder.Add(it.statId, StatVal.OfBonus(it.buffValue)));
        }

        [Server]
        private void ServerOnUnequippedWearable(WearableSlot _, string itemName) {
            var wearable = (IWearable) EqItemsContainer.ItemDef2(itemName);
            wearable.StatBonuses.ForEach(it => _statsHolder.Subtract(it.statId, StatVal.OfBonus(it.buffValue)));
        }

        [TargetRpc]
        private void TargetRpcBackpackChanged(List<string> itemNames) => ClientBackpackChanged?.Invoke(itemNames);

        public override void OnStartClient() {
            _eqHolder.EquippedWearables.Callback += EquippedWearablesOnCallback;

            foreach (var (wearableSlot, itemName) in _eqHolder.EquippedWearables) {
                ClientWearableEquipped?.Invoke(wearableSlot, itemName);
            }
        }

        public override void OnStopClient() {
            _eqHolder.EquippedWearables.Callback -= EquippedWearablesOnCallback;
        }

        [Client]
        private void EquippedWearablesOnCallback(SyncIDictionary<WearableSlot, string>.Operation op,
                                                 WearableSlot wearableSlot,
                                                 string itemName) {
            switch (op)
            {
                case SyncIDictionary<WearableSlot, string>.Operation.OP_ADD:
                    // entry added
                    if (isLocalPlayer) {
                        LocalPlayerWearableEquipped?.Invoke(wearableSlot, itemName);
                    }
                    
                    ClientWearableEquipped?.Invoke(wearableSlot, itemName);
                    break;
                case SyncIDictionary<WearableSlot, string>.Operation.OP_SET:
                    // entry changed
                    break;
                case SyncIDictionary<WearableSlot, string>.Operation.OP_REMOVE:
                    // entry removed
                    if (isLocalPlayer) {
                        LocalPlayerWearableCleared?.Invoke(wearableSlot);
                    }
                    
                    ClientWearableCleared?.Invoke(wearableSlot);
                    break;
                case SyncIDictionary<WearableSlot, string>.Operation.OP_CLEAR:
                    // Dictionary was cleared
                    break;
            }
        }
        
    }

}