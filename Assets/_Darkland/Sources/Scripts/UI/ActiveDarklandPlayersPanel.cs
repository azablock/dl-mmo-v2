using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts;
using Mirror;
using UnityEngine;

namespace Sources.Scripts.UI {

    public class ActiveDarklandPlayersPanel : MonoBehaviour {

        public RectTransform parentRectTransform;
        public GameObject darklandPlayerListItemPrefab;

        private void Awake() {
            DarklandNetworkManager.clientDarklandPlayerConnected += ClientAddDarklandPlayerListItemToPanel;
            DarklandNetworkManager.clientDarklandPlayerDisconnected += ClientRemoveDarklandPlayerListItemToPanel;
            DarklandPlayerMessagesProxy.clientReceived += ClientActionResponseReceived;
            DarklandNetworkManager.clientDarklandManagerStopped += ClearPanel;
        }

        private void OnDestroy() {
            DarklandNetworkManager.clientDarklandPlayerConnected -= ClientAddDarklandPlayerListItemToPanel;
            DarklandNetworkManager.clientDarklandPlayerDisconnected -= ClientRemoveDarklandPlayerListItemToPanel;
            DarklandPlayerMessagesProxy.clientReceived -= ClientActionResponseReceived;
            DarklandNetworkManager.clientDarklandManagerStopped -= ClearPanel;
        }

        [Client]
        private void ClientAddDarklandPlayerListItemToPanel(NetworkIdentity spawnedPlayerNetworkIdentity) {
            var go = Instantiate(darklandPlayerListItemPrefab, parentRectTransform);

            var darklandPlayerInfoListItem = go
                .GetComponent<DarklandPlayerInfoListItem>();
            darklandPlayerInfoListItem
                .ClientInit(spawnedPlayerNetworkIdentity);

            var rectTransform = go.GetComponent<RectTransform>();
            rectTransform.anchoredPosition = new Vector2(0, -60 + -40 * DarklandPlayerInfoListItems().Count);
        }

        [Client]
        private static void ClientRemoveDarklandPlayerListItemToPanel(NetworkIdentity networkIdentity) {
            //todo unshift positions for > "to remove index" list items
            var darklandPlayerInfoListItemToDestroy = ClientListItemWithNetId(networkIdentity.netId);
            Destroy(darklandPlayerInfoListItemToDestroy.gameObject);
        }

        [Client]
        private static void ClientActionResponseReceived(DarklandPlayerMessages.ActionResponseMessage msg) {
            ClientListItemWithNetId(msg.darklandPlayerNetId)
                .ClientUpdate(msg.sentActionRequestMessagesCount);
        }

        [Client]
        private static DarklandPlayerInfoListItem ClientListItemWithNetId(uint netId) {
            return DarklandPlayerInfoListItems()
                .Find(item => item.darklandPlayerNetId.Equals(netId));
        }

        [Client]
        private static List<DarklandPlayerInfoListItem> DarklandPlayerInfoListItems() {
            return FindObjectsOfType<DarklandPlayerInfoListItem>()
                .ToList();
        }

        private void ClearPanel() {
            foreach (RectTransform child in parentRectTransform) {
                Destroy(child.gameObject);
            }
        }
    }

}