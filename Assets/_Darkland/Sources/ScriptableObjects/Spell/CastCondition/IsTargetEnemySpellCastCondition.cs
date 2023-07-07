using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Models.Spell;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(IsTargetEnemySpellCastCondition),
                     menuName = "DL/" + nameof(SpellCastCondition) + "/" + nameof(IsTargetEnemySpellCastCondition))]
    public class IsTargetEnemySpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster, ISpell _) {
            return caster.GetComponent<ITargetNetIdHolder>().IsTargetEnemy();
        }

        public override string InvalidCastMessage() {
            return "Target is not enemy";
        }

    }

}