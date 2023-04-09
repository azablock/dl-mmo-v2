using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(IsTargetFriendlySpellCastCondition),
                     menuName = "DL/"  + nameof(SpellCastCondition) + "/" + nameof(IsTargetFriendlySpellCastCondition))]
    public class IsTargetFriendlySpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster, ISpell _) {
            return caster.GetComponent<ITargetNetIdHolder>().IsTargetFriendly();
        }

        public override string InvalidCastMessage() => "Target is not friendly";

    }

}