using System;
using System.Collections;
using Mirror;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Chat {

    public class ChatPopup : MonoBehaviour {

        [SerializeField]
        private TMP_Text chatPopupText;
        [SerializeField]
        private float popupLifeTime;
        [SerializeField]
        private int popupTextMaxLength;
        
        [Client]
        public IEnumerator ClientShowPopup(string message) {
            var formattedMessage = message[..Math.Min(message.Length, popupTextMaxLength)];
            if (message.Length > popupTextMaxLength) formattedMessage += "...";
            chatPopupText.text = formattedMessage;

            yield return new WaitForSeconds(popupLifeTime);
        }

    }

}