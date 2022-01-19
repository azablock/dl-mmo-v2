using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IHpEventsHolder {
        event Action<int> HpChanged;
        event Action<int> MaxHpChanged;
    }
    
    public interface IHpController : IHpEventsHolder {
        IHpHolder HpHolder { get; }
        void ChangeHp(int hpDelta);
        void ChangeMaxHp(int maxHpDelta);
        void RegainHpToMax();
    }

    public sealed class HpController : IHpController {

        public IHpHolder HpHolder { get; }
        
        public event Action<int> HpChanged;
        public event Action<int> MaxHpChanged;

        public HpController(IHpHolder hpHolder) {
            HpHolder = hpHolder;
            MaxHpChanged += OnMaxHpChanged;
        }

        ~HpController() {
            MaxHpChanged -= OnMaxHpChanged;
        }
        
        public void ChangeHp(int hpDelta) {
            var newHp = Math.Max(0, HpHolder.hp + hpDelta);
            HpHolder.hp = Math.Min(newHp, HpHolder.maxHp);
            HpChanged?.Invoke(HpHolder.hp);
        }

        public void ChangeMaxHp(int maxHpDelta) {
            HpHolder.maxHp = Math.Max(1, HpHolder.maxHp + maxHpDelta);
            MaxHpChanged?.Invoke(HpHolder.maxHp);
        }

        //todo unit tests
        public void RegainHpToMax() {
            ChangeHp(HpHolder.maxHp);
        }

        private void OnMaxHpChanged(int maxHp) {
            if (HpHolder.hp > maxHp) {
                ChangeHp(maxHp);
            }
        }
    }

}