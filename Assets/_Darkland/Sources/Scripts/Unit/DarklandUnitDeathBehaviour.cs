using System;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {

    public interface IDeathEventEmitterHolder {
        IDeathEventEmitter DeathEventEmitter { get; }
    }
    
    public class DarklandUnitDeathBehaviour : MonoBehaviour, IDeathEventEmitterHolder {

        private IStatsHolder _statsHolder;
        public IDeathEventEmitter DeathEventEmitter { get; private set; }

        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            DeathEventEmitter = new DeathEventEmitter(_statsHolder.Stat(StatId.Health));
        }

        [Server]
        public void ServerAddDeathCallback(Action onDeathCallback) => DeathEventEmitter.Death += onDeathCallback;

        [Server]
        public void ServerRemoveDeathCallback(Action onDeathCallback) => DeathEventEmitter.Death -= onDeathCallback;

    }

}