namespace _Darkland.Sources.Models.Unit.Stats2.StatEffect {

    public interface IDirectStatEffect {
        public float delta { get; }
        public StatId statId { get; }
    }

    public class DirectStatEffect : IDirectStatEffect {

        public float delta { get; }
        public StatId statId { get; }

        public DirectStatEffect(float delta, StatId statId) {
            this.delta = delta;
            this.statId = statId;
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