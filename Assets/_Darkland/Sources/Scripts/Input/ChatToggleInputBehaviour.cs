using _Darkland.Sources.Scripts.Presentation.Gameplay.Chat;
using Mirror;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace _Darkland.Sources.Scripts.Input {

    public class ChatToggleInputBehaviour : MonoBehaviour {

        [SerializeField]
        private InputAction chatToggleAction;

        private void OnEnable() {
            DarklandHeroBehaviour.LocalHeroStarted += Connect;
            DarklandHeroBehaviour.LocalHeroStopped += Disconnect;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroStarted -= Connect;
            DarklandHeroBehaviour.LocalHeroStopped -= Disconnect;
        }

        private void Connect() {
            chatToggleAction.Enable();
            chatToggleAction.performed += ClientToggleChatPanel;
        }

        private void Disconnect() {
            chatToggleAction.Disable();
            chatToggleAction.performed -= ClientToggleChatPanel;
        }

        [Client]
        private static void ClientToggleChatPanel(InputAction.CallbackContext _) {
            var chatPanel = FindObjectOfType<ChatPanel>();

            if (InputStateBehaviour._.chatInputActive) {
                chatPanel.ClientLeave();
            }
            else {
                chatPanel.ClientFocus();
            }
        }

    }

}