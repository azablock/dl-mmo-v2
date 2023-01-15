using System.Collections.Generic;
using _Darkland.Sources.Models.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai {

    [CreateAssetMenu(fileName = nameof(FsmTransition), menuName = "DL/Ai/" + nameof(FsmTransition))]
    public class FsmTransition : ScriptableObject, IFsmTransition {

        [SerializeField]
        private List<FsmDecision> decisions;
        [SerializeField]
        private FsmState targetState;

        public List<IFsmDecision> Decisions => new(decisions);
        public IFsmState TargetState => targetState;

    }

}