using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatPreChangeHook) + "/" + nameof(TrimByOtherStatValueStatPreChangeHook),
        fileName = nameof(TrimByOtherStatValueStatPreChangeHook))
    ]
    public class TrimByOtherStatValueStatPreChangeHook : StatPreChangeHook {

        public StatId trimByStatId;

        public override float Apply(IStatsHolder statsHolder, float val) =>
            Mathf.Min(val, statsHolder.Stat(trimByStatId).Get());
    }

}