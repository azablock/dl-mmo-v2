using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit.StatsCounter;
using JetBrains.Annotations;

namespace _Darkland.Sources.Models.Unit.Stats {

    public interface IUnitStatsProvider {
        UnitStats Get();

        IEnumerable<IUnitStatsCounter> UnitStatsCounters { get; }
    }

    public class UnitStatsProvider : IUnitStatsProvider {

        public IEnumerable<IUnitStatsCounter> UnitStatsCounters { get; }

        public UnitStatsProvider([NotNull] IEnumerable<IUnitStatsCounter> unitStatsCounters) {
            UnitStatsCounters = unitStatsCounters;
        }

        public UnitStats Get() {
            return UnitStatsCounters
                   .Select(counter => counter.Get())
                   .Aggregate(new UnitStats(), (current, next) => current + next);
        }
    }

}