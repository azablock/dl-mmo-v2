using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public abstract class DarklandStatsHolder : NetworkBehaviour, IDarklandStatsHolder {

        public HashSet<DarklandStat> stats { get; private set; }
        public HashSet<DarklandStatId> statIds { get; private set; }

        private void Awake() {
            stats = new HashSet<DarklandStat>();

            foreach (var darklandStat in DarklandStatsBootstrap.Init(this)) {
                stats.Add(darklandStat);
            }

            statIds = stats.Select(it => it.id) as HashSet<DarklandStatId>;
        }

        public DarklandStat Stat(DarklandStatId id) => stats.First(it => it.id == id);

        public FloatStat StatValue(DarklandStatId id) => Stat(id).getFunc();

        public Tuple<DarklandStat, DarklandStat> Stats(DarklandStatId firstId, DarklandStatId secondId) =>
            Tuple.Create(Stat(firstId), Stat(secondId));
    }

}