using _Darkland.Sources.Models.Interaction;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(HasNetIdTargetFsmDecision),
                     menuName = "DL/Ai/" + nameof(HasNetIdTargetFsmDecision))]
    public class HasNetIdTargetFsmDecision : FsmDecision {

        public override bool IsValid(GameObject parent) {
            return parent.GetComponent<ITargetNetIdHolder>().TargetNetIdentity != null;
        }

    }

}