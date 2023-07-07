using _Darkland.Sources.Models.Core;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(HasNetIdTargetFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(HasNetIdTargetFsmDecision))]
    public class HasNetIdTargetFsmDecision : FsmDecision {

        public override bool IsValid(GameObject parent) {
            return parent.GetComponent<ITargetNetIdHolder>().HasTarget();
        }

    }

}