namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStatPreChangeHook {
        float Apply(IStatsHolder statsHolder, float val);
    }

}