using System;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    public interface IStat {
        event Action<StatVal> Changed;
        StatId id { get; }
    }

    [Serializable]
    public struct StatVal {

        [SerializeField]
        private float basic;
        [SerializeField]
        private float bonus;

        public static StatVal Of(float basic, float bonus) => new() { basic = basic, bonus = bonus };
        public static StatVal OfBasic(float val) => new() { basic = val };
        public static StatVal OfBonus(float val) => new() { bonus = val };
        
        public static StatVal operator +(StatVal a, StatVal b) =>
            new() {basic = a.basic + b.basic, bonus = a.bonus + b.bonus};

        public float Basic => basic;
        public float Bonus => bonus;
        public float Current => basic + bonus;
        public static StatVal Zero => Of(0, 0);

    }
    
    public class Stat : IStat {
        public readonly Func<StatVal> Get;
        public readonly Action<StatVal> Set;
        public event Action<StatVal> Changed;
        public StatId id { get; }

        public Stat(StatId id, Func<StatVal> get, Action<StatVal> set) {
            this.id = id;
            Get = get;
            Set = set;
            //todo tutaj mozna dodac do Set callback - sprawdza czy np. Health nie ma zmienionej wartosci "Bonus"
            Set += _ => Changed?.Invoke(Get());
        }

        public void Add(StatVal delta) {
            Set(Get() + delta);
        }
    }

}