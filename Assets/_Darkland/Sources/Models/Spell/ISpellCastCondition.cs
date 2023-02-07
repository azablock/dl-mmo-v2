using UnityEngine;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpellCastCondition {

        bool CanCast(GameObject caster, ISpell spell);
        string InvalidCastMessage();

    }

}