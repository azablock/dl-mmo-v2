using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.StatConstraint {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatPreChangeHook) + "/" + nameof(MinValueStatPreChangeHook),
        fileName = nameof(MinValueStatPreChangeHook))
    ]
    public class MinValueStatPreChangeHook : StatPreChangeHook {

        public float min;

        public override float Apply(IStatsHolder statsHolder, float val) => Mathf.Min(min, val);
    }

}