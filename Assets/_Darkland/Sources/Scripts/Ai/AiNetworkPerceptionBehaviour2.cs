using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Scripts.Interaction;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Ai {

    public class AiNetworkPerceptionBehaviour2 : MonoBehaviour, IAiNetworkPerception {

        [SerializeField]
        [Range(1, TargetNetIdHolderBehaviour.MaxTargetDis)]
        private float attackPerceptionRange;
        [Range(1, TargetNetIdHolderBehaviour.MaxTargetDis)]
        [SerializeField]
        private float chasePerceptionRange;
        
        private ITargetNetIdHolder _targetNetIdHolder;
        
        public HashSet<uint> VisiblePlayerNetIds { get; } = new();
        public float AttackPerceptionRange => attackPerceptionRange;
        public float ChasePerceptionRange => chasePerceptionRange;

        private void Start() {
            _targetNetIdHolder = GetComponent<ITargetNetIdHolder>();
            
            Assert.IsTrue(_targetNetIdHolder.MaxTargetDistance > attackPerceptionRange);
            Assert.IsTrue(_targetNetIdHolder.MaxTargetDistance > chasePerceptionRange);
            Assert.IsTrue(chasePerceptionRange >= attackPerceptionRange);

            DarklandNetworkManager.serverOnPlayerDisconnected += ServerOnPlayerDisconnected;
        }

        private void OnDestroy() {
            DarklandNetworkManager.serverOnPlayerDisconnected -= ServerOnPlayerDisconnected;
        }

        [ServerCallback]
        private void OnTriggerEnter(Collider other) {
       
        }

        [ServerCallback]
        private void OnTriggerExit(Collider other) {
         
        }

        [Server]
        private void ServerOnPlayerDisconnected(NetworkIdentity identity) { }
    }

}