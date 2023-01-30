using _Darkland.Sources.Models.Equipment;
using _Darkland.Sources.Scripts.Equipment;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Unit;
using Mirror;
using UnityEngine;
using static _Darkland.Sources.NetworkMessages.TradeMessages;

namespace _Darkland.Sources.Scripts.NetworkMessagesHandler {

    public class TradeMessagesHandler : MonoBehaviour {

        private void Awake() {
            TradeMessagesProxy.ServerBuyItem += ServerHandleBuyItem;
            TradeMessagesProxy.ServerSellItem += ServerHandleSellItem;
        }

        private void OnDestroy() {
            TradeMessagesProxy.ServerBuyItem -= ServerHandleBuyItem;
            TradeMessagesProxy.ServerSellItem -= ServerHandleSellItem;
        }

        [Server]
        private static void ServerHandleBuyItem(NetworkConnectionToClient conn, BuyItemRequestMessage message) {
            var traderHandler = conn.identity.GetComponent<ITradeHandler>();
            traderHandler.BuyItem(EqItemsContainer.ItemDef2(message.itemName));
        }

        [Server]
        private static void ServerHandleSellItem(NetworkConnectionToClient conn, SellItemRequestMessage message) {
            var traderHandler = conn.identity.GetComponent<ITradeHandler>();
            traderHandler.SellItem(message.backpackSlot);
        }

    }

}