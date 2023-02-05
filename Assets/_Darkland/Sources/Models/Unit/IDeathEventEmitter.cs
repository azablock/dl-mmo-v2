using System;
using _Darkland.Sources.Models.Unit.Stats2;
using UnityEngine;

namespace _Darkland.Sources.Models.Unit {

    public interface IDeathEventEmitter {
        event Action Death;
    }

    public sealed class DeathEventEmitter : IDeathEventEmitter {
        public event Action Death;
        public IStat HealthStat { get; }

        public DeathEventEmitter(IStat healthStat) {
            Debug.Assert(healthStat.id == StatId.Health, "argument stat is not a Health stat");
            HealthStat = healthStat;
            HealthStat.Changed += OnHealthChanged;
        }

        ~DeathEventEmitter() {
            HealthStat.Changed -= OnHealthChanged;
        }

        private void OnHealthChanged(StatVal health) {
            if (health.Current == 0) {
                Death?.Invoke();
            }
        }
    }

}