using System;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.ScriptableObjects.StatConstraint;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public abstract class StatsHolder : NetworkBehaviour, IStatsHolder {
        
        public HashSet<Stat> stats { get; private set; }
        public HashSet<StatId> statIds { get; private set; }

        [SerializeField]
        private List<StatConstraintDefinition> statConstraintDefinitions;
        
        private void Awake() {
            stats = new HashSet<Stat>();

            foreach (var darklandStat in DarklandStatsBootstrap.Init(this)) {
                stats.Add(darklandStat);
            }

            statIds = new HashSet<StatId>(stats.Select(it => it.id));
        }

        [Server]
        public Stat Stat(StatId id) {
            if (!statIds.Contains(id)) {
                throw new ArgumentException($"StatsHolder does not contain stat of id {id}");
            }
            
            return stats.First(it => it.id == id);
        }

        [Server]
        public StatValue StatValue(StatId id) => Stat(id).Get();

        [Server]
        public IEnumerable<IStatConstraint> StatConstraints(StatId id) {
            //todo jakos ladniej to
            var definitions = statConstraintDefinitions ?? new List<StatConstraintDefinition>();
            
            var hasConstraintDefinitionWithId = definitions
                                                .Select(it => it.Id)
                                                .Contains(id);

            return hasConstraintDefinitionWithId
                ? definitions
                  .First(it => it.Id == id)
                  .Constraints 
                : new List<StatConstraint>();
        }
        
        [Server]
        public Tuple<Stat, Stat> Stats(StatId firstId, StatId secondId) =>
            Tuple.Create(Stat(firstId), Stat(secondId));
    }

}