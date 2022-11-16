using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Chat {

    public class ChatPanel : MonoBehaviour {

        [SerializeField]
        private int chatMessagesLimit = 4;
        
        [SerializeField]
        private TMP_InputField messageInputField;

        [SerializeField]
        private TMP_Text chatHistoryText;

        private readonly List<string> chatHistory = new();

        private void OnEnable() {
            NetworkServer.RegisterHandler<ChatMessages.ChatMessageRequestMessage>(ServeHandleChatMessage);
            NetworkClient.RegisterHandler<ChatMessages.ChatMessageResponseMessage>(ClientHandleChatMessage);
            messageInputField.onSubmit.AddListener(ClientSendChatMessage);
        }

        private void OnDisable() {
            NetworkServer.UnregisterHandler<ChatMessages.ChatMessageRequestMessage>();
            NetworkClient.UnregisterHandler<ChatMessages.ChatMessageResponseMessage>();
            messageInputField.onSubmit.RemoveListener(ClientSendChatMessage);
            chatHistory.Clear();
        }
        
        [Server]
        private static void ServeHandleChatMessage(NetworkConnectionToClient conn, ChatMessages.ChatMessageRequestMessage msg) {
            var heroName = conn.identity.GetComponent<DarklandHero>().heroName;
            NetworkServer.SendToAll(new ChatMessages.ChatMessageResponseMessage {message = msg.message, heroName = heroName});
        }
        
        [Client]
        private void ClientHandleChatMessage(ChatMessages.ChatMessageResponseMessage msg) {
            if (chatHistory.Count == chatMessagesLimit) chatHistory.RemoveAt(0);
            chatHistory.Add($"[{msg.heroName}]: {msg.message}");
            chatHistoryText.text = chatHistory.Aggregate(string.Empty, (current, it) => current + $"{it}\n");
        }
        
        [Client]
        private void ClientSendChatMessage(string inputValue) {
            NetworkClient.Send(new ChatMessages.ChatMessageRequestMessage {message = inputValue});
            messageInputField.Select();
        }
    }

}