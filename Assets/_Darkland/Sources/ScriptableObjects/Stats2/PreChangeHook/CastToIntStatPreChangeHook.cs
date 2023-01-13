using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatPreChangeHook) + "/" + nameof(CastToIntStatPreChangeHook),
        fileName = nameof(CastToIntStatPreChangeHook))
    ]
    public class CastToIntStatPreChangeHook : StatPreChangeHook {

        public override float Apply(IStatsHolder statsHolder, float val) => (int) val;
    }

}