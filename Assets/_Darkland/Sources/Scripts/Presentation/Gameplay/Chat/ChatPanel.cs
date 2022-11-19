using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using Mirror;
using TMPro;
using UnityEngine;
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

        public event Action<string> MessageInputFieldValueChanged;

        private void OnEnable() {
            messageInputField.onSubmit.AddListener(ClientSendChatMessage);
            messageInputField.onValueChanged.AddListener(ClientOnMessageInputFieldValueChanged);

            ChatMessagesProxy.ClientChatMessageReceived += ClientAddChatMessage;
            ChatMessagesProxy.ClientServerLogReceived += ClientAddServerLogMessage;
        }
        
        private void OnDisable() {
            messageInputField.onSubmit.RemoveListener(ClientSendChatMessage);
            messageInputField.onValueChanged.RemoveListener(ClientOnMessageInputFieldValueChanged);

            ChatMessagesProxy.ClientChatMessageReceived -= ClientAddChatMessage;
            ChatMessagesProxy.ClientServerLogReceived -= ClientAddServerLogMessage;
            
            _chatHistory.Clear();
            chatHistoryText.text = string.Empty;
        }

        [Client]
        private void ClientAddChatMessage(ChatMessages.ChatMessageResponseMessage msg) {
            var isLocalPlayer = msg.senderNetId == DarklandHero.localHero.netId;
            ClientUpdateChat(ChatMessagesFormatter.FormatChatMessage(msg.heroName, msg.message, isLocalPlayer));
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
        private void ClientOnMessageInputFieldValueChanged(string val) => MessageInputFieldValueChanged?.Invoke(val);

        [Client]
        private IEnumerator ClientUpdateScrollbar() {
            yield return null;
            yield return null;
            chatScrollbar.value = 0;
        }
    }

}