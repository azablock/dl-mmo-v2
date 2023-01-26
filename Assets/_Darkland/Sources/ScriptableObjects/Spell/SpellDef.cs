using System.Collections.Generic;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    public abstract class SpellDef : ScriptableObject, ISpell {
        [SerializeField]
        protected string id;
        [SerializeField]
        protected float manaCost;
        [SerializeField]
        protected float cooldown;
        [SerializeField]
        protected float castTime;
        [SerializeField]
        protected TargetType targetType;
        [Space]
        [SerializeField]
        protected List<SpellInstantEffect> instantEffects;
        [SerializeField]
        protected List<SpellTimedEffect> timedEffects;
        [SerializeField]
        protected List<SpellCastCondition> castConditions;

        public string Id => id;
        public float ManaCost => manaCost;
        public float CastTime => castTime;
        public TargetType TargetType => targetType;
        public List<ISpellInstantEffect> InstantEffects => new(instantEffects);
        public List<ISpellTimedEffect> TimedEffects => new(timedEffects);
        public List<ISpellCastCondition> CastConditions => new(castConditions);

        public virtual float Cooldown(GameObject caster) => cooldown;
        public abstract string Description();

    }

}