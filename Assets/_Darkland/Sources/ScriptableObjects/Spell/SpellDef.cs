using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        [SerializeField]
        private SpellCooldownStrategy cooldownStrategy;
        [SerializeField]
        [TextArea]
        private string generalDescription;
        public TargetType TargetType => targetType;

        public string Id => name;
        public string SpellName => Regex.Replace(name, $" {nameof(SpellDef)}", string.Empty);
        public int ManaCost => manaCost;
        public float CastRange => castRange;
        public float CastTime => castTime;
        public float BaseCooldown => cooldown;
        public List<ISpellInstantEffect> InstantEffects => new(instantEffects);
        public List<ISpellTimedEffect> TimedEffects => new(timedEffects);
        public ISpellCooldownStrategy CooldownStrategy => cooldownStrategy;
        public string GeneralDescription => generalDescription;
        public List<ISpellCastCondition> CastConditions => new(castConditions);

        public float Cooldown(GameObject caster) {
            return CooldownStrategy.Cooldown(this, caster);
        }

        public string Description(GameObject caster) {
            var instantEffectDescriptions = instantEffects.Select(it => it.Description(caster)).ToList();
            var timedEffectDescriptions = timedEffects
                .Select(it => it.Description(caster, this))
                .ToList();

            var descriptions = new List<string>();
            descriptions.Add(GeneralDescription);
            descriptions.Add($"Mana cost: {manaCost}");
            descriptions.Add($"Cooldown: {Cooldown(caster):0.00} seconds (base: {BaseCooldown:0.00} seconds)\n");
            descriptions.AddRange(instantEffectDescriptions);
            descriptions.AddRange(timedEffectDescriptions);


            return descriptions.Aggregate(string.Empty, (desc, next) => desc + next + "\n");
        }

    }

}