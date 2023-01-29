using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Presentation.Gameplay.Unit {

    public class UnitEffectsPanel : MonoBehaviour {

        [SerializeField]
        private List<GameObject> unitEffectPrefabs;
        private readonly Dictionary<string, UnitEffectImage> _activeEffects = new();

        private void OnEnable() {
            var unitEffectClientNotifier = DarklandHeroBehaviour.localHero.GetComponent<IUnitEffectClientNotifier>();
            unitEffectClientNotifier.ClientAdded += OnClientAdded;
            unitEffectClientNotifier.ClientRemoved += OnClientRemoved;
            unitEffectClientNotifier.ClientRemovedAll += OnClientRemovedAll;
        }

        private void OnDisable() {
            var unitEffectClientNotifier = DarklandHeroBehaviour.localHero.GetComponent<IUnitEffectClientNotifier>();
            unitEffectClientNotifier.ClientAdded -= OnClientAdded;
            unitEffectClientNotifier.ClientRemoved -= OnClientRemoved;
            unitEffectClientNotifier.ClientRemovedAll -= OnClientRemovedAll;
        }

        [Client]
        private void OnClientAdded(string effectName) {
            var idx = unitEffectPrefabs
                .Select(it => it.GetComponent<UnitEffectImage>())
                .ToList()
                .FindIndex(it => it.unitEffectName.Equals(effectName));
            
            Assert.IsTrue(idx > -1);

            if (_activeEffects.ContainsKey(effectName)) return;

            var unitEffectPrefab = unitEffectPrefabs[idx];
            var unitEffectImage = Instantiate(unitEffectPrefab, transform).GetComponent<UnitEffectImage>();
            // unitEffectImage.ClientInit(unitEffectImage.);
            // unitEffectImage.transform.position += Vector3.down * _activeEffects.Count;
            
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
            foreach (var unitEffectImage in _activeEffects.Values) {
                Destroy(unitEffectImage.gameObject);
            }
            
            _activeEffects.Clear();
        }

    }

}