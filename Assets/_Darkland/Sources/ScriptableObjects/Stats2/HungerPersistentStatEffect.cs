using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.Stats2.PostChangeHook;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    [CreateAssetMenu(menuName = "DL/" + nameof(PersistentStatEffect) + "/" + nameof(HungerPersistentStatEffect),
                     fileName = nameof(HungerPersistentStatEffect))
    ]
    public class HungerPersistentStatEffect : PersistentStatEffect {

        public override IEnumerator<float> Apply(IStatsHolder statsHolder) {
            var hungerStat = statsHolder.Stat(StatId.Hunger);

            hungerStat.Add(StatVal.OfBasic(-1)); //todo change "-1" to hunger rate

            yield return rate;
        }

    }

}