using _Darkland.Sources.Scripts.Presentation.Gameplay.Chat;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Darkland.Sources.Scripts.Input {

    public class ChatFocusInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction chatFocusAction;
        [SerializeField]
        private InputAction chatLeaveAction;

        private void OnEnable() {
            DarklandHeroBehaviour.LocalHeroStarted += Connect;
            DarklandHeroBehaviour.LocalHeroStopped += Disconnect;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroStarted -= Connect;
            DarklandHeroBehaviour.LocalHeroStopped -= Disconnect;
        }

        private void Connect() {
            chatFocusAction.Enable();
            chatFocusAction.performed += ClientFocusChatPanel;

            chatLeaveAction.Enable();
            chatLeaveAction.performed += ClientLeaveChatPanel;
        }

        private void Disconnect() {
            chatFocusAction.Disable();
            chatFocusAction.performed -= ClientFocusChatPanel;

            chatLeaveAction.Disable();
            chatLeaveAction.performed -= ClientLeaveChatPanel;
        }

        [Client]
        private static void ClientFocusChatPanel(InputAction.CallbackContext _) =>
            FindObjectOfType<ChatPanel>().ClientFocus();

        [Client]
        private static void ClientLeaveChatPanel(InputAction.CallbackContext _) => 
            FindObjectOfType<ChatPanel>().ClientLeave();

    }

}