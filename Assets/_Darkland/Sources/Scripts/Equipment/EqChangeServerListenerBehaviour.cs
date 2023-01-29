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

        private void Awake() {
            _eqHolder = GetComponent<IEqHolder>();
        }

        public override void OnStartServer() {
            _eqHolder.ServerBackpackChanged += ServerOnBackpackChanged;
        }

        public override void OnStopServer() {
            _eqHolder.ServerBackpackChanged -= ServerOnBackpackChanged;
        }

        [Server]
        private void ServerOnBackpackChanged(List<IEqItemDef> backpack) {
            Assert.IsTrue(backpack?.Count <= _eqHolder.BackpackSize);
            TargetRpcBackpackChanged(backpack.Select(it => it.ItemName).ToList());
        }

        [TargetRpc]
        private void TargetRpcBackpackChanged(List<string> itemNames) => ClientBackpackChanged?.Invoke(itemNames);

    }

}