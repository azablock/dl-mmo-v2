using System;
using _Darkland.Sources.Models.Unit.Hp;
using _Darkland.Sources.Models.Unit.Stats2;

namespace _Darkland.Sources.Models.Unit {

    public interface IPlayerDeathEventEmitter {
        event Action PlayerDead;
        Stat HealthStat { get; }
    }

    public sealed class PlayerDeathEventEmitter : IPlayerDeathEventEmitter {
        public event Action PlayerDead;
        public Stat HealthStat { get; }

        public PlayerDeathEventEmitter(Stat healthStat) {
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