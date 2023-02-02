using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Scripts.Interaction;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;
using static _Darkland.Sources.Models.Ai.AiPerceptionZoneType;

namespace _Darkland.Sources.Scripts.Ai {

    public class AiNetworkPerceptionBehaviour2 : MonoBehaviour, IAiNetworkPerception {

        [Range(1, TargetNetIdHolderBehaviour.MaxTargetDis)]
        [SerializeField]
        private float passivePerceptionRange;
        [Range(1, TargetNetIdHolderBehaviour.MaxTargetDis)]
        [SerializeField]
        private float attackPerceptionRange;

        private ITargetNetIdHolder _targetNetIdHolder;

        public Dictionary<AiPerceptionZoneType, AiNetworkPerceptionZone> PerceptionZones { get; } = new();

        [ServerCallback]
        private void Start() {
            _targetNetIdHolder = GetComponent<ITargetNetIdHolder>();

            Assert.IsTrue(_targetNetIdHolder.MaxTargetDistance > attackPerceptionRange);
            Assert.IsTrue(_targetNetIdHolder.MaxTargetDistance > passivePerceptionRange);
            Assert.IsTrue(passivePerceptionRange >= attackPerceptionRange);

            PerceptionZones.Add(Passive, new(passivePerceptionRange));
            PerceptionZones.Add(Attack, new(attackPerceptionRange));

            PerceptionZones[Passive].TargetEnteredZone += OnTargetEnteredZone;
            PerceptionZones[Passive].TargetExitedZone += OnTargetExitedZone;

            PerceptionZones[Attack].TargetEnteredZone += OnTargetEnteredZone;
            PerceptionZones[Attack].TargetExitedZone += OnTargetExitedZone;
        }

        [ServerCallback]
        private void OnDestroy() {
            PerceptionZones[Passive].TargetEnteredZone -= OnTargetEnteredZone;
            PerceptionZones[Passive].TargetExitedZone -= OnTargetExitedZone;

            PerceptionZones[Attack].TargetEnteredZone -= OnTargetEnteredZone;
            PerceptionZones[Attack].TargetExitedZone -= OnTargetExitedZone;
        }

        [ServerCallback]
        private void OnTargetEnteredZone(NetworkIdentity target) {
            if (_targetNetIdHolder.HasTarget()) return;

            _targetNetIdHolder.Set(target.netId);
        }

        [ServerCallback]
        private void OnTargetExitedZone(NetworkIdentity target) {
            var netIdEqualToTargetNetId = _targetNetIdHolder.HasTarget()
                                          && _targetNetIdHolder.TargetNetIdentity.netId == target.netId;

            if (!netIdEqualToTargetNetId) return;

            if (PerceptionZones[Attack].targets.Count > 0) {
                _targetNetIdHolder.Set(PerceptionZones[Attack].targets.First().netId);
            }
            else if (PerceptionZones[Passive].targets.Count > 0) {
                _targetNetIdHolder.Set(PerceptionZones[Passive].targets.First().netId);
            }
        }

        public float PerceptionZoneRange(AiPerceptionZoneType zoneType) => PerceptionZones[zoneType].range;

    }

}