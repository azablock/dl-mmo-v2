using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class MovementInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction moveAction;

        private void OnEnable() {
            moveAction.Enable();
            
            moveAction.performed += ClientSendMoveInput;
            moveAction.canceled += ClientSendStopMoveInput;
        }

        private void OnDisable() {
            moveAction.Disable();
            
            moveAction.performed -= ClientSendMoveInput;
            moveAction.canceled -= ClientSendStopMoveInput;
        }

        [Client]
        private static void ClientSendMoveInput(InputAction.CallbackContext context) {
            var input = context.ReadValue<Vector2>();
            var moveDelta = new Vector3Int((int) input.x, (int) input.y, 0);

            NetworkClient.Send(new PlayerInputMessages.MoveRequestMessage {moveDelta = moveDelta});
        }
        
        [Client]
        private static void ClientSendStopMoveInput(InputAction.CallbackContext context) {
            NetworkClient.Send(new PlayerInputMessages.MoveRequestMessage {moveDelta = Vector3Int.zero});
        }
    }

}