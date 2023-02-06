using System;
using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.Models.Unit.Regain {

    [Serializable]
    public struct StatRegainRelation {
        public StatId regainStatId;
        public StatId applyRegainToStatId;
    }

    public struct StatRegainState {
        public StatRegainRelation statRegainRelation;
        public IRegainState regainState;

    }

}