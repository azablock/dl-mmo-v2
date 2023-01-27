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

        private Dictionary<string, Coroutine> _effectCoroutines;
        private IDeathEventEmitter _deathEventEmitter;

        public event Action<IUnitEffect> ServerAdded;
        public event Action<string> ServerRemoved;
        public event Action ServerRemovedAll;


        public override void OnStartServer() {
            _effectCoroutines = new();
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

            if (_effectCoroutines.ContainsKey(effectName) && _effectCoroutines[effectName] != null) {
                effect.PostProcess(gameObject);
                StopCoroutine(_effectCoroutines[effectName]);
            }

            _effectCoroutines[effectName] = StartCoroutine(ServerHandleEffect(effect));
            ServerAdded?.Invoke(effect);
        }

        [Server]
        public void ServerRemove(IUnitEffect effect) {
            //tutaj tego asserta nie mam bo moze sie efekt skonczyc w coroutine
            // Assert.IsTrue(_effectCoroutines.ContainsKey(effectName));

            var effectName = effect.EffectName;
            
            if (!_effectCoroutines.ContainsKey(effectName)) return;
            
            if (_effectCoroutines[effectName] != null) {
                effect.PostProcess(gameObject);
                StopCoroutine(_effectCoroutines[effectName]);
            }

            _effectCoroutines.Remove(effectName);
            ServerRemoved?.Invoke(effectName);
        }

        [Server]
        public void ServerRemoveAll() {
            _effectCoroutines
                .Keys
                .ToList()
                .ForEach(effectName => {
                    if (_effectCoroutines[effectName] != null) StopCoroutine(_effectCoroutines[effectName]);
                    _effectCoroutines.Remove(effectName);
                });

            ServerRemovedAll?.Invoke();
        }

        [Server]
        private IEnumerator ServerHandleEffect(IUnitEffect effect) {
            effect.PreProcess(gameObject);
            yield return effect.Process(gameObject);
            effect.PostProcess(gameObject);
            
            ServerRemove(effect);
        }

    }

}