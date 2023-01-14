using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public interface INetworkMessagesProxy {
        [Server]
        void OnStartServer();
        [Server]
        void OnStopServer();
        [Client]
        void OnStartClient();
        [Client]
        void OnStopClient();
    }

    public class ChatMessagesProxy : MonoBehaviour, INetworkMessagesProxy {
        
        public static event Action<NetworkConnectionToClient, ChatMessages.ChatMessageRequestMessage> ServerChatMessageReceived;
        public static event Action<ChatMessages.ChatMessageResponseMessage> ClientChatMessageReceived;
        public static event Action<ChatMessages.ServerLogResponseMessage> ClientServerLogReceived;

        [Server]
        public void OnStartServer() {
            NetworkServer.RegisterHandler<ChatMessages.ChatMessageRequestMessage>(ServerHandleChatMessage);
        }
        
        [Server]
        public void OnStopServer() {
            NetworkServer.UnregisterHandler<ChatMessages.ChatMessageRequestMessage>();
        }
        
        [Client]
        public void OnStartClient() {
            NetworkClient.RegisterHandler<ChatMessages.ChatMessageResponseMessage>(ClientHandleChatMessage); //msd id == 1807
            NetworkClient.RegisterHandler<ChatMessages.ServerLogResponseMessage>(ClientHandleServerLogMessage);
        }
        
        [Client]
        public void OnStopClient() {
            NetworkClient.UnregisterHandler<ChatMessages.ChatMessageResponseMessage>();
            NetworkClient.UnregisterHandler<ChatMessages.ServerLogResponseMessage>();
        }

        [Server]
        private static void ServerHandleChatMessage(NetworkConnectionToClient conn, ChatMessages.ChatMessageRequestMessage msg) =>
            ServerChatMessageReceived?.Invoke(conn, msg);

        [Client]
        private static void ClientHandleChatMessage(ChatMessages.ChatMessageResponseMessage msg) =>
            ClientChatMessageReceived?.Invoke(msg);

        [Client]
        private static void ClientHandleServerLogMessage(ChatMessages.ServerLogResponseMessage msg) =>
            ClientServerLogReceived?.Invoke(msg);

    }

}