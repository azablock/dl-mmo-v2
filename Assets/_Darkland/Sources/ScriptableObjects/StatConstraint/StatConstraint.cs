using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.StatConstraint {

    public abstract class StatConstraint : ScriptableObject, IStatConstraint {
        public abstract StatValue Apply(IStatsHolder statsHolder, StatValue val);
    }

}