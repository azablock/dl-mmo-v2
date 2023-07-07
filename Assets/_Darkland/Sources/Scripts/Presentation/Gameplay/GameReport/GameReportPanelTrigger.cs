using _Darkland.Sources.Models.Core;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.GameReport {

    public class GameReportPanelTrigger : MonoBehaviour {

        [SerializeField]
        private SpriteRenderer spriteRenderer;

        private void Awake() {
            spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(transform.position);
        }

        private void OnTriggerEnter(Collider _) {
            // var gameReportPanelGameObject = GameplayRootPanel.GameReportPanel.gameObject;
            // gameReportPanelGameObject.SetActive(!gameReportPanelGameObject.activeSelf);
        }

        // public void OnPointerClick(PointerEventData _) {
        // }

        public void Toggle() {
            var gameReportPanelGameObject = GameplayRootPanel.GameReportPanel.gameObject;
            gameReportPanelGameObject.SetActive(!gameReportPanelGameObject.activeSelf);
        }

    }

}