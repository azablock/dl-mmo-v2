using _Darkland.Sources.Models.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(HasNetIdTargetFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(HasNetIdTargetFsmDecision))]
    public class HasNetIdTargetFsmDecision : FsmDecision {

        public override bool IsValid(GameObject parent) => parent.GetComponent<ITargetNetIdHolder>().HasTarget();

    }

}