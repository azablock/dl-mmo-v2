using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IHpCalculator {
        int CalculateHp(IHpHolder hpHolder, int hpDelta);
        int CalculateMaxHp(IHpHolder hpHolder, int maxHpDelta);
    }

    public class HpCalculator : IHpCalculator {

        public int CalculateHp(IHpHolder hpHolder, int hpDelta) {
            var newHp = Math.Max(0, hpHolder.hp + hpDelta);
            return Math.Min(newHp, hpHolder.maxHp);
        }

        public int CalculateMaxHp(IHpHolder hpHolder, int maxHpDelta) {
            return Math.Max(1, hpHolder.maxHp + maxHpDelta);
        }
    }

}