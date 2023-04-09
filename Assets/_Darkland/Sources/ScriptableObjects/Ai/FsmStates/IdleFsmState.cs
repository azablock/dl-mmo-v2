using _Darkland.Sources.Scripts.Ai;
using _Darkland.Sources.Scripts.Movement;
using UnityEngine;

namespace _Darkland.Sources.ScriptableObjects.Ai.FsmStates {
    
    [CreateAssetMenu(fileName = nameof(IdleFsmState), menuName = "DL/Ai/" + nameof(IdleFsmState))]
    public class IdleFsmState : FsmState {

        public override void EnterSelf(GameObject parent) {
            //todo reset AiMovementMemory (idle move options)
            parent.GetComponent<AiCombatMemory>().Clear();
        }

        public override void UpdateSelf(GameObject parent) {
            var nextIdleMoveDelta = parent.GetComponent<AiMovementMemory>().ServerNextIdleMoveDelta();
            parent.GetComponent<MovementBehaviour>().ServerMoveOnce(nextIdleMoveDelta);
        }

    }

}