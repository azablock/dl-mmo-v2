using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class DarklandHeroMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient,DarklandHeroMessages.GetHeroSheetRequestMessage> ServerGetHeroSheet;

        public static event Action<DarklandHeroMessages.GetHeroSheetResponseMessage> ClientGetHeroSheet;
        
        public void OnStartServer() {
            NetworkServer.RegisterHandler<DarklandHeroMessages.GetHeroSheetRequestMessage>(ServerHandleGetHeroSheet);
        }

        public void OnStopServer() {
            NetworkServer.UnregisterHandler<DarklandHeroMessages.GetHeroSheetRequestMessage>();
        }

        public void OnStartClient() {
            NetworkClient.RegisterHandler<DarklandHeroMessages.GetHeroSheetResponseMessage>(ClientHandleGetHeroSheet);
        }

        public void OnStopClient() {
            NetworkClient.UnregisterHandler<DarklandHeroMessages.GetHeroSheetResponseMessage>();
        }

        [Server]
        private static void ServerHandleGetHeroSheet(NetworkConnectionToClient conn,
                                                     DarklandHeroMessages.GetHeroSheetRequestMessage message) =>
            ServerGetHeroSheet?.Invoke(conn, message);

        [Client]
        private static void ClientHandleGetHeroSheet(DarklandHeroMessages.GetHeroSheetResponseMessage message) =>
            ClientGetHeroSheet?.Invoke(message);


    }

}