using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(TargetInAttackPerceptionRangeFsmDecision),
                     menuName = "DL/Ai/" + nameof(TargetInAttackPerceptionRangeFsmDecision))]
    public class TargetInAttackPerceptionRangeFsmDecision : FsmDecision {

        public override bool IsValid(GameObject parent) {
            var targetNetIdHolder = parent.GetComponent<ITargetNetIdHolder>();
            var parentPos = parent.GetComponent<IDiscretePosition>().Pos;
            var targetPos = targetNetIdHolder.TargetNetIdentity.GetComponent<IDiscretePosition>().Pos;
            var range = parent.GetComponent<IAiNetworkPerception>().AttackPerceptionRange;
            
            return Vector3.Distance(parentPos, targetPos) < range;
        }

    }

}