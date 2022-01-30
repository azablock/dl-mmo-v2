using System;
using _Darkland.Sources.Models.Unit.Stats;
using _Darkland.Sources.Models.Unit.Traits;

namespace _Darkland.Sources.Models.Unit.StatsCounter {

    public interface IByUnitTraitsUnitStatsCounter : IUnitStatsCounter {
        Func<UnitTraitsData> GetUnitTraitsDataFunc { get; }
    }

    public sealed class ByUnitTraitsUnitStatsCounter : IByUnitTraitsUnitStatsCounter {

        public ByUnitTraitsUnitStatsCounter(Func<UnitTraitsData> getUnitTraitsDataFunc) {
            GetUnitTraitsDataFunc = getUnitTraitsDataFunc;
        }

        public Func<UnitTraitsData> GetUnitTraitsDataFunc { get; }

        public UnitStats Get() {
            var unitTraitsData = GetUnitTraitsDataFunc();

            return new UnitStats {
                attackSpeed = unitTraitsData.dexterity.current
            };
        }
    }

}