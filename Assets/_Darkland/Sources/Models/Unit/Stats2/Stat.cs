using System;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStat {
        event Action<StatValue> Changed;
        StatId id { get; }
    }
    
    public class Stat : IStat {
        public readonly Func<StatValue> Get;
        public readonly Action<StatValue> Set;
        public event Action<StatValue> Changed;
        public StatId id { get; }

        public Stat(StatId id, Func<StatValue> get, Action<StatValue> set) {
            this.id = id;
            Get = get;
            Set = set;
            Set += _ => Changed?.Invoke(Get());
        }

        public void Add(StatValue delta) {
            Set(Get() + delta);
        }

        public float Basic => Get().basic;
        public float Bonus => Get().bonus;
        public float Current => Get().Current;
    }

}