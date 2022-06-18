using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStatsHolder {
        Stat Stat(StatId id);
        StatValue StatValue(StatId id);
        Tuple<Stat, Stat> Stats(StatId firstId, StatId secondId);
        IEnumerable<IStatConstraint> StatConstraints(StatId id);
        HashSet<Stat> stats { get; }
        HashSet<StatId> statIds { get; }
    }

}