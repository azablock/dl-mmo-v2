using System;
using _Darkland.Sources.Models.Unit.Hp;

namespace _Darkland.Sources.Models.Unit {

    public interface IPlayerDeathEventEmitter {
        event Action PlayerDead;
        IHpEventsHolder HpEventsHolder { get; }
    }
    
    public sealed class PlayerDeathEventEmitter : IPlayerDeathEventEmitter {
        public event Action PlayerDead;
        public IHpEventsHolder HpEventsHolder { get; }

        public PlayerDeathEventEmitter(IHpEventsHolder hpEventsHolder) {
            HpEventsHolder = hpEventsHolder;
            HpEventsHolder.HpChanged += OnHpChanged;
        }

        ~PlayerDeathEventEmitter() {
            HpEventsHolder.HpChanged -= OnHpChanged;
        }

        private void OnHpChanged(int hp) {
            if (hp == 0) {
                PlayerDead?.Invoke();
            }
        }
    }

}