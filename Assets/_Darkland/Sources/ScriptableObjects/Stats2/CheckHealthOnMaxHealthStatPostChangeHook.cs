using System;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    [CreateAssetMenu(
            menuName = "DL/" + nameof(StatPostChangeHook) + "/" + nameof(CheckHealthOnMaxHealthStatPostChangeHook),
            fileName = nameof(CheckHealthOnMaxHealthStatPostChangeHook)
        )
    ]
    public class CheckHealthOnMaxHealthStatPostChangeHook : StatPostChangeHook {

        public override void OnStatChange(IStatsHolder statsHolder) {
            var (healthStat, maxHealthStat) = statsHolder.Stats(StatId.Health, StatId.MaxHealth);
            var maxHealthValue = maxHealthStat.Get();
            var healthValue = healthStat.Get();

            if (healthValue > maxHealthValue)
                healthStat.Set(maxHealthValue);
        }
    }

}