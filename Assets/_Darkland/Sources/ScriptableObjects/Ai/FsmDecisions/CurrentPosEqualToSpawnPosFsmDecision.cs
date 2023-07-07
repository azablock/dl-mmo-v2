using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Scripts.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmDecisions {

    [CreateAssetMenu(fileName = nameof(CurrentPosEqualToSpawnPosFsmDecision),
                     menuName = "DL/Ai/FsmDecision/" + nameof(CurrentPosEqualToSpawnPosFsmDecision))]
    public class CurrentPosEqualToSpawnPosFsmDecision : FsmDecision {

        public override bool IsValid(GameObject parent) {
            return parent.GetComponent<IDiscretePosition>()
                .Pos.Equals(parent.GetComponent<SpawnPositionHolder>().spawnPos);
        }

    }

}