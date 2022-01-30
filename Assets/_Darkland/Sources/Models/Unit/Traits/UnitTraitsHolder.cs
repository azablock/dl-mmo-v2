using System;
using System.Collections.Generic;

namespace _Darkland.Sources.Models.Unit.Traits {

    public sealed class UnitTraitsHolder : IUnitTraitsHolder {

        public event Action<UnitTraitId, UnitTraitValue> Changed;

        private readonly Dictionary<UnitTraitId, UnitTraitValue> _traits = new Dictionary<UnitTraitId, UnitTraitValue> {
            {UnitTraitId.Might, UnitTraitValue.zero},
            {UnitTraitId.Constitution, UnitTraitValue.zero},
            {UnitTraitId.Dexterity, UnitTraitValue.zero},
            {UnitTraitId.Intelligence, UnitTraitValue.zero},
            {UnitTraitId.Soul, UnitTraitValue.zero}
        };

        public void ChangeBasic(UnitTraitId id, int newValue) {
            _traits[id] = _traits[id].WithBasic(Math.Max(0, newValue));
            Changed?.Invoke(id, _traits[id]);
        }

        public void ChangeBonus(UnitTraitId id, int newValue) {
            _traits[id] = _traits[id].WithBonus(Math.Max(0, newValue));
            Changed?.Invoke(id, _traits[id]);
        }

        public UnitTraitValue Get(UnitTraitId id) {
            return _traits[id];
        }

        public UnitTraitValue Might => Get(UnitTraitId.Might);
        public UnitTraitValue Constitution => Get(UnitTraitId.Constitution);
        public UnitTraitValue Dexterity => Get(UnitTraitId.Dexterity);
        public UnitTraitValue Intelligence => Get(UnitTraitId.Intelligence);
        public UnitTraitValue Soul => Get(UnitTraitId.Soul);
    }
}