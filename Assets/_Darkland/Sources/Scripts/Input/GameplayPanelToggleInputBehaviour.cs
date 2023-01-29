using System;
using _Darkland.Sources.Scripts.Presentation.Gameplay;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class GameplayPanelToggleInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction heroSheetAction;

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
        }

        private void Disconnect() {
            heroSheetAction.Disable();
            heroSheetAction.performed -= ClientToggleHeroSheet;
        }

        [Client]
        private static void ClientToggleHeroSheet(InputAction.CallbackContext _) =>
            ClientPerformOnChatOff(() => toggleGroup.ClientToggleHeroSheet());

        [Client]
        private static void ClientPerformOnChatOff(Action action) {
            if (!InputStateBehaviour._.chatInputActive) action();
        }
        
        private static GameplayPanelToggleGroup toggleGroup => FindObjectOfType<GameplayPanelToggleGroup>();
    }

}