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

        private ITargetNetIdHolder _targetNetIdHolder;
        private AiCombatMemory _aiCombatMemory;

        public Dictionary<AiPerceptionZoneType, AiNetworkPerceptionZone> PerceptionZones { get; } = new();

        // [ServerCallback]
        private void Awake() {
            _targetNetIdHolder = GetComponent<ITargetNetIdHolder>();
            _aiCombatMemory = GetComponent<AiCombatMemory>();
            var mobDef = GetComponent<IMobDefHolder>().MobDef;

            Assert.IsTrue(_targetNetIdHolder.MaxTargetDistance >= mobDef.AttackPerceptionRange);
            Assert.IsTrue(_targetNetIdHolder.MaxTargetDistance >= mobDef.PassivePerceptionRange);
            Assert.IsTrue(mobDef.PassivePerceptionRange >= mobDef.AttackPerceptionRange);

            PerceptionZones.Add(Passive, new(mobDef.PassivePerceptionRange));
            PerceptionZones.Add(Attack, new(mobDef.AttackPerceptionRange));

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