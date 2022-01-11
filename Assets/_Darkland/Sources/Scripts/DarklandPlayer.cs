using Mirror;
using Sources.Scripts;
using UnityEngine;

namespace _Darkland.Sources.Scripts {

    public class DarklandPlayer : NetworkBehaviour {

        [SyncVar]
        public string characterName;

        public override void OnStartClient() {
            Debug.Log("[DarklandPlayer]: Client started at " + NetworkTime.time);
            DarklandNetworkManager.clientDarklandPlayerConnected?.Invoke(netIdentity);
        }

        public override void OnStopClient() {
            Debug.Log("[DarklandPlayer]: Client stopped at " + NetworkTime.time);
            DarklandNetworkManager.clientDarklandPlayerDisconnected?.Invoke(netIdentity);
        }
    }

}