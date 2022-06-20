using System;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public interface IPlayerDeathEventEmitter {
        event Action PlayerDead;
    }

    public sealed class PlayerDeathEventEmitter : IPlayerDeathEventEmitter {
        public event Action PlayerDead;
        public IStat HealthStat { get; }

        public PlayerDeathEventEmitter(IStat healthStat) {
            Debug.Assert(healthStat.id == StatId.Health, "argument stat is not a Health stat");
            HealthStat = healthStat;
            HealthStat.Changed += OnHealthChanged;
        }

        ~PlayerDeathEventEmitter() {
            HealthStat.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(StatValue health) {
            if (health.Basic == 0) {
                PlayerDead?.Invoke();
            }
        }
    }

}