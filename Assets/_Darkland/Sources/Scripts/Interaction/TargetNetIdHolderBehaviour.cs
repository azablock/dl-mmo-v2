using System;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Interaction {

    public class TargetNetIdHolderBehaviour : NetworkBehaviour, ITargetNetIdHolder {

        public float maxTargetDistance = 10.0f; //should be equal to NetworkServer.aoi range
        private IDiscretePosition _discretePosition;
        private IDeathEventEmitter _deathEventEmitter;

        public NetworkIdentity targetNetIdentity { get; private set; }

        public event Action<NetworkIdentity> ServerChanged;
        public event Action<NetworkIdentity> ClientChanged;

        public override void OnStartServer() {
            _deathEventEmitter = GetComponent<DeathHandlerBehaviour2>();
            _discretePosition = GetComponent<IDiscretePosition>();
            
            _discretePosition.Changed += ServerOnOwnerPosChanged;
            _deathEventEmitter.Death += ServerDisconnectFromTarget;
        }

        public override void OnStopServer() {
            _discretePosition.Changed -= ServerOnTargetPosChanged;
            _deathEventEmitter.Death -= ServerDisconnectFromTarget;
        }

        [Server]
        public void ServerSet(uint newTargetNetId) {
            if (!NetworkServer.spawned.ContainsKey(newTargetNetId)) return;

            var holderPos = _discretePosition.Pos;
            var newTargetNetIdentity = NetworkServer.spawned[newTargetNetId];
            var targetPos = newTargetNetIdentity.GetComponent<IDiscretePosition>().Pos;

            if (!ServerIsInTargetDistance(holderPos, targetPos)) return;
            
            if (targetNetIdentity != null && targetNetIdentity.netId == newTargetNetId) return;
            
            if (targetNetIdentity != null) ServerDisconnectFromTarget();
                
            targetNetIdentity = newTargetNetIdentity;
            ServerChanged?.Invoke(targetNetIdentity);

            ServerConnectToTarget();

            TargetRpcUpdate(targetNetIdentity);
        }

        [Server]
        private void ServerConnectToTarget() {
            targetNetIdentity.GetComponent<IDiscretePosition>().Changed += ServerOnTargetPosChanged;
            targetNetIdentity.GetComponent<DeathHandlerBehaviour2>().Death += ServerDisconnectFromTarget;
        }

        [Server]
        private void ServerDisconnectFromTarget() {
            targetNetIdentity.GetComponent<IDiscretePosition>().Changed -= ServerOnTargetPosChanged;
            targetNetIdentity.GetComponent<DeathHandlerBehaviour2>().Death -= ServerDisconnectFromTarget;
        }

        [Server]
        private void ServerOnTargetPosChanged(PositionChangeData data) => ServerCheckDistance(_discretePosition.Pos, data.pos);

        [Server]
        private void ServerOnOwnerPosChanged(PositionChangeData data) {
            if (targetNetIdentity == null) return;

            ServerCheckDistance(data.pos, targetNetIdentity.GetComponent<IDiscretePosition>().Pos);
        }

        [Server]
        private void ServerCheckDistance(Vector3Int holderPos, Vector3Int targetPos) {
            if (ServerIsInTargetDistance(holderPos, targetPos)) {
                ServerDisconnectFromTarget();
            }
        }

        [Server]
        private bool ServerIsInTargetDistance(Vector3Int holderPos, Vector3Int targetPos) =>
            Vector3.Distance(holderPos, targetPos) < maxTargetDistance;

        [TargetRpc]
        private void TargetRpcUpdate(NetworkIdentity identity) => ClientChanged?.Invoke(identity);
    }

}