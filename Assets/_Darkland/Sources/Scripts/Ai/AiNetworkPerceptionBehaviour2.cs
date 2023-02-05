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
        private AiCombatMemory _aiCombatMemory;

        public Dictionary<AiPerceptionZoneType, AiNetworkPerceptionZone> PerceptionZones { get; } = new();

        // [ServerCallback]
        private void Awake() {
            _targetNetIdHolder = GetComponent<ITargetNetIdHolder>();
            _aiCombatMemory = GetComponent<AiCombatMemory>();

            Assert.IsTrue(_targetNetIdHolder.MaxTargetDistance >= attackPerceptionRange);
            Assert.IsTrue(_targetNetIdHolder.MaxTargetDistance >= passivePerceptionRange);
            Assert.IsTrue(passivePerceptionRange >= attackPerceptionRange);

            PerceptionZones.Add(Passive, new(passivePerceptionRange));
            PerceptionZones.Add(Attack, new(attackPerceptionRange));

            // PerceptionZones[Passive].TargetEnteredZone += OnTargetEnteredZone;
            PerceptionZones[Passive].TargetExitedZone += OnTargetExitedPassiveZone;

            PerceptionZones[Attack].TargetEnteredZone += OnTargetEnteredAttackZone;
            // PerceptionZones[Attack].TargetExitedZone += OnTargetExitedZone;
        }

        // [ServerCallback]
        private void OnDestroy() {
            // PerceptionZones[Passive].TargetEnteredZone -= OnTargetEnteredZone;
            PerceptionZones[Passive].TargetExitedZone -= OnTargetExitedPassiveZone;

            PerceptionZones[Attack].TargetEnteredZone -= OnTargetEnteredAttackZone;
            // PerceptionZones[Attack].TargetExitedZone -= OnTargetExitedZone;
        }

        [ServerCallback]
        private void OnTargetEnteredAttackZone(NetworkIdentity target) {
            if (_targetNetIdHolder.HasTarget()) return;

            _targetNetIdHolder.Set(target.netId);
        }

        [ServerCallback]
        private void OnTargetExitedPassiveZone(NetworkIdentity target) {
            var netIdEqualToTargetNetId = _targetNetIdHolder.HasTarget()
                                          && _targetNetIdHolder.TargetNetIdentity.netId == target.netId;

            if (!netIdEqualToTargetNetId) return;

            var attackZoneTargets = PerceptionZones[Attack].targets;
            
            if (attackZoneTargets.Count > 0) {
                _targetNetIdHolder.Set(attackZoneTargets.First().netId);
            }

            var passiveZoneTargets = PerceptionZones[Passive].targets;
            var isPassiveZoneTargetInCombatMemory =
                passiveZoneTargets.FirstOrDefault(it => _aiCombatMemory.HasInHistory(it));
            
            if (isPassiveZoneTargetInCombatMemory) {
                _targetNetIdHolder.Set(passiveZoneTargets.First().netId);
            }
            else {
                _targetNetIdHolder.Clear();
            }
        }

        public float PerceptionZoneRange(AiPerceptionZoneType zoneType) => PerceptionZones[zoneType].range;

    }

}