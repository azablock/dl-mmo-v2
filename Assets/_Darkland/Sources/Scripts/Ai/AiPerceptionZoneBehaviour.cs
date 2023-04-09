using System;
using System.Collections.Generic;
using _Darkland.Sources.Models.Ai;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Ai {

    public class AiPerceptionZoneBehaviour : MonoBehaviour {

        [SerializeField]
        private AiPerceptionZoneType zoneType;
        [SerializeField]
        private BoxCollider boxCollider;
        [SerializeField]
        private AiNetworkPerceptionBehaviour2 aiNetworkPerception;

        [ServerCallback]
        private void Start() {
            var range = aiNetworkPerception.PerceptionZoneRange(zoneType);
            boxCollider.size = new Vector3(range, range, 0.1f);
            
            DarklandNetworkManager.serverOnPlayerDisconnected += ServerOnPlayerDisconnected;
        }

        [ServerCallback]
        private void OnDestroy() {
            DarklandNetworkManager.serverOnPlayerDisconnected -= ServerOnPlayerDisconnected;
        }

        [ServerCallback]
        private void OnTriggerEnter(Collider other) {
            var networkIdentity = other.GetComponent<NetworkIdentity>();
            var darklandHero = other.GetComponent<DarklandHeroBehaviour>();
                
            if (!networkIdentity || !darklandHero) return;

            perceptionZone.OnTargetEnter(networkIdentity);
        }

        [ServerCallback]
        private void OnTriggerExit(Collider other) {
            var networkIdentity = other.GetComponent<NetworkIdentity>();
            var darklandHero = other.GetComponent<DarklandHeroBehaviour>();
                
            if (!networkIdentity || !darklandHero) return;

            perceptionZone.OnTargetExit(networkIdentity);
        }

        [Server]
        private void ServerOnPlayerDisconnected(NetworkIdentity identity) {
            perceptionZone.OnTargetExit(identity);
        }

        private AiNetworkPerceptionZone perceptionZone => aiNetworkPerception.PerceptionZones[zoneType];

    }

}