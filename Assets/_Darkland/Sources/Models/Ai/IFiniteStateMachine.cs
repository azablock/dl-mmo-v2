using System.Collections.Generic;

namespace _Darkland.Sources.Models.Ai {

    public interface IFsmDecision {
        bool Decide();
    }

    public interface IFsmTransition {
        IFsmDecision decision { get; }
        int targetStateId { get; }
    }
    
    public interface IFsmState {
        void UpdateSelf();
        int id { get; }
        List<IFsmTransition> transitions { get; }
    }
    
    public interface IFiniteStateMachine {
        void UpdateCurrentState();
        IFsmState currentState { get; }
        List<IFsmState> states { get; }
    }

}