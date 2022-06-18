using System;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.StatConstraint {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatConstraint) + "/" + nameof(TrimByOtherStatValueStatConstraint),
        fileName = nameof(TrimByOtherStatValueStatConstraint))
    ]
    public class TrimByOtherStatValueStatConstraint : StatConstraint {

        public StatId trimByStatId;

        public override StatValue Apply(IStatsHolder statsHolder, StatValue val) {
            var trimmedByStat = statsHolder.Stat(trimByStatId);
            var trimmedValue = Math.Min(val.Basic, trimmedByStat.Basic);

            return StatValue.OfBasic(trimmedValue);
        }
    }

}