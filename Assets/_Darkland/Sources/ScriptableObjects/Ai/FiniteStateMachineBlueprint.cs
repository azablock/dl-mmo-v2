using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai {

    [CreateAssetMenu(fileName = nameof(FiniteStateMachineBlueprint),
                     menuName = "DL/Ai/" + nameof(FiniteStateMachineBlueprint))]
    public class FiniteStateMachineBlueprint : ScriptableObject {

        public FsmState initialState;
        public List<FsmState> states;
        public List<FsmTransition> transitions;

    }

}