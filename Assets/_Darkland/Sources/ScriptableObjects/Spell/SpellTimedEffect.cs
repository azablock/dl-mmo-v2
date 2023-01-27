using System.Collections;
using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    public abstract class SpellTimedEffect : ScriptableObject, ISpellTimedEffect {

        public virtual void PreProcess(GameObject caster) {}
        public virtual void PostProcess(GameObject caster) {}
        public abstract IEnumerator Process(GameObject caster);
        public abstract bool CanProcess(GameObject caster);

    }

}