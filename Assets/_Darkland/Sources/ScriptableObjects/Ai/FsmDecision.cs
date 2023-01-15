using _Darkland.Sources.Models.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai {

    public abstract class FsmDecision : ScriptableObject, IFsmDecision {

        public bool reverted;

        public bool Decide(GameObject parent) {
            var isDecisionValid = IsValid(parent);
            return reverted ? !isDecisionValid : isDecisionValid;
        }

        public abstract bool IsValid(GameObject parent);

        public bool Reverted => reverted;

    }

}