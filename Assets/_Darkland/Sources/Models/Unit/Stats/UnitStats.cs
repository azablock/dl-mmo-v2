using System;

namespace _Darkland.Sources.Models.Unit.Stats {

    [Serializable]
    public struct UnitStats {
        public float attackSpeed;

        public static UnitStats operator+(UnitStats a, UnitStats b) {
            return new UnitStats {
                attackSpeed = a.attackSpeed + b.attackSpeed
            };
        }
    }

}