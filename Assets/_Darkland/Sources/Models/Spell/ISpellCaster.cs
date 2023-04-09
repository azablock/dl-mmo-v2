using System;
using System.Collections.Generic;
using _Darkland.Sources.Scripts.Spell;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpellCaster {
        void CastSpell(int spellIdx);
        List<ISpell> AvailableSpells { get; } //todo hashset
        event Action<SpellCastedEvent> ClientSpellCasted;
    }

}