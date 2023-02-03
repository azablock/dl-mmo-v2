using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Scripts.Ai;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmStates {

    [CreateAssetMenu(fileName = nameof(ComeBackFsmState), menuName = "DL/Ai/" + nameof(ComeBackFsmState))]
    public class ComeBackFsmState : FsmState {

        public override void UpdateSelf(GameObject parent) {
            var spawnPos = parent.GetComponent<SpawnPositionHolder>().spawnPos;
            parent.GetComponent<IDiscretePosition>().Set(spawnPos, true);
        }

    }

}