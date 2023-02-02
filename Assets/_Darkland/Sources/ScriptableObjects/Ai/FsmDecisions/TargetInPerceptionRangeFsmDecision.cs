using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(TargetInPerceptionRangeFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(TargetInPerceptionRangeFsmDecision))]
    public class TargetInPerceptionRangeFsmDecision : FsmDecision {

        public AiPerceptionZoneType zoneType;
        
        public override bool IsValid(GameObject parent) {
            var targetNetIdHolder = parent.GetComponent<ITargetNetIdHolder>();
            var parentPos = parent.GetComponent<IDiscretePosition>().Pos;
            var targetPos = targetNetIdHolder.TargetNetIdentity.GetComponent<IDiscretePosition>().Pos;
            var range = parent.GetComponent<IAiNetworkPerception>().PerceptionZoneRange(zoneType);
            
            return Vector3.Distance(parentPos, targetPos) < range;
        }

    }

}