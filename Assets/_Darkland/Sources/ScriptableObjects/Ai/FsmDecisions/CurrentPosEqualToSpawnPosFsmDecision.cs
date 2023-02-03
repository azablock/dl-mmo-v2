using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(CurrentPosEqualToSpawnPosFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(CurrentPosEqualToSpawnPosFsmDecision))]
    public class CurrentPosEqualToSpawnPosFsmDecision : FsmDecision {

        //todo ai mob save spawn pos on spawn
        public override bool IsValid(GameObject parent) {
            return false;
        }

    }

}