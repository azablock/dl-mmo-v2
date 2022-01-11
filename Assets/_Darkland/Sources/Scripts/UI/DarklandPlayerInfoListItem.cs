using _Darkland.Sources.Scripts;
using Mirror;
using TMPro;
using UnityEngine;

namespace Sources.Scripts.UI {

    public class DarklandPlayerInfoListItem : MonoBehaviour {
        public TextMeshProUGUI characterNameText;
        public TextMeshProUGUI playerSentActionRequestMessagesText;

        public uint darklandPlayerNetId { get; private set; }

        [Client]
        public void ClientInit(NetworkIdentity spawnedPlayerNetworkIdentity) {
            var darklandBot = spawnedPlayerNetworkIdentity.GetComponent<DarklandBot>();
            var sentActionRequestCounter = spawnedPlayerNetworkIdentity.GetComponent<SentActionRequestMessagesCountHolder>();

            darklandPlayerNetId = spawnedPlayerNetworkIdentity.netId;
            // characterNameText.text = $"{darklandPlayer.characterName}";
            characterNameText.text =
                $"DarklandPlayer {(darklandBot ? "BOT" : "")} [netId={spawnedPlayerNetworkIdentity.netId}]";

            //todo bug
            playerSentActionRequestMessagesText.text = $"{sentActionRequestCounter.count}";
        }

        [Client]
        public void ClientUpdate(int sentActionRequestMessagesCount) {
            playerSentActionRequestMessagesText.text = $"{sentActionRequestMessagesCount}";
        }
    }

}