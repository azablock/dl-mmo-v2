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
            chatPopupText.text = $"{message.PadLeft(popupTextMaxLength)}...";
            yield return new WaitForSeconds(popupLifeTime);
        }

    }

}