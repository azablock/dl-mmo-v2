using System.Reflection;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Presentation;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Darkland.Sources.Scripts.Unit {

    public class DarklandUitLight : NetworkBehaviour {

        [SerializeField]
        private Light2D light2D;
        private IDiscretePosition _discretePosition;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartClient() {
            _discretePosition.ClientChanged += DiscretePositionOnClientChanged;
        }

        public override void OnStopClient() {
            _discretePosition.ClientChanged -= DiscretePositionOnClientChanged;
        }

        private void DiscretePositionOnClientChanged(Vector3Int pos) {
            var sortingLayerID = Gfx2dHelper.SortingLayerIdByPos(pos);
            
            var fieldInfo = light2D
                .GetType()
                .GetField("m_ApplyToSortingLayers", BindingFlags.NonPublic | BindingFlags.Instance);
            fieldInfo.SetValue(light2D, new[] {sortingLayerID});
        }

    }

}