using _Darkland.Sources.Models.Stats2;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PostChangeHook {

    [CreateAssetMenu(
        menuName = "DL/" + nameof(StatPostChangeHook) + "/" + nameof(TrimByMaxStatValueStatPostChangeHook2),
        fileName = nameof(TrimByMaxStatValueStatPostChangeHook2))
    ]
    public class TrimByMaxStatValueStatPostChangeHook2 : StatPostChangeHook {

        [SerializeField]
        private StatId trimmedStatId;

        private readonly ITrimToMaxByOtherStatHandler _handler = new TrimToMaxByOtherStatHandler();

        public override void OnStatChange(IStatsHolder statsHolder) {
            _handler.Handle(statsHolder, trimmedStatId, onChangeStatId);
        }

    }

}