using System;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Interaction {

    public class TargetNetIdHolderBehaviour : NetworkBehaviour, ITargetNetIdHolder {

        private IDiscretePosition _discretePosition;
        private IDeathEventEmitter _deathEventEmitter;
        
        public NetworkIdentity TargetNetIdentity { get; private set; }
        public float MaxTargetDistance => MaxTargetDis; 

        public const float MaxTargetDis = 8;//todo should be equal to vis range (AOI NetworkManager)?
        
        public event Action<NetworkIdentity> ServerChanged;
        public event Action<NetworkIdentity> ServerCleared;

        public override void OnStartServer() {
            _deathEventEmitter = GetComponent<DarklandUnitDeathBehaviour>().DeathEventEmitter;
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

            //todo zakladam ze jednak mozna kliknac na siebie samego
            // if (netId == newTargetNetId) return;
            if (TargetNetIdentity != null && TargetNetIdentity.netId == newTargetNetId) return;

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
            //todo tutaj chyba jest jakis maly bug - po smierci gracza gracz mial nadal targetowanego szczura
            //- moze ten if jest zly??
            if (TargetNetIdentity == null) return;
            
            TargetNetIdentity.GetComponent<IDiscretePosition>().Changed -= ServerOnTargetPosChanged;
            TargetNetIdentity.GetComponent<DarklandUnitDeathBehaviour>().DeathEventEmitter.Death -= Clear;

            ServerCleared?.Invoke(TargetNetIdentity);
            
            TargetNetIdentity = null;
        }

        [Server]
        private void ServerConnectToTarget() {
            TargetNetIdentity.GetComponent<IDiscretePosition>().Changed += ServerOnTargetPosChanged;
            TargetNetIdentity.GetComponent<DarklandUnitDeathBehaviour>().DeathEventEmitter.Death += Clear;
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
            var isInTargetDistance = Vector3.Distance(holderPos, targetPos) < MaxTargetDistance;
            var zPositionsMEqual = holderPos.z == targetPos.z;

            return isInTargetDistance && zPositionsMEqual;
        }
        
    }

}