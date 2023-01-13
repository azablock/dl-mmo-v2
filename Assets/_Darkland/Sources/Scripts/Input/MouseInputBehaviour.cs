using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class MouseInputBehaviour : MonoBehaviour {
        [SerializeField]
        private InputAction leftMouseClick;

        private void OnEnable() {
            leftMouseClick.performed += ClientOnLeftMouseClick;
            leftMouseClick.Enable();
        }

        private void OnDisable() {
            leftMouseClick.performed -= ClientOnLeftMouseClick;
            leftMouseClick.Disable();
        }

        [Client]
        private static void ClientOnLeftMouseClick(InputAction.CallbackContext context) {
            if (Camera.main == null) return;
            
            var screenToWorldPoint = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            var pos = new Vector2Int(Mathf.RoundToInt(screenToWorldPoint.x), Mathf.RoundToInt(screenToWorldPoint.y));

            // NetworkClient.Send(new PlayerInputMessages.LeftMouseClickRequestMessage {clickWorldPosition = pos});
        }
    }

}