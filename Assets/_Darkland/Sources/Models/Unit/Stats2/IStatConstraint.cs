namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStatConstraint {
        StatValue Apply(IStatsHolder statsHolder, StatValue val);
    }

}