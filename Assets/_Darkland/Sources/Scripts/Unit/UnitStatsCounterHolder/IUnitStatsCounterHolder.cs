using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.StatsCounter;

namespace _Darkland.Sources.Scripts.Unit.UnitStatsCounterHolder {

    public interface IUnitStatsCounterHolder {
        IUnitStatsCounter UnitStatsCounter { get; }
    }

}