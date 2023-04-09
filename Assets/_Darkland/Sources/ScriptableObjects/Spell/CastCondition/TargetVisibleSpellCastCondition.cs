using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Spell;
using _Darkland.Sources.Scripts.World;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Spell.CastCondition {

    [CreateAssetMenu(fileName = nameof(TargetVisibleSpellCastCondition),
                     menuName = "DL/"  + nameof(SpellCastCondition) + "/" + nameof(TargetVisibleSpellCastCondition))]
    public class TargetVisibleSpellCastCondition : SpellCastCondition {

        public override bool CanCast(GameObject caster, ISpell _) {
            var currentPos = caster.GetComponent<IDiscretePosition>().Pos;
            var targetPos = caster.GetComponent<ITargetNetIdHolder>().TargetPos();

            return CombatTargetUtil.TargetBresenhamVisible(currentPos, targetPos, DarklandWorldBehaviour._);
        }

        public override string InvalidCastMessage() {
            return "Current target not in the light of sight";
        }

    }

}