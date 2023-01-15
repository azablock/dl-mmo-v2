using _Darkland.Sources.Models.Interaction;
using _Darkland.Sources.Models.Presentation;
using _Darkland.Sources.Scripts.Presentation.Unit;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Interaction {

    public class TargetNetIdentityMarkerHandler : MonoBehaviour {

        [SerializeField]
        private GameObject markerPrefab;
        private GameObject _markerInstance;

        private void OnEnable() {
            DarklandHero.LocalHeroStarted += DarklandHeroOnLocalHeroStarted;
            DarklandHero.LocalHeroStopped += DarklandHeroOnLocalHeroStopped;
        }

        private void OnDisable() {
            DarklandHero.LocalHeroStarted -= DarklandHeroOnLocalHeroStarted;
            DarklandHero.LocalHeroStopped -= DarklandHeroOnLocalHeroStopped;
        }

        private void DarklandHeroOnLocalHeroStarted() {
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientChanged += OnClientChanged;
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientCleared += OnClientCleared;
        }

        private void DarklandHeroOnLocalHeroStopped() {
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientChanged -= OnClientChanged;
            DarklandHero.localHero.GetComponent<ITargetNetIdHolder>().ClientCleared -= OnClientCleared;

            ClearMarker();
        }

        private void OnClientChanged(NetworkIdentity targetNetIdentity) {
            var darklandUnit = targetNetIdentity.GetComponent<DarklandUnit>();
            _markerInstance = Instantiate(markerPrefab, darklandUnit.transform);
            
            var sortingLayerId = Gfx2dHelper.SortingLayerIdByPos(darklandUnit.transform.position);
            _markerInstance.GetComponent<SpriteRenderer>().sortingLayerID = sortingLayerId;
        }

        private void OnClientCleared(NetworkIdentity _) => ClearMarker();

        private void ClearMarker() {
            if (_markerInstance) Destroy(_markerInstance);
        }

    }

}