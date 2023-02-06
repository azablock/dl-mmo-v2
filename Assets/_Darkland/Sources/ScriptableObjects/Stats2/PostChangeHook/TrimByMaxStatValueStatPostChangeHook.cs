using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PostChangeHook {

    [CreateAssetMenu(
            menuName = "DL/" + nameof(StatPostChangeHook) + "/" + nameof(TrimByMaxStatValueStatPostChangeHook),
            fileName = nameof(TrimByMaxStatValueStatPostChangeHook)
        )
    ]
    public class TrimByMaxStatValueStatPostChangeHook : StatPostChangeHook {

        [SerializeField]
        private StatId trimmedStatId;
        
        public override void OnStatChange(IStatsHolder statsHolder) {
            var trimmedStat = statsHolder.Stat(trimmedStatId);
            var trimByStat = statsHolder.Stat(onChangeStatId);
    
            var trimmedStatCurrentValue = trimmedStat.Get();
            var trimByStatCurrentValue = trimByStat.Get();

            if (trimmedStatCurrentValue.Current > trimByStatCurrentValue.Current)
                trimmedStat.Set(StatVal.OfBasic(trimByStatCurrentValue.Current));
        }
    }

}