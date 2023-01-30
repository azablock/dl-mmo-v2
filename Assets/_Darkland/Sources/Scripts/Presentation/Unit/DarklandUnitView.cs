using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Presentation;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Unit {

    public class DarklandUnitView : MonoBehaviour {

        public SpriteRenderer unitSpriteRenderer;
        
        private IDiscretePosition _discretePosition;
        private DarklandUnit _darklandUnit;

        private void Awake() {
            _darklandUnit = GetComponentInParent<DarklandUnit>();
            _discretePosition = GetComponentInParent<IDiscretePosition>();

            _discretePosition.ClientChanged += ClientOnChangePosition;
            _darklandUnit.ClientStarted += DarklandUnitOnClientStarted;
        }

        private void OnDestroy() {
            _discretePosition.ClientChanged -= ClientOnChangePosition;
            _darklandUnit.ClientStarted -= DarklandUnitOnClientStarted;
        }

        [Client]
        private void DarklandUnitOnClientStarted() => ClientOnChangePosition(_discretePosition.Pos);

        [Client]
        private void ClientOnChangePosition(Vector3Int pos) {
            var sortingLayerId = Gfx2dHelper.SortingLayerIdByPos(pos);
            unitSpriteRenderer.sortingLayerID = sortingLayerId;
        }
    }

}