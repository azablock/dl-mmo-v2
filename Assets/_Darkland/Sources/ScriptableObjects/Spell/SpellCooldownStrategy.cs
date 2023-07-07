using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    public interface ISpellCooldownStrategy {

        float Cooldown(ISpell spell, GameObject caster);

    }

    public abstract class SpellCooldownStrategy : ScriptableObject, ISpellCooldownStrategy {

        public abstract float Cooldown(ISpell spell, GameObject caster);

    }

}