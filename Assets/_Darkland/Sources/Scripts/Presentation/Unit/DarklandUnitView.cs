using _Darkland.Sources.Models.Core;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Unit {

    public class DarklandUnitView : MonoBehaviour {

        public SpriteRenderer unitSpriteRenderer;
        private DarklandUnit _darklandUnit;

        private IDiscretePosition _discretePosition;

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
        private void DarklandUnitOnClientStarted() {
            ClientOnChangePosition(_discretePosition.Pos);
        }

        [Client]
        private void ClientOnChangePosition(Vector3Int pos) {
            unitSpriteRenderer.sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(pos);
        }

    }

}