using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Scripts.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(HasCurrentTargetInCombatMemoryFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(HasCurrentTargetInCombatMemoryFsmDecision))]
    public class HasCurrentTargetInCombatMemoryFsmDecision : FsmDecision {

        public override bool IsValid(GameObject parent) {
            var targetNetIdentity = parent.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            var targetIsAttacker = parent.GetComponent<AiCombatMemory>().HasInHistory(targetNetIdentity);

            return targetIsAttacker;
        }

    }

}