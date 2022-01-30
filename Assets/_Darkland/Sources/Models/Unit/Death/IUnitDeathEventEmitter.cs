using System;
using _Darkland.Sources.Models.Unit.Hp;

namespace _Darkland.Sources.Models.Unit.Death {

    public interface IUnitDeathEventEmitter {
        event Action UnitDead;
        IHpHolder HpHolder { get; }
    }

    public sealed class UnitDeathEventEmitter : IUnitDeathEventEmitter {
        public event Action UnitDead;
        public IHpHolder HpHolder { get; }

        public UnitDeathEventEmitter(IHpHolder hpEventsHolder) {
            HpHolder = hpEventsHolder;
            HpHolder.HpChanged += OnHpChanged;
        }

        ~UnitDeathEventEmitter() {
            HpHolder.HpChanged -= OnHpChanged;
        }

        private void OnHpChanged(int hp) {
            if (hp == 0) {
                UnitDead?.Invoke();
            }
        }
    }

}