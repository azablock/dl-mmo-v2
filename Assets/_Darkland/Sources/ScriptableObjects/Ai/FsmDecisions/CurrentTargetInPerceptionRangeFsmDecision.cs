using _Darkland.Sources.Models.Ai;
using _Darkland.Sources.Models.Core;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(CurrentTargetInPerceptionRangeFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(CurrentTargetInPerceptionRangeFsmDecision))]
    public class CurrentTargetInPerceptionRangeFsmDecision : FsmDecision {

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