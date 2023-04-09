using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Darkland.Sources.Scripts.Presentation {

    public class TooltipDescription {

        public string title;
        public string content;

        public bool IsEmpty => string.IsNullOrEmpty(title) && string.IsNullOrEmpty(content);
    }
    
    public interface IDescriptionProvider {

        public TooltipDescription Get();

    }
    
    public class TooltipInteractiveBehaviour : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {

        private IDescriptionProvider _descriptionProvider;

        private void Awake() {
            _descriptionProvider = GetComponent<IDescriptionProvider>();
        }

        public void OnPointerEnter(PointerEventData _) {
            var tooltipDescription = _descriptionProvider.Get();
            
            if (tooltipDescription.IsEmpty) return;
            
            GlobalTooltipHolderBehaviour._.ShowOn(gameObject, tooltipDescription);
        }

        public void OnPointerExit(PointerEventData _) {
            GlobalTooltipHolderBehaviour._.Hide();
        }

    }

}