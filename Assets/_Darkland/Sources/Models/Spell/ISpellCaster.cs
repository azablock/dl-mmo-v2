using System;
using _Darkland.Sources.Scripts.Spell;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpellCaster {
        void CastSpell(int spellIdx);
        event Action<SpellCastedEvent> ClientSpellCasted;
    }

}