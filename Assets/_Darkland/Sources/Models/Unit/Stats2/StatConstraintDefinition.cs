using System;
using System.Collections.Generic;
using _Darkland.Sources.ScriptableObjects.StatConstraint;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    //todo probably change to SO
    [Serializable]
    public struct StatConstraintDefinition {
        [SerializeField]
        private StatId id;
        [SerializeField]
        private List<StatConstraint> constraints;

        public readonly List<StatConstraint> Constraints => constraints;
        public readonly StatId Id => id;
    }

}