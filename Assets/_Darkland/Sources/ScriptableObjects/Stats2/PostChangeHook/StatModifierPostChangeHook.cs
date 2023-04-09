using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Hero;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PostChangeHook {

    [CreateAssetMenu(menuName = "DL/" + nameof(StatPostChangeHook) + "/" + nameof(StatModifierPostChangeHook),
                     fileName = nameof(StatModifierPostChangeHook))
    ]
    public class StatModifierPostChangeHook : StatPostChangeHook {

        public override void OnStatChange(IStatsHolder statsHolder) {
            var statModifiersDict = HeroStatsCalculator.statsFormulas[onChangeStatId];

            Assert.IsTrue(HeroStatsCalculator.statsFormulas.ContainsKey(onChangeStatId));
            // Assert.IsTrue(statModifiersDict.Keys.All(key => requiredStatIds.Contains(key)));

            statModifiersDict
                .ToList()
                .ForEach(it => {
                    var targetStatId = it.Key;
                    statsHolder.Stat(targetStatId).Set(HeroStatsCalculator.ValueOf(targetStatId, statsHolder));
                });
        }

    }

}