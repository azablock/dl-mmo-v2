using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using Mirror;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Chat {

    public class ChatPanel : MonoBehaviour {

        [SerializeField]
        private int chatMessagesLimit;
        [SerializeField]
        private TMP_InputField messageInputField;
        [SerializeField]
        private TMP_Text chatHistoryText;
        [SerializeField]
        private Scrollbar chatScrollbar;

        private readonly List<string> _chatHistory = new();

        public event Action MessageInputFieldSelected;
        public event Action MessageInputFieldDeselected;

        private void OnEnable() {
            messageInputField.onSelect.AddListener(ClientMessageInputFieldSelected);
            messageInputField.onDeselect.AddListener(ClientMessageInputFieldDeselected);
            messageInputField.onSubmit.AddListener(ClientSendChatMessage);

            ChatMessagesProxy.ClientChatMessageReceived += ClientAddChatMessage;
            ChatMessagesProxy.ClientServerLogReceived += ClientAddServerLogMessage;
        }
        
        private void OnDisable() {
            messageInputField.onSelect.RemoveListener(ClientMessageInputFieldSelected);
            messageInputField.onDeselect.RemoveListener(ClientMessageInputFieldDeselected);
            messageInputField.onSubmit.RemoveListener(ClientSendChatMessage);

            ChatMessagesProxy.ClientChatMessageReceived -= ClientAddChatMessage;
            ChatMessagesProxy.ClientServerLogReceived -= ClientAddServerLogMessage;
            
            _chatHistory.Clear();
            chatHistoryText.text = string.Empty;
        }

        [Client]
        public void ClientFocus() => messageInputField.ActivateInputField();

        [Client]
        public void ClientLeave() {
            messageInputField.DeactivateInputField();
            EventSystem.current.SetSelectedGameObject(null);
        }

        [Client]
        private void ClientAddChatMessage(ChatMessages.ChatMessageResponseMessage msg) {
            var isLocalPlayer = msg.senderNetId == DarklandHeroBehaviour.localHero.netId;
            ClientUpdateChat(RichTextFormatter.FormatChatMessage(msg.heroName, msg.message, isLocalPlayer));
        }

        [Client]
        private void ClientAddServerLogMessage(ChatMessages.ServerLogResponseMessage msg) => ClientUpdateChat($"{msg.message}");

        [Client]
        private void ClientUpdateChat(string message) {
            if (_chatHistory.Count == chatMessagesLimit) _chatHistory.RemoveAt(0);
            _chatHistory.Add(message);
            chatHistoryText.text = _chatHistory.Aggregate(string.Empty, (current, it) => current + $"{it}\n");

            StartCoroutine(ClientUpdateScrollbar());
        }

        [Client]
        private void ClientSendChatMessage(string inputValue) {
            if (inputValue == null || inputValue.Trim().Equals(string.Empty)) return;

            NetworkClient.Send(new ChatMessages.ChatMessageRequestMessage { message = inputValue });
            messageInputField.text = string.Empty;
            messageInputField.Select();
            messageInputField.ActivateInputField();
        }

        [Client]
        private void ClientMessageInputFieldSelected(string _) => MessageInputFieldSelected?.Invoke();
        
        [Client]
        private void ClientMessageInputFieldDeselected(string _) => MessageInputFieldDeselected?.Invoke();

        
        [Client]
        private IEnumerator ClientUpdateScrollbar() {
            yield return null;
            yield return null;
            chatScrollbar.value = 0;
        }
    }

}