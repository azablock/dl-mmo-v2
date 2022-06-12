using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IDarklandStatsHolder {
        DarklandStat Stat(DarklandStatId id);
        FloatStat StatValue(DarklandStatId id);
        HashSet<DarklandStat> stats { get; }
        HashSet<DarklandStatId> statIds { get; }
        Tuple<DarklandStat, DarklandStat> Stats(DarklandStatId firstId, DarklandStatId secondId);
    }

}