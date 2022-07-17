using System;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    // [Serializable]
    public readonly struct StatValue : IEquatable<StatValue> {

        private StatValue(float basic, float bonus) {
            this.basic = basic;
            this.bonus = bonus;
        }

        public static StatValue Of(float basic, float bonus) {
            return new StatValue(basic, bonus);
        }

        public static StatValue OfBasic(float basic) {
            return new StatValue(basic, 0);
        }

        public static StatValue OfBonus(float bonus) {
            return new StatValue(0, bonus);
        }

        public StatValue WithBasic(float val) {
            return Of(val, bonus);
        }

        public StatValue WithBonus(float val) {
            return Of(basic, val);
        }

        public float Current => basic + bonus;
        public static StatValue Zero => Of(0.0f, 0.0f);

        public readonly float basic;
        public  readonly float bonus;
        
        public static StatValue operator+(StatValue a, StatValue b) {
            return Of(a.basic + b.basic, a.bonus + b.bonus);
        }
        
        public static StatValue operator-(StatValue a, StatValue b) {
            return Of(a.basic - b.basic, a.bonus - b.bonus);
        }

        public bool Equals(StatValue other) {
            return basic.Equals(other.basic) && bonus.Equals(other.bonus);
        }

        public override bool Equals(object obj) {
            return obj is StatValue other && Equals(other);
        }

        public override int GetHashCode() {
            return HashCode.Combine(basic, bonus);
        }
    }

}