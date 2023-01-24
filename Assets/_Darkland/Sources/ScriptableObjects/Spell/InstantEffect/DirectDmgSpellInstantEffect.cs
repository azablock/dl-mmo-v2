using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.InstantEffect {

    [CreateAssetMenu(fileName = nameof(DirectDmgSpellInstantEffect),
                     menuName = "DL/"  + nameof(SpellInstantEffect) + "/" + nameof(DirectDmgSpellInstantEffect))]
    public class DirectDmgSpellInstantEffect : SpellInstantEffect {

        public int damage;
        public DamageType damageType;

        public override void Process(GameObject caster) {
            var targetStatsHolder = caster
                .GetComponent<ITargetNetIdHolder>()
                .TargetNetIdentity
                .GetComponent<IStatsHolder>();
            
            targetStatsHolder.Subtract(StatId.Health, damage);
        }

        public override bool IsValid(GameObject caster) {
            return caster
                .GetComponent<ITargetNetIdHolder>()
                .TargetNetIdentity != null;
        }

    }

}