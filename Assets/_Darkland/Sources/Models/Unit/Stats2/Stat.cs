using System;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public sealed class Stat {
        public readonly StatId id;
        public readonly Func<StatValue> Get;
        public readonly Action<StatValue> Set;
        public event Action<StatValue> Changed;

        public Stat(StatId id, Func<StatValue> get, Action<StatValue> set) {
            this.id = id;
            Get = get;
            Set = set;
            Set += _ => Changed?.Invoke(this.Get());
        }

        public void Add(StatValue delta) {
            Set(Get() + delta);
        }

        public float Basic => Get().Basic;
        public float Bonus => Get().Bonus;
    }

}