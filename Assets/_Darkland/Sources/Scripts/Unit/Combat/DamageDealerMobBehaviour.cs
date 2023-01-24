using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Combat {

    public class DamageDealerMobBehaviour : MonoBehaviour, IDamageDealer {

        [Server]
        public void DealDamage(UnitAttackEvent evt) {
            var healthStat = evt.target.GetComponent<IStatsHolder>().Stat(StatId.Health);
            var newHealthValue = healthStat.Get() - evt.damage;
            healthStat.Set(newHealthValue);
        }

    }

}