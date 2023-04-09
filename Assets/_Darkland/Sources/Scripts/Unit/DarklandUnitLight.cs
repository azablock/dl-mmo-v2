using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Presentation;
using Mirror;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace _Darkland.Sources.Scripts.Unit {

    public class DarklandUnitLight : NetworkBehaviour {

        [SerializeField]
        private Light2D light2D;
        private IDiscretePosition _discretePosition;

        private void Awake() {
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartClient() {
            _discretePosition.ClientChanged += ClientOnPosChanged;
        }

        public override void OnStopClient() {
            _discretePosition.ClientChanged -= ClientOnPosChanged;
        }

        private void ClientOnPosChanged(Vector3Int pos) => Gfx2dHelper.ApplyLight2dSortingLayer(light2D, pos);

    }

}