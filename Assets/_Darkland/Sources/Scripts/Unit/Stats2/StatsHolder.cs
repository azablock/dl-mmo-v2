using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public abstract class StatsHolder : NetworkBehaviour, IStatsHolder {

        public HashSet<Stat> stats { get; private set; }
        public HashSet<StatId> statIds { get; private set; }
        public virtual IStatPreChangeHooksHolder statPreChangeHooksHolder { get; private set; }

        //todo nowy interface z tego
        public event Action<StatId, float> ClientChanged;

        private void Awake() {
            stats = new HashSet<Stat>();
            statPreChangeHooksHolder = GetComponent<IStatPreChangeHooksHolder>();

            foreach (var darklandStat in DarklandStatsBootstrap.Init(this)) {
                stats.Add(darklandStat);
            }

            statIds = new HashSet<StatId>(stats.Select(it => it.id));
        }

        [Server]
        public IStatsHolder Set(StatId id, float val) {
            Stat(id).Set(val);
            return this;
        }

        public IStatsHolder Add(StatId id, float val) {
            Stat(id).Add(val);
            return this;
        }

        public IStatsHolder Subtract(StatId id, float val) {
            Stat(id).Add(-val);
            return this;
        }

        [Server]
        public Stat Stat(StatId id) {
            if (!statIds.Contains(id)) {
                throw new ArgumentException($"StatsHolder does not contain stat of id {id}");
            }

            return stats.First(it => it.id == id);
        }

        [Server]
        public float ValueOf(StatId id) => Stat(id).Get();

        [Server]
        public Tuple<float, float> Values(StatId firstId, StatId secondId) =>
            Tuple.Create(ValueOf(firstId), ValueOf(secondId));

        [Server]
        public Tuple<float, float, float> Values(Tuple<StatId, StatId, StatId> ids) =>
            Tuple.Create(ValueOf(ids.Item1), ValueOf(ids.Item2), ValueOf(ids.Item3));
        
        [Server]
        public Tuple<float, float, float, float> Values(Tuple<StatId, StatId, StatId, StatId> ids) =>
            Tuple.Create(ValueOf(ids.Item1), ValueOf(ids.Item2), ValueOf(ids.Item3), ValueOf(ids.Item4));

        [Server]
        public Tuple<float, float, float, float, float> Values(Tuple<StatId, StatId, StatId, StatId, StatId> ids) =>
            Tuple.Create(ValueOf(ids.Item1), ValueOf(ids.Item2), ValueOf(ids.Item3), ValueOf(ids.Item4), ValueOf(ids.Item5));

        [Server]
        public Tuple<Stat, Stat> Stats(StatId firstId, StatId secondId) =>
            Tuple.Create(Stat(firstId), Stat(secondId));

        [Client]
        protected void InvokeClientChanged(StatId statId, float statValue) {
            ClientChanged?.Invoke(statId, statValue);
        }
    }

}