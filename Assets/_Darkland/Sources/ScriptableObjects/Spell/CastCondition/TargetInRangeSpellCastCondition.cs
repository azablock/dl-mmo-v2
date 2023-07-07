using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(TargetInRangeSpellCastCondition),
                     menuName = "DL/" + nameof(SpellCastCondition) + "/" + nameof(TargetInRangeSpellCastCondition))]
    public class TargetInRangeSpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster, ISpell spell) {
            var castPos = caster.GetComponent<IDiscretePosition>().Pos;
            var targetPos = caster.GetComponent<ITargetNetIdHolder>().TargetPos();

            return Vector3.Distance(castPos, targetPos) < spell.CastRange;
        }

        public override string InvalidCastMessage() {
            return "Target not in spell range";
        }

    }

}