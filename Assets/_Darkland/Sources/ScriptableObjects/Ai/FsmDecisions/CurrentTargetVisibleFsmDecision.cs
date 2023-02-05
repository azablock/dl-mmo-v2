using _Darkland.Sources.Models.Combat;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Scripts.World;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(CurrentTargetVisibleFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(CurrentTargetVisibleFsmDecision))]
    public class CurrentTargetVisibleFsmDecision : FsmDecision {

        //todo refactor this vs. TargetVisibleSpellCastCondition -> or Mob should use SpellCasterBehaviour
        public override bool IsValid(GameObject parent) {
            var currentPos = parent.GetComponent<IDiscretePosition>().Pos;
            var targetPos = parent.GetComponent<ITargetNetIdHolder>().TargetPos();

            return CombatTargetUtil.TargetBresenhamVisible(currentPos, targetPos, DarklandWorldBehaviour._);
        }

    }

}