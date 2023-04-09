using System.Collections.Generic;
using _Darkland.Sources.ScriptableObjects.Spell;
using UnityEngine;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpell {
        string Id { get; }
        string SpellName { get; }
        int ManaCost { get; }
        float CastRange { get; }
        float CastTime { get; }
        float BaseCooldown { get; }
        List<ISpellCastCondition> CastConditions { get; }
        List<ISpellInstantEffect> InstantEffects { get; }
        List<ISpellTimedEffect> TimedEffects { get; }
        ISpellCooldownStrategy CooldownStrategy { get; }
        string GeneralDescription { get; }

        float Cooldown(GameObject caster);
        string Description(GameObject caster);
    }

}