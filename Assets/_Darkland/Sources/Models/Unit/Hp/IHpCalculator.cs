using System;

namespace _Darkland.Sources.Models.Unit.Hp {

    public interface IHpCalculator {
        int CalculateHp(IHpHolder hpHolder, int hpDelta);
        int CalculateMaxHp(IHpHolder hpHolder, int maxHpDelta);
        event Action RecalculateHpRequested;
        IHpHolder HpHolder { get; }
    }

    public class HpCalculator : IHpCalculator {

        public HpCalculator(IHpHolder hpHolder) {
            HpHolder = hpHolder;
        }

        public int CalculateHp(IHpHolder hpHolder, int hpDelta) {
            return 0;
        }

        public int CalculateMaxHp(IHpHolder hpHolder, int maxHpDelta) {
            return 0;
        }

        public event Action RecalculateHpRequested;
        public IHpHolder HpHolder { get; }
    }

}