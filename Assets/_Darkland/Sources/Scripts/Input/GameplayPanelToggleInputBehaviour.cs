using System;
using _Darkland.Sources.Scripts.Presentation.Gameplay;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class GameplayPanelToggleInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction heroSheetAction;

        [SerializeField]
        private InputAction equipmentAction;

        private void OnEnable() {
            DarklandHeroBehaviour.LocalHeroStarted += Connect;
            DarklandHeroBehaviour.LocalHeroStopped += Disconnect;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroStarted -= Connect;
            DarklandHeroBehaviour.LocalHeroStopped -= Disconnect;
        }

        private void Connect() {
            heroSheetAction.Enable();
            heroSheetAction.performed += ClientToggleHeroSheet;
            
            equipmentAction.Enable();
            equipmentAction.performed += ClientToggleEquipment;
        }

        private void Disconnect() {
            heroSheetAction.Disable();
            heroSheetAction.performed -= ClientToggleHeroSheet;
                        
            equipmentAction.Disable();
            equipmentAction.performed -= ClientToggleEquipment;
        }

        [Client]
        private static void ClientToggleHeroSheet(InputAction.CallbackContext _) =>
            ClientPerformOnChatOff(() => toggleGroup.ClientToggleHeroSheet());

        [Client]
        private static void ClientToggleEquipment(InputAction.CallbackContext _) =>
            ClientPerformOnChatOff(() => toggleGroup.ClientToggleEquipment());

        [Client]
        private static void ClientPerformOnChatOff(Action action) {
            if (!InputStateBehaviour._.chatInputActive) action();
        }
        
        private static GameplayPanelToggleGroup toggleGroup => FindObjectOfType<GameplayPanelToggleGroup>();
    }

}