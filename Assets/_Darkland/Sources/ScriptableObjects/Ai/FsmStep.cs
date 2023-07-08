using System.Collections.Generic;
using _Darkland.Sources.Models.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai {

    public abstract class FsmStep : ScriptableObject, IFsmStep {

        public List<FsmDecision> decisions;

        public List<IFsmDecision> Decisions => new(decisions);

        public abstract void Perform(GameObject parent);

    }

}