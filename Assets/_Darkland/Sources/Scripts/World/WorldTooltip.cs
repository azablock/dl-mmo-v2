using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.World {

    public class WorldTooltip : MonoBehaviour {

        [TextArea]
        public string infoMessage;
        public GameObject infoCanvas;
        public TMP_Text infoText;
        public Vector3 canvasOffset;

        private void Awake() {
            infoText.text = infoMessage;
            infoCanvas.transform.position = transform.position + canvasOffset;
        }
    }

}