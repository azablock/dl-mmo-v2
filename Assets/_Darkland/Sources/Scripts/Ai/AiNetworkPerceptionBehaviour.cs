using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.Interaction;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Ai {

    // todo teraz jest bug jak AI zginie (Death event), to wtedy robi sie Clear na _targetNetIdHolder -> i nie mamy aktualnego/dobrego stanu VisiblePlayerNetIds
    public class AiNetworkPerceptionBehaviour : MonoBehaviour, IAiNetworkPerception {

        private ITargetNetIdHolder _targetNetIdHolder;
        private BoxCollider _boxCollider;

        [ServerCallback]
        private void Start() {
            _targetNetIdHolder = GetComponent<ITargetNetIdHolder>();
            _boxCollider = GetComponent<BoxCollider>();
            _boxCollider.size = new Vector3(4.0f, 4.0f, 0.25f);

            DarklandNetworkManager.serverOnPlayerDisconnected += ServerOnPlayerDisconnected;
        }

        [ServerCallback]
        private void OnDestroy() {
            DarklandNetworkManager.serverOnPlayerDisconnected -= ServerOnPlayerDisconnected;
        }

        [ServerCallback]
        private void OnTriggerEnter(Collider other) {
            var darklandHero = other.GetComponent<DarklandHero>();

            if (darklandHero == null) return;

            var heroNetId = darklandHero.netId;
            Assert.IsFalse(VisiblePlayerNetIds.Contains(heroNetId));

            VisiblePlayerNetIds.Add(heroNetId);

            if (_targetNetIdHolder.TargetNetIdentity == null) {
                _targetNetIdHolder.Set(heroNetId);
            }
        }

        [ServerCallback]
        private void OnTriggerExit(Collider other) {
            var darklandHero = other.GetComponent<DarklandHero>();

            if (darklandHero == null) return;

            var heroNetId = darklandHero.netId;

            // Assert.IsTrue(VisiblePlayerNetIds.Contains(heroNetId));
            // VisiblePlayerNetIds.Remove(heroNetId);

            ServerCheckPerception(heroNetId);
        }

        [Server]
        private void ServerOnPlayerDisconnected(NetworkIdentity identity) {
            ServerCheckPerception(identity.netId);
        }

        [Server]
        private void ServerCheckPerception(uint heroNetId) {
            if (VisiblePlayerNetIds.Contains(heroNetId)) {
                VisiblePlayerNetIds.Remove(heroNetId);
            }

            var targetNetIdentity = _targetNetIdHolder.TargetNetIdentity;
            if (targetNetIdentity != null && targetNetIdentity.netId == heroNetId) return;

            if (VisiblePlayerNetIds.Count > 0) {
                _targetNetIdHolder.Set(VisiblePlayerNetIds.First());
            }
        }

        [Server]
        public void ServerOnPlayerConnect(uint playerNetId) {

        }

        [Server]
        public void ServerOnPlayerDisconnect(uint playerNetId) {
        }

        public HashSet<uint> VisiblePlayerNetIds { get; } = new();

    }

}