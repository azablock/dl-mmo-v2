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
        void EnterSelf(GameObject parent);
        void ExitSelf(GameObject parent);
        void UpdateSelf(GameObject parent);
        List<IFsmTransition> Transitions { get; }
        List<IFsmStep> Steps { get; }
    }

    public interface IFsmStep {

        List<IFsmDecision> Decisions { get; }
        void Perform(GameObject parent);

    }

}