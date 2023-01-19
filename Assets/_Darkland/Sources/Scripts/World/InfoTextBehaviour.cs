using _Darkland.Sources.Models.DiscretePosition;
using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    //todo tmp
    public class InfoTextBehaviour : MonoBehaviour {

        [TextArea]
        public string infoMessage;
        public GameObject infoCanvas;
        public TMP_Text infoText;
        public Vector3 canvasOffset;
        public float maxVisibleDistance;

        private void Awake() {
            infoText.text = infoMessage;
            infoCanvas.transform.position = transform.position + canvasOffset;
            // spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(position);
        }

        //todo fuj
        private void FixedUpdate() {
            if (DarklandHero.localHero == null) return;
            var localPlayerPos = DarklandHero.localHero.GetComponent<IDiscretePosition>().Pos;

            if (!LocalPlayerInDistance(localPlayerPos)) Hide();
        }
        
        private void Show() {
            if (DarklandHero.localHero == null) return;
            var localPlayerPos = DarklandHero.localHero.GetComponent<IDiscretePosition>().Pos;
            
            if (!LocalPlayerInDistance(localPlayerPos)) return;
            if ((int) transform.position.z != localPlayerPos.z) return;
            
            infoCanvas.SetActive(true);
        }

        public void Toggle() {
            if (infoCanvas.activeSelf) Hide();
            else Show();
        }

        private void Hide() {
            infoCanvas.SetActive(false);
        }

        private bool LocalPlayerInDistance(Vector3Int localPlayerPos) => Vector3.Distance(localPlayerPos, transform.position) < maxVisibleDistance;
    }

}