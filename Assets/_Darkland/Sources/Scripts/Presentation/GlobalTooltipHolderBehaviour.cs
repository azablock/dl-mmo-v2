using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class GlobalTooltipHolderBehaviour : MonoBehaviour {

        public GlobalTooltip globalTooltip;
        public Vector2 paddingOffset;

        public static GlobalTooltipHolderBehaviour _;

        private void Awake() => _ = this;

        public void ShowOn(GameObject target, TooltipDescription tooltipDescription) {
            globalTooltip.gameObject.SetActive(true);
            
            var quarterWidth = Screen.width / 4.0f;
            var quarterHeight = Screen.height / 4.0f;
            var rectTransform = target.GetComponent<RectTransform>();
            var targetPos = rectTransform.position;
            var xOffset = rectTransform.GetWidth() / 2 + globalTooltip.Width / 2 + paddingOffset.x;
            var yOffset = rectTransform.GetHeight() / 2 + globalTooltip.Height / 2 + paddingOffset.y;

            var xOffsetByTargetPos = targetPos.x < quarterWidth ? xOffset : targetPos.x > 3 * quarterWidth ? -xOffset : 0;
            var yOffsetByTargetPos = targetPos.y < quarterHeight * 2 ? yOffset : -yOffset;
            // var yOffsetByTargetPos = targetPos.y < quarterHeight ? yOffset : targetPos.y > 3 * quarterHeight ? -yOffset : 0;
            var tooltipPos = new Vector2(targetPos.x + xOffsetByTargetPos, targetPos.y + yOffsetByTargetPos);
            
            globalTooltip.Show(tooltipPos, tooltipDescription);
        }

        public void Hide() {
            globalTooltip.Hide();
            globalTooltip.gameObject.SetActive(false);
        }
    }
    
    public static class RectTransformExtensions
    {
 
        public static Canvas GetCanvas(this RectTransform rt)
        {
            return rt.gameObject.GetComponentInParent<Canvas>();
        }
 
        public static float GetWidth(this RectTransform rt)
        {
            var w = (rt.anchorMax.x - rt.anchorMin.x) * Screen.width + rt.sizeDelta.x * rt.GetCanvas().scaleFactor;
            return w;
        }
     
        public static float GetHeight(this RectTransform rt)
        {
            var h = (rt.anchorMax.y - rt.anchorMin.y) * Screen.height + rt.sizeDelta.y * rt.GetCanvas().scaleFactor;
            return h;
        }
    }
}