using System.Collections.Generic;
using _Darkland.Sources.Models.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class UnitEffectsPanel : MonoBehaviour {

        [SerializeField]
        private GameObject unitEffectPrefab;

        private readonly Dictionary<string, UnitEffectImage> _activeEffects = new();

        private void OnEnable() {
            var unitEffectClientNotifier = DarklandHeroBehaviour.localHero.GetComponent<IUnitEffectClientNotifier>();
            // unitEffectClientNotifier.ClientAdded += OnClientAdded;
            // unitEffectClientNotifier.ClientRemoved += OnClientRemoved;
            // unitEffectClientNotifier.ClientRemovedAll += OnClientRemovedAll;
            // unitEffectClientNotifier.ClientNotified += ClientRefreshUnitEffects;
            
            
        }

        private void OnDisable() {
            var unitEffectClientNotifier = DarklandHeroBehaviour.localHero.GetComponent<IUnitEffectClientNotifier>();
            // unitEffectClientNotifier.ClientAdded -= OnClientAdded;
            // unitEffectClientNotifier.ClientRemoved -= OnClientRemoved;
            // unitEffectClientNotifier.ClientRemovedAll -= OnClientRemovedAll;
            
            // unitEffectClientNotifier.ClientNotified -= ClientRefreshUnitEffects;
        }

        [Client]
        private void OnClientAdded(string effectName) {
            var unitEffectImage = Instantiate(unitEffectPrefab, transform).GetComponent<UnitEffectImage>();
            unitEffectImage.ClientInit(effectName);
            
            _activeEffects.Add(effectName, unitEffectImage);
        }

        [Client]
        private void OnClientRemoved(string effectName) {
            Assert.IsTrue(_activeEffects.ContainsKey(effectName));

            var unitEffectImage = _activeEffects[effectName];
            _activeEffects.Remove(effectName);
            Destroy(unitEffectImage.gameObject);
        }

        [Client]
        private void OnClientRemovedAll() {
            // foreach (var unitEffectImage in _activeEffects.Values) {
            //     Destroy(unitEffectImage.gameObject);
            // }
            
            foreach (Transform t in transform) {
                Destroy(t.gameObject);
            }
            
            _activeEffects.Clear();
        }

        public void ClientRefreshUnitEffects(List<string> effectNames) {
            OnClientRemovedAll();
            effectNames.ForEach(OnClientAdded);
        }

    }

}