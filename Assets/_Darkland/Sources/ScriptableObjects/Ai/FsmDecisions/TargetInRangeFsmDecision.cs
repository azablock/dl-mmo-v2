using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(TargetInRangeFsmDecision),
                     menuName = "DL/Ai/" + nameof(TargetInRangeFsmDecision))]
    public class TargetInRangeFsmDecision : FsmDecision {

        [Tooltip("tmp - range will be different based on some gameplay mechanics")]
        public float range;
        
        public override bool IsValid(GameObject parent) {
            var targetNetIdHolder = parent.GetComponent<ITargetNetIdHolder>();
            var parentPos = parent.GetComponent<IDiscretePosition>().Pos;
            var targetPos = targetNetIdHolder.TargetNetIdentity.GetComponent<IDiscretePosition>().Pos;

            return Vector3.Distance(parentPos, targetPos) < range;
        }

    }

}