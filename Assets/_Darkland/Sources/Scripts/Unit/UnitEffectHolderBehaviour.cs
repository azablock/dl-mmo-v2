using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Darkland.Sources.Models.Unit;
using Castle.Core.Internal;
using Mirror;
using UnityEngine;
using UnityEngine.Assertions;

namespace _Darkland.Sources.Scripts.Unit {

    public class UnitEffectHolderBehaviour : NetworkBehaviour, IUnitEffectHolder {

        public class UnitEffectState {

            public bool isAfterPostProcess;
            public Coroutine coroutine;
            public IUnitEffect effect;

        }
        
        private Dictionary<string, UnitEffectState> _effectStates;
        private IDeathEventEmitter _deathEventEmitter;

        public event Action<IUnitEffect> ServerAdded;
        public event Action<string> ServerRemoved;
        public event Action ServerRemovedAll;


        public override void OnStartServer() {
            _effectStates = new();
            _deathEventEmitter = GetComponent<IDeathEventEmitter>();

            _deathEventEmitter.Death += ServerRemoveAll;
        }

        public override void OnStopServer() {
            _deathEventEmitter.Death -= ServerRemoveAll;
        }

        [Server]
        public void ServerAdd(IUnitEffect effect) {
            Assert.IsNotNull(effect);

            var effectName = effect.EffectName;
            Assert.IsFalse(effectName.IsNullOrEmpty());

            if (_effectStates.ContainsKey(effectName) && _effectStates[effectName] != null) {
                effect.PostProcess(gameObject);
                _effectStates[effectName].isAfterPostProcess = true;
                StopCoroutine(_effectStates[effectName].coroutine);
            }

            _effectStates[effectName] = new UnitEffectState {
                coroutine = StartCoroutine(ServerHandleEffect(effect)),
                isAfterPostProcess = false,
                effect = effect
            };

            ServerAdded?.Invoke(effect);
        }

        [Server]
        public void ServerRemove(IUnitEffect effect) {
            var effectName = effect.EffectName;
            Assert.IsTrue(_effectStates.ContainsKey(effectName));

            if (!_effectStates[effectName].isAfterPostProcess) {
                effect.PostProcess(gameObject);
                _effectStates[effectName].isAfterPostProcess = true;
            }
            
            _effectStates.Remove(effectName);
            ServerRemoved?.Invoke(effectName);
        }

        [Server]
        public void ServerRemoveAll() {
            _effectStates
                .Keys
                .ToList()
                .ForEach(effectName => {
                    if (_effectStates[effectName].coroutine != null) StopCoroutine(_effectStates[effectName].coroutine);
                    if (!_effectStates[effectName].isAfterPostProcess) _effectStates[effectName].effect.PostProcess(gameObject);
                    
                    _effectStates.Remove(effectName);
                });

            ServerRemovedAll?.Invoke();
        }

        [Server]
        private IEnumerator ServerHandleEffect(IUnitEffect effect) {
            effect.PreProcess(gameObject);
            yield return effect.Process(gameObject);
            effect.PostProcess(gameObject);
            _effectStates[effect.EffectName].isAfterPostProcess = true;

            ServerRemove(effect);
        }

    }

}