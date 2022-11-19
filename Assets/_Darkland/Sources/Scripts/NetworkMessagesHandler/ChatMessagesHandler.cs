using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandler {

    public class ChatMessagesHandler : MonoBehaviour {

        private void Awake() {
            ChatMessagesProxy.ServerChatMessageReceived += ServerHandleChatMessage;
        }

        private void OnDestroy() {
            ChatMessagesProxy.ServerChatMessageReceived -= ServerHandleChatMessage;
        }

        [Server]
        private static void ServerHandleChatMessage(NetworkConnectionToClient conn, ChatMessages.ChatMessageRequestMessage msg) {
            var netIdentity = conn.identity;
            var heroName = netIdentity.GetComponent<DarklandHero>().heroName;
            var netId = netIdentity.netId;
            NetworkServer.SendToAll(new ChatMessages.ChatMessageResponseMessage { message = msg.message, heroName = heroName, senderNetId = netId });
        }
    }

}