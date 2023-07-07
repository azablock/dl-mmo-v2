using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatPreChangeHook) + "/" + nameof(TrimByOtherStatsCurrentValueStatPreChangeHook),
        fileName = nameof(TrimByOtherStatsCurrentValueStatPreChangeHook))
    ]
    public class TrimByOtherStatsCurrentValueStatPreChangeHook : StatPreChangeHook {

        public StatId trimByStatId;

        public override StatVal Apply(IStatsHolder statsHolder, StatVal val) {
            var result = Mathf.Min(val.Basic, statsHolder.ValueOf(trimByStatId).Current);
            return StatVal.OfBasic(result);
        }

    }

}