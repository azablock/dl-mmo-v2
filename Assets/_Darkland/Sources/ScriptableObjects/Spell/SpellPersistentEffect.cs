using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    public abstract class SpellPersistentEffect : ScriptableObject, ISpellPersistentEffect {

        public abstract void Activate(GameObject caster);
        public abstract void Deactivate(GameObject caster);

    }

}