using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.StatConstraint {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatConstraint) + "/" + nameof(CastToIntStatConstraint),
        fileName = nameof(CastToIntStatConstraint))
    ]
    public class CastToIntStatConstraint : StatConstraint {

        public bool castBasic;
        public bool castBonus;
        
        public override StatValue Apply(IStatsHolder statsHolder, StatValue val) {
            var newBasic = castBasic ? (int) val.Basic : val.Basic;
            var newBonus = castBonus ? (int) val.Bonus : val.Bonus;

            return StatValue.Of(newBasic, newBonus);
        }
    }

}