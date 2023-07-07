using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(TargetInWeaponRangeSpellCastCondition),
                     menuName = "DL/" + nameof(SpellCastCondition) + "/" +
                                nameof(TargetInWeaponRangeSpellCastCondition))]
    public class TargetInWeaponRangeSpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster, ISpell _) {
            var castPos = caster.GetComponent<IDiscretePosition>().Pos;
            var targetPos = caster.GetComponent<ITargetNetIdHolder>().TargetPos();
            var weapon = caster.GetComponent<IEqHolder>().ServerEquippedWeapon();
            var attackRange = weapon?.AttackRange ?? IDamageDealer.UnarmedAttackRange;

            return Vector3.Distance(castPos, targetPos) < attackRange;
        }

        public override string InvalidCastMessage() {
            return "Target not in attack range";
        }

    }

}