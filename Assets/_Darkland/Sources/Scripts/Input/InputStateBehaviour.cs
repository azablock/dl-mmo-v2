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
            _chatPanel.MessageInputFieldSelected += ClientOnMessageInputFieldSelected;
            _chatPanel.MessageInputFieldDeselected += ClientOnMessageInputFieldDeselected;
        }

        private void OnDisable() {
            _chatPanel.MessageInputFieldSelected -= ClientOnMessageInputFieldSelected;
            _chatPanel.MessageInputFieldDeselected -= ClientOnMessageInputFieldDeselected;
        }

        [Client]
        private void ClientOnMessageInputFieldSelected() => chatInputActive = true;

        [Client]
        private void ClientOnMessageInputFieldDeselected() => chatInputActive = false;

    }

}