using System.Collections.Generic;

namespace _Darkland.Sources.Models.Ai {

    public interface IAiNetworkPerception {
        void OnServerPlayerConnect(uint playerNetId);
        void OnServerPlayerDisconnect(uint playerNetId);
        HashSet<uint> visiblePlayerNetIds { get; }
    }

}