using System.Collections.Generic;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    public abstract class SpellDef : ScriptableObject, ISpell {
        [SerializeField]
        private string id;
        [SerializeField]
        private float manaCost;
        [SerializeField]
        private float cooldown;
        [SerializeField]
        private float castTime;
        [SerializeField]
        private TargetType targetType;
        [Space]
        [SerializeField]
        private List<SpellInstantEffect> instantEffects;
        [SerializeField]
        private List<SpellTimedEffect> timedEffects;

        public string Id => id;
        public float ManaCost => manaCost;
        public float Cooldown => cooldown;
        public float CastTime => castTime;
        public TargetType TargetType => targetType;
        public List<ISpellInstantEffect> InstantEffects => new(instantEffects);
        public List<ISpellTimedEffect> TimedEffects => new(timedEffects);

        public abstract string Description();

    }

}