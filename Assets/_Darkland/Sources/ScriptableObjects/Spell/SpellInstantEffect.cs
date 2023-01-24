using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {
    
    public abstract class SpellInstantEffect : ScriptableObject, ISpellInstantEffect {

        public abstract void Process(GameObject caster);
        public abstract bool IsValid(GameObject caster);

    }

}