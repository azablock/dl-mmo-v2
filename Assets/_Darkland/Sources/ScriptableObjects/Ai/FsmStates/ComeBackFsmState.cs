using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Scripts.Ai;
using _Darkland.Sources.Scripts.Movement;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmStates {

    [CreateAssetMenu(fileName = nameof(ComeBackFsmState), menuName = "DL/Ai/" + nameof(ComeBackFsmState))]
    public class ComeBackFsmState : FsmState {

        public override void EnterSelf(GameObject parent) {
            var currentPos = parent.GetComponent<IDiscretePosition>().Pos;
            var spawnPos = parent.GetComponent<SpawnPositionHolder>().spawnPos;

            parent.GetComponent<AiPathHolderBehaviour>().ServerSetPath(currentPos, spawnPos);
        }

        public override void UpdateSelf(GameObject parent) {
            var currentPos = parent.GetComponent<IDiscretePosition>().Pos;
            var pathHolder = parent.GetComponent<AiPathHolderBehaviour>();
            
            if (pathHolder.IsPathEmpty()) return;

            var nextPos = pathHolder.NextPos();
            var movementVector = nextPos - currentPos;
            
            parent.GetComponent<MovementBehaviour>().ServerMoveOnce(movementVector);
        }

    }

}