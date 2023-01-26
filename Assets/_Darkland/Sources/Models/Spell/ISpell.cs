using System.Collections.Generic;
using _Darkland.Sources.ScriptableObjects.Spell;
using UnityEngine;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpell {
        string Id { get; }
        float ManaCost { get; }
        float CastTime { get; }
        List<ISpellCastCondition> CastConditions { get; }
        List<ISpellInstantEffect> InstantEffects { get; }
        List<ISpellTimedEffect> TimedEffects { get; }

        float Cooldown(GameObject caster);
        string Description();
    }

}