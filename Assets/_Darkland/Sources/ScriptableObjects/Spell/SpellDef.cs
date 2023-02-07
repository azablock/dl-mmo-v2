using System.Collections.Generic;
using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Scripts.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell {

    [CreateAssetMenu(fileName = nameof(SpellDef), menuName = "DL/" + nameof(SpellDef))]
    public class SpellDef : ScriptableObject, ISpell {
        [SerializeField]
        [Range(0, 100)]
        private int manaCost;
        [SerializeField]
        private float cooldown;
        [SerializeField]
        private float castTime;
        [SerializeField]
        [Range(0, TargetNetIdHolderBehaviour.MaxTargetDis / 2)]
        private float castRange;
        [SerializeField]
        private TargetType targetType;
        [Space]
        [SerializeField]
        private List<SpellInstantEffect> instantEffects;
        [SerializeField]
        private List<SpellTimedEffect> timedEffects;
        [SerializeField]
        private List<SpellCastCondition> castConditions;

        public string Id => name;
        public int ManaCost => manaCost;
        public float CastRange => castRange;
        public float CastTime => castTime;
        public TargetType TargetType => targetType;
        public List<ISpellInstantEffect> InstantEffects => new(instantEffects);
        public List<ISpellTimedEffect> TimedEffects => new(timedEffects);
        public List<ISpellCastCondition> CastConditions => new(castConditions);

        public float Cooldown(GameObject caster) => cooldown;

        public string Description() {
            return "";
        }

    }

}