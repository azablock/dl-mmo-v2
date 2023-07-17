using System.Collections;
using _Darkland.Sources.NetworkMessages;
using _Darkland.Sources.Scripts.NetworkMessagesProxy;
using _Darkland.Sources.Scripts.Presentation.Gameplay.Chat;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Unit {

    public class DarklandHeroView : MonoBehaviour {

        [SerializeField]
        private ChatPopup chatPopup;
        private DarklandHeroBehaviour _darklandHero;
        private Coroutine _chatPopupCoroutine;

        private void Awake() {
            _darklandHero = GetComponentInParent<DarklandHeroBehaviour>();
        }

        private void OnEnable() {
            ChatMessagesProxy.ClientChatMessageReceived += ClientShowChatPopup;
        }

        private void OnDisable() {
            ChatMessagesProxy.ClientChatMessageReceived -= ClientShowChatPopup;
        }

        [Client]
        private void ClientShowChatPopup(ChatMessages.ChatMessageResponseMessage msg) {
            if (msg.senderNetId != _darklandHero.netId) return;
            if (_chatPopupCoroutine != null) StopCoroutine(_chatPopupCoroutine);
    
            _chatPopupCoroutine = StartCoroutine(ClientShow(msg.message));
        }

        [Client]
        private IEnumerator ClientShow(string chatMessage) {
            chatPopup.gameObject.SetActive(true);
            yield return chatPopup.ClientShowPopup(chatMessage);
            chatPopup.gameObject.SetActive(false);
        }

    }

}