using System;

namespace _Darkland.Sources.Models.Unit.Stats {

    [Serializable]
    public struct UnitStats {
        public float attackSpeed;
        public float hpRegain;
        public int maxHp;
        public int maxMana;

        public static UnitStats operator+(UnitStats a, UnitStats b) {
            return new UnitStats {
                attackSpeed = a.attackSpeed + b.attackSpeed,
                hpRegain = a.hpRegain + b.hpRegain
                
            };
        }
    }

}