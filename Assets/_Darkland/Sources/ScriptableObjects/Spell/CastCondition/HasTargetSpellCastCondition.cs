using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(HasTargetSpellCastCondition),
                     menuName = "DL/" + nameof(SpellCastCondition) + "/" + nameof(HasTargetSpellCastCondition))]
    public class HasTargetSpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster, ISpell _) {
            return caster.GetComponent<ITargetNetIdHolder>().HasTarget();
        }

        public override string InvalidCastMessage() {
            return "No target selected";
        }

    }

}