using System;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Interaction {

    public class TargetNetIdHolderBehaviour : NetworkBehaviour, ITargetNetIdHolder {

        public float maxTargetDistance = 4.0f; //should be equal to NetworkServer.aoi range
        private IDiscretePosition _discretePosition;
        private IDeathEventEmitter _deathEventEmitter;
        
        public NetworkIdentity TargetNetIdentity { get; private set; }

        public event Action<NetworkIdentity> ServerChanged;
        public event Action<NetworkIdentity> ServerCleared;

        public override void OnStartServer() {
            _deathEventEmitter = GetComponent<DeathHandlerBehaviour2>();
            _discretePosition = GetComponent<IDiscretePosition>();

            _discretePosition.Changed += ServerOnOwnerPosChanged;
            _deathEventEmitter.Death += Clear;
            DarklandNetworkManager.serverOnPlayerDisconnected += ServerOnClientDisconnected;
        }

        public override void OnStopServer() {
            Clear();
            
            _discretePosition.Changed -= ServerOnTargetPosChanged;
            _deathEventEmitter.Death -= Clear;
            DarklandNetworkManager.serverOnPlayerDisconnected -= ServerOnClientDisconnected;
        }

        [Server]
        public void Set(uint newTargetNetId) {
            if (!NetworkServer.spawned.ContainsKey(newTargetNetId)) return;
            if (netId == newTargetNetId) return;

            var holderPos = _discretePosition.Pos;
            var newTargetNetIdentity = NetworkServer.spawned[newTargetNetId];
            var targetPos = newTargetNetIdentity.GetComponent<IDiscretePosition>().Pos;

            if (!ServerIsInTargetDistance(holderPos, targetPos)) return;

            if (TargetNetIdentity != null && TargetNetIdentity.netId == newTargetNetId) return;

            if (TargetNetIdentity != null) Clear();

            TargetNetIdentity = newTargetNetIdentity;
            ServerChanged?.Invoke(TargetNetIdentity);

            ServerConnectToTarget();
        }
        
        [Server]
        public void Clear() {
            if (TargetNetIdentity == null) return;
            
            TargetNetIdentity.GetComponent<IDiscretePosition>().Changed -= ServerOnTargetPosChanged;
            TargetNetIdentity.GetComponent<DeathHandlerBehaviour2>().Death -= Clear;

            ServerCleared?.Invoke(TargetNetIdentity);
            
            TargetNetIdentity = null;
        }


        [Server]
        private void ServerConnectToTarget() {
            TargetNetIdentity.GetComponent<IDiscretePosition>().Changed += ServerOnTargetPosChanged;
            TargetNetIdentity.GetComponent<DeathHandlerBehaviour2>().Death += Clear;
        }

        [Server]
        private void ServerOnClientDisconnected(NetworkIdentity identity) {
            if (TargetNetIdentity != null && TargetNetIdentity.netId == identity.netId) {
                Clear();
            }
        }

        [Server]
        private void ServerOnTargetPosChanged(PositionChangeData data) =>
            ServerCheckDistance(_discretePosition.Pos, data.pos);

        [Server]
        private void ServerOnOwnerPosChanged(PositionChangeData data) {
            if (TargetNetIdentity == null) return;
            ServerCheckDistance(data.pos, TargetNetIdentity.GetComponent<IDiscretePosition>().Pos);
        }

        [Server]
        private void ServerCheckDistance(Vector3Int holderPos, Vector3Int targetPos) {
            if (!ServerIsInTargetDistance(holderPos, targetPos) || holderPos.z != targetPos.z) {
                Clear();
            }
        }

        [Server]
        private bool ServerIsInTargetDistance(Vector3Int holderPos, Vector3Int targetPos) =>
            Vector3.Distance(holderPos, targetPos) < maxTargetDistance;
        
    }

}