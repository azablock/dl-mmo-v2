using TMPro;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class GlobalTooltip : MonoBehaviour {

        [SerializeField]
        private TMP_Text titleText;
        [SerializeField]
        private TMP_Text contentText;
        private RectTransform _rectTransform;

        public float Width => _rectTransform.GetWidth();
        public float Height => _rectTransform.GetHeight();

        private void Awake() {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void Show(Vector3 pos, TooltipDescription tooltipDescription) {
            _rectTransform.position = pos;

            titleText.text = tooltipDescription.title;
            contentText.text = tooltipDescription.content;
        }

        public void Hide() {
            titleText.text = string.Empty;
            contentText.text = string.Empty;
        }

    }

}