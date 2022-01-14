using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public class HpHolder : IHpHolder {

        public int hp { get; private set; }
        public int maxHp { get; private set; }
        public event Action<int> hpChanged;
        public event Action<int> maxHpChanged;

        public HpHolder() {
            maxHpChanged += OnMaxHpChanged;
        }

        ~HpHolder() {
            maxHpChanged -= OnMaxHpChanged;
        }

        public void ChangeHp(int hpDelta) {
            var newHp = Math.Max(0, hp + hpDelta);
            hp = Math.Min(newHp, maxHp);
            hpChanged?.Invoke(hp);
        }

        public void ChangeMaxHp(int maxHpDelta) {
            maxHp = Math.Max(1, maxHp + maxHpDelta);
            maxHpChanged?.Invoke(maxHp);
        }

        private void OnMaxHpChanged(int newMaxHp) {
            if (newMaxHp < hp) {
                ChangeHp(newMaxHp);
            }
        }
    }
    
}