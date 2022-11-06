using System;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStat {
        event Action<float> Changed;
        StatId id { get; }
    }
    
    public class Stat : IStat {
        public readonly Func<float> Get;
        public readonly Action<float> Set;
        public event Action<float> Changed;
        public StatId id { get; }

        public Stat(StatId id, Func<float> get, Action<float> set) {
            this.id = id;
            Get = get;
            Set = set;
            Set += _ => Changed?.Invoke(Get());
        }

        public void Add(float delta) {
            Set(Get() + delta);
        }
    }

}