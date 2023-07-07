using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook {

    [CreateAssetMenu(menuName = "DL/" + nameof(StatPreChangeHook) + "/" + nameof(TrimByOtherStatValueStatPreChangeHook),
                     fileName = nameof(TrimByOtherStatValueStatPreChangeHook))
    ]
    public class TrimByOtherStatValueStatPreChangeHook : StatPreChangeHook {

        public StatId trimByStatId;

        public bool applyBasic;
        public bool applyBonus;

        public override StatVal Apply(IStatsHolder statsHolder, StatVal val) {
            var trimByStatVal = statsHolder.Stat(trimByStatId).Get();
            var newBasic = applyBasic ? Mathf.Min(val.Basic, trimByStatVal.Basic) : val.Basic;
            var newBonus = applyBonus ? Mathf.Min(val.Bonus, trimByStatVal.Bonus) : val.Bonus;

            return StatVal.Of(newBasic, newBonus);
        }

    }

}