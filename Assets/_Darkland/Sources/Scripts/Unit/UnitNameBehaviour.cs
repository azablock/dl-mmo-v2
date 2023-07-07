using System;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitNameBehaviour : NetworkBehaviour {

        [SyncVar(hook = nameof(ClientSyncUnitName))]
        public string unitName;

        public event Action<string> ServerUnitNameChanged;
        public event Action<string> ClientUnitNameReceived;

        [Server]
        public void ServerSet(string val) {
            unitName = val;
            ServerUnitNameChanged?.Invoke(unitName);
        }

        [Client]
        private void ClientSyncUnitName(string _, string val) {
            ClientUnitNameReceived?.Invoke(val);
        }

    }

}