using System;
using _Darkland.Sources.Models.DiscretePosition;
using _Darkland.Sources.Models.Unit;
using _Darkland.Sources.Models.Unit.Stats2;
using _Darkland.Sources.Models.Unit.Stats2.StatEffect;
using _Darkland.Sources.Scripts.Unit.Stats2;
using Mirror;
using UnityEngine;

namespace _Darkland.Sources.Scripts.Unit {
    
    [RequireComponent(typeof(IStatsHolder))]
    public class DeathHandlerBehaviour2 : NetworkBehaviour, IDeathEventEmitter {
        
        private IStatsHolder _statsHolder;
        private IStatEffectHandler _statEffectHandler;
        private IDiscretePosition _discretePosition;
        private Stat _healthStat;
        public event Action Death;

        private void Awake() {
            _statsHolder = GetComponent<IStatsHolder>();
            _statEffectHandler = GetComponent<IStatEffectHandler>();
            _discretePosition = GetComponent<IDiscretePosition>();
        }

        public override void OnStartServer() {
            _healthStat = _statsHolder.Stat(StatId.Health);
            _healthStat.Changed += ServerOnHealthChanged;
            Death += ServerOnDead;
        }

        public override void OnStopServer() {
            _healthStat.Changed -= ServerOnHealthChanged;
            Death -= ServerOnDead;
        }

        [Server]
        private void ServerOnDead() {
            var maxHealthValue = _statsHolder.ValueOf(StatId.MaxHealth);
            _statEffectHandler.ApplyDirectEffect(new DirectStatEffect(maxHealthValue, StatId.Health));
            _discretePosition.Set(Vector3Int.zero, true);
        }

        [Server]
        private void ServerOnHealthChanged(float health) {
            if (health == 0) {
                Death?.Invoke();
            }
        }
    }
}