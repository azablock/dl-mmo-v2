using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.NetworkMessages;
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

        private readonly List<string> chatHistory = new();

        private void OnEnable() {
            NetworkServer.RegisterHandler<ChatMessages.ChatMessageRequestMessage>(ServerHandleChatMessage);
            NetworkClient.RegisterHandler<ChatMessages.ChatMessageResponseMessage>(ClientHandleChatMessage);
            NetworkClient.RegisterHandler<ChatMessages.ServerLogResponseMessage>(ClientHandleServerLogMessage);
            messageInputField.onSubmit.AddListener(ClientSendChatMessage);
        }

        private void OnDisable() {
            NetworkServer.UnregisterHandler<ChatMessages.ChatMessageRequestMessage>();
            NetworkClient.UnregisterHandler<ChatMessages.ChatMessageResponseMessage>();
            NetworkClient.UnregisterHandler<ChatMessages.ServerLogResponseMessage>();
            messageInputField.onSubmit.RemoveListener(ClientSendChatMessage);

            chatHistory.Clear();
            chatHistoryText.text = string.Empty;
        }

        [Server]
        private static void ServerHandleChatMessage(NetworkConnectionToClient conn, ChatMessages.ChatMessageRequestMessage msg) {
            var netIdentity = conn.identity;
            var heroName = netIdentity.GetComponent<DarklandHero>().heroName;
            var netId = netIdentity.netId;
            NetworkServer.SendToAll(new ChatMessages.ChatMessageResponseMessage { message = msg.message, heroName = heroName, senderNetId = netId });
        }

        [Client]
        private void ClientHandleChatMessage(ChatMessages.ChatMessageResponseMessage msg) {
            var isLocalPlayer = msg.senderNetId == DarklandHero.localHero.netId;
            ClientUpdateChat(ChatMessagesFormatter.FormatChatMessage(msg.heroName, msg.message, isLocalPlayer));
        }

        [Client]
        private void ClientHandleServerLogMessage(ChatMessages.ServerLogResponseMessage msg) => ClientUpdateChat($"{msg.message}");

        [Client]
        private void ClientUpdateChat(string message) {
            if (chatHistory.Count == chatMessagesLimit) chatHistory.RemoveAt(0);
            chatHistory.Add(message);
            chatHistoryText.text = chatHistory.Aggregate(string.Empty, (current, it) => current + $"{it}\n");

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
        private IEnumerator ClientUpdateScrollbar() {
            yield return null;
            yield return null;
            chatScrollbar.value = 0;
        }
    }

}