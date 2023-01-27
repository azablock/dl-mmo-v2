using _Darkland.Sources.Models.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(IsTargetEnemySpellCastCondition),
                     menuName = "DL/"  + nameof(SpellCastCondition) + "/" + nameof(IsTargetEnemySpellCastCondition))]
    public class IsTargetEnemySpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster) {
            return caster.GetComponent<ITargetNetIdHolder>().IsTargetEnemy();
        }

        public override string InvalidCastMessage() => "Target is not enemy";

    }

}