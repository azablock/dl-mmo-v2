using System;
using _Darkland.Sources.Models.Interaction;
using Mirror;

namespace _Darkland.Sources.Scripts.Interaction {

    public class TargetNetIdClientNotifierBehaviour : NetworkBehaviour, ITargetNetIdClientNotifier {

        private ITargetNetIdHolder _targetNetIdHolder;

        public event Action<NetworkIdentity> ClientChanged;
        public event Action<NetworkIdentity> ClientCleared;

        private void Awake() {
            _targetNetIdHolder = GetComponent<ITargetNetIdHolder>();
        }

        public override void OnStartServer() {
            _targetNetIdHolder.ServerChanged += ServerOnTargetNetIdentityChanged;
            _targetNetIdHolder.ServerCleared += ServerOnTargetNetIdentityCleared;
        }

        public override void OnStopServer() {
            _targetNetIdHolder.ServerChanged -= ServerOnTargetNetIdentityChanged;
            _targetNetIdHolder.ServerCleared -= ServerOnTargetNetIdentityCleared;
        }

        [Server]
        private void ServerOnTargetNetIdentityChanged(NetworkIdentity identity) => TargetRpcUpdate(identity);

        [Server]
        private void ServerOnTargetNetIdentityCleared(NetworkIdentity identity) => TargetRpcClear(identity);

        [TargetRpc]
        private void TargetRpcUpdate(NetworkIdentity identity) => ClientChanged?.Invoke(identity);

        [TargetRpc]
        private void TargetRpcClear(NetworkIdentity identity) => ClientCleared?.Invoke(identity);
    }

}