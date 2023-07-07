using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay {

    public class GameplayPanelToggle : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private GameObject panel;

        private void OnDisable() {
            panel.SetActive(false);
        }

        public void OnPointerClick(PointerEventData _) {
            ClientToggle();
        }

        [Client]
        public void ClientToggle() {
            panel.SetActive(!panel.activeSelf);
        }

    }

}