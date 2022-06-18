using System;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatChangeHook) + "/" + nameof(TrimHealthToMaxHealthStatChangeHook),
        fileName = nameof(TrimHealthToMaxHealthStatChangeHook))
    ]
    public class TrimHealthToMaxHealthStatChangeHook : StatChangeHook {

        public override void Register(IStatsHolder statsHolder) {
            statsHolder.Stat(StatId.Health).Changed += OnChanged(statsHolder);
        }

        public override void Unregister(IStatsHolder statsHolder) {
            statsHolder.Stat(StatId.Health).Changed -= OnChanged(statsHolder);
        }

        private static Action<StatValue> OnChanged(IStatsHolder statsHolder) {
            return healthValue => {
                var (healthStat, maxHealthStat) = statsHolder.Stats(StatId.Health, StatId.MaxHealth);
                var trimmedHealthValue = Math.Min(healthValue.Basic, maxHealthStat.Basic);

                healthStat.Set(StatValue.OfBasic(trimmedHealthValue));
            };
        }
    }

}