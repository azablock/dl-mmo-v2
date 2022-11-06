using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStatsHolder {
        Stat Stat(StatId id);
        float ValueOf(StatId id);
        Tuple<float, float> Values(StatId firstId, StatId secondId);
        Tuple<Stat, Stat> Stats(StatId firstId, StatId secondId);
        HashSet<Stat> stats { get; }
        HashSet<StatId> statIds { get; }
        IStatPreChangeHooksHolder statPreChangeHooksHolder { get; }
        event Action<StatId, float> ClientChanged;
    }

}