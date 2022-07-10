using System;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatChangeHook) + "/" + nameof(CheckHealthOnMaxHealthStatChangeHook),
        fileName = nameof(CheckHealthOnMaxHealthStatChangeHook))
    ]
    public class CheckHealthOnMaxHealthStatChangeHook : StatChangeHook {

        public override void Register(IStatsHolder statsHolder) {
            var healthStat = statsHolder.Stat(StatId.Health);
            var maxHealthStat = statsHolder.Stat(StatId.MaxHealth);
            
            maxHealthStat.Changed += maxHealthStatOnChanged(healthStat, maxHealthStat);
        }

        public override void Unregister(IStatsHolder statsHolder) {
            var healthStat = statsHolder.Stat(StatId.Health);
            var maxHealthStat = statsHolder.Stat(StatId.MaxHealth);

            maxHealthStat.Changed -= maxHealthStatOnChanged(healthStat, maxHealthStat);
        }

        private static Action<StatValue> maxHealthStatOnChanged(Stat healthStat, Stat maxHealthStat) {
            return _ => {
                var healthValue = Math.Min(healthStat.Basic, maxHealthStat.Basic);
                healthStat.Set(StatValue.OfBasic(healthValue));
            };
        }

    }

}