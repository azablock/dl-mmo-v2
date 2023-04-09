using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatPreChangeHook) + "/" + nameof(CastToIntStatPreChangeHook),
        fileName = nameof(CastToIntStatPreChangeHook))
    ]
    public class CastToIntStatPreChangeHook : StatPreChangeHook {

        public override StatVal Apply(IStatsHolder statsHolder, StatVal val) =>
            StatVal.Of((int)val.Basic, (int)val.Bonus);

    }

}