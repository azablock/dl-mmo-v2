using System.Collections.Generic;
using _Darkland.Sources.Models.Ai;
using Mirror;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Darkland.Sources.Scripts.Ai {

    public class AiCombatMemory : MonoBehaviour {

        [SerializeField]
        private AiNetworkPerceptionBehaviour2 aiNetworkPerception;

        private readonly HashSet<NetworkIdentity> _combatTargetsHistory = new();

        [ServerCallback]
        private void Awake() {
            DarklandNetworkManager.serverOnPlayerDisconnected += ServerOnPlayerDisconnected;
            aiNetworkPerception.PerceptionZones[AiPerceptionZoneType.Passive].TargetExitedZone += OnTargetExitedPassiveZone;
        }

        [ServerCallback]
        private void OnDestroy() {
            DarklandNetworkManager.serverOnPlayerDisconnected -= ServerOnPlayerDisconnected;
            aiNetworkPerception.PerceptionZones[AiPerceptionZoneType.Passive].TargetExitedZone -= OnTargetExitedPassiveZone;
        }

        [Server]
        public void Add(NetworkIdentity identity) {
            _combatTargetsHistory.Add(identity);
        }

        public void Clear() {
            _combatTargetsHistory.Clear();
        }

        [Server]
        public bool HasInHistory(NetworkIdentity identity) => _combatTargetsHistory.Contains(identity);

        [Server]
        private void ServerOnPlayerDisconnected(NetworkIdentity identity) {
            _combatTargetsHistory.Remove(identity);
        }

        [Server]
        private void OnTargetExitedPassiveZone(NetworkIdentity identity) {
            _combatTargetsHistory.Remove(identity);
        }

    }

}