using System;

namespace _Darkland.Sources.Models.Unit.Traits {

    public interface IUnitTraitsHolder {
        void ChangeBasic(UnitTraitId id, int newValue);
        void ChangeBonus(UnitTraitId id, int newValue);
        UnitTraitValue Get(UnitTraitId id);
        
        UnitTraitValue Might { get; }
        UnitTraitValue Constitution { get; }
        UnitTraitValue Dexterity { get; }
        UnitTraitValue Intelligence { get; }
        UnitTraitValue Soul { get; }
        
        event Action<UnitTraitId, UnitTraitValue> Changed;
    }

}