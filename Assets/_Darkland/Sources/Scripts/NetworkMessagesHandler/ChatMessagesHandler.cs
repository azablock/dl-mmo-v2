using System;
using _Darkland.Sources.Models.Chat;
using _Darkland.Sources.Models.Persistence.GameReport;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Persistence;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandler {

    public class ChatMessagesHandler : MonoBehaviour {

        private void Awake() {
            ChatMessagesProxy.ServerChatMessageReceived += ServerHandleChatMessage;
            ChatMessagesProxy.ServerGameReportReceived += ServerHandleGameReport;
        }

        private void OnDestroy() {
            ChatMessagesProxy.ServerChatMessageReceived -= ServerHandleChatMessage;
            ChatMessagesProxy.ServerGameReportReceived -= ServerHandleGameReport;
        }

        [Server]
        private static void ServerHandleChatMessage(NetworkConnectionToClient conn, ChatMessages.ChatMessageRequestMessage msg) {
            var netIdentity = conn.identity;
            var heroName = netIdentity.GetComponent<UnitNameBehaviour>().unitName;
            var netId = netIdentity.netId;
            NetworkServer.SendToAll(new ChatMessages.ChatMessageResponseMessage { message = msg.message, heroName = heroName, senderNetId = netId });
        }

        [Server]
        private static void ServerHandleGameReport(NetworkConnectionToClient conn, ChatMessages.GameReportRequestMessage msg) {
            var netIdentity = conn.identity;
            var heroName = netIdentity.GetComponent<UnitNameBehaviour>().unitName;
            var darklandHeroEntity = DarklandDatabaseManager.darklandHeroRepository.FindByName(heroName);
            
            if (darklandHeroEntity == null) return;
            
            var e = new GameReportEntity {
                content = msg.content,
                title = msg.title,
                createDate = DateTime.Now,
                darklandHeroId = darklandHeroEntity.id,
                gameReportType = msg.gameReportType.ToString()
            };

            DarklandDatabaseManager.gameReportRepository.Create(e);
            
            conn.Send(new ChatMessages.ServerLogResponseMessage {message = RichTextFormatter.FormatServerLog("Successfully created game report!")});
        }
    }

}