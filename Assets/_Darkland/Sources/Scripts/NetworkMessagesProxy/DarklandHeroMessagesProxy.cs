using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class DarklandHeroMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient,DarklandHeroMessages.GetHeroSheetRequestMessage> ServerGetHeroSheet;
        public static event Action<NetworkConnectionToClient,DarklandHeroMessages.GetEqRequestMessage> ServerGetEq;
        public static event Action<NetworkConnectionToClient,DarklandHeroMessages.DistributeTraitRequestMessage> ServerDistributeTrait;

        public static event Action<DarklandHeroMessages.GetHeroSheetResponseMessage> ClientGetHeroSheet;
        public static event Action<DarklandHeroMessages.GetEqResponseMessage> ClientGetEq;
        
        public void OnStartServer() {
            NetworkServer.RegisterHandler<DarklandHeroMessages.GetHeroSheetRequestMessage>(ServerHandleGetHeroSheet);
            NetworkServer.RegisterHandler<DarklandHeroMessages.GetEqRequestMessage>(ServerHandleGetEq);
            NetworkServer.RegisterHandler<DarklandHeroMessages.DistributeTraitRequestMessage>(ServerHandleDistributeTrait);
        }
        
        public void OnStopServer() {
            NetworkServer.UnregisterHandler<DarklandHeroMessages.GetHeroSheetRequestMessage>();
            NetworkServer.UnregisterHandler<DarklandHeroMessages.GetEqRequestMessage>();
            NetworkServer.UnregisterHandler<DarklandHeroMessages.DistributeTraitRequestMessage>();
        }

        public void OnStartClient() {
            NetworkClient.RegisterHandler<DarklandHeroMessages.GetHeroSheetResponseMessage>(ClientHandleGetHeroSheet);
            NetworkClient.RegisterHandler<DarklandHeroMessages.GetEqResponseMessage>(ClientHandleGetEq);
        }

        public void OnStopClient() {
            NetworkClient.UnregisterHandler<DarklandHeroMessages.GetHeroSheetResponseMessage>();
            NetworkClient.UnregisterHandler<DarklandHeroMessages.GetEqResponseMessage>();
        }

        [Server]
        private static void ServerHandleGetHeroSheet(NetworkConnectionToClient conn,
                                                     DarklandHeroMessages.GetHeroSheetRequestMessage message) =>
            ServerGetHeroSheet?.Invoke(conn, message);

        [Server]
        private static void ServerHandleGetEq(NetworkConnectionToClient conn,
                                              DarklandHeroMessages.GetEqRequestMessage message) =>
            ServerGetEq?.Invoke(conn, message);

        private static void ServerHandleDistributeTrait(NetworkConnectionToClient conn,
                                                        DarklandHeroMessages.DistributeTraitRequestMessage message) =>
            ServerDistributeTrait?.Invoke(conn, message);

        
        [Client]
        private static void ClientHandleGetHeroSheet(DarklandHeroMessages.GetHeroSheetResponseMessage message) =>
            ClientGetHeroSheet?.Invoke(message);

        [Client]
        private static void ClientHandleGetEq(DarklandHeroMessages.GetEqResponseMessage message) =>
            ClientGetEq?.Invoke(message);

    }

}