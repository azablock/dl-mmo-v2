using _Darkland.Sources.Models.Core;
using _Darkland.Sources.Scripts.Presentation.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Interaction {

    public class TargetNetIdentityMarkerHandler : MonoBehaviour {

        [SerializeField]
        private GameObject markerPrefab;
        private GameObject _markerInstance;

        private void OnEnable() {
            DarklandHeroBehaviour.LocalHeroStarted += DarklandHeroOnLocalHeroStarted;
            DarklandHeroBehaviour.LocalHeroStopped += DarklandHeroOnLocalHeroStopped;
        }

        private void OnDisable() {
            DarklandHeroBehaviour.LocalHeroStarted -= DarklandHeroOnLocalHeroStarted;
            DarklandHeroBehaviour.LocalHeroStopped -= DarklandHeroOnLocalHeroStopped;
        }

        private void DarklandHeroOnLocalHeroStarted() {
            DarklandHeroBehaviour.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientChanged += OnClientChanged;
            DarklandHeroBehaviour.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientCleared += OnClientCleared;
        }

        private void DarklandHeroOnLocalHeroStopped() {
            DarklandHeroBehaviour.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientChanged -= OnClientChanged;
            DarklandHeroBehaviour.localHero.GetComponent<ITargetNetIdClientNotifier>().ClientCleared -= OnClientCleared;

            ClearMarker();
        }

        private void OnClientChanged(NetworkIdentity targetNetIdentity) {
            var darklandUnit = targetNetIdentity.GetComponent<DarklandUnit>();
            _markerInstance = Instantiate(markerPrefab, darklandUnit.transform);

            var sortingLayerId = Gfx2dHelper.SortingLayerIdByPos(darklandUnit.transform.position);
            _markerInstance.GetComponent<SpriteRenderer>().sortingLayerID = sortingLayerId;
        }

        private void OnClientCleared(NetworkIdentity _) {
            ClearMarker();
        }

        private void ClearMarker() {
            if (_markerInstance) Destroy(_markerInstance);
        }

    }

}