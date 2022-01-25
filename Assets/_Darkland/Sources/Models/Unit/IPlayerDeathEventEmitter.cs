using System;
using _Darkland.Sources.Models.Unit.Hp;

namespace _Darkland.Sources.Models.Unit {

    public interface IPlayerDeathEventEmitter {
        event Action PlayerDead;
        IHpHolder HpHolder { get; }
    }

    public sealed class PlayerDeathEventEmitter : IPlayerDeathEventEmitter {
        public event Action PlayerDead;
        public IHpHolder HpHolder { get; }

        public PlayerDeathEventEmitter(IHpHolder hpEventsHolder) {
            HpHolder = hpEventsHolder;
            HpHolder.HpChanged += OnHpChanged;
        }

        ~PlayerDeathEventEmitter() {
            HpHolder.HpChanged -= OnHpChanged;
        }

        private void OnHpChanged(int hp) {
            if (hp == 0) {
                PlayerDead?.Invoke();
            }
        }
    }

}