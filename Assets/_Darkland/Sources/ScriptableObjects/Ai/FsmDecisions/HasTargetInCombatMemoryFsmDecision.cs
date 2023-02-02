using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Scripts.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(HasTargetInCombatMemoryFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(HasTargetInCombatMemoryFsmDecision))]
    public class HasTargetInCombatMemoryFsmDecision : FsmDecision {

        public override bool IsValid(GameObject parent) {
            var targetNetIdentity = parent.GetComponent<ITargetNetIdHolder>().TargetNetIdentity;
            var targetIsAttacker = parent.GetComponent<AiCombatMemory>().HasInHistory(targetNetIdentity);
            
            return targetIsAttacker;
        }

    }

}