using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    public abstract class StatChangeHook : ScriptableObject {
        public StatId[] requiredStatIds;

        public abstract void Register(IStatsHolder statsHolder);
        public abstract void Unregister(IStatsHolder statsHolder);

        public bool CanBeRegistered(IStatsHolder statsHolder) {
            return requiredStatIds.All(id => statsHolder.statIds.Contains(id));
        }
    }

}