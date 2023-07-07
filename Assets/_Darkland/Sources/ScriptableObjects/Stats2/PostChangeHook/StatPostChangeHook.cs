using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PostChangeHook {

    public abstract class StatPostChangeHook : ScriptableObject {

        public StatId[] requiredStatIds;
        public StatId onChangeStatId;

        public abstract void OnStatChange(IStatsHolder statsHolder);

        public bool CanBeRegistered(IStatsHolder statsHolder) {
            return requiredStatIds.All(id => statsHolder.statIds.Contains(id));
        }

    }

}