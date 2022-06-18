using System;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit.Stats2 {

    [Serializable]
    public struct StatValue {

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

        public StatValue WithBasic(float newBasic) {
            return Of(newBasic, bonus);
        }

        public StatValue WithBonus(float newBonus) {
            return Of(basic, newBonus);
        }

        public float Basic => basic;
        public float Bonus => bonus;
        public float Current => Basic + Bonus;
        public static StatValue Zero => Of(0.0f, 0.0f);

        [SerializeField]
        private float basic;

        [SerializeField]
        private float bonus;
        
        public static StatValue operator+(StatValue a, StatValue b) {
            return Of(a.Basic + b.Basic, a.Bonus + b.Bonus);
        }
        
        public static StatValue operator-(StatValue a, StatValue b) {
            return Of(a.Basic - b.Basic, a.Bonus - b.Bonus);
        }

        public bool Equals(StatValue other) {
            return Basic.Equals(other.Basic) && Bonus.Equals(other.Bonus);
        }

        public override bool Equals(object obj) {
            return obj is StatValue other && Equals(other);
        }

        public override int GetHashCode() {
            unchecked {
                return (Basic.GetHashCode() * 397) ^ Bonus.GetHashCode();
            }
        }

    }

}