using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Scripts.Ai;
using _Darkland.Sources.Scripts.Movement;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmStates {

    [CreateAssetMenu(fileName = nameof(ChaseFsmState), menuName = "DL/Ai/" + nameof(ChaseFsmState))]
    public class ChaseFsmState : FsmState {

        //todo nieefektywne - lepiej dodac komponent, ktory słucha na zmiane ruchu targetu - i tylko wtedy update path
        public override void UpdateSelf(GameObject parent) {
            if (!parent.GetComponent<ITargetNetIdHolder>().HasTarget()) return;
            
            var currentPos = parent.GetComponent<IDiscretePosition>().Pos;
            var targetPos = parent.GetComponent<ITargetNetIdHolder>().TargetPos();
            var pathHolder = parent.GetComponent<AiPathHolderBehaviour>();
            var movement = parent.GetComponent<MovementBehaviour>();

            pathHolder.ServerSetPath(currentPos, targetPos);
            
            if (pathHolder.IsPathEmpty()) return;

            var nextPos = pathHolder.NextPos();
            var movementVector = nextPos - currentPos;
            
            movement.ServerMoveOnce(movementVector);
        }

    }

}
