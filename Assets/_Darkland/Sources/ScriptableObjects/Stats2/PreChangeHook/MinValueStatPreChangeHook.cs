using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;
using UnityEngine.Serialization;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook {

    [CreateAssetMenu(menuName = "DL/" + nameof(StatPreChangeHook) + "/" + nameof(MinValueStatPreChangeHook),
                     fileName = nameof(MinValueStatPreChangeHook))
    ]
    public class MinValueStatPreChangeHook : StatPreChangeHook {

        [FormerlySerializedAs("min")]
        public float basicMin;
        public float bonusMin;
        public bool basicApply;
        public bool bonusApply;

        public override StatVal Apply(IStatsHolder statsHolder, StatVal val) {
            var newBasic = basicApply ? Mathf.Max(basicMin, val.Basic) : val.Basic;
            var newBonus = bonusApply ? Mathf.Max(bonusMin, val.Bonus) : val.Bonus;

            return StatVal.Of(newBasic, newBonus);
        }

    }

}