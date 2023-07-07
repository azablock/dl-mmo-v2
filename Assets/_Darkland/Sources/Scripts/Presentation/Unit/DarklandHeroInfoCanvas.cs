using _Darkland.Sources.Models.Core;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Unit {

    public class DarklandHeroInfoCanvas : MonoBehaviour {

        [SerializeField]
        private Canvas canvas;
        private IDiscretePosition _discretePosition;

        private void Awake() {
            _discretePosition = GetComponentInParent<IDiscretePosition>();
            _discretePosition.ClientChanged += ClientOnChangePosition;
        }

        private void OnDestroy() {
            _discretePosition.ClientChanged -= ClientOnChangePosition;
        }

        [Client]
        private void ClientOnChangePosition(Vector3Int pos) {
            var sortingLayerId = Gfx2dHelper.SortingLayerIdByPos(pos);
            canvas.sortingLayerID = sortingLayerId;
        }

    }

}