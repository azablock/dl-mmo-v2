using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Presentation.Unit;
using _Darkland.Sources.Scripts.World;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class MouseInputBehaviour : MonoBehaviour {
        [SerializeField]
        private InputAction leftMouseClick;

        private void OnEnable() {
            DarklandHero.LocalHeroStarted += Connect;
            DarklandHero.LocalHeroStopped += Disconnect;
        }

        private void OnDisable() {
            DarklandHero.LocalHeroStarted -= Connect;
            DarklandHero.LocalHeroStopped -= Disconnect;
        }
        
        private void Connect() {
            leftMouseClick.performed += ClientOnLeftMouseClick;
            leftMouseClick.Enable();
        }

        private void Disconnect() {
            leftMouseClick.performed -= ClientOnLeftMouseClick;
            leftMouseClick.Disable();
        }

        [Client]
        private static void ClientOnLeftMouseClick(InputAction.CallbackContext context) {
            if (Camera.main == null) return;
            
            var ray = FindObjectOfType<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue());
            var hit = Physics.Raycast(ray, out var raycastHit);

            if (!hit) return;

            ClientHandleUnitClick(raycastHit);
            ClientHandleInfoBoardClick(raycastHit);
            //todo handle other collider hit "types"
        }

        private static void ClientHandleUnitClick(RaycastHit raycastHit) {
            var darklandUnit = raycastHit.collider.GetComponentInParent<DarklandUnit>();
            if (darklandUnit == null) return;
            
            NetworkClient.Send(new PlayerInputMessages.NpcClickRequestMessage() {npcNetId = darklandUnit.netId});
        }

        private static void ClientHandleInfoBoardClick(RaycastHit raycastHit) {
            var infoText = raycastHit.collider.GetComponentInParent<InfoTextBehaviour2>();
            if (infoText == null) return;

            infoText.Toggle();
        }

    }

}