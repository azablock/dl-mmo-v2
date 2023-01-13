using System.Collections.Generic;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PostChangeHook {

    public abstract class PersistentStatEffect : ScriptableObject {

        public StatId[] requiredStatIds;

        public float rate;

        public abstract IEnumerator<float> Apply(IStatsHolder statsHolder);
    }

}