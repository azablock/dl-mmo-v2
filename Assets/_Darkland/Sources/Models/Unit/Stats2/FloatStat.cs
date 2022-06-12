using System;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    [Serializable]
    public readonly struct FloatStat {

        public static FloatStat Of(float basic, float bonus) {
            return new FloatStat(basic, bonus);
        }

        private FloatStat(float basic, float bonus) {
            Basic = basic;
            Bonus = bonus;
        }

        public float Basic { get; }
        public float Bonus { get; }
        public float Current => Basic + Bonus;

        public static FloatStat operator+(FloatStat a, FloatStat b) {
            return Of(a.Basic + b.Basic, a.Bonus + b.Bonus);
        }

        public bool Equals(FloatStat other) {
            return Basic.Equals(other.Basic) && Bonus.Equals(other.Bonus);
        }

        public override bool Equals(object obj) {
            return obj is FloatStat other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (Basic.GetHashCode() * 397) ^ Bonus.GetHashCode();
            }
        }

    }

}