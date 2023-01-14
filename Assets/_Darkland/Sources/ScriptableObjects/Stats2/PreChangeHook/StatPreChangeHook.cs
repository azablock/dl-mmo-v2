using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Stats2.PreChangeHook {

    public abstract class StatPreChangeHook : ScriptableObject, IStatPreChangeHook {
        public abstract float Apply(IStatsHolder statsHolder, float val);
    }

}