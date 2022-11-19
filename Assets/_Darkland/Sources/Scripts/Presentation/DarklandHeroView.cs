using _Darkland.Sources.Models.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation {

    public class DarklandHeroView : MonoBehaviour {

        public Canvas heroDebugCanvas;
        public SpriteRenderer heroSpriteRenderer;
        
        private IDiscretePosition _discretePosition;

        private void Awake() {
            _discretePosition = GetComponentInParent<IDiscretePosition>();

            DarklandHero.LocalHeroStarted += ClientOnLocalHeroStarted;
            _discretePosition.ClientChanged += ClientOnChangePosition;
        }

        private void OnDestroy() {
            DarklandHero.LocalHeroStarted -= ClientOnLocalHeroStarted;
            _discretePosition.ClientChanged -= ClientOnChangePosition;
        }

        [Client]
        private void ClientOnLocalHeroStarted() {
            ClientOnChangePosition(_discretePosition.Pos);
        }

        [Client]
        private void ClientOnChangePosition(Vector3Int pos) {
            var sortingLayerId = SortingLayer.NameToID($"Level {pos.z}");
            heroDebugCanvas.sortingLayerID = sortingLayerId;
            heroSpriteRenderer.sortingLayerID = sortingLayerId;
        }
    }

}