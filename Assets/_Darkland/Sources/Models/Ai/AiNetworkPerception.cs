using System.Collections.Generic;

namespace _Darkland.Sources.Models.Ai {

    public interface IAiNetworkPerception {
        HashSet<uint> VisiblePlayerNetIds { get; }
        float AttackPerceptionRange { get; }
        float ChasePerceptionRange { get; }
    }

}