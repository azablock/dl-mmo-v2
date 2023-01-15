using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.Ai {

    public interface IFsmDecision {
        bool Decide(GameObject parent);
        bool IsValid(GameObject parent);
        bool Reverted { get; }
    }

    public interface IFsmTransition {
        List<IFsmDecision> Decisions { get; }
        IFsmState TargetState { get; }
    }
    
    public interface IFsmState {
        void UpdateSelf(GameObject parent);
        List<IFsmTransition> Transitions { get; }
    }

}