using System.Collections.Generic;
using UnityEngine;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpell {
        string Id { get; }
        float ManaCost { get; }
        float CastTime { get; }
        List<ISpellCastCondition> CastConditions { get; }
        List<ISpellInstantEffect> InstantEffects { get; }
        List<ISpellTimedEffect> TimedEffects { get; }
        GameObject SpellVfxPrefab { get; }

        float Cooldown(GameObject caster);
        string Description();
    }

}