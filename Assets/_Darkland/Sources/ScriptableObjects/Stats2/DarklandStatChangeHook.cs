using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    public abstract class DarklandStatChangeHook : ScriptableObject {
        public DarklandStatId[] requiredStatIds;
        
        public abstract void Register(IDarklandStatsHolder statsHolder);
        public abstract void Unregister(IDarklandStatsHolder statsHolder);

        public bool CanBeRegistered(IDarklandStatsHolder statsHolder) {
            return statsHolder.statIds.All(id => requiredStatIds.Contains(id));
        }
    }

}