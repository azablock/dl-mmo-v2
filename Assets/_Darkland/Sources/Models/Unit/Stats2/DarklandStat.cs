using System;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public sealed class DarklandStat {
        public readonly DarklandStatId id;
        public readonly Func<FloatStat> getFunc;
        public readonly Action<FloatStat> setAction;
        public event Action<FloatStat> Changed;

        public DarklandStat(DarklandStatId id, Func<FloatStat> getFunc, Action<FloatStat> setAction) {
            this.id = id;
            this.getFunc = getFunc;
            this.setAction = setAction;
            this.setAction += _ => Changed?.Invoke(this.getFunc());
        }
    }

}