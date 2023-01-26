using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    public interface ISpellCastCondition {

        bool CanCast(GameObject caster);
        string InvalidCastMessage();

    }

    
    public abstract class SpellCastCondition : ScriptableObject, ISpellCastCondition {

        public abstract bool CanCast(GameObject caster);
        public abstract string InvalidCastMessage();

    }

}