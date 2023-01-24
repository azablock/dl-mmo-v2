using System.Collections.Generic;
using _Darkland.Sources.Models.Combat;

namespace _Darkland.Sources.Models.Spell {

    public interface ISpell {
        string Id { get; }
        float ManaCost { get; }
        float Cooldown { get; }
        float CastTime { get; }
        TargetType TargetType { get; }
        List<ISpellInstantEffect> InstantEffects { get; }
        List<ISpellTimedEffect> TimedEffects { get; }
        string Description();
    }

    public interface ISpellCaster {
        void Cast(string spellId);
        bool CanCast(string spellId);
    }

}