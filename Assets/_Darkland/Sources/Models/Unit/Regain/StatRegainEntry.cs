using System;
using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.Models.Unit.Regain {

    [Serializable]
    public struct StatRegainEntry {
        public StatId regainStatId;
        public StatId applyRegainToStatId;
        public IRegainState regainState;
    }

}