using System;
using _Darkland.Sources.NetworkMessages;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class TradeMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient, TradeMessages.BuyItemRequestMessage> ServerBuyItem;
        public static event Action<NetworkConnectionToClient, TradeMessages.SellItemRequestMessage> ServerSellItem;
        
        public void OnStartServer() {
            NetworkServer.RegisterHandler<TradeMessages.BuyItemRequestMessage>(ServerHandleBuyItem);
            NetworkServer.RegisterHandler<TradeMessages.SellItemRequestMessage>(ServerHandleSellItem);
        }

        public void OnStopServer() {
            NetworkServer.UnregisterHandler<TradeMessages.BuyItemRequestMessage>();
            NetworkServer.UnregisterHandler<TradeMessages.SellItemRequestMessage>();
        }

        public void OnStartClient() {
        }

        public void OnStopClient() {
        }

        [Server]
        private static void ServerHandleBuyItem(NetworkConnectionToClient conn, TradeMessages.BuyItemRequestMessage message) =>
            ServerBuyItem?.Invoke(conn, message);

        [Server]
        private static void ServerHandleSellItem(NetworkConnectionToClient conn, TradeMessages.SellItemRequestMessage message) =>
            ServerSellItem?.Invoke(conn, message);

    }

}