using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class MovementInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction moveAction;
        [SerializeField]
        private InputAction changeFloorAction;

        private void OnEnable() {
            moveAction.Enable();
            moveAction.performed += ClientSendMoveInput;
            moveAction.canceled += ClientSendStopMoveInput;

            changeFloorAction.Enable();
            changeFloorAction.performed += ClientSendChangeFloorMoveInput;
        }

        private void OnDisable() {
            moveAction.Disable();
            moveAction.performed -= ClientSendMoveInput;
            moveAction.canceled -= ClientSendStopMoveInput;

            changeFloorAction.Disable();
            changeFloorAction.performed -= ClientSendChangeFloorMoveInput;
        }

        [Client]
        private static void ClientSendMoveInput(InputAction.CallbackContext context) {
            if (InputStateBehaviour._.chatInputActive) return;

            var input = context.ReadValue<Vector2>();
            var movementVector = new Vector3Int((int) input.x, (int) input.y, 0);

            NetworkClient.Send(new PlayerInputMessages.MoveRequestMessage {movementVector = movementVector});
        }

        [Client]
        private static void ClientSendStopMoveInput(InputAction.CallbackContext context) {
            NetworkClient.Send(new PlayerInputMessages.MoveRequestMessage {movementVector = Vector3Int.zero});
        }

        [Client]
        private static void ClientSendChangeFloorMoveInput(InputAction.CallbackContext context) {
            if (InputStateBehaviour._.chatInputActive) return;

            var zDelta = context.ReadValue<float>();
            var movementVector = new Vector3Int(0, 0, (int) zDelta);

            NetworkClient.Send(new PlayerInputMessages.ChangeFloorRequestMessage {movementVector = movementVector});
        }
    }

}