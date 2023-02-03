using System.Collections.Generic;
using _Darkland.Sources.Models.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai {

    public abstract class FsmState : ScriptableObject, IFsmState {

        public List<FsmTransition> transitions;

        public virtual void EnterSelf(GameObject parent) { }

        public virtual void ExitSelf(GameObject parent) { }

        public abstract void UpdateSelf(GameObject parent);

        public List<IFsmTransition> Transitions => new(transitions); //todo nieefektywne!!!

    }

}