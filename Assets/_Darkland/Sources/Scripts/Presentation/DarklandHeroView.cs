using _Darkland.Sources.Scripts.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class DarklandHeroView : MonoBehaviour {

        public Canvas heroDebugCanvas;
        public SpriteRenderer heroSpriteRenderer;
        
        private DiscretePositionBehaviour _discretePositionBehaviour;

        private void Awake() {
            _discretePositionBehaviour = GetComponentInParent<DiscretePositionBehaviour>();
            _discretePositionBehaviour.ClientChanged += ClientOnChangePosition;
        }

        private void OnDestroy() {
            _discretePositionBehaviour.ClientChanged -= ClientOnChangePosition;
        }

        [Client]
        private void ClientOnChangePosition(Vector3Int pos) {
            var sortingLayerId = SortingLayer.NameToID($"Level {pos.z}");
            heroDebugCanvas.sortingLayerID = sortingLayerId;
            heroSpriteRenderer.sortingLayerID = sortingLayerId;
        }
    }

}