using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    public abstract class SpellCastCondition : ScriptableObject, ISpellCastCondition {

        public abstract bool CanCast(GameObject caster, ISpell spell);

        public abstract string InvalidCastMessage();

    }

}