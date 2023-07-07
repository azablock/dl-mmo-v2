using System;
using Mirror;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Unit {

    public class GoldHolderBehaviour : NetworkBehaviour, IGoldHolder {

        [field: SyncVar(hook = nameof(ClientSyncGoldAmount))]
        public int GoldAmount { get; private set; }

        public event Action<int> ClientGoldAmountChanged;

        [Server]
        public void Set(int val) {
            Assert.IsTrue(val >= 0);
            GoldAmount = val;
        }

        [Client]
        private void ClientSyncGoldAmount(int _, int val) {
            ClientGoldAmountChanged?.Invoke(val);
        }

    }

}