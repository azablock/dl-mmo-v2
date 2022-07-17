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
            var newBasic = castBasic ? (int) val.basic : val.basic;
            var newBonus = castBonus ? (int) val.bonus : val.bonus;

            return StatValue.Of(newBasic, newBonus);
        }
    }

}