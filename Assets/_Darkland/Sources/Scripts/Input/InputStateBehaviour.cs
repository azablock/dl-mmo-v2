using _Darkland.Sources.Models.Input;
using _Darkland.Sources.Scripts.Presentation.Gameplay.Chat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Input {

    public class InputStateBehaviour : MonoBehaviour, IInputState {

        private ChatPanel _chatPanel;

        public static InputStateBehaviour _;
        
        public bool chatInputActive { get; private set; }

        private void Awake() => _ = this;

        private void OnEnable() {
            _chatPanel = FindObjectOfType<ChatPanel>();
            _chatPanel.MessageInputFieldValueChanged += ClientOnMessageInputFieldValueChanged;
        }

        private void OnDisable() {
            _chatPanel.MessageInputFieldValueChanged -= ClientOnMessageInputFieldValueChanged;
        }

        [Client]
        private void ClientOnMessageInputFieldValueChanged(string inputFieldValue) => chatInputActive = !string.IsNullOrEmpty(inputFieldValue);
    }

}