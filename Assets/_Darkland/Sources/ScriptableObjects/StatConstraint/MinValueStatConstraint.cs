using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.StatConstraint {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatConstraint) + "/" + nameof(MinValueStatConstraint),
        fileName = nameof(MinValueStatConstraint))
    ]
    public class MinValueStatConstraint : StatConstraint {

        public float minBasicValue;
        public float minBonusValue;
        public bool checkBasic;
        public bool checkBonus;


        public override StatValue Apply(IStatsHolder statsHolder, StatValue val) {
            var newBasic = checkBasic ? Mathf.Max(minBasicValue, val.Basic) : val.Basic;
            var newBonus = checkBonus ? Mathf.Max(minBonusValue, val.Bonus) : val.Bonus;

            return StatValue.Of(newBasic, newBonus);
        }
    }

}