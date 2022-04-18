using System;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit {

    //todo use interface
    public class TargetNetworkIdentityHolderBehaviour : NetworkBehaviour {

        public event Action<NetworkIdentity> Changed;

        [Server]
        public void ServerChangeTarget(NetworkIdentity newTargetNetworkIdentity) {
            _targetNetworkIdentity = newTargetNetworkIdentity;
            Changed?.Invoke(_targetNetworkIdentity);
        }
        
        [Server]
        public NetworkIdentity ServerOwnerNetworkIdentity() => netIdentity;

        [Server]
        public NetworkIdentity ServerTargetNetworkIdentity() => _targetNetworkIdentity;

        private NetworkIdentity _targetNetworkIdentity;

    }

}