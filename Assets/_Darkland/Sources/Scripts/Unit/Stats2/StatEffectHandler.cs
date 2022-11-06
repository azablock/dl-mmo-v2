using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.Unit.Stats2.StatEffect;
using _Darkland.Sources.ScriptableObjects.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit.Stats2 {

    public interface IStatEffectHandler {
        void ApplyDirectEffect(IDirectStatEffect effect);
    }
    
    public class StatEffectHandler : NetworkBehaviour, IStatEffectHandler {

        [SerializeField]
        private PersistentStatEffect[] persistentStatEffects;
        private IStatsHolder _statsHolder;

        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            Debug.Assert(persistentStatEffects.All(it => it.requiredStatIds.All(id => _statsHolder.statIds.Contains(id))));
        }

        public override void OnStartServer() {
            foreach (var effect in persistentStatEffects) {
                StartCoroutine(ApplyPersistentEffect(effect));
            }
        }

        public override void OnStopServer() {
            StopAllCoroutines();
        }

        [Server]
        public void ApplyDirectEffect(IDirectStatEffect effect) {
            Debug.Assert(_statsHolder.statIds.Contains(effect.statId));
            _statsHolder.Stat(effect.statId).Add(effect.delta);
        }

        [Server]
        private IEnumerator<float> ApplyPersistentEffect(PersistentStatEffect effect) {
            while (true) {
                return effect.Apply(_statsHolder);
            }
        }

    }

}