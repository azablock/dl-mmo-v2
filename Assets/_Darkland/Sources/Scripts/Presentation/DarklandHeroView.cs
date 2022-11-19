using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class DarklandHeroView : MonoBehaviour {

        public Canvas heroDebugCanvas;
        public SpriteRenderer heroSpriteRenderer;
        
        private IDiscretePosition _discretePosition;
        private DarklandHero _darklandHero;

        private void Awake() {
            _darklandHero = GetComponentInParent<DarklandHero>();
            _discretePosition = GetComponentInParent<IDiscretePosition>();

            _discretePosition.ClientChanged += ClientOnChangePosition;
            _darklandHero.ClientStarted += DarklandHeroOnClientStarted;
        }

        private void OnDestroy() {
            _discretePosition.ClientChanged -= ClientOnChangePosition;
            _darklandHero.ClientStarted -= DarklandHeroOnClientStarted;
        }

        [Client]
        private void DarklandHeroOnClientStarted() => ClientOnChangePosition(_discretePosition.Pos);

        [Client]
        private void ClientOnChangePosition(Vector3Int pos) {
            var sortingLayerId = SortingLayer.NameToID($"Level {pos.z}");
            heroDebugCanvas.sortingLayerID = sortingLayerId;
            heroSpriteRenderer.sortingLayerID = sortingLayerId;
        }
    }

}