using System;

namespace _Darkland.Sources.Models.Unit.Traits {

    [Serializable]
    public struct UnitTraitValue {
        public int basic;
        public int bonus;

        public int current => basic + bonus;

        public static UnitTraitValue zero => new UnitTraitValue();

        public static UnitTraitValue Of(int newBasic, int newBonus) {
            return new UnitTraitValue {basic = newBasic, bonus = newBonus};
        }

        public UnitTraitValue WithBasic(int newBasic) {
            return new UnitTraitValue {basic = newBasic, bonus = bonus};
        }

        public UnitTraitValue WithBonus(int newBonus) {
            return new UnitTraitValue {basic = basic, bonus = newBonus};
        }

        private UnitTraitValue(int basic, int bonus) {
            this.basic = basic;
            this.bonus = bonus;
        }
    }

}