namespace _Darkland.Sources.Models.Unit.Stats2.StatEffect {

    public interface IDirectStatEffect {
        public StatValue delta { get; }
        public StatId statId { get; }
    }

}