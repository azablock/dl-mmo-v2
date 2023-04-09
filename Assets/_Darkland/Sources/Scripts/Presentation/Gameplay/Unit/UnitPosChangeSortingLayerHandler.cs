using _Darkland.Sources.Models.Presentation;
using _Darkland.Sources.Scripts.DiscretePosition;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class UnitPosChangeSortingLayerHandler : MonoBehaviour {

        [SerializeField]
        private SpriteRenderer spriteRenderer;
        [SerializeField]
        private DiscretePositionBehaviour discretePosition;

        private void Awake() {
            discretePosition.ClientChanged += DiscretePositionOnClientChanged;
        }

        private void OnDestroy() {
            discretePosition.ClientChanged -= DiscretePositionOnClientChanged;
        }

        [Client]
        private void DiscretePositionOnClientChanged(Vector3Int pos) {
            spriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(pos);
        }

    }

}