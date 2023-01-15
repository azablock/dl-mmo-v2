using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatPreChangeHook) + "/" + nameof(MinValueStatPreChangeHook),
        fileName = nameof(MinValueStatPreChangeHook))
    ]
    public class MinValueStatPreChangeHook : StatPreChangeHook {

        public float min;

        public override float Apply(IStatsHolder statsHolder, float val) => Mathf.Max(min, val);
    }

}