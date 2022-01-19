using System;
using _Darkland.Sources.Models.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public class DiscretePositionBehaviour : NetworkBehaviour {

        private IDiscretePosition _discretePosition;

        public event Action<Vector3Int> ClientChanged;

        private void Awake() {
            _discretePosition = new DiscretePosition();
        }

        public override void OnStartServer() {
            _discretePosition.Changed += ClientRpcPositionChanged;
        }

        public override void OnStopServer() {
            _discretePosition.Changed -= ClientRpcPositionChanged;
        }

        [Server]
        public void ServerSet(Vector3Int pos) {
            _discretePosition.Set(pos);
        }

        [ClientRpc]
        private void ClientRpcPositionChanged(Vector3Int pos) {
            ClientChanged?.Invoke(pos);
        }
    }

}