namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStatPreChangeHook {
        StatVal Apply(IStatsHolder statsHolder, StatVal val);
    }

}