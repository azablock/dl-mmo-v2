using System;

namespace _Darkland.Sources.Models.Unit.Traits {

    [Serializable]
    public struct UnitTraitsData {
        public UnitTraitValue might;
        public UnitTraitValue constitution;
        public UnitTraitValue dexterity;
        public UnitTraitValue intelligence;
        public UnitTraitValue soul;
        
        public static class Mapper {

            public static UnitTraitsData Map(IUnitTraitsHolder unitTraitsHolder) {
                return new UnitTraitsData {
                    might = unitTraitsHolder.Might,
                    constitution = unitTraitsHolder.Constitution,
                    dexterity = unitTraitsHolder.Dexterity,
                    intelligence = unitTraitsHolder.Intelligence,
                    soul = unitTraitsHolder.Soul
                };
            }
        }
    }

}