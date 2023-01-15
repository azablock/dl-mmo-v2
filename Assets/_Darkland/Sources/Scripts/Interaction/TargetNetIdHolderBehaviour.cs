using System;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Interaction {

    public class TargetNetIdHolderBehaviour : NetworkBehaviour, ITargetNetIdHolder {

        [SerializeField]
        private float maxTargetDistance;
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
            Assert.IsTrue(NetworkServer.spawned.ContainsKey(newTargetNetId));

            if (netId == newTargetNetId) return;

            var holderPos = _discretePosition.Pos;
            var newTargetNetIdentity = NetworkServer.spawned[newTargetNetId];
            var targetPos = newTargetNetIdentity.GetComponent<IDiscretePosition>().Pos;

            if (!ServerPositionsValid(holderPos, targetPos)) return;

            if (TargetNetIdentity != null && TargetNetIdentity.netId != newTargetNetId) Clear();

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
            if (TargetNetIdentity != null && TargetNetIdentity.netId == identity.netId) Clear();
        }

        [Server]
        private void ServerOnTargetPosChanged(PositionChangeData data) =>
            ServerCheckPositions(_discretePosition.Pos, data.pos);

        [Server]
        private void ServerOnOwnerPosChanged(PositionChangeData data) {
            if (TargetNetIdentity == null) return;
            ServerCheckPositions(data.pos, TargetNetIdentity.GetComponent<IDiscretePosition>().Pos);
        }

        [Server]
        private void ServerCheckPositions(Vector3Int holderPos, Vector3Int targetPos) {
            if (!ServerPositionsValid(holderPos, targetPos)) Clear();
        }

        [Server]
        private bool ServerPositionsValid(Vector3Int holderPos, Vector3Int targetPos) {
            var isInTargetDistance = Vector3.Distance(holderPos, targetPos) < maxTargetDistance;
            var zPositionsMEqual = holderPos.z == targetPos.z;

            return isInTargetDistance && zPositionsMEqual;
        }
        
    }

}