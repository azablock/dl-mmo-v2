using _Darkland.Sources.Models.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(HasNonEmptyPerceptionFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(HasNonEmptyPerceptionFsmDecision))]
    public class HasNonEmptyPerceptionFsmDecision : FsmDecision {

        public AiPerceptionZoneType zoneType;
        
        public override bool IsValid(GameObject parent) {
            return parent.GetComponent<IAiNetworkPerception>().PerceptionZones[zoneType].targets.Count > 0;
        }

    }

}