using System;
using Mirror;
using UnityEngine;
using static _Darkland.Sources.NetworkMessages.TradeMessages;

namespace _Darkland.Sources.Scripts.NetworkMessagesProxy {

    public class TradeMessagesProxy : MonoBehaviour, INetworkMessagesProxy {

        public static event Action<NetworkConnectionToClient, BuyItemRequestMessage> ServerBuyItem;
        public static event Action<NetworkConnectionToClient, SellItemRequestMessage> ServerSellItem;
        
        public void OnStartServer() {
            NetworkServer.RegisterHandler<BuyItemRequestMessage>(ServerHandleBuyItem);
            NetworkServer.RegisterHandler<SellItemRequestMessage>(ServerHandleSellItem);
        }

        public void OnStopServer() {
            NetworkServer.UnregisterHandler<BuyItemRequestMessage>();
            NetworkServer.UnregisterHandler<SellItemRequestMessage>();
        }

        public void OnStartClient() {
        }

        public void OnStopClient() {
        }

        [Server]
        private static void ServerHandleBuyItem(NetworkConnectionToClient conn, BuyItemRequestMessage message) =>
            ServerBuyItem?.Invoke(conn, message);

        [Server]
        private static void ServerHandleSellItem(NetworkConnectionToClient conn, SellItemRequestMessage message) =>
            ServerSellItem?.Invoke(conn, message);

    }

}