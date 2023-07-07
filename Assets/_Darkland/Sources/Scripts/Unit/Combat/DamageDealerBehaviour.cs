using System;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Ai;
using _Darkland.Sources.Scripts.Movement;
using _Darkland.Sources.Scripts.Presentation.Gameplay.Combat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Combat {

    public struct UnitAttackEvent {

        public NetworkIdentity target;
        public int damage;
        public DamageType damageType;

    }

    public struct DamageAppliedEvent {

        public NetworkIdentity target;
        public int targetHealthAfterDamage;

    }

    public class DamageDealerBehaviour : NetworkBehaviour, IDamageDealer {

        [SerializeField]
        private GameObject damageMarkerPrefab;

        public int nextAttackDamageBonus { get; private set; }

        public event Action<DamageAppliedEvent> ServerDamageApplied;

        [Server]
        public void DealDamage(UnitAttackEvent evt) {
            //todo tutaj byl bug z fireballem
            if (evt.target == null) return;

            if (!GetComponent<MovementBehaviour>().ServerIsReadyForNextMove()) return;

            var targetPos = evt.target.GetComponent<IDiscretePosition>().Pos;
            var healthStat = evt.target.GetComponent<IStatsHolder>().Stat(StatId.Health);
            var resultDamage = evt.damage + nextAttackDamageBonus;
            var newHealthValue = healthStat.Get().Basic - resultDamage;
            healthStat.Set(StatVal.OfBasic(newHealthValue));

            nextAttackDamageBonus = 0;
            ServerDamageApplied?.Invoke(new DamageAppliedEvent {
                target = evt.target,
                targetHealthAfterDamage = Mathf.FloorToInt(newHealthValue)
            });

            //todo brzydkie to - nie powinno tu tego byc --------------------------------------------------
            var aiCombatMemory = evt.target.GetComponent<AiCombatMemory>();
            if (aiCombatMemory) {
                aiCombatMemory.Add(netIdentity);

                var targetsTargetNetIdHolder = evt.target.GetComponent<ITargetNetIdHolder>();
                if (!targetsTargetNetIdHolder.HasTarget()) targetsTargetNetIdHolder.Set(netId);
            }
            //todo brzydkie to - nie powinno tu tego byc --------------------------------------------------


            //todo move to new script
            ClientRpcShowDamageMarker(resultDamage, evt.damageType, targetPos);
        }

        [Server]
        public void AddNextAttackDamageBonus(int val) {
            nextAttackDamageBonus += val;
        }

        [ClientRpc]
        private void ClientRpcShowDamageMarker(int damage, DamageType damageType, Vector3Int pos) {
            Instantiate(damageMarkerPrefab, pos, Quaternion.identity)
                .GetComponent<DamageMarker>()
                .ClientInit(damage, damageType);
        }

    }

}