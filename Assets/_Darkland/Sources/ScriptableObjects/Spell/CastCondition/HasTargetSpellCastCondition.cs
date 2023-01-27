using _Darkland.Sources.Models.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(HasTargetSpellCastCondition),
                     menuName = "DL/"  + nameof(SpellCastCondition) + "/" + nameof(HasTargetSpellCastCondition))]
    public class HasTargetSpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster) {
            return caster.GetComponent<ITargetNetIdHolder>().HasTarget();
        }

        public override string InvalidCastMessage() => "No target selected";

    }

}