using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.Npc;
using _Darkland.Sources.Scripts.Presentation.Gameplay.GameReport;
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
            DarklandHeroBehaviour.LocalHeroStarted += Connect;
            DarklandHeroBehaviour.LocalHeroStopped += Disconnect;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroStarted -= Connect;
            DarklandHeroBehaviour.LocalHeroStopped -= Disconnect;
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
            var layerToOmit = LayerMask.GetMask($"Mob Perception Zone");
            layerToOmit = ~layerToOmit;
            
            var maxDistance = 16; //todo magic number
            var hit = Physics.Raycast(ray, out var raycastHit, maxDistance, layerToOmit);

            if (!hit) return;

            ClientHandleUnitClick(raycastHit);
            ClientHandleNpcClick(raycastHit);
            ClientHandleOnGroundEqItemClick(raycastHit);
            ClientHandleInfoBoardClick(raycastHit);
            ClientHandleGameReportTrigger(raycastHit);
            //todo handle other collider hit "types"
        }

        private static void ClientHandleOnGroundEqItemClick(RaycastHit raycastHit) {
            var onGroundEqItem = raycastHit.collider.GetComponent<IOnGroundEqItem>();
            if (onGroundEqItem == null) return;
            
            NetworkClient.Send(new PlayerInputMessages.PickupItemRequestMessage { eqItemPos = onGroundEqItem.Pos });
        }

        private static void ClientHandleNpcClick(RaycastHit raycastHit) {
            var npcTrader = raycastHit.collider.GetComponent<NpcTraderBehaviour>();
            if (npcTrader == null) return; 

            npcTrader.ClientToggle();
        }

        private static void ClientHandleUnitClick(RaycastHit raycastHit) {
            var darklandUnit = raycastHit.collider.GetComponent<DarklandUnit>();
            if (darklandUnit == null) return;
            
            NetworkClient.Send(new PlayerInputMessages.NpcClickRequestMessage {npcNetId = darklandUnit.netId});
        }

        private static void ClientHandleInfoBoardClick(RaycastHit raycastHit) {
            var infoText = raycastHit.collider.GetComponent<LocalPlayerProximityCheckerBehaviour>();
            if (infoText == null) return;

            infoText.Toggle();
        }

        private static void ClientHandleGameReportTrigger(RaycastHit raycastHit) {
            var gameReportPanelTrigger = raycastHit.collider.GetComponent<GameReportPanelTrigger>();
            if (gameReportPanelTrigger == null) return;

            gameReportPanelTrigger.Toggle();
        }

    }

}