using System;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Scripts.Presentation.Gameplay.Combat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Combat {

    public struct UnitAttackEvent {

        public NetworkIdentity target;
        public int damage;
        public DamageType damageType;

    }

    public class DamageDealerBehaviour : NetworkBehaviour, IDamageDealer {

        [SerializeField]
        private GameObject damageMarkerPrefab;

        public event Action<UnitAttackEvent> ServerDamageApplied;

        [Server]
        public void DealDamage(UnitAttackEvent evt) {
            var targetPos = evt.target.GetComponent<IDiscretePosition>().Pos;
            var healthStat = evt.target.GetComponent<IStatsHolder>().Stat(StatId.Health);
            var newHealthValue = healthStat.Get() - evt.damage;
            healthStat.Set(newHealthValue);
            ServerDamageApplied?.Invoke(evt);
            
            //todo move to new script
            ClientRpcShowDamageMarker(evt.damage, targetPos);
        }

        [ClientRpc]
        private void ClientRpcShowDamageMarker(int damage, Vector3Int pos) {
            Instantiate(damageMarkerPrefab, pos, Quaternion.identity)
                .GetComponent<DamageMarker>()
                .ClientInit(damage);
        }

    }

}