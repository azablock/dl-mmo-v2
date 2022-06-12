using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2 {

    public abstract class PersistentDarklandStatsEffect : ScriptableObject {

        public DarklandStatId[] requiredStatIds;

        public float rate;

        public bool CanBeApplied(IDarklandStatsHolder statsHolder) {
            return statsHolder.statIds.All(id => requiredStatIds.Contains(id));
        }

        public abstract IEnumerator<float> Apply(IDarklandStatsHolder statsHolder);
    }

}