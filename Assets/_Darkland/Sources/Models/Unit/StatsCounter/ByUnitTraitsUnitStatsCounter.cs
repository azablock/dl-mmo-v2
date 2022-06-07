using System;
using _Darkland.Sources.Models.Unit.Stats;
using _Darkland.Sources.Models.Unit.Traits;

namespace _Darkland.Sources.Models.Unit.StatsCounter {

    public sealed class ByUnitTraitsUnitStatsCounter : IUnitStatsCounter {

        public ByUnitTraitsUnitStatsCounter(Func<UnitTraitsData> getUnitTraitsDataFunc) {
            _getUnitTraitsDataFunc = getUnitTraitsDataFunc;
        }

        private readonly Func<UnitTraitsData> _getUnitTraitsDataFunc;

        public UnitStats Get() {
            var unitTraitsData = _getUnitTraitsDataFunc();

            return new UnitStats {
                attackSpeed = unitTraitsData.dexterity.current
            };
        }
    }

}