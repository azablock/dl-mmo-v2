using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    public abstract class DirectDarklandStatsEffect {
        public FloatStat delta { get; }
        public DarklandStatId statId { get; }

        public abstract void Apply(IDarklandStatsHolder statsHolder);
        
        public bool CanBeApplied(IDarklandStatsHolder statsHolder) {
            return statsHolder.statIds.Contains(statId);
        }
    }
    
    // public abstract class DirectDarklandStatsEffect : ScriptableObject {
    //
    //
    //     public void Apply(IDarklandStatsHolder statsHolder) {
    //         var darklandStatApi = statsHolder.StatApi(statId);
    //         var statValue = statsHolder.StatValue(statId);
    //
    //         darklandStatApi.setAction(statValue + delta);
    //     }
    //
    //     public bool CanBeApplied(IDarklandStatsHolder statsHolder) {
    //         return statsHolder.statIds.Contains(statId);
    //     }
    // }

}