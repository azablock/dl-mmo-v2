using UnityEngine;
using UnityEngine.EventSystems;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay {

    public class GameplayPanelToggle : MonoBehaviour, IPointerClickHandler {

        [SerializeField]
        private GameObject panel;

        private void OnDisable() => panel.SetActive(false);

        public void OnPointerClick(PointerEventData _) => panel.SetActive(!panel.activeSelf);

    }

}